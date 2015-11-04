using System.ComponentModel.DataAnnotations;

namespace Sermo.UI.Contracts
{
    public class MessageViewModel
    {
        public long ID { get; set; }

        public int RoomID { get; set; }

        [Required]
        [ContentFiltered]
        public string Text { get; set; }

        public string AuthorName { get; set; }
    }
}