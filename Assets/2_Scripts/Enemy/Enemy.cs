using System;
using PB.MANAGER;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Serializable]
    public class EnemyData
    {
        public int MaxHp;
        public float AttackRate;
        public float AttackRange;
        public float MoveSpeed;
    }

    [SerializeField] TeamType teamType;
    [SerializeField] EnemyData data;
    public EnemyData Data => data;

    [SerializeField] LayerMask enemyLayer;


    Renderer render;
    Rigidbody rigid;
    SphereCollider col;

    private Transform target;
    public Transform Target => target;

    public Action<Transform> onEnemySearched;
    public Action<State> onStateChanged;
    private State curState;
    public State CurState => curState;


    private void Awake()
    {
        render = GetComponentInChildren<Renderer>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    private void LateUpdate()
    {
        FindTarget();
    }

    public void SetState(State state)
    {
        curState = state;
        onStateChanged?.Invoke(state);
    }

    private void FindTarget()
    {
        if (target != null)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, enemyLayer);

        if (colliders == null || colliders.Length == 0)
        {
            return;
        }

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = collider.transform;
            }
        }
        if (closestTarget != null)
        {
            target = closestTarget;
            onEnemySearched?.Invoke(target);
        }
    }

    public void Die()
    {
        SetState(State.Dead);
        rigid.isKinematic = true;
        transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);
        col.enabled = false;
        gameObject.layer = 7;
        render.material.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        var key = string.Format("Enemy_{0}", teamType);
        PObjectPoolManager.Instance.ReleaseAfterDelay(key, gameObject, 3);
    }
}
