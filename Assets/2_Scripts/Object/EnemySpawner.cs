using System.Collections;
using PB.MANAGER;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform redTeamEnemySpawnPos;
    [SerializeField] Transform blueTeamEnemySpawnPos;
    [SerializeField] int spawnInterval;
    [SerializeField] int waveCount;

    private void OnEnable()
    {
        GameManager.Instance.onGameStart += StartSpawn;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameStart -= StartSpawn;
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() 
    {
        UIManager.Instance.ShowCoutionText("- Minions have spawned -");
        yield return new WaitForSeconds(spawnInterval);
        for (int i = 0; i < waveCount; i++)
        {
            SpawnEnemy(TeamType.Red);
            SpawnEnemy(TeamType.Blue);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnEnemy(TeamType teamType)
    {
        Transform spawnPos = null;
        string enemyKey = null;
        
        if (teamType == TeamType.Red)
        {
            spawnPos = redTeamEnemySpawnPos;
            enemyKey = "Enemy_Red";
        }
        else if (teamType == TeamType.Blue)
        {
            spawnPos = blueTeamEnemySpawnPos;
            enemyKey = "Enemy_Blue";
        }

        PObjectPoolManager.Instance.Get(enemyKey, spawnPos.position, Quaternion.identity);
    }
}
