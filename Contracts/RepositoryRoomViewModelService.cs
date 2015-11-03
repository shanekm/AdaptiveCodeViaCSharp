using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Contracts
{
    public class RepositoryRoomViewModelService : IRoomViewModelReader, IRoomViewModelWriter
    {
        public RepositoryRoomViewModelService(IRoomRepository repository, IRoomViewModelMapper mapper)
        {
            Contract.Requires<ArgumentNullException>(repository != null);
            Contract.Requires<ArgumentNullException>(mapper != null);

            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            var allRooms = new List<RoomViewModel>();
            var allRoomRecords = repository.GetAllRooms();
            foreach (var roomRecord in allRoomRecords)
            {
                allRooms.Add(mapper.MapRoomRecordToRoomViewModel(roomRecord));
            }
            return allRooms;
        }

        public void CreateRoom(string roomName)
        {
            repository.CreateRoom(roomName);
        }

        private readonly IRoomRepository repository;
        private readonly IRoomViewModelMapper mapper;
    }
}
