using System;
using System.Collections.Generic;
using WarOOP.Models;
using Xunit;

namespace Tests;

public class ArmyTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
            {
                new object[] { typeof(Warrior), 15, typeof(Knight), 7, true },
                new object[] { typeof(Knight), 10, typeof(Knight), 7, true },
                new object[] { typeof(Warrior), 5, typeof(Knight), 10, false },
                new object[] { typeof(Knight), 10, typeof(Knight), 12, false },
            };

    [Theory]
    [MemberData(nameof(TestData))]
    public void ArmyAndArmy_Fight_Correct(Type armyType1, int armyCount1, Type armyType2, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits(armyType1, armyCount1);
        army2.AddUnits(armyType2, armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateWarrior_WrongType_Exception()
    {
        Assert.Throws<Exception>(()=> Warrior.CreateWarrior(typeof(string)));
    }
    
    [Fact]
    public void Fight_EmptyArmies_Exception()
    {
        var army1 = new Army();
        var army2 = new Army();
        
        Assert.Throws<Exception>(()=> Battle.Fight(army1, army2));
    }
    
    [Fact]
    public void Fight_EmptyArmy_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army2.AddUnits(typeof(Warrior), 2);

        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }

    [Fact]
    public void AddUnits_Warrior_Correct()
    {
        var army1 = new Army();
        army1.AddUnits(typeof(Warrior), 3);

        var result = army1.HasUnits();
        
        Assert.True(result);
    }

    [Fact]
    public void AddUnits_Knights_Correct()
    {
        var army1 = new Army();
        army1.AddUnits(typeof(Knight), 3);

        var result = army1.HasUnits();
        
        Assert.True(result);
    }

}