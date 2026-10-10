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

    //     [SerializeField] float axeMoveSpeed;    
    // [SerializeField] float axeRotSpeed;   
    // [SerializeField] float axeGroupRotSpeed;  
    // [SerializeField] int ESkillDamage; 
    // [SerializeField] GameObject[] axePrefab;
    // [SerializeField] Transform[] axePos;
    // [SerializeField] Transform[] originPos;

    // [SerializeField] Transform playerPos;
    // [SerializeField] Transform axeGroup;
    // [SerializeField] Transform axePosGroup;



    // void Update()
    // {
    //     if (axeGroup.gameObject.activeSelf == true)
    //     {
    //         // ���� ȸ��
    //         axeGroup.Rotate(Vector3.up, axeGroupRotSpeed * Time.deltaTime);
    //         axePosGroup.rotation = axeGroup.rotation;

    //         foreach (GameObject axe in axePrefab)
    //         {
    //             axe.transform.Rotate(-Vector3.forward * axeRotSpeed * Time.deltaTime);
    //         }

    //         // ������ �Ÿ� Ȯ��
    //         for (int i = 0; i < axePrefab.Length; i++)
    //         {
    //             axePrefab[i].transform.position = Vector3.MoveTowards(axePrefab[i].transform.position,
    //                                                                   axePos[i].position,
    //                                                                   axeMoveSpeed * Time.deltaTime);
    //         }
    //     }
    // }

    // private void LateUpdate()
    // {
    //     if (axeGroup.gameObject.activeSelf == true)
    //     {
    //         axeGroup.position = playerPos.position + Vector3.up * 0.8f;
    //         axePosGroup.position = axeGroup.position;
    //     }
    // }

    // IEnumerator AxeRoutien()
    // {
    //     axeGroup.gameObject.SetActive(true);

    //     yield return new WaitForSeconds(8);

    //     axeGroup.gameObject.SetActive(false);



    //     for (int i = 0; i < axePrefab.Length; i++)
    //     {
    //         axePrefab[i].transform.position = originPos[i].position;
    //     }
    // }

    // public void OnESkill()
    // {
    //     StartCoroutine(AxeRoutien());
    // }
}
