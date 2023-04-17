using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;
using UserInformation.Model;

namespace UserInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInfoController : ControllerBase
    {
        private readonly CRUDLogContext _logContext;

        public LogInfoController(CRUDLogContext logContext) 
        {
            this._logContext = logContext;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public ActionResult<List<LogInfo>> Get()
        {
            var lstLogInfo = _logContext.LogInfo.ToList().OrderByDescending(x => x.LogInfoId);


            List<LogInfo> lstLog = lstLogInfo.Select(x => new LogInfo
            {
                UserName = x.UserName,
                Password = x.Password,
                IsLogin = x.IsLogin,
            }).ToList();
            return new List<LogInfo>(lstLog);
        }

        //[Authorize]
        [HttpPost]
        public void Post([FromBody] LogInfo logInfo)
        {
            _logContext.LogInfo.Add(logInfo);
            _logContext.SaveChanges();
        }
    }
}
