using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Models.Request.MenuItem;
using SPSP.Services.RecommenderService;

namespace SPSP.Services.MenuItem
{

    // dali menuitem service uopste treba naslijediti IMenuItemService, on vec nasljeduje IMenuItemService
    //prvo cu probat MenuItemService ovako pa cu onda zakomentarisats
    public class MenuItemService : BaseCRUDService<Models.MenuItem, Database.MenuItem, MenuItemSearchObject, MenuItemCreateRequest, MenuItemUpdateRequest>, IMenuItemService
    {
        //protected readonly IRecommenderService recommenderService;
        public MenuItemService(DataDbContext context, IMapper mapper, IRecommenderService recommenderService)
            : base(context, mapper)
        {
            //obzirom da smo pozvali base() ne potrebno je ovdje dodavati i deklarisati context i mapper i funkcije
            //this.recommenderService = recommenderService;
    }

        public override async Task PrepareBeforeCreate(Database.MenuItem entity, MenuItemCreateRequest insert)
        {
            //URADI NESTO SPECIFICNO ZA MENU ITEM SERVICE NPR DODAJ U ATRIBUT
        }

        public override async Task<Models.MenuItem> GetById(int id)
        {
            var entity = await context.Set<Database.MenuItem>()
                .Include(x => x.Category)
                .Include(x => x.Menu)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = mapper.Map<Models.MenuItem>(entity);

            return model;
        }

        public override IQueryable<Database.MenuItem> AddFilter(IQueryable<Database.MenuItem> query, MenuItemSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                query = query.Where(x => x.Name.StartsWith(search.Name));
            }

            if (!string.IsNullOrWhiteSpace(search?.FTS))
            {
                query = query.Where(x => x.Name.Contains(search.FTS) || x.Code.Contains(search.FTS) || x.Description.Contains(search.FTS));
            }

            return base.AddFilter(query, search);
        }

        public override IQueryable<Database.MenuItem> AddInclude(IQueryable<Database.MenuItem> query, MenuItemSearchObject search = null)
        {
            if (search.IsCategoryIncluded == true)
            {
                query = query.Include(x => x.Category);
            }
            if (search.IsMenuIncluded == true)
            {
                query = query.Include(x => x.Menu);
            }

            return base.AddInclude(query, search);
        }


        //recommender:

        //static MLContext mlContext = null;
        //static object isLocked = new object();
        //static ITransformer model = null;

        //public List<Models.MenuItem> Recommend(int id)
        //{
        //    lock (isLocked)
        //    {
        //        if (mlContext == null)
        //        {
        //            mlContext = new MLContext();

        //            var tmpData = context.Orders.Include("OrderItems").ToList();

        //            var data = new List<ProductEntry>();


        //            foreach (var x in tmpData)
        //            {
        //                if (x.OrderItems.Count > 1)
        //                {
        //                    var distinctItemId = x.OrderItems.Select(y => y.MenuItemId).ToList();

        //                    distinctItemId.ForEach(y =>
        //                    {
        //                        var relatedItems = x.OrderItems.Where(z => z.MenuItemId != y);

        //                        foreach (var z in relatedItems)
        //                        {
        //                            data.Add(new ProductEntry()
        //                            {
        //                                ProductID = (uint)y,
        //                                CoPurchaseProductID = (uint)z.MenuItemId,
        //                            });
        //                        }
        //                    });
        //                }
        //            }

        //            var traindata = mlContext.Data.LoadFromEnumerable(data);

        //            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
        //            options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
        //            options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
        //            options.LabelColumnName = "Label";
        //            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
        //            options.Alpha = 0.01;
        //            options.Lambda = 0.025;
        //            options.NumberOfIterations = 100;
        //            options.C = 0.00001;

        //            var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

        //            model = est.Fit(traindata);

        //            //treba snimiti model
        //        }
        //    }

        //    var menuItems = context.MenuItems.Where(x => x.Id != id);

        //    var predictionResult = new List<Tuple<Database.MenuItem, float>>();

        //    foreach (var menuItem in menuItems)
        //    {

        //        var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
        //        var prediction = predictionengine.Predict(
        //                                 new ProductEntry()
        //                                 {
        //                                     ProductID = (uint)id,
        //                                     CoPurchaseProductID = (uint)menuItem.Id
        //                                 });


        //        predictionResult.Add(new Tuple<Database.MenuItem, float>(menuItem, prediction.Score));
        //    }


        //    var finalResult = predictionResult.OrderByDescending(x => x.Item2).Select(x => x.Item1).Take(2).ToList();

        //    //treba snimiti predictionResult za sve menuItems

        //    return mapper.Map<List<Models.MenuItem>>(finalResult);

        //}
    }


}

