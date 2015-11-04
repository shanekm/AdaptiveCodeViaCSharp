using System.Collections.Generic;

namespace Sermo.Data.Contracts
{
    public interface IRoomRepository
    {
        void CreateRoom(string name);

        IEnumerable<RoomRecord> GetAllRooms();
    }
}