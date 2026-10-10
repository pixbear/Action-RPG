using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] int damage; // 10
    [SerializeField] float moveSpeed; // 5
    [SerializeField] float rotSpeed; // 1000
    [SerializeField] float skillDuration; // 2
    [SerializeField] ParticleSystem effect;

    private Vector3 targetDir;

    private void Update()
    {
        transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

    public void SetDir(Vector3 dir)
    {
        targetDir = dir;
    }

    public void DestroySelf()
    {
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlue"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            effect.Play();
        }
    }
}
