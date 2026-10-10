using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    private State curState;
    public State CurState => curState;

    [SerializeField] PlayerDataSO data;
    public PlayerDataSO Data => data;

    private CapsuleCollider col;

    public Action onRespawn;
    public Action<State> onStateChange;
    public Action onDie;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
    }

    public void SetState(State newState)
    {
        curState = newState;
        onStateChange?.Invoke(newState);
    }

    public void SetStatus(PlayerDataSO newStatus)
    {
        data = newStatus;
    }

    public void Die()
    {
        SetState(State.Dead);
        onDie?.Invoke();
        UIManager.Instance.ShowDeadUI(Data.RespawnTime);
        gameObject.layer = LayerMask.NameToLayer("PlayerDead");
        col.enabled = false;
        StartCoroutine(DieRoutien());
    }
    
    IEnumerator DieRoutien()
    {
        yield return new WaitForSecondsRealtime(Data.RespawnTime);
        Respawn();
    }

    private void Respawn()
    {
        SetState(State.Idle);
        UIManager.Instance.HideDeadUI();
        gameObject.layer = LayerMask.NameToLayer("Player");
        col.enabled = true;
        transform.position = GameManager.Instance.RespawnPosition.position;
        onRespawn?.Invoke();
    }
}