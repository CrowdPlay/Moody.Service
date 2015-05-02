using System;
using System.Collections.Generic;
using System.Linq;
using MongoRepository;
using Moody.Models.Data;
using Moody.Models.Requests;

namespace Moody.Data
{
    public class UserService
    {
        private static readonly MongoRepository<User> UserRepository = new MongoRepository<User>();
        private static readonly MongoRepository<Room> RoomRepository = new MongoRepository<Room>();
        private static readonly MongoRepository<Mood> MoodRepository = new MongoRepository<Mood>();

        public void Upsert(RequestUser requestUser)
        {
            var user = GetByHandle(requestUser);

            UserRepository.Update(user);
        }

        public User GetByHandle(RequestUser requestUser)
        {
            var user = GetByHandle(requestUser.TwitterHandle);

            if (user == null)
            {
                var result = User.Create(requestUser);
                UpsertRoom(requestUser);
                return result;
            }
            else
            {
                UpsertRooms(requestUser, user.Room);
                return user.Copy(requestUser);
            }
        }

        private void UpsertRooms(RequestUser requestUser, int oldRoomId)
        {
            if (oldRoomId != requestUser.Room)
            {
                var rooms = RoomRepository.ToList();
                var oldRoom = rooms.Find(r => r.RoomId == oldRoomId);
                oldRoom.RoomUsers.Remove(oldRoom.RoomUsers.First(u => u.Handle == requestUser.TwitterHandle));

                RoomRepository.Update(oldRoom);
            }
            UpsertRoom(requestUser);
        }

        public User GetByHandle(string requestUser)
        {
            var users = UserRepository.ToList();
            return users.Find(u => u.TwitterHandle == requestUser);
        }

        private void UpsertRoom(RequestUser requestUser)
        {
            var rooms = RoomRepository.ToList();
            var room = rooms.Find(r => r.RoomId == requestUser.Room);

            if (room != null)
            {
                UpdateRoom(requestUser, room);
            }
            else
            {
                CreateRoom(requestUser, rooms);
            }
        }

        private static void UpdateRoom(RequestUser requestUser, Room room)
        {
            room.RoomUsers.Add(new RoomUser {Handle = requestUser.TwitterHandle, Mood = requestUser.Mood});
            if (requestUser.Mood != room.Mood)
            {
                var track = GetFirstTrackForMood(requestUser);
                room.CurrentTrackId = track.TrackId;
                room.TrackEndTime = DateTime.UtcNow.Add(track.Duration);
            }

            room.Mood = requestUser.Mood;
            RoomRepository.Update(room);
        }

        private static Track GetFirstTrackForMood(RequestUser requestUser)
        {
            var moods = MoodRepository.ToList();
            var mood = moods.Find(m => m.Name == requestUser.Mood);
            var track = mood.TrackInfo[0];
            return track;
        }

        private static void CreateRoom(RequestUser requestUser, List<Room> rooms)
        {
            var track = GetFirstTrackForMood(requestUser);

            RoomRepository.Add(new Room
            {
                Mood = requestUser.Mood,
                RoomUsers = new[] {new RoomUser {Handle = requestUser.TwitterHandle, Mood = requestUser.Mood}},
                CurrentTrackId = track.TrackId,
                TrackEndTime = DateTime.UtcNow.Add(track.Duration),
                RoomId = rooms.Count + 1
            });
        }
    }
}