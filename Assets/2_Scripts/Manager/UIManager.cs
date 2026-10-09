using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Text CoutionText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCoutionText(string message, float duration = 5f)
    {
        CoutionText.text = message;
        StartCoroutine(CoutionTxtFadeRoutine(duration));
    }

    IEnumerator CoutionTxtFadeRoutine(float duration)
    {
        CoutionText.gameObject.SetActive(true);
        for (float i = 0f; i < 0.8f; i += Time.deltaTime * 2)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        for (float i = 0.8f; i > 0f; i -= Time.deltaTime * 2)
        {
            CoutionText.color = new Color(1, 1, 0, i);
            yield return null;
        }
        CoutionText.gameObject.SetActive(false);
    }
}
