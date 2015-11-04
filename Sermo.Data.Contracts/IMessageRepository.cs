using System.Collections.Generic;

namespace Sermo.Data.Contracts
{
    public interface IMessageRepository
    {
        IEnumerable<MessageRecord> GetMessagesForRoomID(int roomID);

        void AddMessageToRoom(int roomID, string authorName, string text);
    }
}