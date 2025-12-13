using Core.Abstracts;
using Data;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    // IOC (Inversion of Control) Sınıfı: Yapıcı metotları kullanarak içeri almaya çalıştığımız bğımlılıkları burada tanımlayarak new operatörü kullanmadan çözüyoruz. Bu yapıya Dependency Injection (Bağımlılık Enjeksiyonu) denir.
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

            return services;
        }
    }
}
