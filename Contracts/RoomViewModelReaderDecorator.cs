using System;
using System.Collections.Generic;

namespace Contracts
{
    public class RoomViewModelReaderDecorator : IRoomViewModelReader
    {
        private readonly IRoomViewModelReader @delegate;
        private readonly Markdown markdown;

        // Sending in 3rd party library for HTML markdown processing
        public RoomViewModelReaderDecorator(IRoomViewModelReader @delegate, Markdown markdown)
        {
            this.@delegate = @delegate;
            this.markdown = markdown;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            this.@delegate.GetAllRooms(); // pass down
        }


    }
}
