# 進銷存管理系統 (Invoicing System)

本專案為一套基於 .NET 8 與 WinForms 開發的現代化進銷存管理系統，專為中小企業設計，提供完整的進貨、銷貨、庫存管理功能，並整合 PostgreSQL 資料庫與 automated backup 機制。

## 🌟 功能特色

### 📦 核心模組
- **基本資料管理**
  - **客戶管理**: 客戶資料維護、查詢.
  - **廠商管理**: 供應商資料維護.
  - **產品管理**: 產品建檔、價格管理、具備智能自動完成 (Autocomplete) 功能.

- **交易處理**
  - **進貨管理**: 採購單 (Purchase Order)、進貨驗收 (Purchase Receipt)、進貨退回 (Purchase Return).
  - **銷貨管理**: 銷貨單 (Sales Order)、出貨單 (Sales Delivery)、銷貨退回 (Sales Return).
  - **單據列印**: 支援 PDF 輸出與格式化列印 (含頁碼與合計).

- **財務報表**
  - **應收帳款**: 追蹤客戶未付款項.
  - **銷售統計**: 總銷售額查詢與報表.
  - **進貨統計**: 總進貨額查詢與報表.

### 🔌 擴充功能 (Plugins)
系統內建針對特定客戶格式的匯入模組，加速訂單處理：
- **唐詣 (TangYi)**: Excel 訂單匯入.
- **金大 (JinDa)**: 支援新舊格式 Excel 匯入.
- **關貿 (Guanmao)**: 專用格式 PDF 訂單匯入.

### 🛡️ 資料安全與備份
- **自動化備份**: 內建 `backupPGDB` 工具，每日定時將 PostgreSQL 資料庫備份至 **Cloudflare R2** 雲端儲存.
- **備份保留策略**: 支援設定保留天數 (Retention Days)，自動清理舊備份.

## 🛠️ 技術架構

- **前端介面**: Windows Forms (.NET 8)
- **後端邏輯**: C#
- **資料庫**: PostgreSQL 16+
- **ORM**: Entity Framework Core (Code-First / Db-First hybrid approach)
- **資料存取模式**: Repository Pattern & Unit of Work
- **依賴注入**: Microsoft.Extensions.DependencyInjection

## 📂 專案結構

```
net8Invoicing/
├── invoicing/               # WinForms 主程式
│   ├── Financials/          # 財務報表相關表單
│   ├── MasterData/          # 基本資料 (客戶/廠商/產品)
│   ├── Transactions/        # 進銷存交易表單
│   ├── PlugIn/              # 外部資料匯入模組
│   ├── Service/             # 業務邏輯層 (Services)
│   ├── Repository/          # 資料存取層 (Repositories)
│   ├── DB/                  # DbContext 設定
│   └── Program.cs           # 程式進入點與 DI 註冊
│
├── backupPGDB/              # 資料庫備份工具 (Console App)
│   ├── Services/            # 備份執行邏輯 (PostgresDumper, R2StorageService)
│   └── appsettings.json     # 備份設定檔
│
└── backupPGDB.Tests/        # 備份工具單元測試
```

## 🚀 快速開始

### 前置需求
- .NET 8 SDK
- PostgreSQL Server
- Visual Studio 2022 (建議)

### 安裝與執行

1. **設定資料庫連線**
   - 修改 `invoicing/App.config` 中的 `DefaultConnection`.
   - 修改 `backupPGDB/appsettings.json` 中的 `Database` 設定.

2. **資料庫遷移 (Migration)**
   - 確保 PostgreSQL 服務已啟動，並建立相應的 Database.
   - 使用 EF Core CLI 更新資料庫 Schema (如適用).

3. **啟動應用程式**
   - 開啟 `invoicing.sln`.
   - 設定 `invoicing` 為啟動專案，按 `F5` 執行.

4. **設定自動備份 (選用)**
   - 編譯 `backupPGDB` 專案.
   - 在 Windows Task Scheduler 中設定每日排程執行 `backupPGDB.exe`.

## ⚙️ 備份工具設定

`backupPGDB` 需設定 `appsettings.Secret.json` (不包含在版控中) 或環境變數以存取 Cloudflare R2：

```json
{
  "CloudStorage": {
    "ServiceUrl": "https://<ACCOUNT_ID>.r2.cloudflarestorage.com",
    "AccessKey": "<YOUR_ACCESS_KEY>",
    "SecretKey": "<YOUR_SECRET_KEY>",
    "BucketName": "pg-backups"
  }
}
```
