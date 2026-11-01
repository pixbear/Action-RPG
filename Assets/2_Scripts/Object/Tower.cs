using PB.MANAGER;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IDamageable
{
    [System.Serializable]
    public class TowerData
    {
        public int MaxHp;
        public int Damage;
        public float AttackRange;
        public float AttackRate;
    }

    [SerializeField] TeamType teamType;
    [SerializeField] GameObject brokenTower;
    [SerializeField] GameObject attackRangeLine;
    [SerializeField] Transform firePos;
    [SerializeField] LayerMask attackLayer;
    [SerializeField] Slider hpGage;


    [SerializeField]  private TowerData data;
    private float curAttackRate;
    private Transform curTarget = null;
    private int curHp;



    private void Start()
    {
        curHp = data.MaxHp;
        curAttackRate = data.AttackRate;
        attackRangeLine.transform.localScale = new Vector3(data.AttackRange * 2, 1, data.AttackRange * 2);
    }

    private void Update()
    {
        if (curTarget != null)
        {
            curAttackRate -= Time.deltaTime;
            if (curAttackRate <= 0)
            {
                Attack();
                curAttackRate = data.AttackRate;
            }
        }
    }

    private void Attack()
    {
        if (curTarget == null) return;
        var bullet = PObjectPoolManager.Instance.Get("Bullet", firePos.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(curTarget, data.Damage);
    }

    private void LateUpdate()
    {
        SearchClostedTarget();
    }

    private void SearchClostedTarget()
    {
        float closestDistance = Mathf.Infinity;
        curTarget = null;
        Collider[] targets = Physics.OverlapSphere(transform.position, data.AttackRange, attackLayer);
        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                curTarget = target.transform;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp < 0) curHp = 0;
        UpdateUI();

        var dmgTxt = PObjectPoolManager.Instance.Get<WorldSpaceDamageText>("DamageText", transform.position);
        dmgTxt.Set(damage);

        if (curHp <= 0) Die();
    }

    private void UpdateUI()
    {
        hpGage.value = (float)curHp / data.MaxHp;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        brokenTower.SetActive(true);
        Destroy(brokenTower, 5f);
        gameObject.layer = 8;
        ShowCoutionMessage();
    }

    private void ShowCoutionMessage()
    {
        var message = string.Format("- The {0} team tower has been destroyed -", teamType.ToString());
        UIManager.Instance.ShowCoutionText(message);
    }
}
