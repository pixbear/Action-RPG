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

    private PlayerAnimationController anim;
    private PlayerMoveController move;
    private PlayerHpController hp; 
    private CapsuleCollider col;

    private void Awake()
    {
        anim = GetComponent<PlayerAnimationController>();
        move = GetComponent<PlayerMoveController>();
        hp = GetComponent<PlayerHpController>();
        col = GetComponent<CapsuleCollider>();
    }

    public void SetState(State newState)
    {
        curState = newState;
        anim.Set(newState);
    }

    public void SetStatus(PlayerDataSO newStatus)
    {
        data = newStatus;
    }

    public void Die()
    {
        SetState(State.Dead);
        UIManager.Instance.ShowDeadUI(Data.RespawnTime);
        gameObject.layer = LayerMask.NameToLayer("PlayerDead");
        move.Stop();
        hp.DisableUI();
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
        hp.EnableUI();
        hp.SetFullHp();
        col.enabled = true;
        transform.position = GameManager.Instance.RespawnPosition.position;
    }
}