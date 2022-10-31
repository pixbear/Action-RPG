using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionGenerator : MonoBehaviour
{
    [SerializeField] GameObject minPrefabRed;
    [SerializeField] GameObject minPrefabBlue;
    [SerializeField] Transform minGenPosRed;
    [SerializeField] Transform minGenPosBlue;

    [SerializeField] int startGenTime;
    [SerializeField] int minionGenTime; // 미니언 생성 주기 타임




    private void Start()
    {
        Invoke("StartGen", startGenTime);
    }

    void StartGen()
    {
        StartCoroutine(GenMinionRoutien());
    }

    IEnumerator GenMinionRoutien() // 미니언 생성
    {
        // 생성 알림 텍스트 생성
        UI_Manager.Instance.ShowCoutionText(1);

        // 시작시 미니언 4마리 생성
        Instantiate(minPrefabRed, minGenPosRed.position, minGenPosRed.rotation);
        Instantiate(minPrefabBlue, minGenPosBlue.position, minGenPosBlue.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(minPrefabRed, minGenPosRed.position, minGenPosRed.rotation);
        Instantiate(minPrefabBlue, minGenPosBlue.position, minGenPosBlue.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(minPrefabRed, minGenPosRed.position, minGenPosRed.rotation);
        Instantiate(minPrefabBlue, minGenPosBlue.position, minGenPosBlue.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(minPrefabRed, minGenPosRed.position, minGenPosRed.rotation);
        Instantiate(minPrefabBlue, minGenPosBlue.position, minGenPosBlue.rotation);


        // 젠 타임 대기 후 미니언 생성
        yield return new WaitForSeconds(minionGenTime);
        StartCoroutine(GenMinionRoutien());
    }
}
