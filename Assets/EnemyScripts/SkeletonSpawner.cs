using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public int poolSize = 5;
    public float respawnDelay = 10f;
    public List<Transform> spawnPoints;

    private List<GameObject> skeletonPool = new List<GameObject>();
    private int activeEnemies = 0;
    private bool isRespawning = false;
    private bool isActivated = false;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject skeleton = Instantiate(skeletonPrefab);
            skeleton.SetActive(false);
            skeletonPool.Add(skeleton);
        }
    }

    public void ActivateEnemies()
    {
        if (isActivated) return;
        isActivated = true;

        SpawnSkeletons();
    }

    public void SpawnSkeletons()
    {
        if (!isActivated || isRespawning) return;
        if (activeEnemies > 0) return;

        isRespawning = true;
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (activeEnemies > 0)
        {
            isRespawning = false;
            yield break;
        }

        isRespawning = false;

        int spawnCount = Mathf.Min(poolSize, spawnPoints.Count);
        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject skeleton = GetSkeletonFromPool();
            if (skeleton != null)
            {
                int spawnIndex;
                do
                {
                    spawnIndex = Random.Range(0, spawnPoints.Count);
                } while (usedIndexes.Contains(spawnIndex));

                usedIndexes.Add(spawnIndex);
                skeleton.transform.position = spawnPoints[spawnIndex].position;
                skeleton.SetActive(true);

                SkeletonAI ai = skeleton.GetComponent<SkeletonAI>();
                if (ai != null)
                {
                    ai.ActivateAI();
                }

                activeEnemies++;
            }
        }

        UIManager.instance.UpdateEnemyCount(activeEnemies);
    }

    private GameObject GetSkeletonFromPool()
    {
        foreach (GameObject skeleton in skeletonPool)
        {
            if (!skeleton.activeInHierarchy)
            {
                return skeleton;
            }
        }

        GameObject newSkeleton = Instantiate(skeletonPrefab);
        skeletonPool.Add(newSkeleton);
        return newSkeleton;
    }

    public void ReturnToPool(GameObject skeleton)
    {
        skeleton.SetActive(false);
        activeEnemies--;

        UIManager.instance.UpdateEnemyCount(activeEnemies);
        UIManager.instance.UpdateTotalKills(); // 누적 처치 수 업데이트

        if (activeEnemies <= 0 && !isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnAfterDelay());
        }
    }





    //public void ReturnToPool(GameObject skeleton)
    //{
    //    skeleton.SetActive(false);
    //    activeEnemies--;

    //    UIManager.instance.UpdateEnemyCount(activeEnemies);

    //    if (activeEnemies <= 0 && !isRespawning)
    //    {
    //        isRespawning = true;
    //        StartCoroutine(RespawnAfterDelay());
    //    }
    //}
}












