using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class SkillSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SkillBase.KeyType keyType;
    [SerializeField] Image coolTimeImage;
    [SerializeField] Text coolTimeText;
    [SerializeField] Image iconImage;
    public SkillBase.KeyType KeyType => keyType;

    private SkillDataSO skillData;

    public void Init(SkillDataSO data)
    {
        skillData = data;
        iconImage.sprite = skillData.Icon;
    }

    public void StartCooldown()
    {
        coolTimeImage.gameObject.SetActive(true);
        coolTimeImage.fillAmount = 1;
        StartCoroutine(CoolDownRoutine());
    }

    private IEnumerator CoolDownRoutine()
    {
        float time = 0;
        int duration = skillData.CoolTime;

        while (time < duration)
        {
            time += Time.deltaTime;
            coolTimeImage.fillAmount = 1 - (time / duration);
            coolTimeText.text = Mathf.Ceil(duration - time).ToString();
            yield return null;
        }
        coolTimeImage.fillAmount = 0;
        coolTimeText.text = "0";
        coolTimeImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowSkillDesc(keyType, skillData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideSkillDesc();
    }
}
