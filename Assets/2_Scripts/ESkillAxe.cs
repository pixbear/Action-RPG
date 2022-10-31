using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkillAxe : MonoBehaviour
{
    [SerializeField] float axeMoveSpeed;       // 도끼 이동 스피드
    [SerializeField] float axeRotSpeed;        // 도끼 회전 스피드
    [SerializeField] float axeGroupRotSpeed;   // 도끼 그룹 회전 속도
    [SerializeField] int ESkillDamage;         // E 스킬 데미지    

    [SerializeField] GameObject[] axePrefab;
    [SerializeField] Transform[] axePos;
    [SerializeField] Transform[] originPos;

    [SerializeField] Transform playerPos;
    [SerializeField] Transform axeGroup;
    [SerializeField] Transform axePosGroup;



    void Update()
    {
        if (axeGroup.gameObject.activeSelf == true)
        {
            // 도끼 회전
            axeGroup.Rotate(Vector3.up, axeGroupRotSpeed * Time.deltaTime);
            axePosGroup.rotation = axeGroup.rotation;

            foreach (GameObject axe in axePrefab)
            {
                axe.transform.Rotate(-Vector3.forward * axeRotSpeed * Time.deltaTime);
            }

            // 밖으로 거리 확장
            for (int i = 0; i < axePrefab.Length; i++)
            {
                axePrefab[i].transform.position = Vector3.MoveTowards(axePrefab[i].transform.position,
                                                                      axePos[i].position,
                                                                      axeMoveSpeed * Time.deltaTime);
            }
        }
    }

    private void LateUpdate()
    {
        if (axeGroup.gameObject.activeSelf == true)
        {
            axeGroup.position = playerPos.position + Vector3.up * 0.8f;
            axePosGroup.position = axeGroup.position;
        }
    }

    IEnumerator AxeRoutien()
    {
        axeGroup.gameObject.SetActive(true);

        yield return new WaitForSeconds(8);

        axeGroup.gameObject.SetActive(false);

        for (int i = 0; i < axePrefab.Length; i++)
        {
            axePrefab[i].transform.position = originPos[i].position;
        }
    }

    public void OnESkill()
    {
        StartCoroutine(AxeRoutien());
    }
}
