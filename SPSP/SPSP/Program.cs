using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SPSP.Services.Reservation.StateMachine;
using SPSP;
using SPSP.Services.Customer;
using SPSP.Services.Database;
using SPSP.Services.Category;
using SPSP.Services.Employee;
using SPSP.Services.Menu;
using SPSP.Services.QRTable;
using SPSP.Services.Reservation;
using SPSP.Services.UserAccount;
using SPSP.Services.MenuItem;
using SPSP.Services.SaleInvoice;
using SPSP.Services.SaleInvoiceItem;
using SPSP.Services.PurchaseInvoice;
using SPSP.Services.PurchaseInvoiceItem;
using SPSP.Services.Order;
using SPSP.Services.OrderItem;
using SPSP.Services.PaymentGatewayData;
using SPSP.Services.OrderEmailPublisher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SPSP.Services.RecommenderService;
using Microsoft.ML;
using Quartz;
using SPSP.Services.Report;
using SPSP.Services.Promotion;
using SPSP.Services.MoodTracker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<MLContext>();
builder.Services.AddScoped<IRecommenderService, RecommenderService>();

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = JobKey.Create(nameof(RecommenderJob));

    options.AddJob<RecommenderJob>(jobKey)
    .AddTrigger(trigger => trigger.ForJob(jobKey)
        .WithSimpleSchedule(schedule => schedule
            .WithIntervalInSeconds(60)
        )
    );
});

builder.Services.AddQuartzHostedService();

builder.Services.AddTransient<IEmailPublisherService, EmailPublisherService>();
builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IMenuItemService, MenuItemService>();
builder.Services.AddTransient<IMenuItemPredictionService, MenuItemPredictionService>();
builder.Services.AddTransient<IPaymentGatewayDataService, PaymentGatewayDataService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderItemService, OrderItemService>();
builder.Services.AddTransient<ISaleInvoiceService, SaleInvoiceService>();
builder.Services.AddTransient<ISaleInvoiceItemService, SaleInvoiceItemService>();
builder.Services.AddTransient<IPurchaseInvoiceService, PurchaseInvoiceService>();
builder.Services.AddTransient<IPurchaseInvoiceItemService, PurchaseInvoiceItemService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IQRTableService, QRTableService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IMoodTrackerService, MoodTrackerService>();
// ne mogu ga nikako setovati na singleton
builder.Services.AddTransient<BaseState>();
builder.Services.AddTransient<InitialReservationState>();
builder.Services.AddTransient<PendingConfirmationReservationState>();
builder.Services.AddTransient<ConfirmedReservationState>();


builder.Services.AddControllers(x =>
{
    //x.Filters.Add<ErrorFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basicAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "basic"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "basicAuth"}
            },
            new string[]{}
    } });


});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(ICustomerService));
//builder.Services.AddAuthentication("BasicAuthentication")
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var issuer = builder.Configuration["TokenConfig:Issuer"] ?? "";
var audience = builder.Configuration["TokenConfig:Audience"] ?? "";
var signingKey = builder.Configuration["TokenConfig:SigningKey"] ?? "";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "BasicAuthentication"; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
})
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null)
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPSP v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    //dataContext.Database.EnsureCreated();

    var conn = dataContext.Database.GetConnectionString();

    dataContext.Database.Migrate();
}

app.Run();


//jos jedan nacin registracija servisa:
//builder.Services.AddTransient
//    <IService<SPSP.Models.Business, BaseSearchObject>,
//    BaseService<SPSP.Models.Business, Business, BaseSearchObject>>();


// JOB KOJI ĆE SE IZVRŠAVATI SVAKA 24 SATA, 
// ZA POTREBE TESTIRANJA, JOB ĆE IZVRŽITI SAMO JEDNOM I 
// POPUNITI TABELU MenuItemPrediction I TrainedData
//options.AddJob<RecommenderJob>(jobKey)
//    .AddTrigger(trigger => trigger.ForJob(jobKey)
//        .WithSimpleSchedule(schedule => schedule
//            .WithIntervalInHours(24)
//            .RepeatForever()
//        )
//    );
//NAKON TOGA, UMJESTO DA SE NA PODACIMA ALGORITAM PONOVO TRENIRA, 
//APLIKACIJA CE DOHVATATI RECOMMENED PODATKE IZ TABELA