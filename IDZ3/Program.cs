using IDZ3.DF.DFCookers;
using IDZ3.DF.DFEquipment;
using IDZ3.DF.DFEquipmentType;
using IDZ3.DF.DFOperations;
using IDZ3.Services.AgentsMailService;
using IDZ3.Services.LoadDataService;
using IDZ3.Services.LogService;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    const string path = "C:\\HSE\\IDZ3\\test_files\\";

    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILogService, LogService>()
            .AddSingleton<IMailService, MailService>()
            .AddScoped<IFsLoadDataService, FsLoadDataService>()
            .BuildServiceProvider();

        IFsLoadDataService fsLoadDataService = serviceProvider.GetService<IFsLoadDataService>();

        // Install operation_types
        KitchenOperationTypes kitchenOperationTypes = fsLoadDataService
            .LoadDataAsync<KitchenOperationTypes>( path + "operation_types.txt" ).Result;
        DFKithenOperationTypes dFKithenOperationTypes = new DFKithenOperationTypes( kitchenOperationTypes );

        // Install cookers
        CookersList cookersList = fsLoadDataService.LoadDataAsync<CookersList>( path + "cookers.txt" ).Result;
        DFCookers dFCookers = new DFCookers( cookersList );

        // Install equipment
        EquipmentList equipmentList = fsLoadDataService.LoadDataAsync<EquipmentList>( path + "equipment.txt" ).Result;
        DFEquipment dFEquipment = new DFEquipment( equipmentList );

        // Install equipment_type
        EquipmentTypeList equipmentTypeList = fsLoadDataService.LoadDataAsync<EquipmentTypeList>( path + "equipment_type.txt" ).Result;
        DFEquipmentType dFEquipmentType = new DFEquipmentType( equipmentTypeList );

        Console.WriteLine( "test" );
    }
}