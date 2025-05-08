using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserApiService _service;

        public UserController(UserApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll() => _service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _service.Get(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            var created = _service.Create(user);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            var updated = _service.Update(id, user);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
