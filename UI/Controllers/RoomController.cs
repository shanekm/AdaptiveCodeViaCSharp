using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using Contracts;

namespace UI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;

        private readonly IRoomViewModelMapper viewModelMapper;

        // Take 1 - contstructor
        public RoomController(IRoomRepository roomRepository, IRoomViewModelMapper viewModelMapper)
        {
            this.roomRepository = roomRepository;
            this.viewModelMapper = viewModelMapper;
        }

        [HttpGet]
        public ActionResult List()
        {
            var roomListViewModel = new RoomListViewModel();

            // 1. Repository belongs at the data persistance layer
            // 2. Repository returns Room(s) class which does not belong to Controller layer. Only view models do. Controller should not know about data specific classes (Room)
            // 3. Controller now depends on Mapper, it should not
            // 4. There should be a service that uses Repository that retrieves RoomRecord instances and returns RoomViewModel. Use Service that connects mapper and repo
            //      and returns view model which is the only data that controller needs to know about
            // 5. Add segregation in new Service layer so that CQRM (Command Query Repsonsibility Segregation)
            //      can be used later if needed and give optio of varying the implementation of the read and write of the request
            // 6. Controller is not suppose to be responsible for mapping from contracts/records to viewmodels
            var allRoomRecords = roomRepository.GetAllRooms();

            foreach (var roomRecord in allRoomRecords)
            {
                roomListViewModel.Rooms.Add(viewModelMapper.MapRoomRecordToViewModel(roomRecord));
            }

            return View(roomListViewModel);
        }


        // Take 2 - contstructor
        // Created RepositoryRoomViewModelService service that implements reader and writer

        private readonly IRoomViewModelReader reader;

        private readonly IRoomViewModelWriter writer;

        public RoomController(IRoomViewModelReader reader, IRoomViewModelWriter writer) 
        {
            reader = reader;
            writer = writer;
        }

        [HttpGet]
        public ActionResult List()
        {
            // Calls concrete implementation of RepositoryRoomViewModelService reader functionality
            var roomListViewModel = new RoomListViewModel(reader.GetAllRooms());
            return View(roomListViewModel);
        }







        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new CreateRoomViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateRoomViewModel model)
        {
            ActionResult result;

            if (ModelState.IsValid)
            {
                roomRepository.CreateRoom(model.NewRoomName);

                result = RedirectToAction("List");
            }
            else
            {
                result = View("Create", model);
            }

            return result;
        }
    }

    public class CreateRoomViewModel
    {
        [Required]
        public string NewRoomName { get; set; }
    }

    public class RoomListViewModel
    {
    }
}