using System.Collections.Generic;
using Sermo.UI.Contracts;

namespace Sermo.UI.Web.ViewModels
{
    public class MessageListViewModel
    {
        public MessageListViewModel(IEnumerable<MessageViewModel> messages)
        {
            Messages = new List<MessageViewModel>(messages);
        }

        public List<MessageViewModel> Messages { get; private set; }
    }
}