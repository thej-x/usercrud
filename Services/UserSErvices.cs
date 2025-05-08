using UserApi.Models;

namespace UserApi.Services
{
    public class UserApiService
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        public List<User> GetAll() => users;
        public User? Get(int id) => users.FirstOrDefault(u => u.Id == id);
        public User Create(User user)
        {
            user.Id = nextId++;
            users.Add(user);
            return user;
        }
        public bool Update(int id, User user)
        {
            var exsisting = Get(id);
            if (exsisting == null)

                return false;
            exsisting.Name = user.Name;
            exsisting.Email = user.Email;
            return true;
        }
        public bool Delete(int id)
        {
            var user = Get(id);
            if (user == null) return false;

            users.Remove(user);
            return true;
        }
    }
}