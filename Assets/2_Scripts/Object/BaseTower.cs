using PB.MANAGER;
using PB.SYSTEM;
using UnityEngine;
using UnityEngine.UI;

public class BaseTower : MonoBehaviour, IDamageable
{
    [SerializeField] Slider hpGage;         
    [SerializeField] private int maxHp;
    private int curHp;


    private void Start()
    {
        curHp = maxHp;
        UpdateUI();
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
        hpGage.value = (float)curHp / maxHp;
    }

    private void Die()
    {
        PLog.Todo("Game over logic for the tower");
        GameManager.Instance.GameOver();
    }
}
