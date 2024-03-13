using AutoMapper;


namespace SPSP.Services.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Database.Business, Models.Business>();

            CreateMap<Database.Menu, Models.Menu>();

            CreateMap<Database.MenuItem, Models.MenuItem>();
            CreateMap<MenuItemInsertRequest, Database.MenuItem>();
            CreateMap<MenuItemUpdateRequest, Database.MenuItem>();

            CreateMap<Database.Order, Models.Order>();
            CreateMap<Database.OrderItem, Models.OrderItem>();

            CreateMap<Database.Category, Models.Category>();

            CreateMap<Database.QRTable, Models.QRTable>();

            CreateMap<Database.Reservation, Models.Reservation>();

        }
    }
}


