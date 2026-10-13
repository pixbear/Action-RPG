using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private Enemy enemy;
    private NavMeshAgent nav;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        bool isDead = enemy.CurState == State.Dead;
        bool noTarget = enemy.Target == null;
        if (isDead || noTarget) return;

        transform.LookAt(enemy.Target.position);
    }


    private void OnEnable()
    {
        enemy.onEnemySearched += MoveTo;
        enemy.onStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        enemy.onEnemySearched -= MoveTo;
        enemy.onStateChanged -= OnStateChanged;
    }

    private void MoveTo(Transform target)
    {
        nav.SetDestination(target.position);
    }

    private void Stop()
    {
        nav.ResetPath();
    }

    private void OnStateChanged(State state)
    {
        if (state == State.Dead) Stop();
    }
}