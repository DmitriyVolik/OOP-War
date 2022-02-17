using System;
using System.Collections.Generic;
using System.Linq;
using WarOOP.Models;
using WarOOP.Tests.Models;
using Xunit;

namespace WarOOP.Tests;

public class ArmyTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 15, 7, true },
            new object[] { 12, 7, true },
            new object[] { 5, 12, false },
            new object[] { 10, 14, false },
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

        Assert.Throws<Exception>(() => Battle.Fight(army1, army2));
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_ArmyWithDefenders_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(armyCount1);
        army1.AddUnits<Defender>(6);
        army2.AddUnits<Warrior>(armyCount2);
        army2.AddUnits<Defender>(5);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_ArmyWithVampires_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(armyCount1);
        army1.AddUnits<Vampire>(6);
        army2.AddUnits<Warrior>(armyCount2);
        army2.AddUnits<Vampire>(5);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Fight_ArmyWithLancers_Correct(int armyCount1, int armyCount2, bool expected)
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(armyCount1);
        army1.AddUnits<Lancer>(6);
        army2.AddUnits<Warrior>(armyCount2);
        army2.AddUnits<Lancer>(6);

        var result = Battle.Fight(army1, army2);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Fight_EmptyArmyFaAddUnits()
    {
        var army1 = new Army();
        var army2 = new Army();
        army2.AddUnits<Warrior>(2);

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
        army1.AddUnits<Knight>(3);

        var result = army1.HasUnits;

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
        army1.PrepareForFight();
        army2.PrepareForFight();

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
        army1.PrepareForFight();
        army2.PrepareForFight();
        
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
        army1.PrepareForFight();
        army2.PrepareForFight();

        var lancer = army1.GetUnit();
        lancer.AttackTo(army2.GetUnit());
        var defender = (Defender)army2.GetUnit();
        var defenderBehind = (Defender)army2.GetUnit().UnitBehind;
        var defenderExpectedHealth = defender.StartHealth - (lancer.Attack - defender.Defense);
        var result = defender.CurrentHealth == defenderExpectedHealth &&
                     defenderBehind.CurrentHealth == defenderBehind.StartHealth;
        
        Assert.True(result);
    }
    
    [Fact]
    public void AttackTo_WithHealer_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(1);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Healer>(1);
        army1.PrepareForFight();
        army2.PrepareForFight();
        
        army1.GetUnit().AttackTo(army2.GetUnit());
        army2.GetUnit().AttackTo(army1.GetUnit());
        var expectedHealth = army2.GetUnit().StartHealth - army1.GetUnit().Attack + 2;
        var result = army2.GetUnit().CurrentHealth == expectedHealth;
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_1_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(1);
        army2.AddUnits<Warrior>(2);

        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_2_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(2);
        army2.AddUnits<Warrior>(3);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_3_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(5);
        army2.AddUnits<Warrior>(7);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_4_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(20);
        army2.AddUnits<Warrior>(21);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_5_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(10);
        army2.AddUnits<Warrior>(11);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_6_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(11);
        army2.AddUnits<Warrior>(7);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_7_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(5);
        army1.AddUnits<Defender>(4);
        army1.AddUnits<Defender>(5);
        army2.AddUnits<Warrior>(4);
            
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_8_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(5);
        army1.AddUnits<Warrior>(20);
        army2.AddUnits<Warrior>(21);
        army1.AddUnits<Defender>(4);

        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_9_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Warrior>(10);
        army1.AddUnits<Defender>(5);
        army2.AddUnits<Warrior>(5);
        army1.AddUnits<Defender>(10);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_10_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(2);
        army1.AddUnits<Warrior>(1);
        army1.AddUnits<Defender>(1);
        army2.AddUnits<Warrior>(5);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_11_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(5);
        army1.AddUnits<Vampire>(6);
        army1.AddUnits<Warrior>(7);
        army2.AddUnits<Warrior>(6);
        army2.AddUnits<Defender>(6);
        army2.AddUnits<Vampire>(6);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_12_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(2);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(3);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_13_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(11);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(13);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_14_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(9);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(8);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(13);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_15_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(5);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(5);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_16_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(7);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }

    [Fact]
    public void Fight_17_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(7);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Healer>(1);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_18_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(1);
        army1.AddUnits<Warrior>(3);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Knight>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Healer>(1);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void StraightFight_1_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(5);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(5);
        
        var result = Battle.StraightFight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void StraightFight_2_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(7);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.StraightFight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void StraightFight_3_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(7);
        army1.AddUnits<Vampire>(3);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Healer>(1);
        army2.AddUnits<Vampire>(6);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.StraightFight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void StraightFight_4_Correct()
    {
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Lancer>(4);
        army1.AddUnits<Warrior>(3);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Warrior>(4);
        army1.AddUnits<Healer>(1);
        army1.AddUnits<Knight>(2);
        army2.AddUnits<Warrior>(4);
        army2.AddUnits<Defender>(4);
        army2.AddUnits<Healer>(1);
        army2.AddUnits<Vampire>(2);
        army2.AddUnits<Lancer>(4);
        
        var result = Battle.StraightFight(army1, army2);
        
        Assert.True(result);
    }

    [Fact]
    public void Fight_WithWeapons5_Correct()
    {
        var weapon1 = Weapon.CreateMagicWand();
        var weapon2 = Weapon.CreateGreatAxe();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Knight>(1);
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Vampire>(1);
        army2.AddUnits<Healer>(1);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon2);

        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons6_Correct()
    {
        var weapon1 = Weapon.CreateMagicWand();
        var weapon2 = Weapon.CreateGreatAxe();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Knight>(1);
        army1.AddUnits<Lancer>(1);
        army2.AddUnits<Vampire>(1);
        army2.AddUnits<Healer>(1);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon2);

        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons7_Correct()
    {
        var weapon1 = Weapon.CreateKatana();
        var weapon2 = Weapon.CreateShield();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Defender>(2);
        army2.AddUnits<Knight>(1);
        army2.AddUnits<Vampire>(1);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon1);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon1);

        var result = Battle.Fight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_WithWeapons8_Correct()
    {
        var weapon1 = Weapon.CreateCustomWeapon(-20, 6, 1, 40, -2);
        var weapon2 = Weapon.CreateCustomWeapon(20, -2, 2, -55, 3);
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Knight>(3);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Defender>(2);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon1);
        units1[2].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon2);
        units2[2].Equipment.AddWeapon(weapon2);

        var result = Battle.Fight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons9_Correct()
    {
        var weapon1 = Weapon.CreateCustomWeapon(-20, 1, 1, 40, -2);
        var weapon2 = Weapon.CreateCustomWeapon(20, 2, 2, -55, 3);
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Vampire>(3);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Defender>(2);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon1);
        units1[2].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon2);
        units2[2].Equipment.AddWeapon(weapon2);

        var result = Battle.StraightFight(army1, army2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_WithWeapons10_Correct()
    {
        var weapon1 = Weapon.CreateKatana();
        var weapon2 = Weapon.CreateShield();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Vampire>(2);
        army1.AddUnits<Rookie>(2);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Defender>(2);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon1);
        units1[2].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon2);
        units2[2].Equipment.AddWeapon(weapon2);

        var result = Battle.StraightFight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons11_Correct()
    {
        var weapon1 = Weapon.CreateSword();
        var weapon2 = Weapon.CreateGreatAxe();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Vampire>(3);
        army2.AddUnits<Warrior>(1);
        army2.AddUnits<Defender>(1);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon2);
        units1[1].Equipment.AddWeapon(weapon2);
        units1[2].Equipment.AddWeapon(weapon2);
        units2[0].Equipment.AddWeapon(weapon1);
        units2[1].Equipment.AddWeapon(weapon1);

        var result = Battle.StraightFight(army1, army2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons12_Correct()
    {
        var weapon1 = Weapon.CreateKatana();
        var weapon2 = Weapon.CreateMagicWand();
        var army1 = new Army();
        var army2 = new Army();
        army1.AddUnits<Rookie>(3);
        army2.AddUnits<Defender>(1);
        army2.AddUnits<Healer>(1);
        var units1 = army1.AllAlive().ToList();
        var units2 = army2.AllAlive().ToList();
        units1[0].Equipment.AddWeapon(weapon1);
        units1[1].Equipment.AddWeapon(weapon1);
        units1[2].Equipment.AddWeapon(weapon1);
        units2[0].Equipment.AddWeapon(weapon2);
        units2[1].Equipment.AddWeapon(weapon2);

        var result = Battle.StraightFight(army1, army2);
        
        Assert.False(result);
    }
}
