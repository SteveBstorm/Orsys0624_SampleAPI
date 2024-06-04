using MovieAPI.Models;

namespace MovieAPI.Services
{
    public class UserService
    {
        private List<User> users { get; set; }

        public UserService()
        {
            users = new List<User>();
            users.Add(new User
            {
                Id = 1,
                Email = "admin@mail.com",
                Password = "Test1234",
                UserName = "Administrator",
                IsAdmin = true
            }) ;

            users.Add(new User
            {
                Id = 2,
                Email = "user@mail.com",
                Password = "Test1234",
                UserName = "User",
                IsAdmin = false
            });
        }

        public User? Login(string email, string pwd)
        {
            return users.FirstOrDefault(x => x.Email == email && x.Password == pwd);
        }
    }
}
