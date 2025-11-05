using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType {Heal, X2Damage }
    public PowerupType type;

    [SerializeField] private float moveSpeed = 2f; 

    void Update()
    {
        float moveY = (moveSpeed * SpaceshipController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpaceshipController player = collision.GetComponent<SpaceshipController>();
            if (player != null)
            {
                if (type == PowerupType.Heal)
                {
                    player.ActivateHeal(1);
                }
                else if (type == PowerupType.X2Damage)
                {
                    player.ActivateDamageBoost(5f);
                }
            }
            Destroy(gameObject);
        }
    }
}