using PB.MANAGER;
using PB.SYSTEM;
using UnityEngine;
using UnityEngine.UI;

public class BaseTower : MonoBehaviour
{
    [SerializeField] GameObject hpGageBar;  
    [SerializeField] Slider hpGage;         
    private int curHp;

    public void TakeDamage(int damage) 
    {
        curHp -= damage;
        UpdateUI();

        var dmgTxt = PObjectPoolManager.Instance.Get<WorldSpaceDamageText>("DamageText", transform.position);
        dmgTxt.Set(damage);

        if (curHp <= 0) Die();
    }      

    private void UpdateUI()
    {
        hpGage.value = curHp;
    }

    private void Die()
    {
        PLog.Todo("Game over logic for the tower");
        GameManager.Instance.GameOver();
    }
}
