using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorESkill : SkillBase
{
    [SerializeField] float jumpPower;

    // void SkillE()
    // {
    //     if (Input.GetKeyDown(KeyCode.E) && !isAttack && !isEcool && !isSkill)
    //     {
    //         // Cool Time Start 
    //         isSkill = true;
    //         isEcool = true;
    //         StartCoroutine(SkillCoolTimeRoutien(eSkillCoolTime, 2, isEcool));

    //         rigid.AddForce(transform.up * eJumpPower, ForceMode.Impulse);

    //         anim.SetTrigger("isESkill");

    //         RaycastHit hit;
    //         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer))
    //         {
    //             axeMoveDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
    //             transform.forward = axeMoveDir;
    //         }

    //         isSkill = false;
    //     }

    //     if (coolTimeImages[2].fillAmount <= 0)
    //     {
    //         isEcool = false;
    //         coolTimeImages[2].fillAmount = 1;
    //     }
    //}
}
