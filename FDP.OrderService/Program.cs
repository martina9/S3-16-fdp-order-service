using System;
using System.IO;
using System.Linq;
using FDP.Infrastructure;
using FDP.OrderService.Data;
using Topshelf;

namespace FDP.OrderService
{
    public class Program
    { 
        private static void Main(string[] args)
        {
            var path1 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(path1, "App_Data"));


            using (OrderDataContext test = new OrderDataContext())
            {
                var result = test.Users.FirstOrDefault();
            }



            HostFactory.Run(x =>
            { 
                x.Service<ServiceWindsor>();
                x.RunAsLocalSystem();
                x.SetDescription("Order Service");
                x.SetDisplayName("OrderService");
                x.SetServiceName("OrderService");
            });

        }
    }
}