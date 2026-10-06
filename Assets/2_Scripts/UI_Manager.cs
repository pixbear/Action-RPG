using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // �̱���
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


    // �ȳ����� UI
    [SerializeField] Text CoutionText;  
    [SerializeField] float fadeSpeed;  
    [SerializeField] float textTime;    
 
    float maxFade = 0.8f;
    float minFade = 0.0f;




    private void Awake()
    {
        #region �̱���
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

        Application.targetFrameRate = 60;
    }

    public void ShowCoutionText(int _num)
    {
        string showText = null;

        switch (_num)
        {
            case 1:
                showText = "- Minions have spawned -";
                break;
            case 2:
                showText = "- The blue team tower has been destroyed -";
                break;
            case 3:
                showText = "- The red team tower has been destroyed -";
                break;
        }
        CoutionText.text = showText;

        StartCoroutine(FadeRoutien());
    }

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
