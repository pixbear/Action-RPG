using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Component
    Animator     anim;
    NavMeshAgent nav;
    Rigidbody rigid;

    // Object
    [Header("----------------------- OBJ -----------------------")]
    [SerializeField] GameObject attackCollision;
    [SerializeField] ParticleSystem clickEffect;


    // Move
    [Header("----------------------- MOVE -----------------------")]
    [SerializeField] float walkSpeed;  // 걷는 속도
    [SerializeField] float runSpeed;   // 뛰는 속도
    [SerializeField] float turnSpeed;  // 도는 속도

    Vector3 movePoint;                 // 이동 포인트
    Vector3 moveDir;                   // 이동 방향

    // Bool
    public bool isAttack = false;
    public bool isMove = false;
    public bool isDead = false;

    // Camera
    [Header("----------------------- CAMERA -----------------------")]
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mouseHitLayer;

    // HP
    [Header("----------------------- HP -----------------------")]
    [SerializeField] GameObject hpGageBar;  // Hp 바 캔버스
    public Slider hpGage;                   // Hp 게이지
    public int curHp;                       // 현재 Hp
    public int maxHp;                       // 최대 Hp

    [SerializeField] Slider canvasHpGage;   // 캔버스 체력 게이지
    [SerializeField] Text canvasHpText;     // 캔버스 체력 텍스트     

    // Damage
    [Header("----------------------- DAMAGE -----------------------")]
    [SerializeField] GameObject showDamage; // 데미지 텍스트 출력
    [SerializeField] Transform showDmgPos;  // 데미지 출력 위치

    // Dead
    [Header("----------------------- DEAD -----------------------")]
    [SerializeField] int deadRespawnTime = 10;  // 부활 대기시간
    [SerializeField] Text respawnTimer;         // 타이머 텍스트
    [SerializeField] GameObject respawnUI;      // 검은 화면
    [SerializeField] Transform respawnPostion;  // 부활 포지션
    [SerializeField] GameObject body;           // 플레이어 바디
    [SerializeField] GameObject state;          // 플레이어 머리위 스텟 표시창
    // Skill
    [Header("----------------------- SKILL -----------------------")]
    [SerializeField] GameObject[] coolTimeGroup;
    [SerializeField] Image[] coolTimeImages;
    [SerializeField] Text[] coolTimeText;

    // 스킬 쿨타임
    public int qSkillCoolTime;
    public int wSkillCoolTime;
    public int eSkillCoolTime;
    public int rSkillCoolTime;
    public int dSkillCoolTime;
    public int fSkillCoolTime;

    // 스킬 쿨 다 돌았는지 여부
    public bool isSkill;                      
    public bool isQcool , isWcool, isEcool, isRcool, isDcool, isFcool;


    // Q Skill
    [Header("----------------------- Q Skill -----------------------")]
    [SerializeField] GameObject QskillAxePrefab;   // 도끼 프리펩
    [SerializeField] float axeMaxDistance;         // 도끼 최대 사정 거리
    GameObject axeInstance;                        // 복제한 도끼
    Vector3 axeMoveDir;                            // 도끼 이동방향

    bool isAxeThrow = false;                // 도끼 던짐 여부
    bool isAxeMaxDistance = false;          // 도끼 최대거리에 닿았는지 여분

    // W Skill
    [Header("----------------------- W Skill -----------------------")]
    [SerializeField] GameObject WSkillManager;

    // E Skill
    [Header("----------------------- E Skill -----------------------")]
    [SerializeField] float eJumpPower;

    // D Skill
    [Header("----------------------- D Skill -----------------------")]
    [SerializeField] float flashDistance;          // 점멸 거리
    [SerializeField] ParticleSystem dFlashEffect;  // 점멸 효과


    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Navigation
        nav.updateRotation = false;
        nav.speed = walkSpeed;

        // Mouse 화면 밖으로 안 나가게
        Cursor.lockState = CursorLockMode.Confined;

        // Hp
        curHp = maxHp;

        hpGage.maxValue = maxHp;
        hpGage.value = maxHp;
        canvasHpGage.maxValue = maxHp;
        canvasHpGage.value = maxHp;
        canvasHpText.text = maxHp + " / " + curHp;
    }

    private void Update()
    {
        Attack();
        MovePoint();
        Move();
        InputStop();
        ClickEffect();

        if (!isDead)
        {
            // Skill
            SkillQ();
            SkillW();
            SkillE();
            //SkillR();
            Skilld();
            //Skillf();
        }
    }

    private void LateUpdate()
    {
        HpGageBarFollow();
    }   

    void HpGageBarFollow() // Hp 바가 항상 카메라 바라보게
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    void MovePoint() // 마우스 클릭 위치
    {
        if (Input.GetMouseButton(1) && !isDead)
        {
            RaycastHit hit; // 마우스 히트 포인트

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer) && !isAttack)
            {
                movePoint = hit.point;
            }

            if (!isAttack)
            {
                // Start Move
                isMove = true;
                nav.SetDestination(movePoint);
                anim.SetBool("isMove", isMove);
            }
        }
    }

    void ClickEffect()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(clickEffect, movePoint + Vector3.up * 0.5f, Quaternion.identity);
        }
    }

    void Move() // 이동
    {
        if (nav.velocity.sqrMagnitude == 0f)
        {
            isMove = false;
            anim.SetBool("isMove", isMove);
        }
        else
        {
            isMove = true;
            anim.SetBool("isMove", isMove);

            // 이동할 때만 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }

        // 캐릭터 회전 방향
        moveDir = new Vector3(nav.steeringTarget.x, transform.position.y, nav.steeringTarget.z) - transform.position;
    }

    void Stop() // 캐릭터 이동 정지
    {
        nav.ResetPath();
    }

    void InputStop() // S키 누르면 이동 정지
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
    }

    void Attack() // 공격
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;

            Stop();
            anim.SetTrigger("Attack");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack Downward") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Attack Horizontal") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            isAttack = false;
        }
    }

    public void OnAttackCollision() // 공격 콜라이더 활성화
    {
        attackCollision.SetActive(true);
    }
    

    public void TakeDamage(int _damage) // 피해 데미지
    {
        // 체력 감소
        curHp -= _damage;
        hpGage.value -= _damage;
        canvasHpGage.value -= _damage;

        canvasHpText.text = maxHp + " / " + (curHp <= 0 ? "0" : curHp);


        // 데미지 출력
        GameObject showDmgInstance = Instantiate(showDamage, showDmgPos.position, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();

        // 출력 데미지 카메라 바라보기
        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        
        // 데미지 출력 효과
        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);

        // 플레이어 사망
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // 레이어
        gameObject.layer = 8;

        // 애니매이션
        anim.SetTrigger("isDie");

        // 포지션
        Stop();

        // 타이머
        StartCoroutine(DieRoutien());
        Invoke("DestroyDeadBody", 5f);
    }

    void DestroyDeadBody()
    {
        body.SetActive(false);
    }

    IEnumerator DieRoutien()
    {
        respawnUI.SetActive(true);
        state.SetActive(false);

        for (int i = deadRespawnTime; i > 0; i--)
        {
            respawnTimer.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        respawnUI.SetActive(false);

        curHp = maxHp;
        hpGage.value = curHp;
        canvasHpGage.value = curHp;
        canvasHpText.text = maxHp + " / " + curHp;

        deadRespawnTime += 10;
        anim.SetTrigger("isRespawn");

        nav.SetDestination(respawnPostion.position);
        nav.enabled = false;
        transform.position = respawnPostion.position;
        nav.enabled = true;

        body.SetActive(true);
        state.SetActive(true);

        isDead = false;
    }

    // Skill
    #region 스킬

    void SkillQ()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isAxeThrow && !isAttack && !isQcool && !isSkill)
        {
            // Cool Time Start 
            isQcool = true;
            StartCoroutine(SkillCoolTimeRoutien(qSkillCoolTime, 0, isQcool));

            anim.SetTrigger("isQSkill");

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer))
            {
                axeMoveDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
                transform.forward = axeMoveDir;

                isAxeThrow = true;

                transform.forward = axeMoveDir;
                axeInstance = Instantiate(QskillAxePrefab, transform.position + axeMoveDir.normalized
                    , QskillAxePrefab.transform.rotation);
            }
        }

        if (coolTimeImages[0].fillAmount <= 0)
        {
            isQcool = false;
            coolTimeImages[0].fillAmount = 1;
        }

        if (isAxeThrow && !isAxeMaxDistance)
        {
            axeInstance.GetComponent<Axe>().ThrowMove(axeMoveDir);
        }

        if (isAxeThrow)
        {
            if (Vector3.Distance(axeInstance.transform.position, transform.position) > axeMaxDistance)
            {
                isAxeMaxDistance = true;
            }
        }

        if (isAxeMaxDistance)
        {
            // 도끼 컴백
            axeInstance.GetComponent<Axe>().ThrowBack(transform);

            // 도끼 삭제
            if (Vector3.Distance(axeInstance.transform.position, transform.position + Vector3.up * 1) <= 0.1f)
            {
                isAxeThrow = false;
                isAxeMaxDistance = false;

                axeInstance.GetComponent<Axe>().DestroySelf();
            }
        }
    }

    void SkillW()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isWcool &&!isSkill)
        {
            // Cool Time Start 
            isWcool = true;
            StartCoroutine(SkillCoolTimeRoutien(wSkillCoolTime, 1, isWcool));

            anim.SetTrigger("isWSkill");
            WSkillManager.GetComponent<ESkillAxe>().OnESkill();
        }

        if (coolTimeImages[1].fillAmount <= 0)
        {
            isWcool = false;
            coolTimeImages[1].fillAmount = 1;
        }
    }

    void SkillE()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttack && !isEcool && !isSkill)
        {
            // Cool Time Start 
            isSkill = true;
            isEcool = true;
            StartCoroutine(SkillCoolTimeRoutien(eSkillCoolTime, 2, isEcool));

            rigid.AddForce(transform.up * eJumpPower, ForceMode.Impulse);

            anim.SetTrigger("isESkill");

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer))
            {
                axeMoveDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
                transform.forward = axeMoveDir;
            }

            isSkill = false;
            // 착지 , 이펙트 데미지 등...

        }

        if (coolTimeImages[2].fillAmount <= 0)
        {
            isEcool = false;
            coolTimeImages[2].fillAmount = 1;
        }
    }

    void SkillR()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Cool Time Start 
            isWcool = true;
            StartCoroutine(SkillCoolTimeRoutien(wSkillCoolTime, 1, isWcool));
        }

        if (coolTimeImages[1].fillAmount <= 0)
        {
            isWcool = false;
            coolTimeImages[1].fillAmount = 1;
        }
    }

    void Skilld()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isAttack && !isSkill && !isDcool)
        {
            // Cool Time Start 
            isDcool = true;
            StartCoroutine(SkillCoolTimeRoutien(dSkillCoolTime, 4, isDcool));

            // 점멸 효과
            Instantiate(dFlashEffect, transform.position + Vector3.up * 1, Quaternion.identity);


            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer))
            {
                Vector3 dir = hit.point - transform.position;
                transform.forward = dir;
                nav.SetDestination(hit.point);

                if (Vector3.Distance(transform.position, hit.point) > flashDistance)
                {
                    transform.position += dir.normalized * flashDistance;
                }
                else
                {
                    transform.position = hit.point;
                }
            }
        }

        if (coolTimeImages[4].fillAmount <= 0)
        {
            isDcool = false;
            coolTimeImages[4].fillAmount = 1;
        }
    }

    void Skillf()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Cool Time Start 
            isWcool = true;
            StartCoroutine(SkillCoolTimeRoutien(wSkillCoolTime, 1, isWcool));
        }

        if (coolTimeImages[1].fillAmount <= 0)
        {
            isWcool = false;
            coolTimeImages[1].fillAmount = 1;
        }
    }

    IEnumerator SkillCoolTimeRoutien(int _coolTime, int _index, bool isCool)
    {
        coolTimeGroup[_index].SetActive(true);

        for (int i = _coolTime; i > 0; i--)
        {
            coolTimeText[_index].text = i.ToString();

            yield return new WaitForSeconds(1f);

            coolTimeImages[_index].fillAmount -= (1.0f / _coolTime) + 0.001f;
        }

        coolTimeGroup[_index].SetActive(false);
    }
    #endregion
}
