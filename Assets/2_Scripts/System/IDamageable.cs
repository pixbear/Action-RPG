public interface IDamageable
{
    void TakeDamage(int damage);
    bool GetHeal(int healHp);
    bool IsFullHp();
}