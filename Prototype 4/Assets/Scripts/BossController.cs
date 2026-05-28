using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab;
    private int duration = 3;
    private int delay = 15;
    private GameObject currentPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ShootProjectile", 1);
        currentPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }

    void ShootProjectile()
    {
        StartCoroutine(ShootProjectileCoroutine());       
        Invoke("ShootProjectile", delay);
    }

    IEnumerator ShootProjectileCoroutine()
    {
        for (int i = 0; i < duration; i++)
        {
            Vector3 direction = (currentPlayer.transform.position - transform.position).normalized;
            Vector3 spawnPosition = transform.position + direction * 1.8f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Instantiate(projectilePrefab, spawnPosition, rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}
