namespace MartinAI.Services
{
    public interface IAskAI
    {
        Task<string> GetChatGPTResponse(string prompt);
    }
}
