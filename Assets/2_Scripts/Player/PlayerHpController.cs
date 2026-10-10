using PB.MANAGER;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpController : MonoBehaviour
{
    [SerializeField] Slider hpGage;                 
    private Player player;
    private int curHp;
    private int maxHp;

    private void Awake()
    {
        player = GetComponent<Player>();
        maxHp = player.Data.MaxHp;
        curHp = maxHp;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void EnableUI()
    {
        hpGage.transform.parent.gameObject.SetActive(true);
    }
    
    public void DisableUI()
    {
        hpGage.transform.parent.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage) 
    {
        curHp -= damage;
        UpdateUI();

        var dmgTxt = PObjectPoolManager.Instance.Get<WorldSpaceDamageText>("DamageText", transform.position);
        dmgTxt.Set(damage);

        if (curHp <= 0) player.Die();
    }
    
    public bool GetHeal(int healHp)
    {
        if ( player.CurState == Player.State.Dead || IsFullHp()) return false;
    
        var healAmount = Mathf.Min(healHp, maxHp - curHp);
        curHp += healAmount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        hpGage.value = curHp;
        UIManager.Instance.UpdateHpUI(curHp, maxHp);
    }

    public void SetFullHp()
    {
        curHp = maxHp;
        UpdateUI();
    }

    public bool IsFullHp()
    {
        return curHp >= maxHp;
    }
}