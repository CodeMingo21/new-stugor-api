namespace StugorSe_API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CottageId { get; set; }
        public int UserId { get; set; }
    }
}
