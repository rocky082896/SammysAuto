using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMasterDataGenerator
{
    class MasterFileGenerator
    {
        private CSVHelper helper;
        public void GenerateStorLoc()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeStorageLoc(), @"D:\MASTER DATA\storage_location");
        }
        public void GenerateMachine()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), @"D:\MASTER DATA\machine");
        }
        public void GenerateSalesman()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeSalesman(), @"D:\MASTER DATA\salesman");
        }
        public void GenerateDiscountMas()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeDiscountMaster(), @"D:\MASTER DATA\discount_master");
        }
        public void GenerateDetails()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeDetails(), @"D:\MASTER DATA\customer_details");
        }
        public void GenerateCustomer()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeCustomer(), @"D:\MASTER DATA\customer");
        }
        public void GenerateDiscountList()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeDiscountList(), @"D:\MASTER DATA\discount_list");
        }
        public void GenerateBrand()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeBrand(), @"D:\MASTER DATA\brand");
        }
        public void GenerateMaterial()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMaterial(), @"D:MASTER DATA\material");
        }
        public void GeneratePriceList()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializePriceList(), @"D:\MASTER DATA\price_list");
        }

        public void GenerateConversion()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeConversion(), @"D:\MASTER DATA\conversion");
        }
    }
}
