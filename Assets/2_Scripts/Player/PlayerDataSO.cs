using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerDataSO", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public int Level;
    public int MaxHp;
    public int MaxMp;
    public int AttackPower;
    public int AttackRange;
    public int AttackSpeed;
    public int Defense;
    public int MoveSpeed; // 4
    public int RespawnTime;
    
    public SkillDataSO QSkill;
    public SkillDataSO WSkill;
    public SkillDataSO ESkill;
    public SkillDataSO RSkill;
    public SkillDataSO DSkill;
    public SkillDataSO FSkill;
}