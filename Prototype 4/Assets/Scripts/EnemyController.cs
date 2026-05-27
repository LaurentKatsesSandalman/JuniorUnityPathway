using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f; // Speed of the enemy
    private Rigidbody enemyRigidbody;
    private GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    Vector3 movementDirection = (player.transform.position - transform.position).normalized;
        enemyRigidbody.AddForce(movementDirection * speed);
        if(transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
}
