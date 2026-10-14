using System.Collections;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    enum MinionType { Red, Blue };
    [SerializeField] MinionType minionType;

    private void OnEnable()
    {
        StartCoroutine(AttackRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (minionType == MinionType.Red)
        {
            if (other.CompareTag("Player")) return;
            if (other.CompareTag("EnemyRed")) return;
            if (other.CompareTag("TowerRed")) return;

            other.GetComponent<IDamageable>()?.TakeDamage(10);
        }

        if (minionType == MinionType.Blue)
        {
            if (other.CompareTag("Player")) return;
            if (other.CompareTag("EnemyBlue")) return;
            if (other.CompareTag("TowerBlue")) return;

            other.GetComponent<IDamageable>()?.TakeDamage(10);
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
