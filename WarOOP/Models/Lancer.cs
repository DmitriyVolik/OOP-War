namespace WarOOP.Models;

public class Lancer : Warrior
{
    public Lancer()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 6;
    }

    public void AttackTo(Warrior enemy1, Warrior? enemy2)
    {
        var damage = enemy1.GetDamageFrom(this);
        enemy2?.GetDamage(damage / 2);
    }
}