using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void UpdateStatus(int Id, string orderStatus,string? paymentStatus=null);//UpdateStatus update PaymentStatus and OrderStatus both //Id <- this is Order Id here
        void PaymetStatus(int Id, string SessionId, string PaymentIntentId);
    }
}
