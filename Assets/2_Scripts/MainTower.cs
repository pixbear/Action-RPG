using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTower : MonoBehaviour
{
    Camera cam;

    [SerializeField] GameObject hpGageBar;    // 체력 게이지 캔버스
    [SerializeField] GameObject showDamage;   // 출력 데미지
    [SerializeField] int curHp;               // 현재 체력
    [SerializeField] Slider hpGage;           // 체력 게이지 바



    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        HpGageBarLookCam();
    }

    void HpGageBarLookCam()
    {
        hpGageBar.transform.LookAt(hpGageBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    public void TakeDamage(int _damage)
    {
        curHp -= _damage;
        hpGage.value -= _damage;

        // Show Damage
        GameObject showDmgInstance = Instantiate(showDamage, transform.position + Vector3.up * 5, Quaternion.identity);
        Text demageText = showDmgInstance.GetComponentInChildren<Text>();
        demageText.text = _damage.ToString();
        showDmgInstance.transform.LookAt(showDmgInstance.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        showDmgInstance.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 3, 0), ForceMode.Impulse);
        Destroy(showDmgInstance, 0.8f);
    }
}
