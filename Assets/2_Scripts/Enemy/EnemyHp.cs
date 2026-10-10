using PB.MANAGER;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] Slider hpGage;

    private Enemy enemy;
    private int curHp;
    private int maxHp;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        maxHp = enemy.Data.MaxHp;
        curHp = maxHp;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        UpdateUI();

        var dmgTxt = PObjectPoolManager.Instance.Get<WorldSpaceDamageText>("DamageText", transform.position);
        dmgTxt.Set(damage);

        if (curHp <= 0) enemy.Die();
    }

    private void UpdateUI()
    {
        hpGage.value = curHp;
    }

    private void SetFullHp()
    {
        curHp = maxHp;
        UpdateUI();
    }

    public bool IsFullHp()
    {
        return curHp >= maxHp;
    }
}