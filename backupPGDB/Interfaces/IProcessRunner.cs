namespace backupPGDB.Interfaces;

/// <summary>
/// 外部程序執行結果
/// </summary>
/// <param name="ExitCode">程序結束代碼</param>
/// <param name="StandardOutput">標準輸出</param>
/// <param name="StandardError">標準錯誤輸出</param>
public record ProcessResult(int ExitCode, string StandardOutput, string StandardError);

/// <summary>
/// 外部程序執行介面
/// 用於包裝 System.Diagnostics.Process，便於單元測試時 Mock
/// </summary>
public interface IProcessRunner
{
    /// <summary>
    /// 非同步執行外部程序
    /// </summary>
    /// <param name="fileName">程式檔案路徑</param>
    /// <param name="arguments">命令列參數</param>
    /// <param name="environmentVariables">環境變數 (可選)</param>
    /// <param name="cancellationToken">取消權杖</param>
    /// <returns>程序執行結果</returns>
    Task<ProcessResult> RunAsync(
        string fileName,
        string arguments,
        IDictionary<string, string>? environmentVariables = null,
        CancellationToken cancellationToken = default);
}
