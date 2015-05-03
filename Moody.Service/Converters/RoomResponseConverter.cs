using System.Linq;
using Moody.Models.Data;
using Moody.Models.Requests;

namespace Moody.Service.Converters
{
    public class RoomResponseConverter : IConverter<Room, RequestRoom>
    {
        public RequestRoom Convert(Room room)
        {
            return new RequestRoom
            {
                usernames = room.RoomUsers.Select(u => u.Handle).ToArray(),
                mood = room.Mood
            };
        }
    }
}