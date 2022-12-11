using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // 미니언 타입 (레드, 블루)
    enum MinionType { Red, Blue };
    [SerializeField] MinionType minionType;


    // Components
    Animator       anim;
    NavMeshAgent   nav;
    Renderer       render;
    Camera         cam;
    Rigidbody      rigid;
    SphereCollider col;

    public Transform navPos; // 네비게이션 포지션


    // Hp
    [SerializeField] int maxHp; // 최대 체력
    [SerializeField] int curHp; // 현재 체력

    [SerializeField] GameObject hpGageBar;    // 체력 게이지 캔버스
    [SerializeField] Slider hpGage;           // 체력 게이지 바

    [SerializeField] GameObject showDamage;   // 데미지 출력 프리펩
    [SerializeField] Transform damageTextPos; // 데미지 출력 위치


    // Attack 
    [SerializeField] float maxAttackRate;       // 최대 공격 속도
                     float curAttackRate;       // 현재 공격 속도
    [SerializeField] float enemyCheakRange;     // 적 체크 범위
    [SerializeField] float attackRange;         // 공격 범위
    [SerializeField] LayerMask attackLayer;     // 공격할 레이어
    [SerializeField] GameObject attackCollsion; // 공격 판정 콜라이더
    public Transform closeTarget;               // 가까운 타겟
    float attackDistance;                       // 타겟과의 거리

    // Move
    [SerializeField] float lookTargetSpeed;     // 타겟 돌아보는 회전 속도
    [SerializeField] float turnSpeed;           // 돌아보는 속도
    Vector3 moveDir;                            // 이동 방향

    // Bool
    bool isDead = false;
    //bool isAttack = false;


    private void Awake()
    {
        // 카메라 초기화
        cam = FindObjectOfType<Camera>();

        // 타입별 네비 타겟 세팅
        if (minionType == MinionType.Red)
            navPos = GameObject.Find("MinionNavTargetBlue").transform;

        if (minionType == MinionType.Blue)
            navPos = GameObject.Find("MinionNavTargetRed").transform;


        // Get Component
        anim   = GetComponent<Animator>();
        nav    = GetComponent<NavMeshAgent>();
        render = GetComponentInChildren<Renderer>();
        rigid  = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }
        
    private void Start()
    {
        //InvokeRepeating("SearchEnemy", 0f, 0.5f);

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

    void HpGageBarFollow() // 체력 바 카메라 바라보기
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    void SearchEnemy() // 주변의 적 탐색
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
        // Move to Target
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

            // 이동할 때만 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }

        // 캐릭터 회전 방향
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

    void Attack() // 공격
    {
        anim.SetTrigger("Attack");
    }

    public void TakeDamage(int _damage)
    {   
        curHp -= _damage;
        hpGage.value -= _damage;

        // 체력 감소
        curHp -= _damage;
        hpGage.value -= _damage;

        // 데미지 출력
        GameObject showDmgInstance = Instantiate(showDamage, damageTextPos.position, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();

        // 출력 데미지 카메라 바라보기
        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

        // 데미지 출력 효과
        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);

        if (curHp <= 0) // 피가 0이면 다이
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

        // 충돌 비활성화   
        this.gameObject.layer = 7;

        // 죽는 애니매이션
        anim.SetTrigger("Die");

        // 색 변경
        render.material.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        // 네비 비활성화
        nav.enabled = false;

        // 시체 삭제
        Destroy(gameObject, 3f);     
    }

    public void AttackCollision() // 공격 콜라이더
    {
        attackCollsion.SetActive(true);
    }
}
