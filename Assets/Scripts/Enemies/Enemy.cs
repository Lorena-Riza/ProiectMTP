using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float baseLineOfSite = 20f;
    private float lineOfSite;
    public int health = 100;
    private Transform player;
    private Animator animator;
    private bool isDead = false;
    private PlayerInfection playerInfection;
    private Vector2 currentDirection;
    [SerializeField] private AudioClip enemyDamageSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        playerInfection = player.GetComponent<PlayerInfection>();

        // Set initial line of sight
        UpdateLineOfSite();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            currentDirection = direction;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);

            UpdateWalkingAnimation(direction);
        }
        else
        {
            SetIdleAnimation();
        }
    }

    private void UpdateWalkingAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetBool("IsWalkingRight", direction.x > 0);
            animator.SetBool("IsWalkingLeft", direction.x < 0);
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
        }
        else
        {
            animator.SetBool("IsWalkingUp", direction.y > 0);
            animator.SetBool("IsWalkingDown", direction.y < 0);
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsWalkingLeft", false);
        }
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("IsWalkingRight", false);
        animator.SetBool("IsWalkingLeft", false);
        animator.SetBool("IsWalkingUp", false);
        animator.SetBool("IsWalkingDown", false);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        SoundManager.instance.PlaySoundFX(enemyDamageSound, transform.position, 1f);

        if (health <= 0)
        {
            isDead = true;
            TriggerDeathAnimation();
        }
    }

    private void TriggerDeathAnimation()
    {
        animator.SetTrigger("IsDying");
        Destroy(gameObject, 0.7f); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(10);

            Vector2 direction = (other.transform.position - transform.position).normalized;
            TriggerAttackAnimation(direction);
        }
    }

    private void TriggerAttackAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                animator.SetTrigger("IsAttackingRight");
            }
            else
            {
                animator.SetTrigger("IsAttackingLeft");
            }
        }
        else
        {
            if (direction.y > 0)
            {
                animator.SetTrigger("IsAttackingUp");
            }
            else
            {
                animator.SetTrigger("IsAttackingDown");
            }
        }
    }

    public void UpdateLineOfSite()
    {
        lineOfSite = baseLineOfSite * (1 - (playerInfection.currentInfection / (float)playerInfection.maxInfection));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}