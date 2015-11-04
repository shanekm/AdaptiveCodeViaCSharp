using System.Collections.Generic;

namespace Sermo.UI.Contracts
{
    public interface IRoomViewModelReader
    {
        IEnumerable<RoomViewModel> GetAllRooms();

        IEnumerable<MessageViewModel> GetRoomMessages(int roomID);
    }
}