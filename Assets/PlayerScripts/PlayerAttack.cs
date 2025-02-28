using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private int singleAttackCount = 5;

    public Collider swordCollider; // ���� Collider �߰�

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (swordCollider != null)
        {
            swordCollider.enabled = false; // ������ �� ��Ȱ��ȭ
            Debug.Log("Sword Collider �ʱ�ȭ: ��Ȱ��ȭ��");
        }
        else
        {
            Debug.LogError("Sword Collider�� �������� �ʾҽ��ϴ�! Inspector���� �Ҵ��ϼ���.");
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
        ActivateSwordCollider(); // �� �浹 Ȱ��ȭ
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(1.2f); // ���� �ִϸ��̼� ���̿� ���� ����
        isAttacking = false;
        DeactivateSwordCollider(); // �� �浹 ��Ȱ��ȭ
    }

    private void PerformSingleAttack()
    {
        singleAttackCount = (singleAttackCount == 5) ? 6 : 5;
        animator.SetInteger("ComboCount", singleAttackCount);
        animator.SetTrigger("AttackB");
        ActivateSwordCollider(); // �� �浹 Ȱ��ȭ
        StartCoroutine(ResetAttackState());
    }

    private void ActivateSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = true; // �� �浹 Ȱ��ȭ
            Debug.Log("Sword Collider Ȱ��ȭ��!");
        }
        else
        {
            Debug.LogError("Sword Collider�� Ȱ��ȭ���� ����! Inspector���� ���� Ȯ�� �ʿ�.");
        }
    }

    private void DeactivateSwordCollider()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false; // �� �浹 ��Ȱ��ȭ
            Debug.Log("Sword Collider ��Ȱ��ȭ��.");
        }
    }
}





//public class PlayerAttack : MonoBehaviour
//{
//    // ������Ʈ ���� ����
//    private Animator animator; // �÷��̾� �ִϸ�����
//    private bool isAttacking = false; // ���� ������ ����
//    private int singleAttackCount = 5; // ���� ���� �ִϸ��̼� ��ȣ

//    private void Start()
//    {
//        // �ִϸ����� ��������
//        animator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        // �⺻ ���� (���콺 ��Ŭ��)
//        if (Input.GetMouseButtonDown(0) && !isAttacking)
//        {
//            StartAttackSequence();
//        }

//        // ���� ���� (���콺 ��Ŭ��)
//        if (Input.GetMouseButtonDown(1))
//        {
//            PerformSingleAttack();
//        }

//    }

//    // �⺻ ���� �ִϸ��̼� ����
//    private void StartAttackSequence()
//    {
//        isAttacking = true; // ���� ���� ����
//        animator.SetTrigger("Attack"); // �ִϸ��̼� ����

//        StartCoroutine(ResetAttackState()); // ���� ���� �� ���� �ʱ�ȭ
//    }

//    // ���� ���� �� ���� �ʱ�ȭ (�ִϸ��̼��� ���� ��)
//    private IEnumerator ResetAttackState()
//    {
//        yield return new WaitForSeconds(1.5f); // ��ü ���� �ִϸ��̼� ���̸�ŭ ���
//        isAttacking = false; // ���� ���� �����ϰ� ����
//    }

//    // ���� ���� �ִϸ��̼� ���� (5 �� 6 �� 5 �ݺ�)
//    private void PerformSingleAttack()
//    {
//        singleAttackCount = (singleAttackCount == 5) ? 6 : 5; // ���� �ִϸ��̼� ���� (5 �� 6 �ݺ�)
//        animator.SetInteger("ComboCount", singleAttackCount); // �ִϸ����Ϳ� �� ����
//        animator.SetTrigger("AttackB"); // ���� ���� �ִϸ��̼� ����
//    }
//}
