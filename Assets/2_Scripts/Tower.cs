        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    // 타워 타입
    enum TowerType { Red, Blue };
    [SerializeField] TowerType towerType;


    [SerializeField] GameObject brokenTower;     // 타워 파괴된 후 부서진 타워
    [SerializeField] GameObject attackRangeLine; // 타워 공격범위 빨간 표시 라인
    [SerializeField] GameObject towerBullet;     // 타워 공격 발사체 프리펩
    [SerializeField] Transform player;           // 플레이어 위치
    [SerializeField] Transform firePos;          // 공격 발사 위치
    [SerializeField] Camera cam;                 // 카메라
     

    // Attack
                     float curAttackRate;     // 현재 공격 속도
    [SerializeField] float maxAttackRate;     // 최대 공격 속도
    [SerializeField] float bulletSpeed;       // 발사체 스피드
    [SerializeField] float attackCheakRange;  // 적 체크 범위
    [SerializeField] LayerMask attackLayer;   // 공격 레이어
    public Transform closeTarget = null;      // 가까운 타겟
    public Transform attackTarget = null;     // 공격할 타겟

    // Hp
    [SerializeField] int maxHp;               // 최대 체력
    [SerializeField] int curHp;               // 현재 체력
    [SerializeField] GameObject showDamage;   // 출력 데미지
    [SerializeField] GameObject hpGageBar;    // 체력 게이지 캔버스
    [SerializeField] Slider hpGage;           // 체력 게이지 바

    GameObject bulletInstance;                // 발사된 발사체

    private void Start()
    {
        curHp = maxHp;
        curAttackRate = maxAttackRate;

        //InvokeRepeating("EnemySearch", 0f, 0.5f);
    }

    private void Update()
    {
        TowerAttackRangeRedLine(); // 공격 범위 레드라인 활/비활성화
        BulletAttack();
    }

    private void LateUpdate()
    {
        EnemySearch();
        HpGageBarFollow();
        BulletFollowTarget();
    }

    void EnemySearch() // 주변의 적 탐색
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, attackCheakRange, attackLayer);

        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (closeTarget != null)
                    return;

                closeTarget = col.transform;
            }
        }

        if (closeTarget == null)
        {
            return;
        }
        else
        {
            if (Vector3.Distance(transform.position, closeTarget.position) > attackCheakRange)
            {
                closeTarget = null;
            }
        }
    }

    void BulletAttack()
    {
        if (closeTarget != null)
        {
            curAttackRate -= Time.deltaTime;

            if (curAttackRate <= 0)
            {
                TryAttack();
                curAttackRate = maxAttackRate;
            }
        }
    }

    void TryAttack()
    {
        bulletInstance = Instantiate(towerBullet, firePos.position, Quaternion.identity);
        attackTarget = closeTarget;
    }

    void BulletFollowTarget()
    {
        if (bulletInstance == null)
        {
            return;
        }
        else
        {
            bulletInstance.transform.position = Vector3.MoveTowards(bulletInstance.transform.position, attackTarget.position, bulletSpeed * Time.deltaTime);

            if (Vector3.Distance(bulletInstance.transform.position, attackTarget.position) < 0.1f)
            {
                if (attackTarget.CompareTag("Player"))
                {
                    int _damage = Random.Range(25, 30);
                    attackTarget.GetComponent<PlayerController>().TakeDamage(_damage);
                }
                else if (attackTarget.CompareTag("EnemyRed"))
                {
                    int _damage = Random.Range(25, 30);
                    attackTarget.GetComponent<Enemy>().TakeDamage(_damage);
                }
                else if (attackTarget.CompareTag("EnemyBlue"))
                {
                    int _damage = Random.Range(25, 30);
                    attackTarget.GetComponent<Enemy>().TakeDamage(_damage);
                }

                Destroy(bulletInstance);
                attackTarget = null;
            }
        }
    }

    void TowerAttackRangeRedLine()
    {
        if (towerType == TowerType.Blue)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 7f)
            {
                attackRangeLine.SetActive(true);
            }
            else
            {
                attackRangeLine.SetActive(false);
            }
        }
    }

    void HpGageBarFollow()
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    public void TakeDamage(int _damage)
    {
        curHp -= _damage;
        hpGage.value -= _damage;

        // Show Damage
        GameObject showDmgInstance = Instantiate(showDamage, transform.position + Vector3.up * 5, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();
        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);

        if (curHp <= 0) // 피가 0이면 다이
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        brokenTower.SetActive(true);
        Destroy(brokenTower, 5f);
        gameObject.layer = 8;
        // 포탑 파괴 안내 문자
        UI_Manager.Instance.ShowCoutionText(towerType == TowerType.Blue ? 2 : 3);

        // 파괴 괴는 소리 ?
    }
}
