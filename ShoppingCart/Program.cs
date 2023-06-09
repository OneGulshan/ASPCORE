using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Infrastructure.IRepository;
using DataAccessLayer.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using CommonHelper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using DataAccessLayer.DbInitializer;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//UnitofWork ko Web Project ke ander access karne ke liye hamne yahan as a DI yaha UnitOfWork ko use kar liya
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//three type of life time services hoti hai hamare pass AddSingleton <- 1 hi obj banta hai chahe kitni bhi request ho jae, AddScoped <- 1 Client(Browser) ke liye 1 hi obj banega, to scoped zyada use karna chaiye, AddTransient <- every request make an obj
builder.Services.AddScoped<IDbInitializer, DbInitialize>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("PaymentSettings")); //paymet integration configure here
builder.Services./*AddDefaultIdentity*/AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders() // replace AddDefaultIdentity by AddIdentity bec we create Roles with identity, with this we also add AddDefaultTokenProviders by this method we got email tocken and every type of tocken inbuild in this method, Role ham tabhi add kar sakte hain jab ham AddDefaultTokenProviders add karte hain
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSingleton<IEmailSender, EmailSender>(); // ham yhan Email ki service ko sirf 1 bar hi use kar rahe hain isliye hamne yahan AddSingleton ka use kia hai or isse hamare entire the project sirf 1 hi obj banna hai iska, 2 sre browser se hoga to hamari 2sri req jaegi to 1 client se hamein 1 baar hi call karna hai
builder.Services.ConfigureApplicationCookie(options =>
{ // ye set karne ke baad ham log directly redirect kar sakte hain apne pages ke uppar agar ham login nahi hai to means agar ham Anonymous user hain to ham directly Login Page par ja sakte hain simply <- this is done by using Authorize attribute on Details methods if user not Authorized then redirect on Login Page directly by using these cookies
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Access Denied ka error agar hamare pass aae to ye path set ho jae by using ConfigureApplicationCookie Cookies
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
});

builder.Services.AddRazorPages(); // Razor Pages ko Map karne ke baad hamein service bhi add karni padti hai agar ham roles use kar rahe hote hain to or Role use karne ke liye hamein Email sender class bhi use or impliment karni padegi
builder.Services.AddDistributedMemoryCache(); // for enabling these Session settings in browser
builder.Services.AddSession(option => //hamein Cart ke ander items dikhe chaiye or parallelly vo cart items cart se del bhi hona chaiye usi ke liye hamne yahan session banaya hai session hi banta bhi hai, Session value pure bowser me chalti hai jab tak browser chalta hai tab tak session chalta hai and Session ka kuch timespan hota hai bydefault kuch hi seconds tak, ham yahan 100 min le kar chalte hai jo ham set kar dete hain vahi time set bhi ho jata hai in session
{
    option.IdleTimeout = TimeSpan.FromMinutes(100); // these are the Session values setting here
    option.Cookie.HttpOnly = true; // For enabling/Setting Keys for Session
    option.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
StripeConfiguration.ApiKey =
    builder.Configuration.GetSection("PaymentSettings:SecretKey").Get<string>();//apikey hamari string formate me thi jise hamne as a string convert kar bhej dia hai
dataSeadding();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void dataSeadding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitializer.Initialize();
    }
}
