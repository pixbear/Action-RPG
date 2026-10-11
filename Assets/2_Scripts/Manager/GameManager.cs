using UnityEngine;
using UnityEngine.UI;
using System;
using PB.SYSTEM;

public enum TeamType
{
    Red,
    Blue
}

public enum State
{
    Idle,
    Move,
    Attack,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject player;
    public GameObject Player => player;

    [SerializeField] Transform respawnPosition;
    public Transform RespawnPosition => respawnPosition;


    [SerializeField] int targetFrameRate = 60;
    [SerializeField] Text inGameplayTimeTxt;


    public Action onGameStart;


    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = targetFrameRate;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        onGameStart?.Invoke();
    }

    private void Update()
    {
        float time = Time.time;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        inGameplayTimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        PLog.Todo("Implement game over logic");
    }
}