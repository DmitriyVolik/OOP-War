using WarOOP.Models;

namespace WarOOP.Abstractions;

public interface IArmy
{
    public void AddUnits(Type type, int count);
    
    public Warrior GetUnit();
}