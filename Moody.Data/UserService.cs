using MongoRepository;
using Moody.Models.Data;
using Moody.Models.Requests;

namespace Moody.Data
{
    public class UserService
    {
        private static readonly MongoRepository<User> UserRepository = new MongoRepository<User>();

        public void Upsert(RequestUser requestUser)
        {
            var user = User.Create(requestUser);
            UserRepository.Update(user);
        }
    }
}