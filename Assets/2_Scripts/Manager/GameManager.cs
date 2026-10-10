using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject player;
    public GameObject Player => player;

    [SerializeField] Transform respawnPosition;
    public Transform RespawnPosition => respawnPosition;


    [SerializeField] int targetFrameRate = 60;
    [SerializeField] Text inGameplayTimeTxt;


    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = targetFrameRate;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        float time = Time.time;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        inGameplayTimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}