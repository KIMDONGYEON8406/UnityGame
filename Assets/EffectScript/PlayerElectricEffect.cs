using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElectricEffect : MonoBehaviour
{
    public GameObject electricEffectPrefab; // 전기 이펙트 프리팹
    private GameObject currentEffect;       // 현재 활성화된 이펙트
    private CharacterController controller; // 플레이어 이동 감지

    void Start()
    {
        controller = GetComponent<CharacterController>(); // CharacterController 가져오기
    }

    void Update()
    {
        // 플레이어의 이동 속도를 체크
        if (controller.velocity.magnitude > 0.1f) // 이동 중이면
        {
            if (currentEffect == null) // 현재 이펙트가 없을 때만 생성
            {
                currentEffect = Instantiate(electricEffectPrefab, GetFootPosition(), Quaternion.identity);
                currentEffect.transform.parent = transform; // 플레이어를 따라다니게 설정
            }
        }
        else // 플레이어가 멈추면
        {
            if (currentEffect != null) // 이펙트 삭제
            {
                Destroy(currentEffect);
                currentEffect = null;
            }
        }

        // 이펙트가 계속해서 발 밑에 위치하도록 업데이트
        if (currentEffect != null)
        {
            currentEffect.transform.position = GetFootPosition();
        }
    }

    // 플레이어 발 위치를 반환하는 함수
    Vector3 GetFootPosition()
    {
        return new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
    }
}

