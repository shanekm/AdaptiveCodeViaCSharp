using System;
using System.Collections.Generic;

namespace Contracts
{
    // This class should be in its own project
    public class RoomViewModelReaderMarkdownDecorator : IRoomViewModelReader
    {
        // Sending in 3rd party library
        public RoomViewModelReaderMarkdownDecorator(IRoomViewModelReader @delegate, Markdown markdown)
        {
            this.@delegate = @delegate;
            this.markdown = markdown;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            return @delegate.GetAllRooms();
        }

        public IEnumerable<MessageViewModel> GetRoomMessages(int roomID)
        {
            var roomMessages = @delegate.GetRoomMessages(roomID);

            // Cleaning up with markdown 3rd library class
            foreach (var viewModel in roomMessages)
            {
                viewModel.Text = markdown.Transform(viewModel.Text);
            }

            return roomMessages;
        }

        private readonly IRoomViewModelReader @delegate;
        private readonly Markdown markdown;
    }
}
