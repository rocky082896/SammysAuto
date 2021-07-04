using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestService
{
    class MasterFileGenerator
    {
        private CSVHelper helper;
        public void GenerateStorLoc()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeStorageLoc(), "PG/Master file/storage_location");
        }
        public void GenerateMachine()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/machine");
        }
        public void GenerateSalesman()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeSalesman(), "PG/Master file/salesman");
        }
        public void GenerateDiscountMas()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeDiscountMaster(), "PG/Master file/discount_master");
        }
        public void GenerateDetails()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/customer_details");
        }
        public void GenerateCustomer()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/customer");
        }
        public void GenerateDiscountList()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/discount_list");
        }
        public void GenerateBrand()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/brand");
        }
        public void GenerateMaterial()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/material");
        }
        public void GeneratePriceList()
        {
            helper = new CSVHelper();
            helper.DataTableToCsv(ExporterClass.InitializeMachineMaster(), "PG/Master file/price_list");
        }
    }
}
