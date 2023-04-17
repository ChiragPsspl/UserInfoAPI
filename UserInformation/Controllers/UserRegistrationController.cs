using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using UserInformation.Model;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace UserInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly CRUDUserRegistrationContext _registrationContext;
        private readonly CRUDLogContext _logContext;

        public UserRegistrationController(CRUDUserRegistrationContext registrationContext, CRUDLogContext _logContext)
        {
            this._registrationContext = registrationContext;
            this._logContext = _logContext;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var lstUserRegistration = _registrationContext.userregistration.ToList();
            return Ok(lstUserRegistration);
        }

        [Authorize]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistration userInfo)
        {
            _registrationContext.userregistration.Add(userInfo);
            _registrationContext.SaveChanges();
            return Ok();
        }

        [HttpGet("VerifyUser")]
        public IActionResult VerifyUser(string emailId, string password)
        {
            if (string.IsNullOrEmpty(emailId) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }

            string message = string.Empty;            
            UserRegistration user = _registrationContext.userregistration.ToList().Find(x => x.EmailId == emailId && x.Password == password);            
            LogInfo logInfo = new LogInfo();
            UserRegistrationDTO userRegistrationDTO = new UserRegistrationDTO();
            if (user != null)
            {
                logInfo.UserRegistrationId = user.UserRegistrationId;
                logInfo.IsLogin = true;
                message = "Login Successfully";

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthentication@777"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: "http://localhost:7182", audience: "http://localhost:7182", claims: new List<Claim>(), expires: DateTime.Now.AddHours(24), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);                
                userRegistrationDTO.UserName = user.UserName;
                userRegistrationDTO.EmailId = user.EmailId;
                userRegistrationDTO.Password = user.Password;
                userRegistrationDTO.Address = user.Address;
                userRegistrationDTO.Token = tokenString;
                //return Ok(new JWTTokenResponse
                //{
                //    Token = tokenString
                //});


            }
            else
            {
                logInfo.UserRegistrationId = null;
                logInfo.IsLogin = false;
                message = "Invalid User Name and Password";
            }

            // Add Data into LogInfo Table

            logInfo.UserName = emailId;
            logInfo.Password = password;            
            _logContext.LogInfo.Add(logInfo);
            _logContext.SaveChanges();

            return Ok(userRegistrationDTO);
        }


    }
}

