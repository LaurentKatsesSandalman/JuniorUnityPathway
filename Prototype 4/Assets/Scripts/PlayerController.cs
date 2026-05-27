using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 3f;
    private float powerUpStrength = 55f;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerUpDuration = 5f;
    private float powerUpTimer = 0f;
    public GameObject powerUpIndicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRigidbody.AddForce(focalPoint.transform.forward * verticalInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
