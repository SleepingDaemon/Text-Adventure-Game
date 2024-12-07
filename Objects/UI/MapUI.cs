using TextAdventureGame.Objects.RoomSystem;

namespace TextAdventureGame.Objects.UI
{
    public class MapUI
    {
        public static void DisplayMap(Node[,] map, int playerX, int playerY)
        {
            Console.Clear();
            Console.WriteLine(" Heart Map: \n");

            int height = map.GetLength(0);
            int width = map.GetLength(1);

            Console.Write("    ");
            for (int y = 0; y < width; y++)
            {
                Console.Write($" {y,3}  ");
            }
            Console.WriteLine();

            Console.Write("    ┌");
            for (int y = 0; y < width - 1; y++)
            {
                Console.Write("─────┬");
            }
            Console.WriteLine("─────┐");

            for(int x = 0; x < height; x++)
            {
                Console.Write($"{x,3} │");

                for (int y = 0; y < width; y++)
                {
                    if (x == playerX && y == playerY)
                        Console.Write("  *  │");                    
                    else
                        Console.Write(map[x,y].GetNodeSymbol());
                }

                Console.WriteLine();

                if(x < height - 1)
                {
                    Console.Write("    ├");
                    for (int y = 0; y < width - 1; y++)
                    {
                        Console.Write("─────┼");
                    }

                    Console.WriteLine("─────┤");
                }
            }

            Console.Write("    └");
            for (int y = 0; y < width - 1; y++)
            {
                Console.Write("─────┴");
            }

            Console.WriteLine("─────┘");


            Console.WriteLine($" Legend:");
            Console.WriteLine($" [*] Player Position - [S] Safe Node -   [E] Enemy - [P] Puzzle\n [M] Memory Fragment - [L] Locked Node - [?] Undiscovered Node ");
        }
    }
}
