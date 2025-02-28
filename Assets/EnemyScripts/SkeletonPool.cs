using System.Collections.Generic;
using UnityEngine;

public class SkeletonPool : MonoBehaviour
{
    public GameObject skeletonPrefab; // �ϳ��� ���̷��� ������
    public int poolSize = 15; // ������ �ִ� ����
    private Queue<GameObject> skeletonPool = new Queue<GameObject>();

    private void Start()
    {
        // ���̷����� �̸� �����Ͽ� ��Ȱ��ȭ
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

        // Ǯ�� ���̷����� ������ ������ ���
        if (skeletonPool.Count > 0)
        {
            skeleton = skeletonPool.Dequeue();
            Debug.Log("Ǯ���� ���̷��� ���� : " + skeleton.name);
        }
        else
        {
            // Ǯ�� ���� ���̷����� ������ ���� ����
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


