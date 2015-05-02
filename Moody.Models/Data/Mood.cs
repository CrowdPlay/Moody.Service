using MongoRepository;

namespace Moody.Models.Data
{
    public class Mood : Entity
    {
        public string Name { get; set; }
        public Track[] TrackInfo { get; set; }
    }
}