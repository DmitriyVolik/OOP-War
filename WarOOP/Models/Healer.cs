namespace WarOOP.Models;

public class Healer : Warrior
{
    public int MedKitCount { get; private set;}
    
    private int _healPower;
    
    public int HealPower
    {
        get
        {
            if (_healPower + Equipment.HealPower < 0)
            {
                return 0;
            }

            return _healPower + Equipment.HealPower;
        }
        
        private set => _healPower = value;
    }

    public Healer()
    {
        HealPower = 2;
        CurrentHealth = 60;
        StartHealth = CurrentHealth;
        Attack = 0;
    }

    public override void PrepareForBattle()
    {
        MedKitCount = 100;
    }

    protected internal override void Action(Warrior warrior)
    {
        if (MedKitCount > 0)
        {
            Heal(warrior);
            MedKitCount--;
        }
        UnitBehind?.Action(this);
    }

    private void Heal(Warrior warrior)
    {
        if (warrior.CurrentHealth + HealPower > warrior.StartHealth)
        {
            warrior.CurrentHealth = warrior.StartHealth;
        }
        else
        {
            warrior.CurrentHealth += HealPower;
        }
    }
}