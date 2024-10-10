using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MedipolUniversity.Data;
var builder = WebApplication.CreateBuilder(args);

// Bu kodda, SchoolContext veritabanı bağlamını, SQL Server kullanarak yapılandırmış ve bağlantı dizesini appsettings.json dosyasındaki "SchoolContext" anahtarına göre ayarladım
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

//AddDatabaseDeveloperPageExceptionFilter() veritabanı hataları için ayrıntılı bilgi sağlar
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else {
    //UseDeveloperExceptionPage() genel hata detaylarını gösterir, UseMigrationsEndPoint() ise veri tabanı geçişlerini yönetir.
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
