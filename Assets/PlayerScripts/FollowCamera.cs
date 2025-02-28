using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // ���� Ÿ��(�÷��̾� ���� Transform�� �޾ƿ�)
    public Transform targetTr;

    // ī�޶��� Transform (���� ������Ʈ�� Transform)
    private Transform camTr;

    // Ÿ�ٰ��� �Ÿ� ���� (�ν����Ϳ��� �����̴��� ���� ����)
    [Range(2.0f, 100f)]
    public float distance = 10.0f;

    // ī�޶��� ���� ���� (Y�� ����)
    [Range(0, 100f)]
    public float height = 2.0f;

    // ī�޶� Ÿ���� �ٶ� �� ������ �߰� (�⺻������ ��¦ ���� �ٶ󺸰� ����)
    public float targetOffset = 2.0f;

    void Start()
    {
        // ���� ������Ʈ(ī�޶�)�� Transform�� ������
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // ī�޶� ��ġ ����:
        // Ÿ���� ��ġ���� ����(-Z ����)���� distance ��ŭ �̵��ϰ�, ����(height)�� �߰�
        camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // ī�޶� Ÿ���� �ٶ󺸵��� ����
        camTr.LookAt(targetTr.position + targetTr.up * targetOffset);
    }
}
