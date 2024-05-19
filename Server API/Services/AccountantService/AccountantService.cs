﻿using Server_API.Context;
using Server_API.Models.Entity;
using Server_API.Models.WinFormsModels;

namespace Server_API.Services.AccountantService
{
    public class AccountantService : IAccountantService
    {
        private readonly PSICRODbContext _context;
        public AccountantService(PSICRODbContext context)
        {
            _context = context;
        }
        public void AddPaymentBill(string username, PaymentBillAccountantViewModel pbv)
        {
            var service = _context.CompanyServices.FirstOrDefault(service => service.Name == pbv.ServiceName);
            if (service == null)
            {
                _context.CompanyServices.Add(new CompanyService { Name = pbv.ServiceName });
                _context.SaveChanges();

                PaymentBill paymentBill = new PaymentBill()
                {
                    Client = _context.Clients.FirstOrDefault(client => client.Login == username)!,
                    Service = _context.CompanyServices.FirstOrDefault(service => service.Name == pbv.ServiceName)!,
                    Amount = pbv.Amount,
                    IssueDate = pbv.IssueDate,
                    DueToDate = pbv.DueToDate,
                };

                _context.PaymentBills.Add(paymentBill);
                _context.SaveChanges();
            }
            else
            {
                PaymentBill paymentBill = new PaymentBill()
                {
                    Client = _context.Clients.FirstOrDefault(client => client.Login == username)!,
                    Service = _context.CompanyServices.FirstOrDefault(service => service.Name == pbv.ServiceName)!,
                    Amount = pbv.Amount,
                    IssueDate = pbv.IssueDate,
                    DueToDate = pbv.DueToDate,
                };

                _context.PaymentBills.Add(paymentBill);
                _context.SaveChanges();
            }
        }

        public List<string> GetAllClients()
        {
           return _context.Clients.Select(client => client.Login).ToList();
        }

        public List<PaymentBillAccountantViewModel> GetPaymentBillsByClientLogin(string username)
        {
            var client = _context.Clients.FirstOrDefault(client => client.Login == username);
            var query = (from pb in _context.PaymentBills
                         join c in _context.Clients on pb.Client.Id equals c.Id
                         join cs in _context.CompanyServices on pb.Service.Id equals cs.Id
                         where c.Login == username
                         select new PaymentBillAccountantViewModel
                         {
                             PaymentBillId = pb.Id,
                             ServiceName = cs.Name,
                             Amount = pb.Amount,
                             IssueDate = pb.IssueDate,
                             DueToDate = pb.DueToDate,
                             PaymentDate = (DateTime?)pb.PaymentDate,
                             PaymentStatus = Enum.GetName(pb.PaymentStatus)
                         }).ToList();
            return query;
        }

        public bool UpdatePaymentBillById(int id, PaymentBillAccountantViewModel pbv)
        {
            var bill = _context.PaymentBills.FirstOrDefault(bill => bill.Id==id);
            var serviceName = _context.CompanyServices.FirstOrDefault(service => service.Name == pbv.ServiceName);
            if (bill == null || serviceName == null)
            {
                return false;
            }
            else
            {
                bill.Service = serviceName;
                bill.Amount = pbv.Amount;
                bill.IssueDate = pbv.IssueDate;
                bill.DueToDate = pbv.DueToDate;
                _context.SaveChanges();
                return true;
            }
        }

        public bool DeletePaymnentBillById(int id)
        {
            var bill = _context.PaymentBills.FirstOrDefault(bill => bill.Id == id);
            if (bill == null)
            {
                return false;
            }
            else
            {
                _context.PaymentBills.Remove(bill);
                _context.SaveChanges();
                return true;
            }
        }

    }
}
