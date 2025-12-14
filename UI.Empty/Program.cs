using Business;
using Core.Abstracts.IServices;

var builder = WebApplication.CreateBuilder(args);
// builder alaný: Hazýrladýmýz uygulamanýn baþlamadan önce yapýlandýrýlmasý için kullanýlýr. Bu alanda servisleri ekleyebilir, yapýlandýrma ayarlarýný belirleyebilir ve diðer baþlangýç iþlemlerini gerçekleþtirebiliriz. Kýsaca uylama yapýlandýrýlýr.

builder.Services.AddDataAccessDependencies(builder.Configuration); // Business/IOC.cs içindeki AddDataAccessDependencies metodu çaðrýlýr. Bu metod, veri eriþim baðýmlýlýklarýný servis koleksiyonuna ekler. builder.Configuration parametresi, uygulamanýn yapýlandýrma ayarlarýna eriþim saðlar.

var app = builder.Build(); // Build metodu, yapýlandýrýlmýþ uygulama nesnesini oluþturur. Bu nesne, uygulamanýn çalýþmasý için gerekli tüm bileþenleri içerir ve uygulamanýn baþlatýlmasýný saðlar.

// app alaný: Uygulamanýn kendisini temsil eder. Bu alanda middleware (ara yazýlýmlar) ekleyebilir, yönlendirme iþlemlerini tanýmlayabilir ve uygulamanýn çalýþma zamanýndaki davranýþýný belirleyebiliriz. app.Run() komutuna kadar baþarýyla ulaþmaya çalýþýrýz. Ziyaretçiden gelen isten app.Run()'a ulaþmazsa hata döner.

// Routing Alaný (.net framework tarafýnda routeconfig.cs)
// Empty Project basit bir endpoint api gibi davranýr. Gelen isteði karþýlamak için routing tanýmlanýr. Fakat tam teþekküllü bir api projesi deðildir.
app.MapGet("/", async (ICustomerService service) => await service.GetAllAsync());

app.Run();
