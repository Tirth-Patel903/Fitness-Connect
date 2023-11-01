using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;
using FitnessConnect.Models;

namespace FitnessConnect.Services
{
    public class ChatboxRepository : IChatboxRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatboxRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MessageViewModel> GetAllMessages(string id)
        {
            List<MessageModel> RecieverMessages = new List<MessageModel>();
            RecieverMessages = _context.Messages.Where(a=>a.RecieverId == id).ToList();
            List<MessageModel> SenderMessages = new List<MessageModel>();
            SenderMessages = _context.Messages.Where(a => a.SenderId == id).ToList();
            List<MessageModel> messages = RecieverMessages.Concat(SenderMessages).ToList();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users = _context.Users.ToList();
            var comment = (from msg in messages
                           join user in users on msg.RecieverId equals user.Id
                           select new
                           {
                               Avatar = user.FirstName[0] + "" + user.LastName[0],
                               Email = user.Email,
                               ProfileImg = user.ProfileImg,
                               CreatedOn = msg.CreatedOn,
                               Comment = msg.Message
                           }).ToList();
            List<MessageViewModel> messageViewModels = comment.Select(c => new MessageViewModel
            {
                Avatar = c.Avatar,
                Email = c.Email,
                ProfileImg = c.ProfileImg,
                CreatedOn = c.CreatedOn,
                Message = c.Comment
            }).OrderBy(a=>a.CreatedOn).ToList();
            return messageViewModels;
        }

        public void saveMessages(MessageModel messageModel)
        {
            _context.Messages.Add(messageModel);
            _context.SaveChanges();
        }
    }
}
