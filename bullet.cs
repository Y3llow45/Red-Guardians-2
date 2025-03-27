using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);

    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }

        if (collision.gameObject.CompareTag("zombie"))
        {
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController == null)
            {
                Debug.LogError("GameController not found in the scene!");
            }
            else
            {
                gameController.AddScore(10);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}