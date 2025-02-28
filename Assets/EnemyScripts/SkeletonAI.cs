using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    public float attackRange = 2f; // 공격 범위
    public float attackCooldown = 1.5f; // 공격 쿨다운
    public float rotationSpeed = 5f; // 회전 속도
    public float attackDamage = 10f; // 공격력 추가

    private Transform player;
    private Animator animator;
    private bool canAttack = true;
    private bool isDead = false;
    private bool canMove = false; // 포탈을 통과하기 전까지 이동 불가
    private SkeletonSpawner spawner;
    public GameObject plasmaExplosionEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spawner = FindObjectOfType<SkeletonSpawner>();
    }

    private void Update()
    {
        if (isDead || !canMove || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && canAttack)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);

        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);

        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        if (other.CompareTag("Sword") && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");

        //  Plasma Explosion Effect 생성
        if (plasmaExplosionEffect != null)
        {
            GameObject explosionEffect = Instantiate(plasmaExplosionEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(explosionEffect, 1.5f); // 1.5초 후 자동 삭제
            Debug.Log("Plasma Explosion Effect Created!");
        }


        Invoke(nameof(ReturnToPool), 1.5f);
    }

    private void ReturnToPool()
    {
        isDead = false;
        gameObject.SetActive(false);

        if (spawner != null)
        {
            spawner.ReturnToPool(gameObject);
        }
    }

    public void ActivateAI()
    {
        canMove = true; // 포탈을 통과하면 이동 시작
    }
}









//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Sword") && !isDead) // 검에 맞으면 사망
//    {
//        isDead = true;
//        animator.SetTrigger("Die");
//        Destroy(gameObject, 1.5f); // 1.5초 후 삭제 (애니메이션 재생 후)
//    }
//}

