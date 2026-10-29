using System ;

int health = 40 ;
for (int turn = 1; turn <= 6; turn++) 
{
   health -= 8;
   Console.WriteLine($"Turn {turn}: poison, {health} HP left");
   if (health <= 0)
   {
       Console.WriteLine("Aya is down");
       break;
   }
}

