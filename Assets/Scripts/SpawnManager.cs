using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const int MAX_ENEMY = 5;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(),
            powerupPrefab.transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), 
                enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            // waveNumber++;
            // if (waveNumber > MAX_ENEMY)
            // {
            //     waveNumber = MAX_ENEMY;
            // }
            if (waveNumber < MAX_ENEMY)
            {
                waveNumber++;
            }
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(),
                powerupPrefab.transform.rotation);
        }        
    }
}
