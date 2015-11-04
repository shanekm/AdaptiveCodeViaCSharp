using System.Collections.Generic;
using Sermo.UI.Contracts;

namespace Sermo.Markdown
{
    public class RoomViewModelReaderMarkdownDecorator : IRoomViewModelReader
    {
        public RoomViewModelReaderMarkdownDecorator(IRoomViewModelReader @delegate, MarkdownDeep.Markdown markdown)
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

            foreach (var viewModel in roomMessages)
            {
                viewModel.Text = markdown.Transform(viewModel.Text);
            }

            return roomMessages;
        }

        private readonly IRoomViewModelReader @delegate;
        private readonly MarkdownDeep.Markdown markdown;
    }
}
