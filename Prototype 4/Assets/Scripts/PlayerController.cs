using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 3f;
    private float powerUpStrength = 55f;
    private GameObject focalPoint;
    public bool hasBouncePowerUp = false;
    public bool hasShootPowerUp = false;
    private float powerUpDuration = 5f;
    private float powerUpTimer = 0f;
    public GameObject powerUpIndicator;
    public GameObject projectilePrefab;
    private GameObject[] currentEnemyList;
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
        if (other.CompareTag("BouncePU"))
        {
            Destroy(other.gameObject);
            hasBouncePowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(BouncePowerUpCountdownRoutine());
        }
        else if (other.CompareTag("ShootPU"))
        {
            Destroy(other.gameObject);
            hasShootPowerUp = true;
            StartCoroutine(ShootPowerUpCountdownRoutine());
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasBouncePowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator BouncePowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasBouncePowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    IEnumerator ShootPowerUpCountdownRoutine()
    {
        for (int i = 0; i < powerUpDuration; i++)
        {
            yield return new WaitForSeconds(1);

            ShootThemAll();
        }
        hasShootPowerUp = false;
    }

    void ShootThemAll()
    {
        currentEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < currentEnemyList.Length; i++)
        {
            //Vector3 spawnOffset = new Vector3(1.8f, 0, 0);
           
            Vector3 direction = (currentEnemyList[i].transform.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + direction * 1.8f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Instantiate(projectilePrefab, spawnPosition, rotation);
        }
    }


}
