using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;

namespace FitnessConnect.Services
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public CommonRepository(ApplicationDBContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public bool ContactUsSubmission(Contact contact)
        {
            try
            {
                _context.Contact.Add(contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                AddLogger("CommonRepository", "ContactUsSubmission", ex.Message);
                return false;
            }
        }
        public void AddLogger(String controllerName, String methodName, String message)
        {
            LoggerModel logger = new LoggerModel();
            logger.ControllerName = controllerName;
            logger.MethodName = methodName;
            logger.Message = message;
            logger.CreatedOn = DateTime.Now;
            logger.CreatedById = null;
            logger.ModifiedOn = DateTime.Now;
            logger.ModifiedById = null;
            logger.IsActive = true;
            _context.Logger.Add(logger);
            _context.SaveChanges();
        }

        public List<Contact> GetContacts()
        {
            return _context.Contact.ToList();
        }
    }
}
