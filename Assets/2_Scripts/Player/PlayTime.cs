using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTime : MonoBehaviour
{
    [SerializeField] Text timeText;

    //int second = 0; // УЪ
    int minute = 0; // Ка

    int maxSecond = 60;
    //int maxMinute = 60;



    private void Start()
    {
        StartCoroutine(PlayTimeRoutien());
    }

    IEnumerator PlayTimeRoutien()
    {
        for (int second = 0; second < maxSecond; second++)
        {
            timeText.text = (minute < 10 ? "0" + minute : minute) + ":" +
                            (second < 10 ? "0" + second : second);

            yield return new WaitForSeconds(1f);
        }
        minute ++;

        StartCoroutine(PlayTimeRoutien());
    }
}
