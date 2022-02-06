using System.Collections.Generic;
using WarOOP.Models;
using Xunit;

namespace Tests;

public class ArmyTests
{
    public static IEnumerable<object[]> TestData1 =>
        new List<object[]>
        {
            new object[] { "Warrior", 15, "Knight", 7 },
            new object[] { "Knight", 10, "Knight", 7 },
            new object[] { "Knight", 5, "Warrior", 7 },
            new object[] { "Warrior", 10, "Warrior", 7 }
        };

    public static IEnumerable<object[]> TestData2 =>
        new List<object[]>
        {
            new object[] { "Warrior", 5, "Knight", 10 },
            new object[] { "Knight", 10, "Knight", 12 },
            new object[] { "Knight", 5, "Warrior", 9 },
            new object[] { "Warrior", 10, "Warrior", 11 }
        };

    [Theory]
    [MemberData(nameof(TestData1))]
    public void FirstWinSecond(string armyType1, int armyCount1, string armyType2, int armyCount2)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits(armyType1, armyCount1);
        army2.AddUnits(armyType2, armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData2))]
    public void SecondWinFirst(string armyType1, int armyCount1, string armyType2, int armyCount2)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits(armyType1, armyCount1);
        army2.AddUnits(armyType2, armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.False(result);
    }
    
    [Fact]
    public void AddWarriors()
    {
        var army1 = new Army();
        army1.AddUnits("Warrior", 3);

        var result = army1.HasUnits;
        
        Assert.True(result);
    }

    [Fact]
    public void AddKnights()
    {
        var army1 = new Army();
        army1.AddUnits("Knight", 3);

        var result = army1.HasUnits;
        
        Assert.True(result);
    }

}