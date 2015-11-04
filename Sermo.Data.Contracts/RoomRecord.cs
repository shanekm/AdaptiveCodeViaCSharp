namespace Sermo.Data.Contracts
{
    public class RoomRecord
    {
        public RoomRecord(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}