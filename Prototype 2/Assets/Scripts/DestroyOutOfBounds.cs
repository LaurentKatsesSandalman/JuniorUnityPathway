using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private int life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z <  lowerBound || transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        if(transform.position.z < lowerBound) { 
            life--;
            Debug.Log("Player Lives: " + life);
        }
        if(life <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
