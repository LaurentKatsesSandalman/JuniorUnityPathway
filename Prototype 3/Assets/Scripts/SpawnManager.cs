using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public float spawnTimeMin = 1f;
    public float spawnTimeMax = 3f;
    private float spawnPosX = 18.5f;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnRandomPrefab", Random.Range(spawnTimeMin, spawnTimeMax));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomPrefab()
    {
        if (!playerControllerScript.gameOver)
        {
            Vector3 spawnPos = new Vector3(spawnPosX, 0, 0);
            Instantiate(SpawnPrefab, spawnPos, SpawnPrefab.transform.rotation);
            Invoke("SpawnRandomPrefab", Random.Range(spawnTimeMin, spawnTimeMax));
        }
       
    }
}
