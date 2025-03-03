using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private int singleAttackCount = 5;

    public Collider swordCollider; // 검의 Collider 추가

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (swordCollider != null)
        {
            swordCollider.enabled = false; // 시작할 때 비활성화
            Debug.Log("Sword Collider 초기화: 비활성화됨");
        }
        else
        {
            Debug.LogError("Sword Collider가 설정되지 않았습니다! Inspector에서 할당하세요.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttackSequence();
        }

        if (Input.GetMouseButtonDown(1))
        {
            PerformSingleAttack();
        }
    }

    private void StartAttackSequence()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        ActivateSwordCollider(); // 검 충돌 활성화
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(1.2f); // 공격 애니메이션 길이에 맞춰 조절
        isAttacking = false;
        DeactivateSwordCollider(); // 검 충돌 비활성화
    }

    private void PerformSingleAttack()
    {
        singleAttackCount = (singleAttackCount == 5) ? 6 : 5;
        animator.SetInteger("ComboCount", singleAttackCount);
        animator.SetTrigger("AttackB");
        ActivateSwordCollider(); // 검 충돌 활성화
        StartCoroutine(ResetAttackState());
    }

    private void ActivateSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = true; // 검 충돌 활성화
            Debug.Log("Sword Collider 활성화됨!");
        }
        else
        {
            Debug.LogError("Sword Collider가 활성화되지 않음! Inspector에서 설정 확인 필요.");
        }
    }

    private void DeactivateSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false; // 검 충돌 비활성화
            Debug.Log("Sword Collider 비활성화됨.");
        }
    }
}





//public class PlayerAttack : MonoBehaviour
//{
//    // 컴포넌트 참조 변수
//    private Animator animator; // 플레이어 애니메이터
//    private bool isAttacking = false; // 공격 중인지 여부
//    private int singleAttackCount = 5; // 단일 공격 애니메이션 번호

//    private void Start()
//    {
//        // 애니메이터 가져오기
//        animator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        // 기본 공격 (마우스 좌클릭)
//        if (Input.GetMouseButtonDown(0) && !isAttacking)
//        {
//            StartAttackSequence();
//        }

//        // 단일 공격 (마우스 우클릭)
//        if (Input.GetMouseButtonDown(1))
//        {
//            PerformSingleAttack();
//        }

//    }

//    // 기본 공격 애니메이션 실행
//    private void StartAttackSequence()
//    {
//        isAttacking = true; // 공격 상태 설정
//        animator.SetTrigger("Attack"); // 애니메이션 실행

//        StartCoroutine(ResetAttackState()); // 공격 종료 후 상태 초기화
//    }

//    // 공격 종료 후 상태 초기화 (애니메이션이 끝난 후)
//    private IEnumerator ResetAttackState()
//    {
//        yield return new WaitForSeconds(1.5f); // 전체 공격 애니메이션 길이만큼 대기
//        isAttacking = false; // 다음 공격 가능하게 변경
//    }

//    // 단일 공격 애니메이션 실행 (5 → 6 → 5 반복)
//    private void PerformSingleAttack()
//    {
//        singleAttackCount = (singleAttackCount == 5) ? 6 : 5; // 공격 애니메이션 변경 (5 ↔ 6 반복)
//        animator.SetInteger("ComboCount", singleAttackCount); // 애니메이터에 값 전달
//        animator.SetTrigger("AttackB"); // 단일 공격 애니메이션 실행
//    }
//}
