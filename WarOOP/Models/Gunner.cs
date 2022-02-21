namespace WarOOP.Models;

public class Gunner : Warrior
{
    public int AmmunitionCount { get; private set; }
    
    private int AttackCount { get; set; }
    
    public Gunner()
    {
        CurrentHealth = 20;
        StartHealth = CurrentHealth;
        Attack = 0;
        AmmunitionCount = 20;
        Attack = 2;
        AttackCount = 1;
    }

    protected internal override void Action(Warrior warrior, Warrior enemy)
    {
        if (AmmunitionCount > 0 && AttackCount % 5 == 0)
        {
            enemy.GetDamageFrom(new Hit(Attack, this));
            
            while (true)
            {
                enemy = enemy.UnitBehind;
                if (enemy == null)
                {
                    break;
                }
                enemy.GetDamageFrom(new Hit(Attack, this));
            }
            AmmunitionCount--;
        }
        AttackCount++;
    }

    public override void AttackTo(Warrior enemy)
    {
        if (AmmunitionCount > 0)
        {
            enemy.GetDamageFrom(new Hit(Attack * AmmunitionCount, this));
            CurrentHealth = 0;
        }
        Action(this, enemy);
    }
}