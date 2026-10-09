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
        // 레드 미니언
        if (minionType == MinionType.Red)
        {
            if (other.CompareTag("EnemyBlue"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<Enemy>().TakeDamage(_damage);
            }

            if (other.CompareTag("TowerBlue"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<Tower>().TakeDamage(_damage);
            }
        }

        // 블루 미니언
        if (minionType == MinionType.Blue)
        {
            if (other.CompareTag("Player"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<PlayerController>().TakeDamage(_damage);
            }

            if (other.CompareTag("EnemyRed"))
            {
                int _damage = Random.Range(10, 15);
                other.GetComponent<Enemy>().TakeDamage(_damage);
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
