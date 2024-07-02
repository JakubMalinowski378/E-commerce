using System.Runtime.InteropServices.Marshalling;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
namespace E_commerce.API.Controllers
{
    public class UserController:BaseController
    {
        private readonly EcommerceDbContext _context;

        public UserController(EcommerceDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }
        [HttpGet("id")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);
            return user;
        }
    }
}
