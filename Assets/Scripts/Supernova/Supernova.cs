using UnityEngine;

public class Supernova : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int health = 2;
    [SerializeField] private GameObject explosionEffect;
    [Header("Power-up Drops")]
    [SerializeField] private GameObject healPowerupPrefab;
    [SerializeField] private GameObject damagePowerupPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float powerupDropChance = 0.1f; // 10%
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float pushX = Random.Range(-1f, 1f);
        float pushY = Random.Range(-1f, 0);
        rb.linearVelocity = new Vector2(pushX, pushY);

    }

    void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * SpaceshipController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        HandlePowerupDrop();
        Destroy(gameObject);
    }

    private void HandlePowerupDrop()
    {
        // 10% of dropping power-up
        if (Random.Range(0f, 1f) <= powerupDropChance)
        {
            // 50% chance to drop either heal or damage power-up
            GameObject prefabToDrop = (Random.Range(0, 2) == 0) ? healPowerupPrefab : damagePowerupPrefab;

            if (prefabToDrop != null)
            {
                Instantiate(prefabToDrop, transform.position, transform.rotation);
            }
        }
    }
}
