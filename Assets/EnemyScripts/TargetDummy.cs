using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"�浹 ������: {other.gameObject.name}"); //  �浹�� ������Ʈ �̸� ���

        if (other.CompareTag("Sword")) // �˰� �浹�ϸ�
        {
            Debug.Log(" �˰� �浹! �� ����");
            Destroy(gameObject); // �� ����
        }
    }
}


