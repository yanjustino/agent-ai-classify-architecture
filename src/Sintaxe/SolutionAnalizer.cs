using System.Diagnostics;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace ArchReview.Sintaxe;

/// <summary>
/// Classe para analíse de solution .net
/// </summary>
public class SolutionAnalizer
{
    public SolutionAnalizer()
    {
        MSBuildLocator.RegisterDefaults();
    }
    
    /// <summary>
    /// Executa a análise do solution path
    /// </summary>
    /// <param name="solutionPath"></param>
    /// <returns></returns>
    public async Task<string> GetFilePathFromSyntaxTrees(string solutionPath)
    {
        using var workspace = CreateWorkspace();
        var path = Path.GetFullPath(solutionPath);
        var solution = await workspace.OpenSolutionAsync(path);
        var result = await GetProjectFilePaths(solution);

        return string.Join("\n", result);
    }

    private static async Task<List<string>> GetProjectFilePaths(Solution solution)
    {
        var result = new List<string>();
        var basePath = Path.GetDirectoryName(solution.FilePath);

        // Iterar sobre os projetos na solução
        foreach (var project in solution.Projects)
        {
            var compilation = await project.GetCompilationAsync();
            foreach (var x in compilation?.SyntaxTrees.ToList() ?? [])
            {
                var fullPath = x.FilePath;
                var relativePath = Path.GetRelativePath(basePath!, fullPath);
                
                result.Add(relativePath);
            }
        }

        return result;
    }

    private static async Task RunDotnetBuild(string slnPath)
    {
        var dotnetProcess = Process.Start(new ProcessStartInfo(@"dotnet", $"build \"{slnPath}\"")
        {
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        });
        await dotnetProcess!.WaitForExitAsync();
    }

    private static MSBuildWorkspace CreateWorkspace()
    {
        if (!MSBuildLocator.IsRegistered)
        {
            var instances = MSBuildLocator.QueryVisualStudioInstances().ToArray();
            MSBuildLocator.RegisterInstance(instances.OrderByDescending(x => x.Version).First());
        }

        var workspace = MSBuildWorkspace.Create();
        return workspace;
    }    
}