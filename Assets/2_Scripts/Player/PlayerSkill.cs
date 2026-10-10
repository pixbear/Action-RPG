using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public SkillBase QSkill;
    public SkillBase WSkill;
    public SkillBase ESkill;
    public SkillBase RSkill;
    public SkillBase DSkill;
    public SkillBase FSkill;

    private void Update()
    {
        if (QSkill != null && Input.GetKeyDown(KeyCode.Q)) QSkill.TryUseSkill();
        if (WSkill != null && Input.GetKeyDown(KeyCode.W)) WSkill.TryUseSkill();
        if (ESkill != null && Input.GetKeyDown(KeyCode.E)) ESkill.TryUseSkill();
        if (RSkill != null && Input.GetKeyDown(KeyCode.R)) RSkill.TryUseSkill();
        if (DSkill != null && Input.GetKeyDown(KeyCode.D)) DSkill.TryUseSkill();
        if (FSkill != null && Input.GetKeyDown(KeyCode.F)) FSkill.TryUseSkill();
    }
}