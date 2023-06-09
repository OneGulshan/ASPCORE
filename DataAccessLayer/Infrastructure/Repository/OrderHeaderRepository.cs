
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository//IOrderHeaderRepository <- Repository of T hai ye
    {
        private readonly AppDbContext _context;
        public OrderHeaderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void PaymetStatus(int Id, string SessionId, string PaymentIntentId)
        {
            var orderHeader = _context.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            orderHeader.DateOfPayment = DateTime.Now;
            orderHeader.PaymentIntentId = PaymentIntentId;//PaymentIntentId and SessionId decide ho jata hai to PaymentDate bhi decide ho jae
            orderHeader.SessionId = SessionId;
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
            }
            if (paymentStatus != null)
            {
                order.PaymentStatus = paymentStatus;
            }
        }
    }
}
