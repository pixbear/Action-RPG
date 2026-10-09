using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    enum MinionType { Red, Blue };
    [SerializeField] MinionType minionType;


    // Components
    Animator       anim;
    NavMeshAgent   nav;
    Renderer       render;
    Camera         cam;
    Rigidbody      rigid;
    SphereCollider col;

    public Transform navPos; 


    // Hp
    [SerializeField] int maxHp; 
    [SerializeField] int curHp; 
    [SerializeField] GameObject hpGageBar;    
    [SerializeField] Slider hpGage;           

    [SerializeField] GameObject showDamage;   
    [SerializeField] Transform damageTextPos; 


    // Attack 
    [SerializeField] float maxAttackRate;     
                     float curAttackRate;       
    [SerializeField] float enemyCheakRange;     
    [SerializeField] float attackRange;         
    [SerializeField] LayerMask attackLayer;     
    [SerializeField] GameObject attackCollsion; 
    public Transform closeTarget;               
    float attackDistance;                       

    // Move
    [SerializeField] float lookTargetSpeed;     
    [SerializeField] float turnSpeed;           
    Vector3 moveDir;                            

    // Bool
    bool isDead = false;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();

        if (minionType == MinionType.Red)
            navPos = GameObject.Find("MinionNavTargetBlue").transform;

        if (minionType == MinionType.Blue)
            navPos = GameObject.Find("MinionNavTargetRed").transform;

        anim   = GetComponent<Animator>();
        nav    = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        rigid  = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }
        
    private void Start()
    {
        nav.updateRotation = false;
        nav.SetDestination(navPos.position);

        curAttackRate = maxAttackRate;
    }

    private void Update()
    {
        Move();
        TryAttack();
    }

    private void LateUpdate()
    {
        SearchEnemy();
        HpGageBarFollow();
    }

    void HpGageBarFollow() 
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    void SearchEnemy() 
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, enemyCheakRange, attackLayer);

        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (closeTarget != null)
                    return;

                closeTarget = col.transform;
                nav.SetDestination(closeTarget.position);
            }
        }

        if (closeTarget == null)
        {
            return;
        }
        else
        {
            nav.SetDestination(navPos.position);
        }


    }

    void Move()
    {
        if (!isDead)
        {
            if (closeTarget == null)
            {
                nav.SetDestination(navPos.position);
            }
            else
            {
                nav.SetDestination(closeTarget.position);
            }
        }

        // Move && Rot
        if (nav.velocity.sqrMagnitude == 0f)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }

        moveDir = new Vector3(nav.steeringTarget.x, transform.position.y, nav.steeringTarget.z) - transform.position;
    }

    void TryAttack()
    {
        if (closeTarget == null || isDead)
            return;
        else
        {
            if (Vector3.Distance(transform.position, closeTarget.position) < attackRange)
            {
                transform.LookAt(closeTarget.position);

                curAttackRate -= Time.deltaTime;

                if (curAttackRate <= 0)
                {
                    Attack();
                    curAttackRate = maxAttackRate;
                }             
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void TakeDamage(int _damage)
    {   
        curHp -= _damage;
        hpGage.value -= _damage;

        curHp -= _damage;
        hpGage.value -= _damage;

        GameObject showDmgInstance = Instantiate(showDamage, damageTextPos.position, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();

        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);

        if (curHp <= 0) // �ǰ� 0�̸� ����
        {
            Die();
        }
    }

    void Stop()
    {
        nav.ResetPath();
    }

    void Die()
    {
        isDead = true;

        Stop();
        nav.updateRotation = false;

        rigid.isKinematic = true;

        transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);

        col.enabled = false;

        this.gameObject.layer = 7;

        anim.SetTrigger("Die");

        render.material.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        nav.enabled = false;

        Destroy(gameObject, 3f);     
    }

    public void AttackCollision() 
    {
        attackCollsion.SetActive(true);
    }
}
