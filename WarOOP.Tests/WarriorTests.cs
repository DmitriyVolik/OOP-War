using System.Collections.Generic;
using WarOOP.Models;
using Xunit;

namespace Tests;

public class WarriorTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new Knight(), new Warrior() },
            new object[] { new Warrior(), new Warrior() },
            new object[] { new Knight(), new Knight() },
        };
    
    public static IEnumerable<object[]> TestData2 =>
        new List<object[]>
        {
            new object[] { new Warrior(), new Knight() },
            new object[] { new Warrior(), new Defender() },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void FirstWinSecond(Warrior warrior1, Warrior warrior2)
    {
        var result = Battle.Fight(warrior1, warrior2);

        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData2))]
    public void SecondWinFirst(Warrior warrior1, Warrior warrior2)
    {
        
        var result = Battle.Fight(warrior1, warrior2);

        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void WarriorAttackWarrior(Warrior warrior1, Warrior warrior2)
    {
        warrior1.AttackTo(warrior2);

        Assert.True(warrior2.CurrentHealth < warrior2.StartHealth);
    }

    [Fact]
    public void WarriorAttackDefender()
    {
        var warrior = new Warrior();
        var defender = new Defender();
        
        warrior.AttackTo(defender);
        var result = defender.CurrentHealth == defender.StartHealth - (warrior.Attack - defender.Defense);
        
        Assert.True(result);
    }

}