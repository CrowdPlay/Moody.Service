namespace Moody.Models.Requests
{
    public class RequestUser : RequestUserWithoutRoom
    {
        public int Room { get; set; }
    }
}
