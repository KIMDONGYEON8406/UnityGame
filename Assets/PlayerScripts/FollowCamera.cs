using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 따라갈 타겟(플레이어 등의 Transform을 받아옴)
    public Transform targetTr;

    // 카메라의 Transform (현재 오브젝트의 Transform)
    private Transform camTr;

    // 타겟과의 거리 조절 (인스펙터에서 슬라이더로 조절 가능)
    [Range(2.0f, 100f)]
    public float distance = 10.0f;

    // 카메라의 높이 조절 (Y축 기준)
    [Range(0, 100f)]
    public float height = 2.0f;

    // 카메라가 타겟을 바라볼 때 오프셋 추가 (기본적으로 살짝 위를 바라보게 설정)
    public float targetOffset = 2.0f;

    void Start()
    {
        // 현재 오브젝트(카메라)의 Transform을 가져옴
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 카메라 위치 설정:
        // 타겟의 위치에서 뒤쪽(-Z 방향)으로 distance 만큼 이동하고, 높이(height)를 추가
        camTr.position = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // 카메라가 타겟을 바라보도록 설정
        camTr.LookAt(targetTr.position + targetTr.up * targetOffset);
    }
}
