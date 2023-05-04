using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Data.Entities
{
    public class Application : BaseEntity
    {
        public string? SampleNumber { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public int CassettesCount { get; set; }
        public int FragmentCount { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Created;

        public long? RoomId { get; set; }
        public Room? Room { get; set; }

      //  public long? SampleDataId { get; set; }
       // public SampleData? SampleData { get; set; }

        public long? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
