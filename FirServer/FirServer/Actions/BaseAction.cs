using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public abstract class BaseAction : BaseBehaviour, IAction
    {
        public abstract ResultData OnExecute(string method, JsonData data);
    }
}
