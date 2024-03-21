using AutoMapper;
using SPSP.Models.Request.Customer;
using SPSP.Models.Request.Employee;
using SPSP.Models.Request.Reservation;
using SPSP.Models.Request.UserAccount;

namespace SPSP.Services.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Business, Models.Business>();

            CreateMap<Database.Menu, Models.Menu>();

            CreateMap<Database.MenuItem, Models.MenuItem>();
            CreateMap<MenuItemCreateRequest, Database.MenuItem>();
            CreateMap<MenuItemUpdateRequest, Database.MenuItem>();

            CreateMap<Database.Order, Models.Order>();
            CreateMap<Database.OrderItem, Models.OrderItem>();

            CreateMap<Database.Category, Models.Category>();

            CreateMap<Database.QRTable, Models.QRTable>();

            CreateMap<Database.Reservation, Models.Reservation>();
            CreateMap<ReservationCreateRequest, Database.Reservation>();
            CreateMap<ReservationUpdateRequest, Database.Reservation>();

            CreateMap<Database.Employee, Models.Employee>();
            CreateMap<EmployeeCreateRequest, Database.Employee>();
            CreateMap<EmployeeUpdateRequest, Database.Employee>();

            CreateMap<Database.Customer, Models.Customer>();
            CreateMap<CustomerCreateRequest, Database.Customer>();
            CreateMap<CustomerUpdateRequest, Database.Customer>();

            CreateMap<Database.UserAccount, Models.UserAccount>();
            CreateMap<UserAccountCreateRequest, Database.UserAccount>();
            CreateMap<UserAccountUpdateRequest, Database.UserAccount>();

        }
    }
}


