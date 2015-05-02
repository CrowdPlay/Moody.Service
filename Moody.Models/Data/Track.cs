namespace Moody.Models.Data
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