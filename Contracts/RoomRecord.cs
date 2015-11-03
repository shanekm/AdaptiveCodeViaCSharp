namespace Contracts
{
    public class RoomRecord
    {
        public RoomRecord(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}