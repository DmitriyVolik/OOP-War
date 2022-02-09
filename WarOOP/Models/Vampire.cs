namespace WarOOP.Models;

public class Vampire : Warrior
{
    public readonly int Vampirism; 
    
    public Vampire()
    {
        CurrentHealth = 40;
        StartHealth = CurrentHealth;
        Attack = 4;
        Vampirism = 50;
    }

    public override void AttackTo(Warrior enemy)
    {
        if (IsAlive)
        {
            var damage = enemy.GetDamageFrom(this);
            CurrentHealth += (damage * Vampirism) / 100;
            if (CurrentHealth > StartHealth)
            {
                CurrentHealth -= CurrentHealth - StartHealth;
            }
        }
    }
}