using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleRPG;

public class Game
{
    static void Main(string[] args)
    {
        Weapon weapon = new();
        Player player = new();
        Monster monster = new();

        player.GetName();
        player.HP();
        //bool On = true;
        //while (On) { }

    }
}
public class Menu
{
    public void Display()
    {
        Weapon weapon = new();
        Player player = new();


    }
}

public abstract class Character
{
    public string? Name { get; set; }
    public int BaseAttack { get; set; }
    public int HealthPoints { get; set; }
    public abstract void HP();
    public abstract void BaseAtck();
    public abstract void GetName();
 
}
public class Weapon : Character
{
    public int baseHp = 15;
    public int baseAtck = 15;
    public string[] Weapons = ["Staff", "Sword", "Dagger", "Mace", "Gun"];


    public override void HP()
    {
        Random rand = new();
        int bonusHp = rand.Next(10, 36);// 10 - 35
        HealthPoints = Convert.ToInt32((baseHp + bonusHp) / 2);
    }
    public override void BaseAtck()
    {
        Random rand = new();
        int bonusAtck = rand.Next(100, 121); // 100 - 120
        BaseAttack = Convert.ToInt32((baseAtck + bonusAtck) / 25);
    }
    public override void GetName()
    {
        Random rand = new(); // new instance of Random Number Generator (Pseudo - random )
        int index = rand.Next(Weapons.Length); 
        Name = Weapons[index]; 
        Console.WriteLine("You've equiped a " + Name); 
    }
}

public class Player : Character
{
    public int baseHp = 50;
    public int baseAtck = 15;


    public override void GetName()
    {
        Console.WriteLine("What's your name Adventurer? ");
        Name = Console.ReadLine();
    }

    public override void HP()
    {
        Random rand = new();
        int bonusHp = rand.Next(10, 16);//10 - 15
        HealthPoints = baseHp + bonusHp;
        Console.WriteLine(HealthPoints);

    }
    public override void BaseAtck()
    {
        Random rand = new();
        int bonusAtck = rand.Next(5, 11);// 5 - 10
        BaseAttack = baseAtck + bonusAtck;
    }    
    public void Heal()
    {
        int AddHp = 20;
        //AddHp + HealthPoints = HealthPoints;
        
    }
    public void Crit()
    {
        Random random = new();
        int bonusCrit = random.Next(1, 101);//1-100
        if (bonusCrit > 85) { }
    }


}
public class Monster : Character
{
    public string[] Names = ["Orc", "Goblin", "Kobold" ];
    public int baseHp = 45;
    public int baseAtck = 8;

    public override void GetName()
    {
        Random rand = new(); // random gen (Pseudo - random )
        int index = rand.Next(Names.Length); // chooses a "random" name from string[] Names
        Name = Names[index]; // sets name to the random name generated
        Console.WriteLine("You've encountered a " + Name); // calls and prints name
    }
    public override void BaseAtck()
    {
        //Math - RNG logic
    }
    public override void HP()
    {
        //Math - RNG logic
    }
}
