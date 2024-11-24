internal class Program
{
    /*  Ideas for the Text Adventure Game (Phase 1: C# Fundamentals. No OOP)
     *  
     *  Array of int representing rooms
     *  Player class will have a name, inventory, and attributes (HP, Attack Points)
     *  direction for player to choose which room to go next
     *  a room can contain an empty room, enemy, boss, treasure
     * 
     */

    public enum RoomType { Empty, Weapon, Treasure, Key, Enemy, EnemyBoss }
    public enum GameState { Initializing, Choices, Battle, GameOver, Ending }

    private static void Main(string[] args)
    {
        // Game properties
        GameState state = GameState.Initializing;

        // Player properties
        string playerName = "Hero";
        int healthPoints = 5;
        int attackPoints = 1;
        List<int> playerChoices = new();

        // Room properties
        RoomType roomType = RoomType.Empty;
        int roomNumber = 0;
        int arraySize = 3;
        int[,] rooms = new int[arraySize, arraySize];
        bool rightDoor, leftDoor, upDoor, downDoor = false;
        bool hasEnemy = false;
        bool hasEnemyBoss = false;
        bool hasTreasure = false;
        bool hasKey = false;
        bool hasSword = false;

        
        for (int x = 0; x < rooms.GetLength(0); x++)
        {
            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                roomNumber++;
                rooms[x, y] = roomNumber;
                InitializeRooms(roomNumber, rooms, x, y);

                Console.WriteLine(x + ", " + y + " = " + roomNumber + " " + roomType.ToString());
            }
        }

        // GAME LOOP
        while(state != GameState.GameOver)
        {
            if (state == GameState.GameOver) return;


        }

        Console.ReadLine();

        //METHODS
        void SetRoom(int x, int y, bool rDoor, bool lDoor, bool uDoor, bool dDoor, RoomType type)
        {
            if (rooms[x, y] == roomNumber)
            {
                rightDoor = rDoor;
                leftDoor = lDoor;
                upDoor = uDoor;
                downDoor = dDoor;
                roomType = type;
            }
        }

        void InitializeRooms(int roomNumber, int[,] rooms, int x, int y)
        {
            if (roomNumber == 1)
                SetRoom(x, y, true, false, false, true, RoomType.Empty);
            else if (roomNumber == 2)
                SetRoom(x, y, true, true, false, true, RoomType.Enemy);
            else if (roomNumber == 3)
                SetRoom(x, y, false, true, false, true, RoomType.Treasure);
            else if (roomNumber == 4)
                SetRoom(x, y, true, false, true, true, RoomType.Weapon);
            else if (roomNumber == 5)
                SetRoom(x, y, true, true, true, true, RoomType.Key);
            else if (roomNumber == 6)
                SetRoom(x, y, false, true, true, false, RoomType.Enemy);
            else if (roomNumber == 7)
                SetRoom(x, y, true, false, true, false, RoomType.Enemy);
            else if (roomNumber == 8)
                SetRoom(x, y, false, true, true, false, RoomType.Treasure);
            else if (roomNumber == 9)
                SetRoom(x, y, false, false, false, false, RoomType.EnemyBoss);
        }
    }
}