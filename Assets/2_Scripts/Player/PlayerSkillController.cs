using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{   
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
}