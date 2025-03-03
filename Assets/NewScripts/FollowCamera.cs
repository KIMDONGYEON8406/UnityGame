using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FollowCamera : MonoBehaviour
//{
//    // ���� Ÿ��(�÷��̾� ���� Transform�� �޾ƿ�)
//    public Transform targetTr;

//    // ī�޶��� Transform (���� ������Ʈ�� Transform)
//    private Transform camTr;

//    // Ÿ�ٰ��� �Ÿ� ���� (�ν����Ϳ��� �����̴��� ���� ����)
//    [Range(2.0f, 100f)]
//    public float distance = 10.0f;

//    // ī�޶��� ���� ���� (Y�� ����)
//    [Range(0, 100f)]
//    public float height = 2.0f;

//    // ī�޶� Ÿ���� �ٶ� �� ������ �߰� (�⺻������ ��¦ ���� �ٶ󺸰� ����)
//    public float targetOffset = 2.0f;

//    void Start()
//    {
//        // ���� ������Ʈ(ī�޶�)�� Transform�� ������
//        camTr = GetComponent<Transform>();
//    }

//    void LateUpdate()
//    {
//        // ī�޶� ��ġ ����:
//        // Ÿ���� ��ġ���� ����(-Z ����)���� distance ��ŭ �̵��ϰ�, ����(height)�� �߰�
//        camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

//        // ī�޶� Ÿ���� �ٶ󺸵��� ����
//        camTr.LookAt(targetTr.position + targetTr.up * targetOffset);
//    }




public class FollowCamera : MonoBehaviour
{
    // ���� Ÿ��(�÷��̾� ���� Transform�� �޾ƿ�)
    public Transform targetTr;

    // ī�޶��� Transform (���� ������Ʈ�� Transform)
    private Transform camTr;

    // Ÿ�ٰ��� �Ÿ� ���� (�ν����Ϳ��� ���� ����)
    [Range(2.0f, 100f)]
    public float distance = 10.0f;

    // ī�޶��� ���� ���� (Y�� ����)
    [Range(0, 100f)]
    public float height = 2.0f;

    // ī�޶� Ÿ���� �ٶ� �� ������ �߰� (�⺻������ ��¦ ���� �ٶ󺸰� ����)
    public float targetOffset = 2.0f;

    // ī�޶� �̵� �ε巴�� ����
    public float smoothSpeed = 0.1f;

    private void Start()
    {
        // ���� ������Ʈ(ī�޶�)�� Transform�� ������
        camTr = GetComponent<Transform>();

        // �� �̵� �� �÷��̾� �ڵ� ã��
        if (targetTr == null)
        {
            FindPlayerTarget();
        }
    }

    private void LateUpdate()
    {
        if (targetTr == null)
        {
            FindPlayerTarget();
            return;
        }

        // ī�޶� ��ġ ����
        Vector3 desiredPosition = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);
        camTr.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        camTr.LookAt(targetTr.position + targetTr.up * targetOffset);
    }

    private void FindPlayerTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetTr = player.transform;
            Debug.Log(" FollowCamera: ���ο� Ÿ�� ������ �� " + targetTr.name);
        }
        else
        {
            Debug.LogError(" FollowCamera: ������ 'Player'�� ã�� �� ����");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogError(" FollowCamera: ���ο� Ÿ���� null��!");
            return;
        }

        targetTr = newTarget;
        Debug.Log($" FollowCamera: ���ο� Ÿ�� ������ �� {targetTr.name}");
    }
}







