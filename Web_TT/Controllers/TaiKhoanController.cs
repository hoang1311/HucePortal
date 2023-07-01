using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_TT.Common;
using Web_TT.Ctrl;
using Web_TT.Models;

namespace Web_TT.Controllers
{
    public class TaiKhoanController : Controller
    {
        public ActionResult Index()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return RedirectToAction("Login", "TaiKhoan");

            }
            return View();
        }
        public ActionResult Create()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return RedirectToAction("Login", "TaiKhoan");

            }
            return View();
        }
        public ActionResult Edit()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return RedirectToAction("Login", "TaiKhoan");

            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login_act()
        {
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();

            string tk = Request["txt_taikhoan"].Trim();
            string mk = Request["txt_matkhau"];

            //validate input
            if (string.IsNullOrEmpty(tk) || string.IsNullOrEmpty(mk))
            {
                FunctResult_ett<bool> rs_fail = new FunctResult_ett<bool>();
                rs_fail.ErrCode = EnumErrCode.InvalidInput;
                rs_fail.ErrDesc = Constant.MSG_InputEmpty;
                rs_fail.Data = false;
                return JsonConvert.SerializeObject(rs_fail);
            }
            var rs = ctrl.Login(tk, mk);
            //tạo sesstion
            if (rs.ErrCode == EnumErrCode.Success)
            {
                Session[Constant.Sesstion_User_Tk_Acc] = tk;               
            }
            return JsonConvert.SerializeObject(rs);
        }    
        [HttpGet]
        public ActionResult Logout()
        {
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();
            var rs = ctrl.Logout(Session[Constant.Sesstion_User_Tk_Acc].ToString());

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
        [HttpPost]
        public string Get_Info_TaiKhoan()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return null;
            }
            return Session[Constant.Sesstion_User_Tk_Acc].ToString();
        }
        [HttpGet]
        public string GetData()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";               
            }
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();

            string search_val = "";
            var search_type = EnumDataType.All;          
            var rs = ctrl.Search(Session[Constant.Sesstion_User_Tk_Acc].ToString(), search_val, search_type);
            return JsonConvert.SerializeObject(rs);
        }
        [HttpGet]
        public string GetDataByID()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";
            }
            string id = Request["id"];
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();
            var rs = ctrl.GetDataByID(Session[Constant.Sesstion_User_Tk_Acc].ToString(),id);
            return JsonConvert.SerializeObject(rs);
        }
        [HttpPost]
        public string Create_act()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";
            }
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();
            tbl_TaiKhoan obj = new tbl_TaiKhoan();   
            
            obj.ID = Request["txt_TaiKhoan"];
            obj.TaiKhoan = Request["txt_TaiKhoan"];
            obj.MatKhau = Request["txt_MatKhau"];           
            var rs = ctrl.Create(Session[Constant.Sesstion_User_Tk_Acc].ToString(),obj);
            return JsonConvert.SerializeObject(rs);
        }
        [HttpPost]
        public string Edit_act()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";
            }
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();
            tbl_TaiKhoan obj = new tbl_TaiKhoan();

            obj.ID = Request["txt_ID"];
            obj.TaiKhoan = Request["txt_TaiKhoan"];
            obj.MatKhau = Request["txt_MatKhau"];
            var rs = ctrl.Edit(Session[Constant.Sesstion_User_Tk_Acc].ToString(), obj);
            return JsonConvert.SerializeObject(rs);
        }
        [HttpPost]
        public string Delete()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";
            }
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();
            string id = Request["id"];           
            var rs = ctrl.Delete(Session[Constant.Sesstion_User_Tk_Acc].ToString(), id);
            return JsonConvert.SerializeObject(rs);
        }
        [HttpGet]
        public string Search()
        {
            if (Session[Constant.Sesstion_User_Tk_Acc] == null)
            {
                return "-1";
            }
            TaiKhoan_Ctrl ctrl = new TaiKhoan_Ctrl();

            string search_val = Request["txt_search_val"];
            string search_type_string = Request["slc_search_type"];
            EnumDataType search_type;
            Enum.TryParse<EnumDataType>(search_type_string, out search_type);
            
            var rs = ctrl.Search(Session[Constant.Sesstion_User_Tk_Acc].ToString(), search_val, search_type);
            return JsonConvert.SerializeObject(rs);
        }

    }
}