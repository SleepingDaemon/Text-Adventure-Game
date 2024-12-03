namespace TextAdventureGame.Objects.RoomSystem
{
    public class Room
    {
        private int _roomNumber = 0;
        private bool _hasEnemy = false;
        private bool _isBossRoom = false;
        private bool _hasItem = false;
        private bool _isATrap = false;

        public Room(int roomNumber)
        {
            _roomNumber = roomNumber;
        }
    }
}
