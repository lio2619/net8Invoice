namespace backupPGDB.Configuration;

/// <summary>
/// PostgreSQL 資料庫連線設定 (Strongly Typed Configuration)
/// </summary>
public sealed class DatabaseSettings
{
    /// <summary>
    /// 資料庫主機位址
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// 資料庫埠號
    /// </summary>
    public int Port { get; set; } = 5432;

    /// <summary>
    /// 資料庫名稱
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 資料庫使用者名稱
    /// </summary>
    public string Username { get; set; } = "postgres";

    /// <summary>
    /// 資料庫密碼 (來自 appsettings.Secret.json)
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// pg_dump.exe 完整路徑
    /// </summary>
    public string PgDumpPath { get; set; } = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";
}
