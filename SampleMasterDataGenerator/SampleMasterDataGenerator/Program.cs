using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMasterDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterFileGenerator master = new MasterFileGenerator();
            master.GenerateBrand();
            master.GenerateCustomer();
            master.GenerateDetails();
            master.GenerateDiscountList();
            master.GenerateDiscountMas();
            master.GenerateMachine();
            master.GenerateSalesman();
            master.GenerateConversion();
            master.GenerateMaterial();
            master.GeneratePriceList();
            master.GenerateStorLoc();
        }
    }
}
