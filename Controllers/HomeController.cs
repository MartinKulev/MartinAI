using MartinAI.Services;
using MartinAI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MartinAI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAskAI askAI;

        public HomeController(IAskAI askAI)
        {
            this.askAI = askAI;
        }

        public async Task<IActionResult> Ask(string prompt)
        {
            if (prompt != null)
            {
                string response = await askAI.GetChatGPTResponse(prompt);
                AskAIViewModel vm = new AskAIViewModel(prompt, response);
                return View(vm);
            }
            return View();
        }
    }
}
