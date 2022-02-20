using WarOOP.Models;

namespace WarOOP.Abstractions;

public interface IArmy
{
    void AddUnits<T>(int count) where T : Warrior, new();
    
    public Warrior GetUnit();
}