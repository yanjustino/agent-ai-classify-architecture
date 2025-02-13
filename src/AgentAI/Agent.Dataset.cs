namespace ArchReview.AgentAI;

public partial class Agent
{
    private const string MVC = "mvc.dataset";
    private const string VERTICAL = "vertical.dataset";
    private const string PORTS = "ports.dataset";
    private const string CLEAN = "clean.dataset";
    
    public static string ReadDataset(string dataset)
    {
        var path = Path.Combine("dataset", dataset);
        var line = File.ReadAllLines(path);
        return $"[{string.Join(",", line)}]";
    }
}