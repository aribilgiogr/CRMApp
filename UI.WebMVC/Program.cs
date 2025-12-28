using Business;
using Core.Abstracts.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDataAccessDependencies(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Identity kullanýrken giriþ yapan kullanýcý bilgilerinin kullanýlabilmesi için.
app.UseAuthorization(); // Sayfa eriþimlerini yönetir.

// Basit endpoint protok yapýsýdýr, özel API kütüphanelerine ihtiyaç duymaz.
app.MapPost("/activities/lead/{id}", async (IActivityService service, int id) => await service.GetActivitiesByLeadId(id));
app.MapPost("/activities/customer/{id}", async (IActivityService service, int id) => await service.GetActivitiesByCustomerId(id));
app.MapPost("/activities/opportunity/{id}", async (IActivityService service, int id) => await service.GetActivitiesByOpportunityId(id));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
