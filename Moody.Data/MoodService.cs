using System.Collections.Generic;
using System.Linq;
using MongoRepository;
using Moody.Models.Data;

namespace Moody.Data
{
    public class MoodService
    {
        private static readonly MongoRepository<Mood> MoodRepository = new MongoRepository<Mood>();

        public IEnumerable<string> GetAll()
        {
            return MoodRepository.ToList().Select(m => m.Name);
        }
    }
}