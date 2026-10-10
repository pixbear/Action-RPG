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

    private void OnEnable()
    {
        enemy.onEnemySearched += OnEnemySearched;
        enemy.onStateChanged += (state) =>
        {
            if (state == State.Dead) Stop();
        };
    }

    private void OnDisable()
    {
        enemy.onEnemySearched -= OnEnemySearched;
        enemy.onStateChanged -= (state) =>
        {
            if (state == State.Dead) Stop();
        };
    }

    private void OnEnemySearched(Transform target)
    {
        enemy.SetState(State.Move);
        nav.SetDestination(target.position);
    }

    private void Stop()
    {
        nav.ResetPath();
    }
}