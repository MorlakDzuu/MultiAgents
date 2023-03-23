using IDZ3.DFs.DFCookers;
using IDZ3.DFs.DFDishCards;
using IDZ3.DFs.DFEquipment;
using IDZ3.DFs.DFEquipmentType;
using IDZ3.DFs.DFMenu;
using IDZ3.DFs.DFOperations;
using IDZ3.DFs.DFProducts;
using IDZ3.DFs.DFProductTypes;
using IDZ3.DFs.DFVisitors;
using IDZ3.Services.LoadDataService;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    const string path = "C:\\HSE\\IDZ3\\test_files\\";

    public static void LoadFiles()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IFsLoadDataService, FsLoadDataService>()
            .BuildServiceProvider();

        IFsLoadDataService fsLoadDataService = serviceProvider.GetService<IFsLoadDataService>();

        // Install operation_types
        OperationList kitchenOperationTypes = fsLoadDataService.LoadDataAsync<OperationList>( path + "operation_types.txt" ).Result;
        DFOperation.SetValue( kitchenOperationTypes );

        // Install cookers
        CookersList cookersList = fsLoadDataService.LoadDataAsync<CookersList>( path + "cookers.txt" ).Result;
        DFCookers.SetValue( cookersList );

        // Install equipment
        EquipmentList equipmentList = fsLoadDataService.LoadDataAsync<EquipmentList>( path + "equipment.txt" ).Result;
        DFEquipment.SetValue( equipmentList );

        // Install equipment_type
        EquipmentTypeList equipmentTypeList = fsLoadDataService.LoadDataAsync<EquipmentTypeList>( path + "equipment_type.txt" ).Result;
        DFEquipmentType.SetValue( equipmentTypeList );

        // Install products
        ProductList productList = fsLoadDataService.LoadDataAsync<ProductList>( path + "products.txt" ).Result;
        DFProducts.SetValue( productList );

        // Install products_types
        ProductTypeList productTypeList = fsLoadDataService.LoadDataAsync<ProductTypeList>( path + "product_types.txt" ).Result;
        DFProductTypes.SetValue( productTypeList );

        // Install dish_cards
        DishCardList dishCardList = fsLoadDataService.LoadDataAsync<DishCardList>( path + "dish_cards.txt" ).Result;
        DFDishCards.SetValue( dishCardList );

        // Install menu_dishes
        Menu menu = fsLoadDataService.LoadDataAsync<Menu>( path + "menu_dishes.txt" ).Result;
        DFMenu.SetValue( menu );

        // Install visitors_orders
        VisitorOrderList visitorOrderList = fsLoadDataService.LoadDataAsync<VisitorOrderList>( path + "visitors_orders.txt" ).Result;
        DFVisitors.SetValue( visitorOrderList );

        Console.WriteLine();
    }
    
    public static void Main()
    {
        //AgentFabric.AdminAgentCreate();

        LoadFiles();

        Thread.Sleep( 5000 );
        //LogService.Instance().WriteLogs();
    }
}