using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy enemy;
    private Animator anim;

    private void Awake()
    {        
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        enemy.onStateChanged += Set;
    }

    private void OnDisable()
    {
        enemy.onStateChanged -= Set;
    }

    public void Set(State state)
    {
        switch (state)
        {
            case State.Idle:
                anim.SetBool("isMove", false);
                break;
            case State.Move:
                anim.SetBool("isMove", true);
                break;
            case State.Attack:
                anim.SetTrigger("Attack");
                break;
            case State.Dead:
                anim.SetTrigger("Dead");
                break;
        }
    }
}