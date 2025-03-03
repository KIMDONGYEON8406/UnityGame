using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FollowCamera : MonoBehaviour
//{
//    // 따라갈 타겟(플레이어 등의 Transform을 받아옴)
//    public Transform targetTr;

//    // 카메라의 Transform (현재 오브젝트의 Transform)
//    private Transform camTr;

//    // 타겟과의 거리 조절 (인스펙터에서 슬라이더로 조절 가능)
//    [Range(2.0f, 100f)]
//    public float distance = 10.0f;

//    // 카메라의 높이 조절 (Y축 기준)
//    [Range(0, 100f)]
//    public float height = 2.0f;

//    // 카메라가 타겟을 바라볼 때 오프셋 추가 (기본적으로 살짝 위를 바라보게 설정)
//    public float targetOffset = 2.0f;

//    void Start()
//    {
//        // 현재 오브젝트(카메라)의 Transform을 가져옴
//        camTr = GetComponent<Transform>();
//    }

//    void LateUpdate()
//    {
//        // 카메라 위치 설정:
//        // 타겟의 위치에서 뒤쪽(-Z 방향)으로 distance 만큼 이동하고, 높이(height)를 추가
//        camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

//        // 카메라가 타겟을 바라보도록 설정
//        camTr.LookAt(targetTr.position + targetTr.up * targetOffset);
//    }




public class FollowCamera : MonoBehaviour
{
    // 따라갈 타겟(플레이어 등의 Transform을 받아옴)
    public Transform targetTr;

    // 카메라의 Transform (현재 오브젝트의 Transform)
    private Transform camTr;

    // 타겟과의 거리 조절 (인스펙터에서 조정 가능)
    [Range(2.0f, 100f)]
    public float distance = 10.0f;

    // 카메라의 높이 조절 (Y축 기준)
    [Range(0, 100f)]
    public float height = 2.0f;

    // 카메라가 타겟을 바라볼 때 오프셋 추가 (기본적으로 살짝 위를 바라보게 설정)
    public float targetOffset = 2.0f;

    // 카메라 이동 부드럽게 조정
    public float smoothSpeed = 0.1f;

    private void Start()
    {
        // 현재 오브젝트(카메라)의 Transform을 가져옴
        camTr = GetComponent<Transform>();

        // 씬 이동 후 플레이어 자동 찾기
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

        // 카메라 위치 조정
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
            Debug.Log(" FollowCamera: 새로운 타겟 설정됨 → " + targetTr.name);
        }
        else
        {
            Debug.LogError(" FollowCamera: 씬에서 'Player'를 찾을 수 없음");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
        {
            Debug.LogError(" FollowCamera: 새로운 타겟이 null임!");
            return;
        }

        targetTr = newTarget;
        Debug.Log($" FollowCamera: 새로운 타겟 설정됨 → {targetTr.name}");
    }
}







