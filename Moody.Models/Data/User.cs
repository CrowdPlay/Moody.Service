using MongoRepository;
using Moody.Models.Requests;

namespace Moody.Models.Data
{
    public class User : Entity
    {
        public string Mood { get; set; }
        public int Room { get; set; }
        public string TwitterHandle { get; set; }

        public static User Create(RequestUser requestUser)
        {
            return new User
            {
                Mood = requestUser.Mood,
                Room = requestUser.Room,
                TwitterHandle = requestUser.TwitterHandle
            };
        }

        public User Copy(RequestUser requestUser)
        {
            Mood = requestUser.Mood;
            Room = requestUser.Room;
            TwitterHandle = requestUser.TwitterHandle;
            return this;
        }
    }
}