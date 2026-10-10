using System.Collections;
using UnityEngine;

public class CollisionAttack : MonoBehaviour
{
    enum MinionType { Red, Blue };
    [SerializeField] MinionType minionType;

    private void OnEnable()
    {
        StartCoroutine("AttackRoutien");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlue"))
        {
            int _damage = Random.Range(20, 25);
            other.GetComponent<EnemyHp>().TakeDamage(_damage);
        }

        if (other.CompareTag("TowerBlue"))
        {
            int _damage = Random.Range(20, 25);
            other.GetComponent<Tower>().TakeDamage(_damage);
        }
    }

    IEnumerator AttackRoutien()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
