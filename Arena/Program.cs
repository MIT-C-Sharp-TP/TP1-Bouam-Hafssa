
using System;
using System.Collections.Generic; 

Player player= new Player("Aya",100,2);
List<Enemy> enemies = new List<Enemy>();
enemies.Add(new Goblin());
enemies.Add(new Orc());
enemies.Add(new Goblin());
enemies.Add(new Boss());
Random rng = new Random(7);//possible change cette partie new random()
int turn = 0;

Console.WriteLine("=== Arena ===");
Console.WriteLine($"{player.Name}faces{enemies.Count} enemies:");
while (player.IsAlive && enemies.Count()> 0)
{
    turn++;
    Enemy enemy = enemies[0];
    Console.WriteLine();
    Console.WriteLine($"Turn{turn}:{player.Name}{player.Health}HP|{enemy.Name} {enemy.Health}HP");
     
     //enemy.TakeDamage(rng.Next(14,25));
     Console.WriteLine("1 attack, 2 drink a potion, 3 run away > ");
     String text = Console.ReadLine();
     if (!int.TryParse(text,out int choice))
     {
        Console.WriteLine("Type 1,2 or 3");
        continue;
     }
     switch (choice)
     {
        case 1:
           enemy.TakeDamage(rng.Next(14,25));
            break;
        case 2:
            player.DrinkPotion();
            break;
        case 3:
            Console.WriteLine("you run away.");
            return;
        default:
            Console.WriteLine("Type 1,2 or 3");
            continue;
     }
     if(!enemy.IsAlive)
     {
        Console.WriteLine($"{enemy.Name} is defeated!");
        enemies.RemoveAt(0);
        continue;
     }
     enemy.Attack(player,rng);
     if(!player.IsAlive)
     {
        Console.WriteLine($"{player.Name} is defeated !");
     }
     if (player.IsAlive && enemies.Count == 0)
    {
    Console.WriteLine($"Victory in {turn} turns!");
    }
}


public class Player
{
    private int health;
    public string Name { get; }
    public int MaxHealth { get; }
    public int Potions { get; private set; }
    public event Action<string> Died;
    public int Health
    {
        get { return health;}
        private set => health = Math.Clamp(value, 0, MaxHealth);
    }
    public bool IsAlive => Health > 0;
    public Player(string name, int maxHealth, int potions)
    {
        Name = name;
        MaxHealth = maxHealth;
        Potions = potions;
        Health = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        Health = Math.Max(Health - amount, 0);
        Console.WriteLine($"{Name} takes {amount} damage. ({Health} HP).");
    }
    public void DrinkPotion()
    {
        if (Potions > 0)
        {
            Potions--;
            Health = Math.Min(Health + 30, MaxHealth); // Assume each potion heals 30 health
            Console.WriteLine($"{Name} drinks a potion. ({Health} HP). Potions left: {Potions}.");
        }
        else
        {
            Console.WriteLine($"{Name} has no potions left!");
            return;
        }
    }
}

public abstract class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    public bool IsAlive => Health > 0;
    protected Enemy(string name, int health)
    {
        Name = name;
        Health = health;
    }
    public virtual  void TakeDamage(int amount)
    {
        Health = Math.Max(Health - amount, 0);
        Console.WriteLine($"{Name} takes {amount} damage. ({Health} HP).");
    }
    public abstract void Attack(Player player, Random rng);
}

public class Goblin : Enemy
{
    public Goblin (): base("Goblin", 30) {}
    public override void Attack(Player target, Random rng)
    {
       Console.WriteLine("Goblin stabs!");
       target.TakeDamage(rng.Next(4, 9)); 
    }
}
public class Orc : Enemy
{
    public Orc (): base("Orc", 60) {}
    public override void Attack(Player target, Random rng)
    {
       Console.WriteLine("Orc swings its club!");
       target.TakeDamage(rng.Next(8, 15)); 
    }
  }  
public class Boss : Enemy 
{
public int Armour{ get;}= 6;
public Boss(): base("Boss",60)
{
}
public override void TakeDamage(int amount) {

   int reduced= Math.Max(amount-Armour,1);
   Console.WriteLine($"The armour absorbs {amount-reduced} damage.");
   base.TakeDamage(reduced); 
}
        public override void Attack(Player target, Random rng)
        {
            Console.WriteLine ("Boss slams the ground!");
            target.TakeDamage(rng.Next(9,17));

        }
}
