using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Flash : SkillBase
{
        // D Skill
//     [Header("----------------------- D Skill -----------------------")]
//     [SerializeField] float flashDistance;          // ���� �Ÿ�
//     [SerializeField] ParticleSystem dFlashEffect;  // ���� ȿ��
//  void Skilld()
//     {
//         if (Input.GetKeyDown(KeyCode.D) && !isAttack && !isSkill && !isDcool)
//         {
//             // Cool Time Start 
//             isDcool = true;
//             StartCoroutine(SkillCoolTimeRoutien(dSkillCoolTime, 4, isDcool));

//             Instantiate(dFlashEffect, transform.position + Vector3.up * 1, Quaternion.identity);


//             RaycastHit hit;

//             if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mouseHitLayer))
//             {
//                 Vector3 dir = hit.point - transform.position;
//                 transform.forward = dir;
//                 nav.SetDestination(hit.point);

//                 if (Vector3.Distance(transform.position, hit.point) > flashDistance)
//                 {
//                     transform.position += dir.normalized * flashDistance;
//                 }
//                 else
//                 {
//                     transform.position = hit.point;
//                 }
//             }
//         }

//         if (coolTimeImages[4].fillAmount <= 0)
//         {
//             isDcool = false;
//             coolTimeImages[4].fillAmount = 1;
//         }
//     }

}
