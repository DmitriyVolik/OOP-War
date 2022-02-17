namespace WarOOP.Models;

public class Vampire : Warrior
{
    private int _vampirism;

    public int Vampirism
    {
        get
        {
            if (_vampirism + Equipment.Vampirism < 0)
            {
                return 0;
            }

            return _vampirism + Equipment.Vampirism;
        }
        private set => _vampirism = value;
    }

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
            //Console.WriteLine($"_____{enemy.CurrentHealth}");
            var damage = enemy.GetDamageFrom(new Hit(Attack, this));
            //Console.WriteLine($"_____{Attack}");
            //Console.WriteLine($"_____{enemy.CurrentHealth}");
            CurrentHealth += (damage * Vampirism) / 100;
            if (CurrentHealth > StartHealth)
            {
                CurrentHealth -= CurrentHealth - StartHealth;
            }
            Action(this);
        }
    }

}