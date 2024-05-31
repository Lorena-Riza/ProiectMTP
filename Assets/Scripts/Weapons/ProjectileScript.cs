using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float force;
    public int damage;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        rb.velocity = direction * force;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collided with: " + other.gameObject.name);
        Destroy(gameObject);

        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int environmentLayer = LayerMask.NameToLayer("Environment");

        if (other.gameObject.layer == enemyLayer || other.gameObject.layer == environmentLayer)
        {
            if (other.gameObject.layer == enemyLayer)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
