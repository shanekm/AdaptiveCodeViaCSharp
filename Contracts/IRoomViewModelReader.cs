using System.Collections.Generic;

namespace Contracts
{
    public interface IRoomViewModelReader
    {
        IEnumerable<RoomViewModel> GetAllRooms();
    }
}