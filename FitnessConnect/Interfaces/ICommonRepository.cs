using FitnessConnect.Areas.Identity.Data;

namespace FitnessConnect.Interfaces
{
    public interface ICommonRepository
    {
        bool ContactUsSubmission(Contact contact);
        void AddLogger(String controllerName, String methodName, String message);
    }
}