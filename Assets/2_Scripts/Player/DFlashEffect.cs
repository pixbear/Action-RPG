using UnityEngine;

public class DFlashEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 1f);
    }
}
