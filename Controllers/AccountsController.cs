using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ReviewApiApp.ViewModels;
using System.Runtime.InteropServices;
using System.Text;

namespace ReviewApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public AccountsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        

        [HttpPost("Authenticate")]

        // The First Way
        //public ActionResult Authenticate(string username, string password)
        //{

        //} 
        
        // The Second Way :It is better than first way and it is convention.
        public ActionResult Authenticate(AuthRequest authrequest )
        {

            var user = ValidateInformationUser(authrequest.Id, authrequest.FirstName, authrequest.LastName);
            if (user is null)
                return Unauthorized(); // 401 code 

            // here put token code


            // 1.make secretkey to array of bytes.
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]));
            // 2.
            var signingCre = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // HmacSha256 : It is an Algorithm of encryption.

            return Ok(user);
        }

        private AuthRequest ValidateInformationUser(int id, string firstName, string lastName)
        {

            // here must write authentication code : compiler goes to DB and doing an Authentication operation by using EF Core.
            // but for speed way you will return a constant object .
            return new  AuthRequest (){ Id = 1, FirstName = "khaled", LastName = "al khaledwn" };
        }
    }
}
