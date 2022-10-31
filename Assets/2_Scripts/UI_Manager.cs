using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_Manager : MonoBehaviour
{
    // 싱글톤
    private static UI_Manager instance;
    public static UI_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }


    // 안내문자 UI
    [SerializeField] Text CoutionText;  // 안내 텍스트
    [SerializeField] float fadeSpeed;   // 안내 문자 페이드 스피드
    [SerializeField] float textTime;    // 안내 문지 지속 시간
 
    float maxFade = 0.8f;
    float minFade = 0.0f;




    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    }

    // 안내 문자 (추가 수정 가능)
    public void ShowCoutionText(int _num)
    {
        string showText = null;

        switch (_num)
        {
            case 1:
                showText = "- 미니언들이 생성 되었습니다 -";
                break;
            case 2:
                showText = "- 적 포탑이 파괴 되었습니다 -";
                break;
            case 3:
                showText = "- 아군 포탑이 파괴 되었습니다 -";
                break;
        }
        CoutionText.text = showText;

        StartCoroutine(FadeRoutien());
    }

    // 안내 문자 페이드
    IEnumerator FadeRoutien()
    {
        CoutionText.gameObject.SetActive(true);
        for (float i = minFade; i < maxFade; i += Time.deltaTime * fadeSpeed)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }

        yield return new WaitForSeconds(textTime);

        for (float i = maxFade; i > minFade; i -= Time.deltaTime * fadeSpeed)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }
        CoutionText.gameObject.SetActive(false);
    }
}
