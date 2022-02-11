namespace WarOOP.Models;

public class Hit
{
    public int Damage;

    public Warrior Enemy;
    
    public Hit(int damage, Warrior enemy)
    {
        Damage = damage;
        Enemy = enemy;
    }
}