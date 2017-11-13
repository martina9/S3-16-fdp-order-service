using FDP.OrderService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDP.OrderService
{
    public static class DataUtility
    {
        public static OrderDataContext GetDataContext(OrderDataContext dataContext)
        {
            if(dataContext == null)
            {
                dataContext = new OrderDataContext();
            }
            else if(dataContext.IsDisposed)
            {
                dataContext = new OrderDataContext();
            } 
            return dataContext;
        }
    }
}
