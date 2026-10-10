using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Text CoutionText;
    [SerializeField] Slider hpGage;
    [SerializeField] Text hpText;
    [SerializeField] GameObject deadUI;
    [SerializeField] Text respawnTimer;
    [SerializeField] List<SkillSlotUI> skillUIs;
    [SerializeField] Text keyTypeAndNameTxt;
    [SerializeField] Text descTxt;
    [SerializeField] Text cooltimeTxt;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCoutionText(string message, float duration = 5f)
    {
        CoutionText.text = message;
        StartCoroutine(CoutionTxtFadeRoutine(duration));
    }

    IEnumerator CoutionTxtFadeRoutine(float duration)
    {
        CoutionText.gameObject.SetActive(true);
        for (float i = 0f; i < 0.8f; i += Time.deltaTime * 2)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        for (float i = 0.8f; i > 0f; i -= Time.deltaTime * 2)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }
        CoutionText.gameObject.SetActive(false);
    }

    public void UpdateHpUI(int curHp, int maxHp)
    {
        hpGage.value = curHp;
        hpText.text = maxHp + " / " + (curHp <= 0 ? "0" : curHp);
    }

    public void ShowDeadUI(int respawnTime)
    {
        deadUI.SetActive(true);
        StartCoroutine(DeadCountdownRoutine(respawnTime));
    }

    public void HideDeadUI()
    {
        deadUI.SetActive(false);
        StopCoroutine(DeadCountdownRoutine(0));
    }

    private IEnumerator DeadCountdownRoutine(int respawnTime)
    {
        for (int i = respawnTime; i > 0; i--)
        {
            respawnTimer.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void StartCooldown(SkillBase.KeyType skillType)
    {
        var skillUI = skillUIs.Find(ui => ui.KeyType == skillType);
        if (skillUI != null)  skillUI.StartCooldown();
    }

    public void SetSkillSlot(SkillBase.KeyType keyType, SkillDataSO data)
    {
        var skillUI = skillUIs.Find(ui => ui.KeyType == keyType);
        if (skillUI != null) skillUI.Init(data);
    }

    public void ShowSkillDesc(SkillBase.KeyType keyType, SkillDataSO data)
    {
        keyTypeAndNameTxt.transform.parent.gameObject.SetActive(true);
        keyTypeAndNameTxt.text = string.Format("{0} : {1}", keyType, data.Name);
        descTxt.text = data.Desc;
        cooltimeTxt.text = string.Format("Cooldown: {0}s", data.CoolTime);
    }

    public void HideSkillDesc()
    {
        keyTypeAndNameTxt.transform.parent.gameObject.SetActive(false);
        keyTypeAndNameTxt.text = "";
        descTxt.text = "";
        cooltimeTxt.text = "";
    }   
}
