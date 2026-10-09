using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject player;
    public GameObject Player => player;
    [SerializeField] int targetFrameRate = 60;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = targetFrameRate;
    }
}