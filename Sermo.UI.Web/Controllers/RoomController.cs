using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Web.Mvc;
using Sermo.UI.Contracts;
using Sermo.UI.Web.ViewModels;

namespace Sermo.UI.Web.Controllers
{
    public class RoomController : Controller
    {
        // Take 1 - contstructor
        //private readonly IRoomRepository roomRepository;

        //private readonly IRoomViewModelMapper viewModelMapper;

        // Take 1 - contstructor
        //public RoomController(IRoomRepository roomRepository, IRoomViewModelMapper viewModelMapper)
        //{
        //    this.roomRepository = roomRepository;
        //    this.viewModelMapper = viewModelMapper;
        //}

        //[HttpGet]
        //public ActionResult ListTake1()
        //{
        //    var roomListViewModel = new RoomListViewModel();

        //    // 1. Repository belongs at the data persistance layer
        //    // 2. Repository returns Room(s) class which does not belong to Controller layer. Only view models do. Controller should not know about data specific classes (Room)
        //    // 3. Controller now depends on Mapper, it should not
        //    // 4. There should be a service that uses Repository that retrieves RoomRecord instances and returns RoomViewModel. Use Service that connects mapper and repo
        //    //      and returns view model which is the only data that controller needs to know about
        //    // 5. Add segregation in new Service layer so that CQRM (Command Query Repsonsibility Segregation)
        //    //      can be used later if needed and give optio of varying the implementation of the read and write of the request
        //    // 6. Controller is not suppose to be responsible for mapping from contracts/records to viewmodels
        //    var allRoomRecords = roomRepository.GetAllRooms();

        //    foreach (var roomRecord in allRoomRecords)
        //    {
        //        roomListViewModel.Rooms.Add(viewModelMapper.MapRoomRecordToViewModel(roomRecord));
        //    }

        //    return View(roomListViewModel);
        //}


        // Take 2 - contstructor
        // Created RepositoryRoomViewModelService service that implements reader and writer
        // IoC will pass in Concrete implementaion RepositoryRoomViewModelService
        private readonly IRoomViewModelReader reader;

        private readonly IRoomViewModelWriter writer;

        public RoomController(IRoomViewModelReader reader, IRoomViewModelWriter writer)
        {
            Contract.Requires<ArgumentNullException>(reader != null); // Preconditions
            Contract.Requires<ArgumentNullException>(writer != null);

            this.reader = reader;
            this.writer = writer;
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
            return View(new RoomViewModel());
        }

        [HttpPost]
        public ActionResult Create(RoomViewModel model)
        {
            ActionResult result;
            if (ModelState.IsValid)
            {
                writer.CreateRoom(model);
                result = RedirectToAction("List");
            }
            else
            {
                result = View("Create", model);
            }

            return result;
        }

        [HttpGet]
        public ActionResult Messages(int roomID)
        {
            var messageListViewModel = new MessageListViewModel(reader.GetRoomMessages(roomID));
            return View(messageListViewModel);
        }

        [HttpPost]
        public ActionResult AddMessage(MessageViewModel messageViewModel)
        {
            ActionResult result;

            if (ModelState.IsValid)
            {
                writer.AddMessage(messageViewModel);
                result = Json(messageViewModel);
            }
            else
            {
                result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return result;
        }
    }
}