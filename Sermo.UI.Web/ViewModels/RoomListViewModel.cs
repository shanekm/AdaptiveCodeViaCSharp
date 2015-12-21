using System.Collections.Generic;
using Sermo.UI.Contracts;

namespace Sermo.UI.Web.ViewModels
{
    public class RoomListViewModel
    {
        public RoomListViewModel(IEnumerable<RoomViewModel> rooms)
        {
            Rooms = new List<RoomViewModel>(rooms);
        }

        public List<RoomViewModel> Rooms { get; private set; }
    }
}