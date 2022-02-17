namespace WarOOP.Models;

public class Weapon
{
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Vampirism { get; private set; } 
    public int HealPower { get; private set; }
    
    private Weapon()
    {
        
    }

    public static Weapon CreateCustomWeapon(int health, int attack, int defense, int vampirism, int healPower)
    {
        return new Weapon
        {
            Health = health,
            Attack = attack,
            Defense = defense,
            Vampirism = vampirism,
            HealPower = healPower
        };
    }

    public static Weapon CreateSword()
    {
        return new Weapon
        {
            Health = 5,
            Attack = 2
        };
    }
    
    public static Weapon CreateShield()
    {
        return new Weapon
        {
            Health = 20,
            Attack = -1,
            Defense = 2
        };
    }
    
    public static Weapon CreateGreatAxe()
    {
        return new Weapon
        {
            Health = -15,
            Attack = 5,
            Defense = 2,
            Vampirism= 10
        };
    }
    
    public static Weapon CreateKatana()
    {
        return new Weapon
        {
            Health = -20,
            Attack = 6,
            Defense = -2,
            Vampirism= 50
        };
    }
    
    public static Weapon CreateMagicWand()
    {
        return new Weapon
        {
            Health = 30,
            Attack = 3,
            HealPower = 3
        };
    }
}