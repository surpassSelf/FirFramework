using LitJson;
using FirServer.Controllers;

namespace FirServer.Actions
{
    public interface IAction
    {
        ResultData OnExecute(string method, JsonData data);
    }
}
