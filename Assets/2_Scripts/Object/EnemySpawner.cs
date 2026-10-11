using System.Collections;
using PB.MANAGER;
using UnityEngine;

public class MinionGenerator : MonoBehaviour
{
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
        if (teamType == TeamType.Red)
        {
            var spawnPos = WorldManager.Instance.GetEnemySpawnPos(TeamType.Red).position;
            PObjectPoolManager.Instance.Get(string.Format("Enemy_{0}", TeamType.Red), spawnPos, Quaternion.identity);
        }
        else if (teamType == TeamType.Blue)
        {
            var spawnPos = WorldManager.Instance.GetEnemySpawnPos(TeamType.Blue).position;
            PObjectPoolManager.Instance.Get(string.Format("Enemy_{0}", TeamType.Blue), spawnPos, Quaternion.identity);
        }
    }
}
