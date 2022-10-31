using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionUI : MonoBehaviour
{
    [SerializeField] GameObject player;


    // 스킬설명 UI 
    [SerializeField] Image skillDescriptionGroup;
    [SerializeField] Text skillNameText;
    [SerializeField] Text skillDescriptionText;
    [SerializeField] Text skillCoolTimeText;
    [SerializeField] float offset;



    // Skill SkillDescription
    public void MouseOn(int _index)
    {
        switch (_index)
        {
            case 1:
                skillNameText.text = "Q : 도끼 투척";
                skillDescriptionText.text = "전방으로 도끼를던져 닿은 적에게 25~30의 데미지를 입힌다.";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().qSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
            case 2:
                skillNameText.text = "W : 회전 도끼";
                skillDescriptionText.text = "작은 도끼를 4개 생성해 플레이어 주위를 돌며 닿은 적에게 3~5의 피해를 입힌다.";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().wSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
            case 3:
                skillNameText.text = "E : 도끼 투척";
                skillDescriptionText.text = "";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().eSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
            case 4:
                skillNameText.text = "R : 도끼 투척";
                skillDescriptionText.text = "";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().rSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
            case 5:
                skillNameText.text = "D : 순간 이동";
                skillDescriptionText.text = "마우스 방향으로 일정거리 순간이동한다.";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().dSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
            case 6:
                skillNameText.text = "F : 도끼 투척";
                skillDescriptionText.text = "";
                skillCoolTimeText.text = "쿨타임 " + player.GetComponent<PlayerController>().fSkillCoolTime + "초";

                StartCoroutine(ShowSkillDescriptionRoutien());
                break;
        }
    }

    public void MouseOut()
    {
        StopAllCoroutines();
        skillDescriptionGroup.gameObject.SetActive(false);
    }

    IEnumerator ShowSkillDescriptionRoutien()
    {
        yield return new WaitForSeconds(0.5f);

        skillDescriptionGroup.gameObject.SetActive(true);
        skillDescriptionGroup.transform.position = Input.mousePosition + Vector3.up * offset;
    }
}
