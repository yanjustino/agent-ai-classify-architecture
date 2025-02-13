using OllamaSharp;

namespace ArchReview.AgentAI;

public partial class Agent: IDisposable
{
    private readonly OllamaApiClient _ollama;
    
    public Agent()
    {
        var uri = new Uri("http://localhost:11434");
        _ollama = new OllamaApiClient(uri);
        _ollama.SelectedModel = "llama3:latest";        
    }
    
    public async Task Run(string fileTree)
    {
        var prompt = GetPrompt(fileTree);
        await foreach (var stream in _ollama.GenerateAsync(prompt))
            Console.Write(stream?.Response);
    }

    public void Dispose()
    {
        _ollama.Dispose();
        GC.SuppressFinalize(this);
    }
}