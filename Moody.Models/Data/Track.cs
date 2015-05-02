using System;
using MongoRepository;

namespace Moody.Models.Data
{
    public class Track : Entity
    {
        public int TrackId { get; set; }
        public TimeSpan Duration { get; set; }
    }
}