using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Models;

namespace FitnessConnect.Interfaces
{
    public interface IChatboxRepository
    {
        void saveMessages(MessageModel messageModel);
        List<MessageViewModel> GetAllMessages(string id);
    }
}
