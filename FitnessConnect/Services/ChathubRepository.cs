using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FitnessConnect.Services
{
    public class ChathubRepository : Hub
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChatboxRepository _chatboxrepo;
        private static Dictionary<string, List<string>> chatSessions = new Dictionary<string, List<string>>();

        public ChathubRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor,IChatboxRepository chatboxRepository)
        {
            _context = context;
            _chatboxrepo = chatboxRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async void SendMessage(string Message, string RecieverId)
        {
            string senderName = Context.User.Identity.Name;
            var timestamp = DateTime.Now;
            //Avatar = user.FirstName[0] + "" + user.LastName[0],
            var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            MessageModel model = new MessageModel();
            model.Message = Message;
            model.SenderId = UserId;
            model.RecieverId = RecieverId;
            model.CreatedById = UserId;
            model.CreatedOn = DateTime.Now;
            model.ModifiedById = UserId;
            model.ModifiedOn = DateTime.Now;
            model.IsActive = true;
            _chatboxrepo.saveMessages(model);
            await Clients.User(RecieverId).SendAsync("ReceiveMessage", senderName, Message, timestamp);
        }
        public async Task SendChatRequest(string senderUserId, string receiverUserId)
        {
            // Store the chat request in your database.
            // Add the sender and receiver to a pending chat request list.

            // Notify the receiver of the incoming request.
            await Clients.User(receiverUserId).SendAsync("ChatRequest", senderUserId);
        }

        public async Task AcceptChatRequest(string senderUserId, string receiverUserId)
        {
            // Create a chat session identifier or name and add both users to the session.
            string chatSession = $"{senderUserId}_{receiverUserId}";
            if (!chatSessions.ContainsKey(chatSession))
            {
                chatSessions.Add(chatSession, new List<string> { senderUserId, receiverUserId });
            }

            // Remove the request from the pending request list in your database.

            // Notify both users that the chat request is accepted.
            await Clients.Users(chatSessions[chatSession]).SendAsync("ChatRequestAccepted", chatSession);
        }
    }
}
