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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

//builder.Services.AddTransient
//    <IService<SPSP.Models.Business, BaseSearchObject>,
//    BaseService<SPSP.Models.Business, Business, BaseSearchObject>>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IMenuItemService, MenuItemService>();
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

builder.Services.AddTransient<BaseState>();
builder.Services.AddTransient<InitialReservationState>();
builder.Services.AddTransient<PendingConfirmationReservationState>();
builder.Services.AddTransient<ConfirmedReservationState>();
builder.Services.AddTransient<OnHoldReservationState>();


builder.Services.AddControllers(x =>
{
    //x.Filters.Add<ErrorFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
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