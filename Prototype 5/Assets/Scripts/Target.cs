using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float minHeight = 12;
    private float maxHeight = 15f;
    private float maxRight = 4;
    private float invisibleStart = -2;
    private float torqueRange = 3;
    public int pointValue;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        gameManager = FindAnyObjectByType<GameManager>();

        targetRB.AddForce(RandomForce(minHeight, maxHeight), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(torqueRange), ForceMode.Impulse);

        transform.position = SpawnPosition(maxRight, invisibleStart);
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 RandomForce(float minHeight, float maxHeight)
    {
        return Vector3.up * Random.Range(minHeight, maxHeight);
    }
    Vector3 RandomTorque(float torqueRange)
    {
        return new Vector3(Random.Range(-torqueRange, torqueRange), Random.Range(-torqueRange, torqueRange), Random.Range(-torqueRange, torqueRange));
    }
    Vector3 SpawnPosition(float maxRight, float invisibleStart)
    {
        return new Vector3(Random.Range(-maxRight, maxRight), invisibleStart);
    }
    private void OnMouseDown()
    {
        string name = this.gameObject.name;
        if (!gameManager.gameOver)
        {
            if (name.StartsWith("Good"))
            {
                gameManager.UpdateScore(pointValue);
            }
            else if (name.StartsWith("Bad"))
            {
                gameManager.UpdateLives(1);
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string name = this.gameObject.name;
        if (name.StartsWith("Good"))
        {
            if(!gameManager.gameOver)
            {
                gameManager.UpdateScore(-pointValue);
            }
        }
        Destroy(gameObject);
    }
}
