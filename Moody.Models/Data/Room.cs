using System;
using System.Collections.Generic;
using MongoRepository;

namespace Moody.Models.Data
{
    public class Room : Entity
    {
        public IList<RoomUser> RoomUsers { get; set; }
        public string Mood { get; set; }
        public int CurrentTrackId { get; set; }
        public DateTime TrackEndTime { get; set; }
        public int RoomId { get; set; }
    }

    public class RoomUser
    {
        public string Handle { get; set; }
        public string Mood { get; set; }
    }
}