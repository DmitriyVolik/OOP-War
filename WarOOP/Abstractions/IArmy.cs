using WarOOP.Models;

namespace WarOOP.Abstractions;

public interface IArmy
{
    public void AddUnits(string type, int count);
    
    public Warrior GetUnit();
}