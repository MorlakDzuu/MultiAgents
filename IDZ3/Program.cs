using IDZ3;
using IDZ3.Agents.Admin;
using IDZ3.Agents.Visitor;
using IDZ3.DFs.DFCookers;
using IDZ3.DFs.DFDishCards;
using IDZ3.DFs.DFEquipment;
using IDZ3.DFs.DFEquipmentType;
using IDZ3.DFs.DFMenu;
using IDZ3.DFs.DFOperations;
using IDZ3.DFs.DFProducts;
using IDZ3.DFs.DFProductTypes;
using IDZ3.DFs.DFVisitors;
using IDZ3.Services.AgentFabric;
using IDZ3.Services.LoadDataService;
using IDZ3.Services.SourceLogService;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void LoadFiles( string pathInput )
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IFsLoadDataService, FsLoadDataService>()
            .BuildServiceProvider();

        IFsLoadDataService fsLoadDataService = serviceProvider.GetService<IFsLoadDataService>();

        // Install operation_types
        OperationList kitchenOperationTypes = fsLoadDataService.LoadDataAsync<OperationList>( pathInput + "operation_types.txt" ).Result;
        DFOperation.SetValue( kitchenOperationTypes );

        // Install cookers
        CookersList cookersList = fsLoadDataService.LoadDataAsync<CookersList>( pathInput + "cookers.txt" ).Result;
        DFCookers.SetValue( cookersList );

        // Install equipment
        EquipmentList equipmentList = fsLoadDataService.LoadDataAsync<EquipmentList>( pathInput + "equipment.txt" ).Result;
        DFEquipment.SetValue( equipmentList );

        // Install equipment_type
        EquipmentTypeList equipmentTypeList = fsLoadDataService.LoadDataAsync<EquipmentTypeList>( pathInput + "equipment_type.txt" ).Result;
        DFEquipmentType.SetValue( equipmentTypeList );

        // Install products
        ProductList productList = fsLoadDataService.LoadDataAsync<ProductList>( pathInput + "products.txt" ).Result;
        DFProducts.SetValue( productList );

        // Install products_types
        ProductTypeList productTypeList = fsLoadDataService.LoadDataAsync<ProductTypeList>( pathInput + "product_types.txt" ).Result;
        DFProductTypes.SetValue( productTypeList );

        // Install dish_cards
        DishCardList dishCardList = fsLoadDataService.LoadDataAsync<DishCardList>( pathInput + "dish_cards.txt" ).Result;
        DFDishCards.SetValue( dishCardList );

        // Install menu_dishes
        Menu menu = fsLoadDataService.LoadDataAsync<Menu>( pathInput + "menu_dishes.txt" ).Result;
        DFMenu.SetValue( menu );

        // Install visitors_orders
        VisitorOrderList visitorOrderList = fsLoadDataService.LoadDataAsync<VisitorOrderList>( pathInput + "visitors_orders.txt" ).Result;
        DFVisitors.SetValue( visitorOrderList );

        Console.WriteLine();
    }
    
    public static void Main()
    {
        const string pathInput = "C:\\HSE\\IDZ3\\test_files\\";
        const string pathOutput = "C:\\HSE\\IDZ3\\output\\";

        try
        {
            LoadFiles( pathInput );
        } 
        catch( Exception ex ) {
            LogService.Instance().LogError( $"Can't load files, exception: {ex.Message}" );
        }


        CafeBuilder cafeBuilder = new CafeBuilder( pathOutput );
        cafeBuilder.VisitorGroupsNumber = 2;
        cafeBuilder.Interval = 2000;
        cafeBuilder.AdminExistsTime = 20000;

        cafeBuilder.BuildAdmin();
        cafeBuilder.BuildVisiors();

        try
        {
            LogService.Instance().StoreLogs( pathOutput + "logs.txt" );
        } 
        catch( Exception ex )
        {
            Console.WriteLine($"Exception while store logs: {ex.Message}");
        }
    }
}