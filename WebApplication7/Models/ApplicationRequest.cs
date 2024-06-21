using System.ComponentModel.DataAnnotations.Schema;
using WebApplication7.Areas.Identity.Data;

namespace WebApplication7.Models
{
    public class ApplicationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CEO { get; set; }
        public string DateOfBirth { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public MessageType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public Stats Status { get; set; }

        // Внешний ключ для связи с таблицей пользователей
        public string UserId { get; set; }

        // Навигационное свойство для связи с пользователем
        [ForeignKey("UserId")]
        public virtual WebApplicationUser User { get; set; }
    }
    public enum MessageType
    {
        Personal,
        Group
    }
    public enum Stats
    {
        Pending,
        Denied,
        Approved
    }
}

