using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AnimeWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

// 配置 SQLite 数据库连接字符串
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// 配置 Identity 服务
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

// //Email service
// builder.Services.AddTransient<EmailService>();


// // 添加服务到容器
// builder.Services.AddControllersWithViews();
// builder.Services.AddRazorPages();

// 配置 Identity 选项
builder.Services.Configure<IdentityOptions>(options =>
{
    // 密码设置
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // 锁定设置
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // 用户设置
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// 配置应用程序 Cookie 设置
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie 设置
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// 配置 HTTP 请求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // 默认 HSTS 值为 30 天。对于生产环境，可以根据需要更改此值。
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
