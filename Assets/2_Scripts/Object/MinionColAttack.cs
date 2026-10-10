using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionColAttack : MonoBehaviour
{
    enum MinionType { Red, Blue };
    [SerializeField] MinionType minionType;

    private void OnEnable()
    {
        StartCoroutine("AttackRoutien");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (minionType == MinionType.Red)
        {
            if (other.CompareTag("EnemyBlue"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<EnemyHp>().TakeDamage(_damage);
            }

            if (other.CompareTag("TowerBlue"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<Tower>().TakeDamage(_damage);
            }
        }

        if (minionType == MinionType.Blue)
        {
            if (other.CompareTag("Player"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<PlayerHp>().TakeDamage(_damage);
            }

            if (other.CompareTag("EnemyRed"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<EnemyHp>().TakeDamage(_damage);
            }

            if (other.CompareTag("TowerRed"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<Tower>().TakeDamage(_damage);
            }
        }
    }

    IEnumerator AttackRoutien()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
