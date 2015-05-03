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

        public int Upsert(RequestUser requestUser)
        {
            var result = GetByHandle(requestUser);
            
            UserRepository.Update(result.Item1);

            return result.Item2;
        }

        public Tuple<User, int> GetByHandle(RequestUser requestUser)
        {
            var user = GetByHandle(requestUser.TwitterHandle);

            if (user == null)
            {
                var roomId = UpsertRoom(requestUser);
                requestUser.Room = roomId;
                var result = User.Create(requestUser);
                return new Tuple<User, int>(result, roomId);
            }
            else
            {
                var roomId = UpsertRooms(requestUser, user.Room);
                requestUser.Room = roomId;
                return new Tuple<User, int>(user.Copy(requestUser), roomId);
            }
        }

        private int UpsertRooms(RequestUser requestUser, int oldRoomId)
        {
            if (oldRoomId != requestUser.Room)
            {
                var rooms = RoomRepository.ToList();
                var oldRoom = rooms.Find(r => r.RoomId == oldRoomId);
                oldRoom.RoomUsers.Remove(oldRoom.RoomUsers.First(u => u.Handle == requestUser.TwitterHandle));

                RoomRepository.Update(oldRoom);
            }
            return UpsertRoom(requestUser);
        }

        public User GetByHandle(string requestUser)
        {
            var users = UserRepository.ToList();
            return users.Find(u => u.TwitterHandle == requestUser);
        }

        private int UpsertRoom(RequestUser requestUser)
        {
            var rooms = RoomRepository.ToList();
            var room = rooms.Find(r => r.RoomId == requestUser.Room);

            if (room != null)
            {
                UpdateRoom(requestUser, room);
                return room.RoomId;
            }
            else
            {
                return CreateRoom(requestUser, rooms);
            }
        }

        private static void UpdateRoom(RequestUser requestUser, Room room)
        {
            var roomUser = room.RoomUsers.FirstOrDefault(u => u.Handle == requestUser.TwitterHandle);
            if (roomUser == null)
            {
                room.RoomUsers.Add(new RoomUser { Handle = requestUser.TwitterHandle, Mood = requestUser.Mood });
            }
            else
            {
                roomUser.Mood = requestUser.Mood;
            }

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
            var mood = moods.Find(m => String.Equals(m.Name, requestUser.Mood, StringComparison.InvariantCultureIgnoreCase));
            var track = mood.TrackInfo[0];
            return track;
        }

        private static int CreateRoom(RequestUser requestUser, List<Room> rooms)
        {
            var track = GetFirstTrackForMood(requestUser);

            var roomId = rooms.Count + 1;
            RoomRepository.Add(new Room
            {
                Mood = requestUser.Mood,
                RoomUsers = new[] {new RoomUser {Handle = requestUser.TwitterHandle, Mood = requestUser.Mood}},
                CurrentTrackId = track.TrackId,
                TrackEndTime = DateTime.UtcNow.Add(track.Duration),
                RoomId = roomId
            });
            return roomId;
        }

        public void Upsert(RequestUserWithoutRoom requestUser)
        {
            var user = GetByHandle(requestUser.TwitterHandle);

            if (user != null)
            {
                var userWithRoom = new RequestUser()
                {
                    Mood = requestUser.Mood,
                    Room = user.Room,
                    TwitterHandle = requestUser.TwitterHandle
                };
                Upsert(userWithRoom);
            }
        }
    }
}