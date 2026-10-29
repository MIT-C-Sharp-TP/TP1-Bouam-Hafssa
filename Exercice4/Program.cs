using System;

int heal(int health, int amount, int maxHealth)
{
    health += amount;
    return Math.Min(health,100);
}
int result = heal(80, 30, 100);
Console.WriteLine($"80 + 30 -> {result}");