using UnityEngine;

public class Infinichick : MonoBehaviour
{
    void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * SpaceshipController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }
}
