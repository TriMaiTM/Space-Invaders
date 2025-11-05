using UnityEngine;

public class Atom : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10; 

    void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * SpaceshipController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}