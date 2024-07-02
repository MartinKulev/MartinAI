namespace MartinAI.ViewModels
{
    public class AskAIViewModel
    {
        public AskAIViewModel(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
        }

        public string Prompt { get; set; }

        public string Response { get; set; }
    }
}
