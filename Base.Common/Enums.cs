using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Common
{
    public class Enums
    {
        #region System
        public enum TypeSelect : byte
        {
            None = 0,
            Index = 1,
            Edit = 2,
            Detail = 3,
            Popup = 4,
            SelectItem = 5,
            AllInclude = 10,
        }
        public struct RegexDefine
        {
            public const string Email = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            public const string Year = @"^\d{4}$";
            public const string Interger = @"^\d*$";//"^0|([1-9]+[0-9]*)$";
            public const string IntergerAm = @"^(\-\d+)|(\d*)$";//@"^0|(\-?[1-9]+[0-9]*)$";
            public const string Numeric = @"^(\d+(\.\d+)?)$";
            public const string NumericAm = @"^(\-?\d+(\.\d+)?)$";
            public const string PhoneNumber = @"^(\+?[0-9\s\-\.]{9,15})$";
            public const string AscII = @"^([a-zA-Z\s]+)$";
            public const string Unicode = "^([\u00c0-\u020f\u1ea0-\u1ef9a-zA-Z0-9_\\-\\.\\s]*)$";
            public const string Code = @"^[a-zA-Z0-9_\-\.]+$";
            public const string CodeVN = "^[\u00c0-\u020fa-zA-Z0-9_\\-\\.]+$";
            public const string CharacterNumber = @"^[a-zA-Z0-9]+$";
            public const string LstCodeVN = "^[\u00c0-\u020fa-zA-Z0-9_\\-\\.\\,]+$";
            public const string CardNumber = @"^[a-zA-Z0-9_ \-\.]+$";
            public const string DateVN = @"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))$";
            public const string DateTimeVN = @"^((((31\/(0?[13578]|1[02]))|((29|30)\/(0?[1,3-9]|1[0-2])))\/(1[6-9]|[2-9]\d)?\d{2})|(29\/0?2\/(((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|(0?[1-9]|1\d|2[0-8])\/((0?[1-9])|(1[0-2]))\/((1[6-9]|[2-9]\d)?\d{2}))\s([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            public const string DateIso = @"^$";
            public const string MaSoThue = @"^([a-zA-Z0-9\s\-]*)$";
            public const string Time24 = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            public const string Time12VN = "^(0?[0-9]|1[0-2]):[0-5][0-9] (SA|CH)$";
            public const string Time12EN = "^(0?[0-9]|1[0-2]):[0-5][0-9] (AM|PM)$";
        }

        public struct RegexMessage
        {
            public const string Email = "Email nhập sai định dạng";
            public const string Year = "Năm nhập sai định dạng";
            public const string Interger = "{0} phải là kiểu số nguyên.";
            public const string Date = "{0} phải có giá trị từ 1 đến 31.";
            public const string PercentFromZero = "{0} phải có giá trị từ 0 đến 100.";
            public const string PercentFromOne = "{0} phải có giá trị từ 1 đến 100.";
            public const string Numeric = "{0} phải là kiểu số và lớn hơn 0.";
            public const string PhoneNumber = "{0} sai định dạng (chỉ nhập số từ 9 đến 15 ký tự)";
            public const string AscII = "Bạn chỉ được nhập ký tự AscII";
            public const string Unicode = "Bạn chỉ được nhập chữ";
            public const string Code = "Bạn chỉ được nhập số, ký tự AscII và ký tự: '.', '_', '-'";
            public const string CharacterNumber = "Bạn chỉ được nhập chữ và số";
            public const string CodeVN = "Bạn chỉ được nhập số, chữ và ký tự: '.', '_', '-'";
            public const string LstCodeVN = "Bạn chỉ được nhập số, chữ và ký tự: '.', '_', '-', ','";
            public const string CardNumber = "Bạn chỉ được nhập số, chữ và ký tự: '.', '_', '-'";
            public const string DateVN = "Ngày phải có định dạng ngày/tháng/năm(21/11/1990)";
            public const string DateIso = "Ngày phải có định dạng năm/tháng/ngày(1990/11/21)";
            public const string MaSoThue = "{0} không đúng định dạng (chỉ nhập số, chữ, '-' và dấu cách).";
            public const string Time24 = "{0} phải nhập 00:00 - 23:59";
            public const string Time12VN = "{0} phải nhập 12:00 SA - 12:00 CH";
            public const string Time12EN = "{0} phải nhập 12:00 AM - 12:00 PM";
        }

        public struct ErrorMessage
        {
            public const string Required = "{0} không được trống.";
            public const string RequiredSelect = "Bạn chưa chọn {0}.";
            public const string StringLengthMax = "{0} không được quá {1} ký tự.";
            public const string WrongFormat = "{0} không đúng định dạng.";
            public const string WrongFormatString = "{0} không đúng định dạng (chỉ nhập chữ và số).";
            public const string WrongFormatMST = "{0} không đúng định dạng (chỉ nhập số - và dấu cách).";
            public const string WrongFormatPhone = "{0} không đúng định dạng (chỉ nhập số - và dấu cách).";
            public const string StringLengthMinMax = "{0} phải có từ {2} tới {1} ký tự.";
            public const string SameKey = "{0} này đã được sử dụng.";
            public const string OnlyNumber = "{0} này chỉ được nhập số.";
            public const string RangeMinMax = "{0} phải có giá trị từ {1} tới {2}.";
            public const string SaveLessZero = "Có lỗi xảy ra, bạn kiểm tra lại dữ liệu.";
            public const string CompareGreaterThan = "{0} phải lớn hơn {1}";
            public const string CompareLessThan = "{0} phải nhỏ hơn {1}";
            public const string CompareDifferent = "{0} phải khác {1}";
            public const string GreaterThanZero = "Giá trị phải > 0";

            public const string FileRequired = "Bạn chưa nhập {0}.";

            //Bussiness
            public const string Exists = "{0} đã tồn tại.";

            public const string NotExists = "{0} không tồn tại.";

            //Diem Tiep Nhan Message
            public const string SameKeyDTN = "{0} không thể xóa do đang được phân hướng.";

            // KhungTyLe
            public const string OverLap = "{0} không được nhỏ hơn ngày hiện tại";

            public const string OverLap2 = "{0} không được lớn hơn ngày hiện tại";
            public const string SumError = "{0}Tổng tỷ lệ tại các cấp phải bằng 100%";
            public const string ErrorNHL = "{0}phải lớn hơn hoặc bằng ngày hiệu lực";

            //Hop Dong
            public const string WrongHD = "Hợp Đồng chưa có báo";

            //SuVu
            public const string WrongSV = "Đơn vị nhận hoặc bưu cục nhận bắt buộc phải có";

            public const string SpecialChar = "{0} chỉ được phép nhập các ký tự chữ số";
        }

        public struct FormatType
        {
            public const string FormatDateVN = "dd/MM/yyyy";
            public const string FormatDateTimeVN = "dd/MM/yyyy HH:mm";
            public const string FormatTime = "HH:mm";
            public const string Currency = "##,#.## vnđ";
            public const string TrongLuong = "##,#.## g";
            public const string Percent = "##,#.##\\%";
            public const string Integer = "##,#";
            public const string Number = "##,#.##}";
        }

        public struct FormatModel
        {
            public const string FormatDateVN = "{0:dd/MM/yyyy}";
            public const string FormatDateTimeVN = "{0:dd/MM/yyyy HH:mm}";
            public const string FormatTime = "{0:HH:mm}";
            public const string Currency = "{0:##,#.## vnđ}";
            public const string TrongLuong = "{0:##,#.## g}";
            public const string Percent = "{0:##,#.##\\%}";
            public const string Integer = "{0:##,#}";
            public const string Number = "{0:##,0.##}";
        }
        public struct SysConfig
        {
            public const string PathImage = "path_image";
        }

        public struct SICategory
        {
            public const byte C_Face = 1;
        }

        public struct SICategoryDetail
        {
            public const byte C_Source_HTD = 1;
        }
        #endregion
    }
}
