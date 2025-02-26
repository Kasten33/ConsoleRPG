using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleRPG;

public class Game
{
    static void Main(string[] args)
    {
        Weapon weapon = new();
        Player player = new();
        Battle battle = new(player);

        player.HP();
        player.BaseAtck();
        player.GetName();
        Console.WriteLine(player.Name + ", What would you like to do?");
        bool On = true;
        while (On)
        {
            Console.WriteLine("Fight   " + "Menu    " + "Equip Item     " + "Exit Game");
            string? Choice = Console.ReadLine();
            if (Choice == "menu" || Choice == "Menu")
            {
                Console.WriteLine("HP " + player.HealthPoints);
                Console.WriteLine("Attack " + player.BaseAttack);
            }
            else if (Choice == "equip item" || Choice == "Equip Item" || Choice == "equip" || Choice == "Equip")
            {
                weapon.GetName();
                weapon.HP();
                weapon.BaseAtck();

                player.HealthPoints = weapon.HealthPoints + player.HealthPoints;
                player.BaseAttack = weapon.BaseAttack + player.BaseAttack;
                Console.WriteLine("You've equiped a " + weapon.Name);
            }
            else if (Choice == "Fight" || Choice == "fight")
            {
                Console.WriteLine("You've gone into the forest");
                battle.Encounter();
                if (player.HealthPoints <= 0)
                {
                    Console.WriteLine("Game Over");
                    break;
                }
            }
            else if (Choice == "Exit" || Choice == "exit" || Choice == "Exit Game" || Choice == "exit game")
            {
                Console.WriteLine("Thanks for Playing");
                break;
            }
            else
            {
                Console.WriteLine("Please Choose a Valid option");
            }
        }

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
    }
    public override void BaseAtck()
    {
        Random rand = new();
        int bonusAtck = rand.Next(5, 11);// 5 - 10
        BaseAttack = baseAtck + bonusAtck;
    }    
    public void Heal()
    {
        HealthPoints = HealthPoints + 20;
    }
    public void Crit()
    {
        Random random = new();
        int bonusCrit = random.Next(1, 101);//1-100
        if (bonusCrit > 75) 
        {
            BaseAttack = BaseAttack * 2;
        }
    }
}
public class Monster : Character
{
    public string[] Names = ["Wolf", "Goblin", "Kobold", "Slime" ];
    public int baseHp = 45;
    public int baseAtck = 8;

    public override void GetName()
    {
        Random rand = new(); // random gen (Pseudo - random )
        int index = rand.Next(Names.Length); // chooses a "random" name from string[] Names
        Name = Names[index]; // sets name to the random name generated
    }
    public override void BaseAtck()
    {
        Random rand = new();
        int bonusAtck = rand.Next(5, 11);// 5 - 10
        BaseAttack = baseAtck + bonusAtck;
    }
    public override void HP()
    {
        Random rand = new();
        int bonusHp = rand.Next(10, 16);//10 - 15
        HealthPoints = baseHp + bonusHp;
    }
}
public class Battle
{
    Monster monster = new();
    Weapon weapon = new();
    Player player;
    public Battle(Player player)
    {
        this.player = player;
    }
    public int AcidSplash = 10;
    public int Bite = 5;
    public int Stab = 30;
    public int SpearThrust = 25;
    public int Attack = 0;
    bool Turn = true;
    bool Block = false;
    public void Encounter()
    {

        weapon.HP();
        weapon.BaseAtck();
        monster.GetName();
        monster.HP();
        monster.BaseAtck();

        monster.HealthPoints = weapon.HealthPoints + monster.HealthPoints;
        monster.BaseAttack = weapon.BaseAttack + monster.BaseAttack;
        Console.WriteLine("You've Encountered a " + monster.Name);
        while (monster.HealthPoints > 0)
        {
            if (Turn == true)
            {

                Console.WriteLine("Its your turn, what will you do?");
                Console.WriteLine("Heal " + " Block " + " Attack " + " Run");
                string? Choice = Console.ReadLine();
                if (Choice == "Heal" || Choice == "heal")
                {
                    player.Heal();
                    Console.WriteLine("You've Healed yourself for 20 HP and now have " + player.HealthPoints);
                    Turn = false;
                }
                else if (Choice == "Block" || Choice == "block")
                {
                    Block = true;
                    Turn = false;
                }
                else if (Choice == "Attack" || Choice == "attack")
                {
                    player.Crit();
                    monster.HealthPoints = monster.HealthPoints - player.BaseAttack;
                    Console.WriteLine("You've hit the " + monster.Name +" for " + player.BaseAttack + ", " + monster.Name + " has " + monster.HealthPoints + " left");
                    
                    
                    if (player.BaseAttack > 60)
                    {
                        player.BaseAttack = player.BaseAttack / 2;
                    }
                    Turn = false;
                }
                else if (Choice == "Run" || Choice == "run")
                {
                    Console.WriteLine("You've run from combat");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Please choose a Valid move");
            }
            if (Block == true)
            {
                Console.WriteLine("Enemy did no Damage");
                Turn = true;
                Block = false;
            }
            if (Turn == false)
            {
                if (monster.Name == "Wolf")
                {
                    Attack = Bite;
                    Random rand = new();
                    int attackRoll = rand.Next(1, 11);
                    if (attackRoll >= 7)
                    {
                        monster.BaseAttack = Attack + monster.BaseAttack;
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;

                        Console.WriteLine("You've been Bitten! You take " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left!");
                        monster.BaseAttack = monster.BaseAttack - Attack;
                        Turn = true;
                    }
                    else
                    {
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;
                        Console.WriteLine("You've been hit for " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left");
                        Turn = true;
                    }

                }
                else if (monster.Name == "Goblin")
                {
                    Attack = Stab;
                    Random rand = new();
                    int attackRoll = rand.Next(1, 11);
                    if (attackRoll >= 7)
                    {
                        monster.BaseAttack = Attack + monster.BaseAttack;
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;

                        Console.WriteLine("You've been Stabbed! You take " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left!");
                        monster.BaseAttack = monster.BaseAttack - Attack;
                        Turn = true;
                    }
                    else
                    {
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;
                        Console.WriteLine("You've been hit for " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left");
                        Turn = true;
                    }
                }
                else if (monster.Name == "Kobold")
                {
                    Attack = SpearThrust;
                    Random rand = new();
                    int attackRoll = rand.Next(1, 11);
                    if (attackRoll >= 7)
                    {
                        monster.BaseAttack = Attack + monster.BaseAttack;
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;

                        Console.WriteLine("You've taken a Spear to the thigh! You take " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left!");
                        monster.BaseAttack = monster.BaseAttack - Attack;
                        Turn = true;
                    }
                    else
                    {
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;
                        Console.WriteLine("You've been hit for " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left");
                        Turn = true;
                    }
                }
                else
                {
                    Attack = AcidSplash;
                    Random rand = new();
                    int attackRoll = rand.Next(1, 11);
                    if (attackRoll >= 7)
                    {
                        monster.BaseAttack = Attack + monster.BaseAttack;
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;

                        Console.WriteLine("You've been hit with Acid Splash! You take " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left!");
                        monster.BaseAttack = monster.BaseAttack - Attack;
                        Turn = true;
                    }
                    else
                    {
                        player.HealthPoints = player.HealthPoints - monster.BaseAttack;
                        Console.WriteLine("You've been hit for " + monster.BaseAttack + " You have " + player.HealthPoints + " HP left");
                        Turn = true;
                    }
                }
            }
            if (monster.HealthPoints <= 0)
            {
                Console.WriteLine("Congradulations, You Won!");
                break;
            }
        }
        
    }
}