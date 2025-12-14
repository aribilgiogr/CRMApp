using Business.Profiles;
using Business.Services;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Data;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    // IOC (Inversion of Control) Sınıfı: Yapıcı metotları kullanarak içeri almaya çalıştığımız bağımlılıkları burada tanımlayarak new operatörü kullanmadan çözüyoruz. Bu yapıya Dependency Injection (Bağımlılık Enjeksiyonu) denir.
    public static class IOC
    {
        // Bu alana istediğimiz uzantıları ekleyebiliriz.

        // Veri tabanı ile ilgili bağımlılıkları eklemek için bir uzantı metodu.
        // IConfiguration parametresi, uygulamanın yapılandırma ayarlarına erişim sağlar.
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Veri tabanı ile ilgili bağımlılıkları burada ekleyin.
            services.AddDbContext<CrmDbContext>(options => options.UseSqlite(configuration.GetConnectionString("crmdb")));

            // Diğer veri erişim bağımlılıklarını burada ekleyin. Ortak bir UnitOfWork sınıfı ekleyebilirsiniz.
            // UnitofWork sınıfı yapıcı parametre olarak CrmDbContext alır, bu sebeble önce DbContext eklenmelidir.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Class Mapping için AutoMapper'ı ekleyin. .Net Framework taranfında kendimiz AutoMapperConfig.Initialize(); şeklinde yapıyorduk. Burada ise servis koleksiyonuna ekliyoruz.

            services.AddAutoMapper(config =>
            {
                // Tüm profilleri otomatik olarak yükle
                config.AddMaps(typeof(CustomerProfiles));
            });


            // AddScoped metodu, belirli bir hizmetin yaşam süresini tanımlar. Scoped yaşam süresi, her istemci isteği için tek bir örnek oluşturur ve bu örnek, isteğin tamamı boyunca kullanılır.
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ILeadService, LeadService>();

            return services;
        }
    }
}
