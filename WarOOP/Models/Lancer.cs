namespace WarOOP.Models;

public class Lancer : Warrior
{
    public Lancer()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 6;
    }

    public override void AttackTo(Army army)
    {
        var damage = army.GetUnit().GetDamageFrom(new Hit(Attack,this));
        army.GetNextUnit()?.GetDamageFrom(new Hit(damage/2,this));
    }
}