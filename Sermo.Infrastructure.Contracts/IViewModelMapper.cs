using Sermo.Data.Contracts;
using Sermo.UI.Contracts;

namespace Sermo.Infrastructure.Contracts
{
    public interface IViewModelMapper
    {
        RoomViewModel MapRoomRecordToRoomViewModel(RoomRecord roomRecord);

        RoomRecord MapRoomViewModelToRoomRecord(RoomViewModel roomViewModel);

        MessageViewModel MapMessageRecordToMessageViewModel(MessageRecord messageRecord);

        MessageRecord MapMessageViewModelToMessageRecord(MessageViewModel messageViewModel);
    }
}