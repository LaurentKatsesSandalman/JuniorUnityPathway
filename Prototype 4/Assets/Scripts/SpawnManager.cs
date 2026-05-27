using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9f;
    private int wave = 1;
    private int currentEnnemyCount;
    private int currentPowerupCount;
    public int maxPowerupCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave(wave);
        wave++;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnnemyCount = FindObjectsByType<EnemyController>().Length;
        if (currentEnnemyCount == 0)
        {
            SpawnEnemyWave(wave);
            wave++;
        }
    }
    void SpawnEnemyWave(int wave)
    {
        for (int i = 0; i < wave; i++)
        {
            SpawnPrefab(enemyPrefab);
        }
        currentPowerupCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        
        if (currentPowerupCount+Mathf.CeilToInt(wave / 2f) <= maxPowerupCount)
        {
            SpawnPowerupWave(Mathf.CeilToInt(wave / 2f));
        }
        else
        {
            SpawnPowerupWave(maxPowerupCount - currentPowerupCount);
        }
    }
    void SpawnPowerupWave(int powerupCount)
    {
       
        for (int i = 0; i < powerupCount; i++)
        {
            SpawnPrefab(powerupPrefab);
        }
    }
    void SpawnPrefab(GameObject prefab)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        Instantiate(prefab, spawnPosition, prefab.transform.rotation);
    }
}
