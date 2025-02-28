using System.Collections.Generic;
using UnityEngine;

public class SkeletonPool : MonoBehaviour
{
    public GameObject skeletonPrefab; // 하나의 스켈레톤 프리팹
    public int poolSize = 15; // 생성할 최대 개수
    private Queue<GameObject> skeletonPool = new Queue<GameObject>();

    private void Start()
    {
        // 스켈레톤을 미리 생성하여 비활성화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject skeleton = Instantiate(skeletonPrefab);
            skeleton.SetActive(false);
            skeletonPool.Enqueue(skeleton);
        }
    }

    public GameObject GetSkeleton(Vector3 spawnPosition)
    {
        GameObject skeleton;

        // 풀에 스켈레톤이 있으면 꺼내서 사용
        if (skeletonPool.Count > 0)
        {
            skeleton = skeletonPool.Dequeue();
            Debug.Log("풀에서 스켈레톤 꺼냄 : " + skeleton.name);
        }
        else
        {
            // 풀에 남은 스켈레톤이 없으면 새로 생성
            skeleton = Instantiate(skeletonPrefab);
        }

        skeleton.transform.position = spawnPosition;
        skeleton.SetActive(true);
        return skeleton;
    }

    public void ReturnSkeleton(GameObject skeleton)
    {
        skeleton.SetActive(false);
        skeletonPool.Enqueue(skeleton);
    }
}


