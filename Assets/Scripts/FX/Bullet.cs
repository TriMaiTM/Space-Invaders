using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private int damage = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * speed;
    }

    void Update()
    {
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Atom") || collision.CompareTag("Bullet"))
        {
            return;
        }

        Supernova supernova = collision.GetComponent<Supernova>();
        if (supernova != null)
        {
            supernova.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

}