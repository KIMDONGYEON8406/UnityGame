using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public float moveSpeed = 2f; // �̵� �ӵ�
    public float attackRange = 2f; // ���� ����
    public float attackCooldown = 1.5f; // ���� ��ٿ�
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float attackDamage = 10f; // ���ݷ� �߰�

    private Transform player;
    private Animator animator;
    private bool canAttack = true;
    private bool isDead = false;
    private bool canMove = false; // ��Ż�� ����ϱ� ������ �̵� �Ұ�
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

        //  Plasma Explosion Effect ����
        if (plasmaExplosionEffect != null)
        {
            GameObject explosionEffect = Instantiate(plasmaExplosionEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(explosionEffect, 1.5f); // 1.5�� �� �ڵ� ����
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
        canMove = true; // ��Ż�� ����ϸ� �̵� ����
    }
}









//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Sword") && !isDead) // �˿� ������ ���
//    {
//        isDead = true;
//        animator.SetTrigger("Die");
//        Destroy(gameObject, 1.5f); // 1.5�� �� ���� (�ִϸ��̼� ��� ��)
//    }
//}

