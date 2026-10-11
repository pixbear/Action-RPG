using System;
using PB.MANAGER;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Serializable]
    public class EnemyData
    {
        public int MaxHp;
        public float AttackRate;
        public float EnemyCheakRange;
        public float AttackRange;
        public float MoveSpeed;
    }

    [SerializeField] TeamType teamType;
    [SerializeField] EnemyData data;
    public EnemyData Data => data;

    [SerializeField] LayerMask enemyLayer;


    NavMeshAgent   nav;
    Renderer       render;
    Rigidbody      rigid;
    SphereCollider col;

    private Transform oppositionBase;
    private Transform closeTarget;                                
    public Transform CloseTarget => closeTarget;

    public Action<Transform> onEnemySearched;
    public Action<State> onStateChanged;
    private State curState;
    public State CurState => curState;


    private void Awake()
    {
        nav    = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        rigid  = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        var oppositionType = teamType == TeamType.Red ? TeamType.Blue : TeamType.Red;
        oppositionBase = WorldManager.Instance.GetBase(oppositionType).transform;
        nav.SetDestination(oppositionBase.position);
    }
        
    private void Update()
    {
        SearchEnemy();
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    public void SetState(State state)
    {
        curState = state;
        onStateChanged?.Invoke(state);
    }

    private void SearchEnemy()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, data.EnemyCheakRange, enemyLayer);

        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider col in cols)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closest = col.transform;
                closestDistance = distance;
            }
        }

        closeTarget = closest;

        if (closeTarget != null)
        {
            onEnemySearched?.Invoke(closeTarget);
        }
        else
        {
            onEnemySearched?.Invoke(oppositionBase);
        }
    }

    private void LookAtTarget()
    {
        bool isDead = curState == State.Dead;
        bool noTarget = closeTarget == null;
        if (isDead || noTarget) return;

        transform.LookAt(closeTarget.position);
    }


    public void Die()
    {
        SetState(State.Dead);
        rigid.isKinematic = true;
        transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);
        col.enabled = false;
        gameObject.layer = 7;
        render.material.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        nav.enabled = false;
        var key = string.Format("Enemy_{0}", teamType);
        PObjectPoolManager.Instance.ReleaseAfterDelay(key, gameObject, 3);
    }
}
