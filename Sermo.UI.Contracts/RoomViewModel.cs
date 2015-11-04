using System.ComponentModel.DataAnnotations;

namespace Sermo.UI.Contracts
{
    public class RoomViewModel
    {
        [Required]
        [ContentFiltered]
        public string Name { get; set; }
    }
}