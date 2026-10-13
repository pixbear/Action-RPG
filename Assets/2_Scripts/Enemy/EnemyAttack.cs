using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject attackCollsion;

    private Enemy enemy;
    private float attackTimer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        bool isDead = enemy.CurState == State.Dead;
        bool noTarget = enemy.Target == null;
        if (isDead || noTarget) return;

        var attackRange = enemy.Data.AttackRange;
        var maxAttackRate = enemy.Data.AttackRate;

        if (Vector3.Distance(transform.position, enemy.Target.position) < attackRange)
        {
            transform.LookAt(enemy.Target.position);
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = maxAttackRate;
            }
        }
    }

    private void Attack()
    {
        enemy.SetState(State.Attack);
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        attackCollsion.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackCollsion.SetActive(false);
    }
}