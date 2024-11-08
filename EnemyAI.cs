using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Common, MiniBoss };
    public EnemyType enemyType;

    public Transform[] patrolPoints;
    public float speed = 5f;
    public float detectionRange = 10f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    public GameObject projectilePrefab; // Dla mini-bossa

    private int currentPatrolPoint = 0;
    private Transform player;
    private bool isAttacking = false;
    private float lastAttackTime;
    private int currentHealth;
    private int miniBossHealth = 50;
    private float miniBossTeleportCooldown = 5f;
    private float lastTeleportTime;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (enemyType == EnemyType.MiniBoss)
        {
            currentHealth = miniBossHealth;
            speed *= 1.5f;
            attackDamage *= 2;
            lastTeleportTime = Time.time;

        }
        else
        {
            currentHealth = 20;
        }
        lastAttackTime = Time.time;

    }

    void Update()
    {

        if (enemyType == EnemyType.MiniBoss && Time.time - lastTeleportTime > miniBossTeleportCooldown)
        {
            TeleportBehindPlayer();
        }



        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer < detectionRange)
        {
            if (!isAttacking && Time.time - lastAttackTime > attackCooldown)
            {
                Attack();


            }
        }

        else if (!isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.1f)
            {

                currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;


            }




        }



    }

    void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        if (enemyType == EnemyType.Common)
        {

            // Logika ataku wręcz
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        }

        else

        {
            // Logika ataku dystansowego (mini-boss)
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().damage = attackDamage; // Ustawienie obrażeń pocisku

        }




         Invoke("ResetAttack", attackCooldown);

    }





    void ResetAttack()
    {
        isAttacking = false;
    }



    void TeleportBehindPlayer()
    {

        Vector3 teleportPosition = player.position - player.forward * 5f;
        transform.position = teleportPosition;

        lastTeleportTime = Time.time;


    }




    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Die();

        }

    }

    void Die()
    {
        // Dodaj tutaj logikę śmierci przeciwnika (np. animacja, punkty, zniknięcie)

        GameManager.instance.AddScore(enemyType == EnemyType.Common ? 100 : 500);




        Destroy(gameObject);
    }



}
