using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefab;
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
        currentEnnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnnemyCount == 0)
        {
            SpawnEnemyWave(wave);
            wave++;
        }
    }
    void SpawnEnemyWave(int wave)
    {
        if (wave % 5 == 0)
        {
            SpawnPrefab(enemyPrefab[enemyPrefab.Length - 1]);
        }
        else
        {
            for (int i = 0; i < wave; i++)
            {
                int x = Random.Range(0, enemyPrefab.Length - 1);
                SpawnPrefab(enemyPrefab[x]);
            }
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
            int x = Random.Range(0, powerupPrefab.Length);
            SpawnPrefab(powerupPrefab[x]);
        }
    }
    void SpawnPrefab(GameObject prefab)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        Instantiate(prefab, spawnPosition, prefab.transform.rotation);
    }
}
