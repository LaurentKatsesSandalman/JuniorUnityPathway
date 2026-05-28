using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 3f;
    private float powerUpStrength = 55f;
    private float jumpStrength = 10f;
    private float smashPowerUpStrength = 200f;
    private GameObject focalPoint;
    public bool hasBouncePowerUp = false;
    public bool hasShootPowerUp = false;
    public bool hasSmashPowerUp = false;
    public bool isOnGround = true;
    private bool megaJumpOn=false;
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
        if (hasSmashPowerUp && Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            StartCoroutine(MegaJump());
        }

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
        else if (other.CompareTag("SmashPU"))
        {
            Destroy(other.gameObject);
            hasSmashPowerUp = true;
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
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if(megaJumpOn)
            {
                SmashThemAll();
                megaJumpOn = false;
            }
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

    IEnumerator MegaJump()
    {
        megaJumpOn = true;
        playerRigidbody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        playerRigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.linearVelocity = Vector3.zero;
        playerRigidbody.AddForce(Vector3.down * jumpStrength, ForceMode.Impulse);
        
        hasSmashPowerUp = false;
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

    void SmashThemAll()
    {
        currentEnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < currentEnemyList.Length; i++)
        {
            Rigidbody enemyRigidbody = currentEnemyList[i].GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (currentEnemyList[i].transform.position - transform.position);
            Vector3 awayFromPlayerNormalized = awayFromPlayer.normalized;
            float distance = awayFromPlayer.magnitude;
            enemyRigidbody.AddForce(awayFromPlayerNormalized * 1 / distance * smashPowerUpStrength, ForceMode.Impulse);
            Debug.Log("Smash hit " + currentEnemyList[i].name + " with force " + (1 / distance * smashPowerUpStrength));
        }
    }


}
