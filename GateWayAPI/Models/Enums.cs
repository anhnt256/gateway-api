using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayManagement.Models
{
    public enum ResponseCode
    {
        ServerError = -1,
        Success = 0,
        RequiredLogin = 1,
        ParamsInvalid = 2,
        ExistedUsername = 3,
        ExistedEmail = 4,
        AccountInActive = 5,
        RequireUserName = 6,
    }

    public enum MessageType
    {
        Drink = 1, // Nước uống
        Hours = 2, // Giờ chơi
        Food = 3, // Đồ ăn
        Combo = 4, // Combo
        Card = 5, // Thẻ
        Wallet = 6, // Nạp tiền vô ví
        Chat = 7, // Giao tiếp
    }

    public enum OrderStatus
    {
        Cancel = 1, // Hủy đơn hàng
        Received = 2, // Mới nhận đơn
        Processing = 3, // Đơn hàng đang được xử lý
        Finished = 4, // Hoàn tất đơn hàng
    }

    public enum PaymentType
    {
        GuestFee = 1, // Khách tại quầy
        OrderFee = 2, // Phí dịch vụ
        TimeFee = 4, // Thời gian phí
        TransferFee = 8, // Chuyển tiền
        PromotionFee = 16, // Tặng tiền từ chính sách khuyến mãi
        GiftFee = 11, // Admin tặng tiền
    }

    public enum RecipeGroupType
    {
        Wallet = 0, // Nạp tiền vô ví
        Hours = 1, // Nạp giờ
        Drink = 2, // Nước uống
        Food = 3, // Đồ ăn
        Combo = 4, // Combo
        Card = 5, // Thẻ
    }

    public enum ShiftType
    {
        Morning = 1, // Ca sáng
        Afternoon = 2, // Ca chiều
        Night = 3, // Ca đêm
    }

    public enum ServiceGroup
    {
        Food = 1, // Thức ăn
        Drink = 2, // Nước uống
        Card = 3, // Thẻ
        Topping = 4, // Món thêm
    }


    public enum ComputerStatus
    {
        Off = 1, // Tắt máy
        Ready = 2, // Sẵn sàng
        On = 3 // Đang sử dụng
    }

}
