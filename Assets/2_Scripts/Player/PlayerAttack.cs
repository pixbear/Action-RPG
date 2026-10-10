using UnityEngine;

public class PlayerAttack : MonoBehaviour
{  
    [SerializeField] GameObject attackCollision;
    private Player player;
   
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        TryAttack();
    }

    private void TryAttack() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(State.Attack);
        }

        // var animStateInfo = animController.Anim.GetCurrentAnimatorStateInfo(0);
        // bool isAttack = animStateInfo.IsName("Attack Downward") || animStateInfo.IsName("Attack Horizontal");

        // if (isAttack && animStateInfo.normalizedTime > 1.0f)
        // {
        //     player.SetState(Player.State.Idle);
        // }
    }
}