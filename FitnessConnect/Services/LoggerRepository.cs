using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Service.Interface;
using NLog;
using ILogger = NLog.ILogger;

namespace FitnessConnect.Service.Repository
{
    public class LoggerRepository : ILoggerService
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger _nLog;

        public LoggerRepository(ApplicationDBContext context)
        {
            _context = context;
            _nLog = LogManager.GetCurrentClassLogger();
        }
        public void Insert(string UserId, string ControllerName, string ActionName, Exception ex)
        {
            try
            {
                string ErrorMsg = ex.Message;
                string filePath = "c:\\Temp\\CustomLogs.txt";

                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Close();
                }

                string Log = "UserId: " + UserId + " Controller: " + ControllerName + ", Method: " + ActionName + ", Error msg: " + ErrorMsg + " Created On: " + DateTime.Now;

                // Write the current time to a new line in the file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(Log);
                }

                _nLog.Error(ex.Message + ex.StackTrace);

                LoggerModel model = new LoggerModel();
                model.Id = UserId;
                model.ControllerName = ControllerName;
                model.MethodName = ActionName;
                model.Message = ErrorMsg;
                model.CreatedOn = DateTime.Now;
                _context.Logger.Add(model);
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                _nLog.Error(err.Message + err.StackTrace);
            }
        }
        public void PaymentLog(string userId, string ControllerName, string ActionName, string response)
        {
            try
            {
               
                string filePath = "c:\\Temp\\PaymentLog.txt";

                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Close();
                }

                string Log = "UserId: " + userId + " Controller: " + ControllerName + ", Method: " + ActionName + ", Status Code: " + response + " Created On: " + DateTime.Now;

                // Write the current time to a new line in the file
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(Log);
                }
            }
            catch (Exception err)
            {
                _nLog.Error(err.Message + err.StackTrace);
            }
        }
    }
}
