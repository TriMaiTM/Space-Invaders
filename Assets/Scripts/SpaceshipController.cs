using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public static SpaceshipController Instance;
    public GameObject[] exhaust;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;
    public float boost = 1f;
    private float boostPower = 5f;
    private bool boosting = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegeneration;
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float fireRate = 0.5f; 
    private float nextFireTime = 0f;
    private int currentDamage = 1;
    private bool isDamageBoosted = false;
    private float damageBoostEndTime;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject destroyEffect;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        foreach (GameObject ex in exhaust)
            ex.SetActive(false);
        energy = maxEnergy;
        health = maxHealth;
        HUDController.Instance.UpdateEnergySlider(energy, maxEnergy);
        HUDController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);
            playerDirection = new Vector2(directionX, directionY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnableBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                DisableBoost();
            }

            if (Input.GetButton("Fire1") && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
            if (isDamageBoosted && Time.time > damageBoostEndTime)
            {
                DeactivateDamageBoost();
            }
        }
        
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
        if (boosting)
        {
            if (energy >= 0.2f) energy -= 0.2f;
            else
            {
                DisableBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegeneration;
            }
        }
        HUDController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void EnableBoost()
    {
        if (energy > 10)
        {
            animator.SetBool("boost", true);
            foreach (GameObject ex in exhaust)
                ex.SetActive(true);
            boost = boostPower;
            boosting = true;
        }
    }

    public void DisableBoost()
    {
        animator.SetBool("boost", false);
        foreach (GameObject ex in exhaust)
            ex.SetActive(false);
        boost = 1f;
        boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Supernova"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        HUDController.Instance.UpdateHealthSlider(health, maxHealth);
        if (health <= 0)
        {
            boost = 0;
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.Instance.GameOver();
        }
    }
    void Shoot()
    {
        GameObject shootBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = shootBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(currentDamage);
        }
    }
    public void ActivateHeal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        HUDController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    public void ActivateDamageBoost(float duration)
    {
        isDamageBoosted = true;
        currentDamage = 2;
        damageBoostEndTime = Time.time + duration;

        Debug.Log("DAMAGE BOOST ACTIVATED!");
    }

    private void DeactivateDamageBoost()
    {
        isDamageBoosted = false;
        currentDamage = 1; 

        Debug.Log("Damage boost ended.");
    }
}
