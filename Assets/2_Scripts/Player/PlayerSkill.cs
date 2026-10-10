using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] SkillBase qSkill;
    [SerializeField] SkillBase wSkill;
    [SerializeField] SkillBase eSkill;
    [SerializeField] SkillBase rSkill;
    [SerializeField] SkillBase dSkill;
    [SerializeField] SkillBase fSkill;

    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        var data = player.Data;
        qSkill.Init(SkillBase.KeyType.Q, data.QSkill);
        wSkill.Init(SkillBase.KeyType.W, data.WSkill);
        eSkill.Init(SkillBase.KeyType.E, data.ESkill);
        rSkill.Init(SkillBase.KeyType.R, data.RSkill);
        dSkill.Init(SkillBase.KeyType.D, data.DSkill);
        fSkill.Init(SkillBase.KeyType.F, data.FSkill);
    }
}