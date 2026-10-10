using PB.MANAGER;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceDamageText : MonoBehaviour
{
    [SerializeField] float forceAmount = 3f;
    [SerializeField] float destroyDelay = 1f;

    private Text txt;
    private Rigidbody rb;

    private void Awake()
    {
        txt = GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
    }

    public void Set(int damage)
    {
        txt.text = damage.ToString();
    }

    private void OnEnable()
    {
        AddForce();
        PObjectPoolManager.Instance.ReleaseAfterDelay("DamageText", gameObject, destroyDelay);
    }

    private void AddForce()
    {
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), forceAmount, 0), ForceMode.Impulse);
    }
}