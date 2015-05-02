using Moody.Models.Data;
using Moody.Models.Requests;

namespace Moody.Service
{
    public class UserResponseConverter : IConverter<User, RequestUser>
    {
        public RequestUser Convert(User user)
        {
            return new RequestUser
            {
                Mood = user.Mood,
                Room = user.Room,
                TwitterHandle = user.TwitterHandle
            };
        }
    }
}