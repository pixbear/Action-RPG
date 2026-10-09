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
    CapsuleCollider col;

    // Object
    [Header("----------------------- OBJ -----------------------")]
    [SerializeField] GameObject attackCollision;
    [SerializeField] ParticleSystem clickEffect;


    // Move
    [Header("----------------------- MOVE -----------------------")]
    [SerializeField] float walkSpeed;  // �ȴ� �ӵ�
    [SerializeField] float runSpeed;   // �ٴ� �ӵ�
    [SerializeField] float turnSpeed;  // ���� �ӵ�

    Vector3 movePoint;                 // �̵� ����Ʈ
    Vector3 moveDir;                   // �̵� ����

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
    [SerializeField] GameObject hpGageBar;  // Hp �� ĵ����
    public Slider hpGage;                   // Hp ������
    public int curHp;                       // ���� Hp
    public int maxHp;                       // �ִ� Hp

    [SerializeField] Slider canvasHpGage;   // ĵ���� ü�� ������
    [SerializeField] Text canvasHpText;     // ĵ���� ü�� �ؽ�Ʈ     

    // Damage
    [Header("----------------------- DAMAGE -----------------------")]
    [SerializeField] GameObject showDamage; // ������ �ؽ�Ʈ ���
    [SerializeField] Transform showDmgPos;  // ������ ��� ��ġ

    // Dead
    [Header("----------------------- DEAD -----------------------")]
    [SerializeField] int deadRespawnTime = 10;  // ��Ȱ ���ð�
    [SerializeField] Text respawnTimer;         // Ÿ�̸� �ؽ�Ʈ
    [SerializeField] GameObject respawnUI;      // ���� ȭ��
    [SerializeField] Transform respawnPostion;  // ��Ȱ ������
    [SerializeField] GameObject body;           // �÷��̾� �ٵ�
    [SerializeField] GameObject state;          // �÷��̾� �Ӹ��� ���� ǥ��â
    [SerializeField] LayerMask deadLayer;       // ���� �� ���̾�
    [SerializeField] LayerMask playerLayer;     // ���� ���̾�
    // Skill
    [Header("----------------------- SKILL -----------------------")]
    [SerializeField] GameObject[] coolTimeGroup;
    [SerializeField] Image[] coolTimeImages;
    [SerializeField] Text[] coolTimeText;

    // ��ų ��Ÿ��
    public int qSkillCoolTime;
    public int wSkillCoolTime;
    public int eSkillCoolTime;
    public int rSkillCoolTime;
    public int dSkillCoolTime;
    public int fSkillCoolTime;

    // ��ų �� �� ���Ҵ��� ����
    public bool isSkill;                      
    public bool isQcool , isWcool, isEcool, isRcool, isDcool, isFcool;


    // Q Skill
    [Header("----------------------- Q Skill -----------------------")]
    [SerializeField] GameObject QskillAxePrefab;   // ���� ������
    [SerializeField] float axeMaxDistance;         // ���� �ִ� ���� �Ÿ�
    GameObject axeInstance;                        // ������ ����
    Vector3 axeMoveDir;                            // ���� �̵�����

    bool isAxeThrow = false;                // ���� ���� ����
    bool isAxeMaxDistance = false;          // ���� �ִ�Ÿ��� ��Ҵ��� ����

    // W Skill
    [Header("----------------------- W Skill -----------------------")]
    [SerializeField] GameObject WSkillManager;
    [SerializeField] ParticleSystem wEffect;

    // E Skill
    [Header("----------------------- E Skill -----------------------")]
    [SerializeField] float eJumpPower;

    // D Skill
    [Header("----------------------- D Skill -----------------------")]
    [SerializeField] float flashDistance;          // ���� �Ÿ�
    [SerializeField] ParticleSystem dFlashEffect;  // ���� ȿ��


    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        // Navigation
        nav.updateRotation = false;
        nav.speed = walkSpeed;

        // Mouse ȭ�� ������ �� ������
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

    void HpGageBarFollow() // Hp �ٰ� �׻� ī�޶� �ٶ󺸰�
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    void MovePoint() // ���콺 Ŭ�� ��ġ
    {
        if (Input.GetMouseButton(1) && !isDead)
        {
            RaycastHit hit; // ���콺 ��Ʈ ����Ʈ

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

    void Move() // �̵�
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

            // �̵��� ���� ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }

        // ĳ���� ȸ�� ����
        moveDir = new Vector3(nav.steeringTarget.x, transform.position.y, nav.steeringTarget.z) - transform.position;
    }

    void Stop() // ĳ���� �̵� ����
    {
        nav.ResetPath();
    }

    void InputStop() // SŰ ������ �̵� ����
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
    }

    void Attack() // ����
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

    public void OnAttackCollision() // ���� �ݶ��̴� Ȱ��ȭ
    {
        attackCollision.SetActive(true);
    }
    

    public void TakeDamage(int _damage) // ���� ������
    {
        // ü�� ����
        curHp -= _damage;
        hpGage.value -= _damage;
        canvasHpGage.value -= _damage;

        canvasHpText.text = maxHp + " / " + (curHp <= 0 ? "0" : curHp);


        // ������ ���
        GameObject showDmgInstance = Instantiate(showDamage, showDmgPos.position, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();

        // ��� ������ ī�޶� �ٶ󺸱�
        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        
        // ������ ��� ȿ��
        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);

        // �÷��̾� ���
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // ���̾�
        gameObject.layer = 8;

        // �ִϸ��̼�
        anim.SetTrigger("isDie");

        // ������
        Stop();

        // Ÿ�̸�
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
        col.enabled = false;

        // ���̾� ����

        for (int i = deadRespawnTime; i > 0; i--)
        {
            respawnTimer.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        respawnUI.SetActive(false);

        gameObject.layer = playerLayer;
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
        col.enabled = true;

        isDead = false;
    }

    public void GetHeal(int healHp)
    {
        curHp += healHp;
        hpGage.value += healHp;
        canvasHpGage.value += healHp;
        canvasHpText.text = maxHp + " / " + (curHp <= 0 ? "0" : curHp);

        if (curHp > maxHp)
        {
            curHp = maxHp;
            hpGage.value = maxHp;
            canvasHpGage.value += maxHp;
            canvasHpText.text = maxHp + " / " + (curHp <= 0 ? "0" : curHp);
        }
    }

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
            axeInstance.GetComponent<Axe>().ThrowBack(transform);

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

            wEffect.gameObject.SetActive(true);
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
}
