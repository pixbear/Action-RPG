using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    [SerializeField] GameObject RedTeamBase;
    [SerializeField] GameObject BlueTeamBase;

    
    public GameObject GetBase(TeamType teamType)
    {
        if (teamType == TeamType.Red) return RedTeamBase;
        else if (teamType == TeamType.Blue) return BlueTeamBase;
        else return null;
    }

    private void Awake()
    {
        Instance = this;
    }
}