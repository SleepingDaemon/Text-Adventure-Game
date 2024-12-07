using TextAdventureGame.Objects.Character;

namespace TextAdventureGame.Objects.RoomSystem
{
    public class NodeManager
    {
        private Node[,]? _heartMap;

        public NodeManager()
        {
        }

        public Node[,]? HeartMap { get => _heartMap; set => _heartMap = value; }

        public void InitializeHeartMap()
        {
            _heartMap = new Node[5, 5];

            for(int x  = 0; x < 5; x++)
            {
                for(int y = 0; y < 5; y++)
                {
                    _heartMap[x, y] = new Node($" Player is at {x}, {y}", false, false, "Safe");
                }
            }

            _heartMap[0, 2] = new Node(" Enemy encounter!", false, true, "Enemy");
            _heartMap[1, 4] = new Node(" Enemy encounter!", false, true, "Enemy");
            _heartMap[3, 0] = new Node(" Enemy encounter!", false, true, "Enemy");
            _heartMap[3, 1] = new Node(" Enemy encounter!", false, true, "Enemy");
            _heartMap[4, 4] = new Node(" Enemy encounter!", false, true, "Enemy");
        }

        public void ConnectHeartNodes()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x > 0) _heartMap[x, y]?.Connections.Add("up", _heartMap[x - 1, y]);
                    if (x < 4) _heartMap[x, y]?.Connections.Add("down", _heartMap[x + 1, y]);
                    if (y > 0) _heartMap[x, y]?.Connections.Add("left", _heartMap[x, y - 1]);
                    if (y < 4) _heartMap[x, y]?.Connections.Add("right", _heartMap[x, y + 1]);
                }
            }
        }
    }
}
