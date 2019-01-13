using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using FirServer.Common;
using FirServer.Defines;
using FirServer.Managers;
using FirServer.Models;

namespace kickit_web.Pages
{
    public class AdminModel : PageModel
    {
        private string _ReturnUrl = "https://fb-api.junfine.com/admin";
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get { return _ReturnUrl; } }
        public string Message { get; set; }
        public DataRowCollection Rows { get; set; }

        public void OnGet()
        {
            if (!IsVaild())
            {
                Message = "Authentication Failed!!";
                return;
            }
            Message = "Welcome Admin Page.";
            var mgr = ManagerCenter.Instance.GetManager(ManagerNames.MODEL);
            var modelMgr = mgr as ModelManager;
            if (modelMgr != null)
            {
                var userModel = modelMgr.GetModel(ModelNames.User) as UserModel;
                if (userModel != null)
                {
                    Rows = userModel.GetAll();
                }
            }
        }

        public bool IsVaild()
        {
            byte[] data = HttpContext.Session.Get(AppConst.loginKey);
            if (data == null)
            {
                return false;
            }
            string str = System.Text.Encoding.ASCII.GetString(data);
            return str == AppConst.loginPass;
        }
    }
}