using System.Collections.Generic;
using System.Threading;
using WarOOP.Models;
using WarOOP.Tests.Models;
using Xunit;

namespace WarOOP.Tests;

public class WarriorTests
{
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new Warrior(), new Knight(), false },
            new object[] { new Knight(), new Warrior(), true },
            new object[] { new Warrior(), new Warrior(), true },
            new object[] { new Knight(), new Knight(), true },
            new object[] { new Warrior(), new Defender(), false },
            new object[] { new Defender(), new Warrior(), true },
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
    public void Fight_WithWeapons1_Correct()
    {
        var unit1 = new Warrior();
        var unit2 = new Vampire();
        var weapon1 = Weapon.CreateCustomWeapon(-10, 5, 0, 40, 0);
        var weapon2 = Weapon.CreateSword();
        unit1.Equipment.AddWeapon(weapon1);
        unit2.Equipment.AddWeapon(weapon2);
        
        var result = Battle.Fight(unit1, unit2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWeapons2_Correct()
    {
        var unit1 = new Defender();
        var unit2 = new Lancer();
        var weapon1 = Weapon.CreateShield();
        var weapon2 = Weapon.CreateGreatAxe();
        unit1.Equipment.AddWeapon(weapon1);
        unit2.Equipment.AddWeapon(weapon2);
        
        var result = Battle.Fight(unit1, unit2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_WithWeapons3_Correct()
    {
        var unit1 = new Healer();
        var unit2 = new Knight();
        var weapon1 = Weapon.CreateMagicWand();
        var weapon2 = Weapon.CreateKatana();
        unit1.Equipment.AddWeapon(weapon1);
        unit2.Equipment.AddWeapon(weapon2);
        
        var result = Battle.Fight(unit1, unit2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_WithWeapons4_Correct()
    {
        var unit1 = new Defender();
        var unit2 = new Vampire();
        var weapon1 = Weapon.CreateShield();
        var weapon2 = Weapon.CreateMagicWand();
        var weapon3 = Weapon.CreateShield();
        var weapon4 = Weapon.CreateKatana();
        unit1.Equipment.AddWeapon(weapon1);
        unit1.Equipment.AddWeapon(weapon2);
        unit2.Equipment.AddWeapon(weapon3);
        unit2.Equipment.AddWeapon(weapon4);
        
        var result = Battle.Fight(unit1, unit2);
        
        Assert.False(result);
    }

    [Fact]
    public void Fight_WithWarlord1_Correct()
    {
        var unit1 = new Defender();
        var unit2 = new Warlord();

        var result = Battle.Fight(unit1, unit2);
        
        Assert.False(result);
    }
    
    [Fact]
    public void Fight_WithWarlord2_Correct()
    {
        var unit1 = new Warlord();
        var unit2 = new Vampire();

        var result = Battle.Fight(unit1, unit2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithWarlord3_Correct()
    {
        var unit1 = new Warlord();
        var unit2 = new Knight();

        var result = Battle.Fight(unit1, unit2);
        
        Assert.True(result);
    }
    
    [Fact]
    public void Fight_WithGunner_Correct()
    {
        var gunner = new Gunner();
        var warrior = new Warrior();
        
        gunner.AttackTo(warrior);
        var result = gunner.CurrentHealth == 1 && warrior.CurrentHealth == 10;

    }
}