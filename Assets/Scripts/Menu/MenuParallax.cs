using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundWidth;
    float backgroundHeight;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    void Update()
    {
        float moveY = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, moveY);
        if (Mathf.Abs(transform.position.y) - backgroundHeight > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f);
        }
    }
}
