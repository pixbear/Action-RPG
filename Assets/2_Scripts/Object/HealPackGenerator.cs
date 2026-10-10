using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealPackGenerator : MonoBehaviour
{
    [SerializeField] int healHp;
    [SerializeField] int healPackCoolTime;

    [SerializeField] GameObject healPack;
    [SerializeField] Text coolTimeText;

    private bool isCooltime;
    private float rotSpeed = 30f;


    private void Update()
    {
        healPack.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCooltime) return;

        if (other.CompareTag("Player"))
        {
            PlayerHp playerHp = other.GetComponent<PlayerHp>();

            if (playerHp.GetHeal(healHp))
            {
                StartCoroutine(HealPackGenRoutien());
            }
        }
    }

    IEnumerator HealPackGenRoutien()
    {
        isCooltime = true;
        healPack.SetActive(false);
        coolTimeText.gameObject.SetActive(true);

        for (int i = healPackCoolTime; i > 0; i--)
        {
            coolTimeText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        isCooltime = false;
        healPack.SetActive(true);
        coolTimeText.gameObject.SetActive(false);
    }
}
