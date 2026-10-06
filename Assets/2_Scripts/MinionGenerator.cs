using System.Collections;
using UnityEngine;

public class MinionGenerator : MonoBehaviour
{
    [SerializeField] GameObject minPrefabRed;
    [SerializeField] GameObject minPrefabBlue;
    [SerializeField] Transform minGenPosRed;
    [SerializeField] Transform minGenPosBlue;

    [SerializeField] int startGenTime;
    [SerializeField] int minionGenTime; // �̴Ͼ� ���� �ֱ� Ÿ��




    private void Start()
    {
        Invoke("StartGen", startGenTime);
    }

    void StartGen()
    {
        StartCoroutine(GenMinionRoutien());
    }

    IEnumerator GenMinionRoutien() 
    {
        UI_Manager.Instance.ShowCoutionText(1);

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

        yield return new WaitForSeconds(minionGenTime);
        StartCoroutine(GenMinionRoutien());
    }
}
