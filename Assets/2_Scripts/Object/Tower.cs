        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    // Ÿ�� Ÿ��
    enum TowerType { Red, Blue };
    [SerializeField] TowerType towerType;


    [SerializeField] GameObject brokenTower;     // Ÿ�� �ı��� �� �μ��� Ÿ��
    [SerializeField] GameObject attackRangeLine; // Ÿ�� ���ݹ��� ���� ǥ�� ����
    [SerializeField] GameObject towerBullet;     // Ÿ�� ���� �߻�ü ������
    [SerializeField] Transform player;           // �÷��̾� ��ġ
    [SerializeField] Transform firePos;          // ���� �߻� ��ġ
    [SerializeField] Camera cam;                 // ī�޶�
     

    // Attack
                     float curAttackRate;     // ���� ���� �ӵ�
    [SerializeField] float maxAttackRate;     // �ִ� ���� �ӵ�
    [SerializeField] float bulletSpeed;       // �߻�ü ���ǵ�
    [SerializeField] float attackCheakRange;  // �� üũ ����
    [SerializeField] LayerMask attackLayer;   // ���� ���̾�
    public Transform closeTarget = null;      // ����� Ÿ��
    public Transform attackTarget = null;     // ������ Ÿ��

    // Hp
    [SerializeField] int maxHp;               // �ִ� ü��
    [SerializeField] int curHp;               // ���� ü��
    [SerializeField] GameObject showDamage;   // ��� ������
    [SerializeField] GameObject hpGageBar;    // ü�� ������ ĵ����
    [SerializeField] Slider hpGage;           // ü�� ������ ��

    GameObject bulletInstance;                // �߻�� �߻�ü

    private void Start()
    {
        curHp = maxHp;
        curAttackRate = maxAttackRate;

        //InvokeRepeating("EnemySearch", 0f, 0.5f);
    }

    private void Update()
    {
        TowerAttackRangeRedLine(); // ���� ���� ������� Ȱ/��Ȱ��ȭ
        BulletAttack();
    }

    private void LateUpdate()
    {
        EnemySearch();
        HpGageBarFollow();
        BulletFollowTarget();
    }

    void EnemySearch() // �ֺ��� �� Ž��
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
                    attackTarget.GetComponent<PlayerHp>().TakeDamage(_damage);
                }
                else if (attackTarget.CompareTag("EnemyRed"))
                {
                    int _damage = Random.Range(25, 30);
                    attackTarget.GetComponent<EnemyHp>().TakeDamage(_damage);
                }
                else if (attackTarget.CompareTag("EnemyBlue"))
                {
                    int _damage = Random.Range(25, 30);
                    attackTarget.GetComponent<EnemyHp>().TakeDamage(_damage);
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

        if (curHp <= 0) // �ǰ� 0�̸� ����
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
        if (towerType == TowerType.Blue)
        {
            UIManager.Instance.ShowCoutionText("- The blue team tower has been destroyed -");
        }
        else
        {
            UIManager.Instance.ShowCoutionText("- The red team tower has been destroyed -");
        }
    }
}
