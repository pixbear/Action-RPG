using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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


    Animator       anim;
    NavMeshAgent   nav;
    Renderer       render;
    Camera         cam;
    Rigidbody      rigid;
    SphereCollider col;

    private Transform enemyBase;
    private Transform closeTarget;                                
    public Transform CloseTarget => closeTarget;

    private Vector3 moveDir;
    private bool isDead = false;

    public Action<Transform> onEnemySearched;
    public Action<State> onStateChanged;
    private State curState;
    public State CurState => curState;

    private void Start()
    {
        enemyBase = WorldManager.Instance.GetBase(teamType).transform;
        nav.updateRotation = false;
        nav.SetDestination(enemyBase.position);

        anim   = GetComponent<Animator>();
        nav    = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        rigid  = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }
        
    private void Update()
    {
        SearchEnemy();
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
            onEnemySearched?.Invoke(enemyBase);
        }
    }


    public void Die()
    {
        SetState(State.Dead);

        nav.updateRotation = false;

        rigid.isKinematic = true;

        transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);

        col.enabled = false;

        this.gameObject.layer = 7;

        render.material.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        nav.enabled = false;

        Destroy(gameObject, 3f);     
    }
}
