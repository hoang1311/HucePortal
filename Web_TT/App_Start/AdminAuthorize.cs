using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_TT.Models;

namespace Web_TT.App_Start
{
    public class AdminAuthorize: AuthorizeAttribute
    {
        public string ID { get; set; }
        public static DatabaseDataContext db = new DatabaseDataContext();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // check đăng nhập
            tbl_TaiKhoan tk_session = (tbl_TaiKhoan)HttpContext.Current.Session["user"];
            if (tk_session != null)
            {
                #region // check quyền
                var qr = db.tbl_PhanQuyens.Where(o => o.ID_TaiKhoan == tk_session.ID && o.ID_ChucNang == ID);
                if (qr.Any())
                {
                    return;
                }
                else
                {
                    var returnURL = filterContext.RequestContext.HttpContext.Request.RawUrl;
                    filterContext.Result = new RedirectToRouteResult(new
                        System.Web.Routing.RouteValueDictionary(
                        new
                        {
                            controller = "LoiPhanQuyen",
                            action = "BaoLoi",
                            returnURL = returnURL.ToString()
                        }
                        ));
                }
                #endregion
                return;
            }
            else
            {
                var returnURL = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new 
                    System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "TaiKhoan",
                        action = "Login",
                        returnURL = returnURL.ToString()
                    }
                    ));
            }
           
        }
    }
}