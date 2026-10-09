using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPackGenerator : MonoBehaviour
{          
    float rotSpeed = 30f;                         // ÈúÆÑ È¸Àü ¼Óµµ

    [SerializeField] int healHp;                  // Èú ·®
    [SerializeField] int healPackCoolTime;        // ÄðÅ¸ÀÓ    
    [SerializeField] GameObject healPack;         // ÈúÆÑ
    [SerializeField] GameObject healPackCanvas;   // ÈúÆÑ ÄðÅ¸ÀÓ Äµ¹ö½º
    [SerializeField] Text coolTimeText;           // ÄðÅ¸ÀÓ ÅØ½ºÆ®



    private void Start()
    {
        coolTimeText.text = healPackCoolTime.ToString();
    }

    private void Update()
    {
        healPack.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            // ÇÃ·¹ÀÌ¾î Ã¼·Â Áõ°¡
            if (player.curHp == player.maxHp)
            {
                return;
            }
            else
            {
                player.GetHeal(healHp);

                // ÈúÆÑ ¼û±â±â / ÄðÅ¸ÀÓ ½ÃÀÛ
                healPack.SetActive(false);
                healPackCanvas.SetActive(true);
                StartCoroutine("HealPackGenRoutien");
            }
        }
    }

    IEnumerator HealPackGenRoutien()
    {
        for (int i = healPackCoolTime; i > 0; i--)
        {            
            coolTimeText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        //yield return new WaitForSeconds(1f);

        healPack.SetActive(true);
        healPackCanvas.SetActive(false);
    }
}
