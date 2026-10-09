using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] float axeMoveSpeed; 
    [SerializeField] float axeRotSpeed;  
    [SerializeField] int QSkillDamage;   
    [SerializeField] ParticleSystem qEffect;

    private void Update()
    {
        transform.Rotate(-Vector3.forward * axeRotSpeed * Time.deltaTime);
    }

    public void ThrowMove(Vector3 _dir)
    {
        transform.position += _dir.normalized * axeMoveSpeed * Time.deltaTime;
    }

    public void ThrowBack(Transform _pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, _pos.position + Vector3.up * 1, axeMoveSpeed * Time.deltaTime);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlue"))
        {
            other.GetComponent<Enemy>().TakeDamage(QSkillDamage);
            qEffect.Play();
        }
    }
}
