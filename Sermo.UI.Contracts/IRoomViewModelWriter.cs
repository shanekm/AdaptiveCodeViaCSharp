namespace Sermo.UI.Contracts
{
    public interface IRoomViewModelWriter
    {
        void CreateRoom(RoomViewModel roomViewModel);

        void AddMessage(MessageViewModel messageViewModel);
    }
}