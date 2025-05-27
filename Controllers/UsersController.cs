using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using almondCoveApi.Data;
using almondCoveApi.Models.Domain;
using almondCoveApi.Repositories.Interfaces;
using almondCoveApi.Models.DTO;
using almondCoveApi.Enums;

namespace almondCoveApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [IgnoreAntiforgeryToken]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserProfiles()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null || !users.Any())
            {
                _logger.LogInformation("No user profiles found.");
            }
            return Ok(users ?? Enumerable.Empty<User>());
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            var user = new User()
            {
                Email = userDTO.Email.Trim(),
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                IsActive = true,
                SessionKey = "some random key",

                DateAdded = DateTime.Now
            };
            MatchUser matchres = await _userRepository.ExistsAsync(user);
            if (matchres == MatchUser.UserName)
            {
                _logger.LogInformation("Username {username} already exists", userDTO.UserName);
                return Conflict("Username already exists");
            }
            else if (matchres == MatchUser.Email)
            {
                _logger.LogInformation("Email {Email} already exists", userDTO.Email);
                return Conflict("An account with this email already exists");
            }
            else
            {
                try
                {
                    await _userRepository.CreateAsync(user);
                    _logger.LogInformation("User {UserName} created successfully", user.UserName);
                    return Ok("account for " + userDTO.UserName + " created successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating user {UserName}: {ErrorMessage}", userDTO.UserName, ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
                }
            }
           
        }
    }
}
