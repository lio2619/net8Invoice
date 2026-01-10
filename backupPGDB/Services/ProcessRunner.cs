using backupPGDB.Interfaces;
using System.Diagnostics;

namespace backupPGDB.Services;

/// <summary>
/// 外部程序執行服務
/// 封裝 System.Diagnostics.Process
/// </summary>
public sealed class ProcessRunner : IProcessRunner
{
    /// <inheritdoc/>
    public async Task<ProcessResult> RunAsync(
        string fileName,
        string arguments,
        IDictionary<string, string>? environmentVariables = null,
        CancellationToken cancellationToken = default)
    {
        using var process = new Process();
        
        process.StartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // 加入環境變數
        if (environmentVariables != null)
        {
            foreach (var (key, value) in environmentVariables)
            {
                process.StartInfo.EnvironmentVariables[key] = value;
            }
        }

        process.Start();

        // 非同步讀取輸出
        var outputTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var errorTask = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        var standardOutput = await outputTask;
        var standardError = await errorTask;

        return new ProcessResult(process.ExitCode, standardOutput, standardError);
    }
}
