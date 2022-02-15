namespace WarOOP.Models;

public class Healer : Warrior
{
    public int MedKitCount { get; private set;}

    public Healer()
    {
        CurrentHealth = 60;
        StartHealth = CurrentHealth;
        Attack = 0;
        MedKitCount = 100;
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
        if (warrior.CurrentHealth + 2 > warrior.StartHealth)
        {
            warrior.CurrentHealth += warrior.StartHealth - warrior.CurrentHealth;
        }
        else
        {
            warrior.CurrentHealth += 2;
        }
    }
}