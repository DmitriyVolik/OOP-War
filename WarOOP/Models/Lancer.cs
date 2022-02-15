namespace WarOOP.Models;

public class Lancer : Warrior
{
    public Lancer()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 6;
    }

    public override void AttackTo(Warrior enemy)
    {
        var damage = enemy.GetDamageFrom(new Hit(Attack,this));

        var nextUnit = enemy.UnitBehind;
        while (nextUnit is { IsAlive: false })
        {
            nextUnit = nextUnit.UnitBehind;
        }
        nextUnit?.GetDamageFrom(new Hit(damage/2,this));
        Action(this);
    }
}