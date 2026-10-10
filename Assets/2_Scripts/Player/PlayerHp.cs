using PB.MANAGER;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
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

    private void OnEnable()
    {
        player.onRespawn += SetFullHp;
    }

    private void OnDisable()
    {
        player.onRespawn -= SetFullHp;
    }

    private void Start()
    {
        UpdateUI();
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