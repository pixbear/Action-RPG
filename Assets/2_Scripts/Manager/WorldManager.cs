using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    [SerializeField] GameObject RedTeamBase;
    [SerializeField] GameObject BlueTeamBase;
    [SerializeField] Transform BlueTeamEnemySpawnPos;
    [SerializeField] Transform RedTeamEnemySpawnPos;

    
    public GameObject GetBase(TeamType teamType)
    {
        if (teamType == TeamType.Red) return RedTeamBase;
        else if (teamType == TeamType.Blue) return BlueTeamBase;
        else return null;
    }

    public Transform GetEnemySpawnPos(TeamType teamType)
    {
        if (teamType == TeamType.Red) return RedTeamEnemySpawnPos;
        else if (teamType == TeamType.Blue) return BlueTeamEnemySpawnPos;
        else return null;
    }

    private void Awake()
    {
        Instance = this;
    }
}