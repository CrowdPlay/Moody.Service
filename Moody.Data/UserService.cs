﻿using System.Linq;
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
            var user = GetByHandle(requestUser);
            UserRepository.Update(user);
        }

        public User GetByHandle(RequestUser requestUser)
        {
            var users = UserRepository.ToList();
            var user = users.Find(u => u.TwitterHandle == requestUser.TwitterHandle);
            return user == null ? User.Create(requestUser) : user.Copy(requestUser);
        }
    }
}