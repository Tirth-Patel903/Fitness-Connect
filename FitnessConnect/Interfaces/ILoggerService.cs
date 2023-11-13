using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Interface
{
    public interface ILoggerService
    {
        public void Insert(string UserId, string ControllerName, string ActionName, Exception ex);
        public void PaymentLog(string userId, string ControllerName, string ActionName, string response);
    }
}
