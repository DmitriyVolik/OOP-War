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
                new object[] { 15, 7, true },
                new object[] { 10, 7, true },
                new object[] { 5, 10, false },
                new object[] { 10, 12, false },
            };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_WarriorsAndWarriors_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();

        army1.AddUnits<Warrior>(armyCount1);
        army2.AddUnits<Warrior>(armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_WarriorsAndKnights_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();

        army1.AddUnits<Warrior>(armyCount1);
        army2.AddUnits<Knight>(armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_KnightsAndKnights_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();

        army1.AddUnits<Knight>(armyCount1);
        army2.AddUnits<Knight>(armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_KnightsAndWarriors_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();

        army1.AddUnits<Warrior>(armyCount1);
        army2.AddUnits<Knight>(armyCount2);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Fight_EmptyArmies_Exception()
    {
        var army1 = new Army();
        var army2 = new Army();
        
        Assert.Throws<Exception>(()=> Battle.Fight(army1, army2));
    }
    
    [Theory]
    [MemberData(nameof(TestData1))]
    public void FightWithDefenders(string armyType1, int armyCount1, string armyType2, int armyCount2)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits(armyType1, armyCount1);
        army1.AddUnits("Defender", 6);
        army2.AddUnits(armyType2, armyCount2);
        army2.AddUnits("Defender", 5);

        var result = Battle.Fight(army1, army2);

        Assert.True(result);
    }

    [Fact]
    public void Fight_EmptyArmy_False()
    {
        var army1 = new Army();
        var army2 = new Army();
        army2.AddUnits<Warrior>( 2);

        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }

    [Fact]
    public void AddUnits_Warrior_Correct()
    {
        var army1 = new Army();
        army1.AddUnits<Warrior>(3);

        var result = army1.HasUnits;
        
        Assert.True(result);
    }

    [Fact]
    public void AddUnits_Knights_Correct()
    {
        var army1 = new Army();
        army1.AddUnits<Knight>( 3);

        var result = army1.HasUnits;
        
        Assert.True(result);
    }

}
