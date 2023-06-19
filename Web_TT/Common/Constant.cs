using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_TT.Common
{
    public class Constant
    {
        public const string MSG_Login_Success = "Đăng nhập hệ thống thành công!";
        public const string MSG_Login_False = "Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại!";
        public const string MSG_LOGIN_REQUIRED = "Bạn cần đăng nhập để thực hiện chức năng này!";
        public const string MSG_InputEmpty = "Vui lòng nhập đầy đủ dữ liệu";

        public const string MSG_GetData_Success = "Lấy dữ liệu {0} thành công!";
        public const string MSG_Insert_Success = "Thêm mới {0} thành công!";
        public const string MSG_Delete_Success = "Xoá {0} thành công!";
        public const string MSG_Update_Success = "Cập nhật {0} thành công!";
        public const string MSG_DUPPLICATE = "Đã tồn tại {0} có {1} trong CSDL";
        public const string MSG_Not_Found_Data = "Không tìm thấy dữ liệu {0} trong CSDL";
        public const string MSG_Error = "Có lỗi xảy ra trong quá trình {0}. Vui lòng quay lại sau! (Chi tiết lỗi: {1})";

        public const string Sesstion_User_Tk_Acc = "User_Acc";
    }
}