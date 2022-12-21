using BuySite.Models.Entity;
using Stateless;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.Entity
{
    public enum OrderStateEnum
    { 
    Pending,//訂單已完成
    Processing,//支付處理中
    Completed,//交易成功
    Declined,//交易失敗
    Cancelled,//訂單取消
    Refund,//以退款
    }
    public enum OrderStateTriggerEnum
    { 
    PlaceOrder,//支付
    Approve,//支付成功
    Reject,//支付失敗
    Cancel,//取消
    Return//退貨
    }
    public class Order
    {
        public Order()
        {
            StateMachineinit();
        }
        [Key]
        public Guid Id { get; set; }
        public string Userid { get; set; }
        public User User { get; set; }
        public ICollection<LineItem> OrderItems { get; set; }
        public OrderStateEnum State { get; set; }
        public DateTime CreateDateUTC { get; set; }
        public string TransactionMatadata { get; set; }
        StateMachine<OrderStateEnum, OrderStateTriggerEnum> _machine;
        public void StateMachineinit()
        {
            _machine = new StateMachine<OrderStateEnum, OrderStateTriggerEnum>(OrderStateEnum.Pending);
            _machine.Configure(OrderStateEnum.Pending)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Cancel, OrderStateEnum.Cancelled);
            _machine.Configure(OrderStateEnum.Processing)
                .Permit(OrderStateTriggerEnum.Approve, OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Reject, OrderStateEnum.Declined);
            _machine.Configure(OrderStateEnum.Declined)
                .Permit(OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing);
            _machine.Configure(OrderStateEnum.Completed)
                .Permit(OrderStateTriggerEnum.Return, OrderStateEnum.Refund);
        }
    }
}
