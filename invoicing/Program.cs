using invoicing.DB.DBContext;
using invoicing.Event;
using invoicing.MasterData;
using invoicing.Repository;
using invoicing.Repository.Interface;
using invoicing.Service;
using invoicing.Service.Interface;
using invoicing.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace invoicing
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();

            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<HomeScreenForm>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // 讀取 App.config 中的連線字串
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // 註冊 DbContext
            services.AddDbContext<InvoicIngDbContext>(options =>
                options.UseNpgsql(connectionString));

            // 註冊服務
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // 在這裡註冊其他的 Service
            services.AddScoped<IFormUIService, FormUIService>();
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped<IPrintService, PrintService>();

            // 註冊你的表單
            services.AddTransient<HomeScreenForm>();
            // 在這裡註冊所有需要依賴注入的其他表單
            services.AddTransient<CustomerManageForm>();
            services.AddTransient<CustomerForm>();
            services.AddTransient<SupplierManageForm>();
            services.AddTransient<SupplierForm>();
            services.AddTransient<ProductManageForm>();
            services.AddTransient<ProductForm>();
            services.AddTransient<PurchaseOrderForm>();
            services.AddTransient<PurchaseReceiptForm>();
            services.AddTransient<PurchaseReturnForm>();
            services.AddTransient<SalesDeliveryForm>();
            services.AddTransient<SalesOrderForm>();
            services.AddTransient<SalesReturnForm>();

            //事件註冊
            services.AddSingleton<EventBus>();
        }
    }
}
