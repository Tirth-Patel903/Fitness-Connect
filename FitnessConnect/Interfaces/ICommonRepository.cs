using FitnessConnect.Areas.Identity.Data;

namespace FitnessConnect.Interfaces
{
    public interface ICommonRepository
    {
        bool ContactUsSubmission(Contact contact);
        List<Contact> GetContacts();
        void AddLogger(String controllerName, String methodName, String message);
    }
}