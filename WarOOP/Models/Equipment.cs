namespace WarOOP.Models;

public class Equipment
{
    public int Health { get; private set; }

    public int Attack { get; private set; } 

    public int Defense { get; private set; }

    public int Vampirism { get; private set; }

    public int HealPower { get; private set; }

    private readonly List<Weapon> _weapons;

    public Equipment()
    {
        _weapons = new List<Weapon>();
    }

    public void AddWeapon(Weapon weapon)
    {
        Health += weapon.Health;
        Attack += weapon.Attack;
        Defense += weapon.Defense;
        Vampirism += weapon.Vampirism;
        HealPower += weapon.HealPower;
        _weapons.Add(weapon);
    }
}