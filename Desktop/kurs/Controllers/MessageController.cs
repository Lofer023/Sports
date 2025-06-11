using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;
using YourApp.Models;
using YourApp.ViewModels;

namespace YourApp.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Chat(int partnerId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var messages = await _context.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == partnerId) ||
                            (m.SenderId == partnerId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            var model = new ChatViewModel
            {
                PartnerId = partnerId,
                Messages = messages
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Chat", new { partnerId = model.PartnerId });

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var message = new Message
            {
                SenderId = userId,
                ReceiverId = model.PartnerId,
                Content = model.NewMessage,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { partnerId = model.PartnerId });
        }
    }
}
