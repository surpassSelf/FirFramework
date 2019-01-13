using LitJson;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using FirServer.Actions;
using FirServer.Common;
using FirServer.Defines;
using FirServer.Managers;

namespace FirServer.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("WebPolicy")]
    public class GatewayController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(Startup.repository.Name, typeof(GatewayController));

        [HttpGet]
        public IActionResult Get()
        {
            return BadRequest(ModelState);
        }

        //[HttpPost("~/api/people")]
        [HttpPost]
        public IActionResult Post([FromBody]PostData data)
        {
            if (ModelState.IsValid)
            {
                logger.Debug("action:" + data.Action + " method:" + data.Method + " data:" + data.Data);
                var action = GetAction(data.Action);
                if (action != null && !string.IsNullOrEmpty(data.Data))
                {
                    var json = JsonMapper.ToObject(data.Data);
                    var retData = action.OnExecute(data.Method, json);

                    logger.Debug("action:" + data.Action + " method:" + data.Method + " retdata:" + retData);
                    return Ok(retData);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// 获取ACTION
        /// </summary>
        /// <param name="actName"></param>
        /// <returns></returns>
        IAction GetAction(string actName)
        {
            var manager = ManagerCenter.Instance.GetManager(ManagerNames.ACTION);
            if (manager != null)
            {
                var actMgr = manager as ActionManager;
                if (actMgr != null)
                {
                   return actMgr.GetAction(actName);
                }
            }
            return null;
        }
    }
}
