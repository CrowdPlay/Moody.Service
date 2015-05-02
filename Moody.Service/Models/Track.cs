namespace Moody.Service.Models
{
    public class Track
    {
        public int id { get; private set; }

        public Track(int trackId)
        {
            id = trackId;
        }
    }
}