using System;
using System.Collections.Generic;
using System.Linq;
using MongoRepository;
using Moody.Models.Data;

namespace Moody.Data
{
    public class RoomService
    {
        private static readonly MongoRepository<Room> RoomRepository = new MongoRepository<Room>();
        private static readonly MongoRepository<Mood> MoodRepository = new MongoRepository<Mood>();

        public Track GetCurrentTrack(int id)
        {
            var rooms = RoomRepository.ToList();
            var room = rooms.Find(r => r.RoomId == id);
            return new Track { TrackId = EnsureCurrentTrackId(room) };
        }

        private int EnsureCurrentTrackId(Room room)
        {
            return room.TrackEndTime > DateTime.UtcNow ? room.CurrentTrackId : UpdateWithNextTrack(room);
        }

        private int UpdateWithNextTrack(Room room)
        {
            var moods = MoodRepository.ToList();
            var mood = moods.Find(m => String.Equals(m.Name, room.Mood, StringComparison.InvariantCultureIgnoreCase));
            var currentTrack = mood.TrackInfo.First(t => t.TrackId == room.CurrentTrackId);

            SetNextTrackInfo(room, mood, currentTrack);
            RoomRepository.Update(room);

            return room.CurrentTrackId;
        }

        private static void SetNextTrackInfo(Room room, Mood mood, Track track)
        {
            var currentTrackIndex = Array.IndexOf(mood.TrackInfo, track);
            if (currentTrackIndex < mood.TrackInfo.Length - 1)
            {
                room.CurrentTrackId++;
            }
            else
            {
                room.CurrentTrackId = 0;
            }

            room.TrackEndTime = DateTime.UtcNow.Add(mood.TrackInfo.First(t => t.TrackId == room.CurrentTrackId).Duration);
        }

        public IEnumerable<Room> GetAll()
        {
            return RoomRepository.ToList();
        }
    }
}