using System.Collections.Generic;
using WarOOP.Models;
using WarOOP.Tests.Models;
using Xunit;

namespace WarOOP.Tests;

public class WarriorTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new Knight(), new Warrior(), true },
            new object[] { new Warrior(), new Warrior(), true },
            new object[] { new Knight(), new Knight(), true },
            new object[] { new Warrior(), new Knight(), false },
            new object[] { new Warrior(), new Defender(), false },
            new object[] { new Defender(), new Vampire(), true },
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_Warriors_Correct(Warrior warrior1, Warrior warrior2, bool expected)
    {
        var result = Battle.Fight(warrior1, warrior2);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void AttackTo_Warriors_Correct(Warrior warrior1, Warrior warrior2, bool expected)
    {
        warrior1.AttackTo(warrior2);

        Assert.True(warrior2.CurrentHealth < warrior2.StartHealth);
    }

    [Fact]
    public void Fight_WarriorAndDefender_Correct()
    {
        var warrior = new Warrior();
        var defender = new Defender();

        warrior.AttackTo(defender);
        var result = defender.CurrentHealth == defender.StartHealth - (warrior.Attack - defender.Defense);

        Assert.True(result);
    }

    [Fact]
    public void AttackTo_VampireToWarrior_Correct()
    {
        var warrior = new Warrior();
        var vampire = new Vampire();

        warrior.AttackTo(vampire);
        vampire.AttackTo(warrior);
        var expectedVampireHealth = vampire.StartHealth - warrior.Attack + vampire.Attack * vampire.Vampirism / 100;
        var result = vampire.CurrentHealth == expectedVampireHealth;

        Assert.True(result);
    }

    [Fact]
    public void AttackTo_VampireToDefender_Correct()
    {
        var defender = new Defender();
        var vampire = new Vampire();

        defender.AttackTo(vampire);
        vampire.AttackTo(defender);
        var expectedVampireHealth = vampire.StartHealth - defender.Attack +
                                    (vampire.Attack - defender.Defense) * vampire.Vampirism / 100;
        var result = vampire.CurrentHealth == expectedVampireHealth;


        Assert.True(result);
    }
    
    [Fact]
    public void AttackTo_RookieToDefender_Correct()
    {
        var rookie = new Rookie();
        var defender = new Defender();

        rookie.AttackTo(defender);
        var result = defender.CurrentHealth == defender.StartHealth;

        Assert.True(result);
    }
    
    [Fact]
    public void AttackTo_VampireFirst_Correct()
    {
        var vampire = new Vampire();
        var warrior = new Warrior();

        vampire.AttackTo(warrior);
        var result = vampire.CurrentHealth == vampire.StartHealth;

        Assert.True(result);
    }
    
    [Fact]
    public void AttackTo_LancerToWarriors_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Warrior>(1);
        //army2.SetUnitsBehind();

        var lancer = army1.GetUnit();
        lancer.AttackTo(army2.GetUnit());
        var expectedHealth1 = army2.GetUnit().StartHealth - lancer.Attack;
        var expectedHealth2 = army2.GetUnit().UnitBehind.StartHealth - lancer.Attack / 2;
        var result = army2.GetUnit().CurrentHealth == expectedHealth1 &&
                     army2.GetUnit().UnitBehind.CurrentHealth == expectedHealth2;

        Assert.True(result);
    }
    
    [Fact]
    public void Army_UnitsShiftForLancer_Correct()
    {
        var testWarrior = new Warrior();   
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<RookieLowHp>(1);
        army2.AddUnits<Warrior>(1);
        //army2.SetUnitsBehind();

        var lancer = army1.GetUnit();
        lancer.AttackTo(army2.GetUnit());
        lancer.AttackTo(army2.GetUnit());
        var expectedHealth = testWarrior.StartHealth - lancer.Attack / 2;
        var result = army2.GetUnit().UnitBehind.UnitBehind.CurrentHealth == expectedHealth;
        
        Assert.True(result);
    }
    
    [Fact]
    public void AttackTo_LancerToDefenders_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Defender>(2);
        //army2.SetUnitsBehind();

        var lancer = army1.GetUnit();
        lancer.AttackTo(army2.GetUnit());
        var defender = (Defender)army2.GetUnit();
        var defenderBehind = (Defender)army2.GetUnit().UnitBehind;
        var defenderExpectedHealth = defender.StartHealth - (lancer.Attack - defender.Defense);
        var result = defender.CurrentHealth == defenderExpectedHealth &&
                     defenderBehind.CurrentHealth == defenderBehind.StartHealth;
        
        Assert.True(result);
    }
}