using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_TT.Common;
using Web_TT.Models;

namespace Web_TT.Ctrl
{
    public class TaiKhoan_Ctrl
    {
        private readonly DatabaseDataContext db;
        private static readonly string Name = "Tài khoản";
        public TaiKhoan_Ctrl()
        {
            db = new DatabaseDataContext();
        }
        public FunctResult_ett<tbl_TaiKhoan> Login(string tk, string mk)
        {
            FunctResult_ett<tbl_TaiKhoan> rs = new FunctResult_ett<tbl_TaiKhoan>();

            try
            {
                var qr = db.tbl_TaiKhoans.Where(o => o.TaiKhoan == tk && o.MatKhau == ShareFunct.CreateMD5(mk) && (o.Is_Delete == null || o.Is_Delete == 0));

                if (qr.Any())
                {
                    //đăng nhập thành công
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = Constant.MSG_Login_Success;
                    var tk_obj = qr.SingleOrDefault();

                    tk_obj.Is_Login = true;
                    db.SubmitChanges();

                    rs.Data = tk_obj;
                }
                else
                {
                    //đăng nhập thất bại
                    rs.ErrCode = EnumErrCode.Fail;
                    rs.ErrDesc = Constant.MSG_Login_False;
                    rs.Data = null;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "Đăng nhập", ex.Message);
                rs.Data = null;
            }

            return rs;
        }
        public FunctResult_ett<tbl_TaiKhoan> Logout(string tk)
        {
            FunctResult_ett<tbl_TaiKhoan> rs = new FunctResult_ett<tbl_TaiKhoan>();

            try
            {
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = null;
                    return rs;
                }

                var qr = db.tbl_TaiKhoans.Where(o => o.TaiKhoan == tk && (o.Is_Delete == null || o.Is_Delete == 0));          
                
                var obj = qr.SingleOrDefault();
                obj.Is_Login = false;
                db.SubmitChanges();
                rs.Data = obj;                           
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "Đăng xuất"+ Name, ex.Message);
                rs.Data = null;
            }

            return rs;
        }
        public FunctResult_ett<List<tbl_TaiKhoan>> GetData(string tk)
        {
            FunctResult_ett<List<tbl_TaiKhoan>> rs = new FunctResult_ett<List<tbl_TaiKhoan>>();
            try
            {
                
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = null;
                    return rs;
                }               
                var qr = db.tbl_TaiKhoans.Where(o => o.TaiKhoan != tk && (o.Is_Delete == null || o.Is_Delete == 0));
                if (qr.Any())
                {
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = string.Format(Constant.MSG_GetData_Success, Name);
                    rs.Data = qr.ToList();
                }
            }
            catch(Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "lấy dữ liệu" + Name, ex.Message);
                rs.Data = null;
            }
            return rs;
        }
        public FunctResult_ett<tbl_TaiKhoan> GetDataByID(string tk, string id)
        {
            FunctResult_ett<tbl_TaiKhoan> rs = new FunctResult_ett<tbl_TaiKhoan>();
            try
            {               
                    var qr = db.tbl_TaiKhoans.Where(o => o.ID == id && (o.Is_Delete == null || o.Is_Delete == 0));
                    if (qr.Any())
                    {                     
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = Constant.MSG_GetData_Success;
                        rs.Data = qr.SingleOrDefault();
                    }
                    else
                    {
                        rs.ErrCode = EnumErrCode.EmptyData;
                        rs.ErrDesc = string.Format(Constant.MSG_Not_Found_Data, Name);
                    }              
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "lấy dữ liệu" + Name, ex.Message);
                rs.Data = null;
            }
            return rs;
        }
        public FunctResult_ett<tbl_TaiKhoan> Create(string tk,tbl_TaiKhoan obj)
        {
            FunctResult_ett<tbl_TaiKhoan> rs = new FunctResult_ett<tbl_TaiKhoan>();
            try
            {
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = null;
                    return rs;
                }

                var qr = db.tbl_TaiKhoans.Where(o => o.ID == obj.ID && (o.Is_Delete == null || o.Is_Delete == 0));
                if (qr.Any())
                {
                    rs.ErrCode = EnumErrCode.ExistentUnique;
                    rs.ErrDesc = string.Format(Constant.MSG_DUPPLICATE, Name, "ID= " + obj.ID);
                    rs.Data = null;
                }
                else
                {
                    var qr1 = db.tbl_TaiKhoans.Where(o => o.TaiKhoan == obj.TaiKhoan && o.MatKhau == obj.MatKhau && (o.Is_Delete == null || o.Is_Delete == 0));
                    if (qr1.Any())
                    {

                        rs.ErrCode = EnumErrCode.ExistentUnique;
                        rs.ErrDesc = string.Format(Constant.MSG_DUPPLICATE, Name, "Tài khoản= " + obj.TaiKhoan + "Mật khẩu=" + obj.MatKhau);
                        rs.Data = null;
                    }
                    else
                    {
                        obj.MatKhau = ShareFunct.CreateMD5(obj.MatKhau);
                        obj.CreateBy = tk;
                        obj.CreateDate = DateTime.Now;
                        db.tbl_TaiKhoans.InsertOnSubmit(obj);
                        db.SubmitChanges();
                        rs.ErrCode = EnumErrCode.Success;
                        rs.ErrDesc = string.Format(Constant.MSG_Insert_Success, Name);
                        rs.Data = obj;
                    }

                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "thêm mới" + Name, ex.Message);
                rs.Data = null;
            }
            return rs;
        }
        public FunctResult_ett<tbl_TaiKhoan> Edit(string tk, tbl_TaiKhoan obj)
        {
            FunctResult_ett<tbl_TaiKhoan> rs = new FunctResult_ett<tbl_TaiKhoan>();
            try
            {
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = null;
                    return rs;
                }

                var qr = db.tbl_TaiKhoans.Where(o => o.ID == obj.ID && (o.Is_Delete == null || o.Is_Delete == 0));
                if (!qr.Any())
                {
                    rs.ErrCode = EnumErrCode.EmptyData;
                    rs.ErrDesc = string.Format(Constant.MSG_Not_Found_Data, Name);
                    rs.Data = null;                  
                }

                var qr1 = db.tbl_TaiKhoans.Where(o => o.TaiKhoan == obj.TaiKhoan && o.MatKhau == obj.MatKhau && (o.Is_Delete == null || o.Is_Delete == 0));
                if (qr1.Any())
                {
                    rs.ErrCode = EnumErrCode.ExistentUnique;
                    rs.ErrDesc = string.Format(Constant.MSG_DUPPLICATE, Name, " Tài khoản= " + obj.TaiKhoan + " Mật khẩu= " + obj.MatKhau);
                    rs.Data = null;
                }
                else
                {
                    var crr_obj = qr.SingleOrDefault();
                    crr_obj.TaiKhoan = obj.TaiKhoan;
                    if (crr_obj.MatKhau != obj.MatKhau)
                    {
                        crr_obj.MatKhau = ShareFunct.CreateMD5(obj.MatKhau);
                    }

                    crr_obj.LastEditBy = tk;
                    crr_obj.LastEditDate = DateTime.Now;
                    db.SubmitChanges();

                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = string.Format(Constant.MSG_Update_Success, Name);
                    rs.Data = obj;
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "cập nhật" + Name, ex.Message);
                rs.Data = null;
            }
            return rs;
        }
        public FunctResult_ett<bool> Delete(string tk, string id)
        {
            FunctResult_ett<bool> rs = new FunctResult_ett<bool>();
            try
            {
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = false;
                    return rs;
                }

                var qr = db.tbl_TaiKhoans.Where(o => o.ID == id && (o.Is_Delete == null || o.Is_Delete == 0));
                if (qr.Any())
                {
                    var obj = qr.SingleOrDefault();
                    obj.Is_Delete = 1;
                    obj.DeleteBy = tk;
                    obj.DeleteDate = DateTime.Now;
                    db.SubmitChanges();

                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = string.Format(Constant.MSG_Delete_Success,  Name);
                    rs.Data = true;
                }
                else
                {
                    rs.ErrCode = EnumErrCode.EmptyData;
                    rs.ErrDesc = string.Format(Constant.MSG_Not_Found_Data, Name);
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "xóa" + Name, ex.Message);
                rs.Data = false;
            }
            return rs;
        }
        public FunctResult_ett<List<tbl_TaiKhoan>> Search(string tk, string search_val, EnumDataType data_type = EnumDataType.All, int crr_page = 1, int page_size = 10)
        {
            FunctResult_ett<List<tbl_TaiKhoan>> rs = new FunctResult_ett<List<tbl_TaiKhoan>>();

            try
            {
                if (!ShareFunct.isLogin(db, tk))
                {
                    rs.ErrCode = EnumErrCode.NotYetLogin;
                    rs.ErrDesc = Constant.MSG_LOGIN_REQUIRED;
                    rs.Data = null;
                    return rs;
                }
                IQueryable<tbl_TaiKhoan> qrs = null;
                IQueryable<tbl_TaiKhoan> qr = null;

                switch (data_type)
                {
                    case EnumDataType.Ma:
                        qrs = db.tbl_TaiKhoans.Where(o => o.ID == search_val && (o.Is_Delete == null || o.Is_Delete == 0));
                        break;
                    case EnumDataType.TaiKhoan:                       
                        qrs = db.tbl_TaiKhoans.Where(o => o.TaiKhoan.Contains(search_val) && (o.Is_Delete == null || o.Is_Delete == 0));
                        break;                    
                    case EnumDataType.All:
                        qrs = db.tbl_TaiKhoans.Where(o => o.TaiKhoan != tk && (o.Is_Delete == null || o.Is_Delete == 0));
                        break;
                }
                if (qrs.Any())
                {
                    if (data_type == EnumDataType.All)
                    {
                        qr = qrs;
                    }
                    else
                    {
                        qr = qrs.Skip((crr_page - 1) * page_size).Take(page_size);
                    }
                    rs.ErrCode = EnumErrCode.Success;
                    rs.ErrDesc = string.Format(Constant.MSG_GetData_Success, Name);
                    rs.Data = qr.ToList();
                }
                else
                {
                    rs.ErrCode = EnumErrCode.EmptyData;
                    rs.ErrDesc = string.Format(Constant.MSG_Not_Found_Data, Name);
                }
            }
            catch (Exception ex)
            {
                rs.ErrCode = EnumErrCode.Error;
                rs.ErrDesc = string.Format(Constant.MSG_Error, "tìm kiếm" + Name, ex.Message);
            }
            return rs;
        }
    }
}