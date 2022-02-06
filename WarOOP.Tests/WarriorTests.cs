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
            new object[] { new Knight(), new Knight() }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void FirstWinSecond(Warrior warrior1, Warrior warrior2)
    {
        var result = Battle.Fight(warrior1, warrior2);

        Assert.True(result);
    }

    [Fact]
    public void SecondWinFirst()
    {
        var warrior1 = new Warrior();
        var warrior2 = new Knight();

        warrior1.AttackTo(warrior2);
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

}