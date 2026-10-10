using UnityEngine;

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

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void TryAttack()
    {
        bool isDead = enemy.CurState == State.Dead;
        bool noTarget = enemy.CloseTarget == null;
        if (isDead || noTarget) return;

        var attackRange = enemy.Data.AttackRange;
        var maxAttackRate = enemy.Data.AttackRate;

        if (Vector3.Distance(transform.position, enemy.CloseTarget.position) < attackRange)
        {
            transform.LookAt(enemy.CloseTarget.position);
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = maxAttackRate;
            }
        }
    }

    private void LookAtTarget()
    {
        bool isDead = enemy.CurState == State.Dead;
        bool noTarget = enemy.CloseTarget == null;
        if (isDead || noTarget) return;

        transform.LookAt(enemy.CloseTarget.position);
    }

    private void Attack()
    {
        enemy.SetState(State.Attack);
        attackCollsion.SetActive(true);
    }
}