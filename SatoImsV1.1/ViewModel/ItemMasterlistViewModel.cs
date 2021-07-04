using Prism.Commands;
using SatoImsV1._1.Data;
using SatoImsV1._1.Data.Dtos;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace SatoImsV1._1.ViewModel
{
    public class Currency
    {
       public string currency { get; set; }
    }

    public class ItemMasterlistViewModel : ViewModelBase
    {
        private ObservableCollection<InventoryDto> _inventoryDtoList;

        private IMSContext _context;
        private Repository _repository;
        private InventoryDto _inventoryDto;

        private int _numberOfData;
        private string _searchItem;
        private ObservableCollection<Supplier> _suppliers;
        private Supplier _selectedSupplier;
        private int _fromDay;
        private int _toDay;

        public DelegateCommand FilterDataGrid { get; set; }
        public DelegateCommand SearchDataGrid { get; set; }
        public DelegateCommand AgingCommand { get; set; }
        public DelegateCommand PlusCommand { get; set; }
        public DelegateCommand MinusCommand { get; set; }
        public DelegateCommand SelectionChangeCmd { get; set; }

        protected override void RegisterCommands()
        {
            FilterDataGrid = new DelegateCommand(FetchDatagridItems);
            SearchDataGrid = new DelegateCommand(SearchDatagridItems);
            AgingCommand = new DelegateCommand(Aging);
            PlusCommand = new DelegateCommand(Plus);
            MinusCommand = new DelegateCommand(Minus);
            SelectionChangeCmd = new DelegateCommand(getSummaryAmount);
        }

        private void Aging()
        {
            FetchItems();
        }
        protected override void RegisterCollections()
        {
            _context = new IMSContext();
            _repository = new Repository();
           
            NumberOfData = 5;
            FromDay = 0;
            ToDay = 1000;

            SearchItem = string.Empty;

            FetchSuppliers();
            FetchItems();
            FetchCurrency();
        }

        private void SearchDatagridItems()
        {
            FetchItems();
        }

        private void Minus()
        {
            NumberOfData -= 5;
        }

        private void Plus()
        {
            NumberOfData += 5;
        }

        public string SearchItem
        {
            get => _searchItem; set
            {
                _searchItem = value;
                OnPropertyChanged();
            }
        }


        private void FetchDatagridItems()
        {
            FetchItems();
        }


        public int NumberOfData
        {
            get => _numberOfData; set
            {
                _numberOfData = value;
                if (_numberOfData < 0)
                    _numberOfData = 0;

                OnPropertyChanged();
            }
        }

        public int FromDay
        {
            get => _fromDay; set
            {
                _fromDay = value;
                OnPropertyChanged();
            }
        }

        public int ToDay
        {
            get => _toDay; set
            {
                _toDay = value;
                OnPropertyChanged();
            }
        }

        public InventoryDto InventoryDto
        {
            get => _inventoryDto; set
            {
                _inventoryDto = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<InventoryDto> InventoryDtoList
        {
            get => _inventoryDtoList; set
            {
                _inventoryDtoList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Supplier> Suppliers
        {
            get => _suppliers; set
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }

        public Supplier SelectedSupplier
        {
            get => _selectedSupplier; set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Currency> _currencyItem;
        public ObservableCollection<Currency> CurrencyItem
        {
            get { return _currencyItem; }
            set { _currencyItem = value; OnPropertyChanged(); }
        }

        private Currency _selectedCurrency = new Currency();
        public Currency selectedCurrency
        {
            get { return _selectedCurrency; }
            set { _selectedCurrency = value;OnPropertyChanged(); }

        }

        private double _summaryAmount;
        public double SummaryAmount
        {
            get { return _summaryAmount; }
            set { _summaryAmount = value; OnPropertyChanged(nameof(SummaryAmount)); }
        }

        private void FetchSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>();
            var sup = _repository.All<Supplier>(x => x.status == 1).ToList();
            foreach (var item in sup)
            {
                Suppliers.Add(item);
            }
        }


        private double _phpAmount;
        public double PhpAmount
        {

            get { return _phpAmount; }
            set { _phpAmount = value; OnPropertyChanged("PhpAmount"); }
        }

        private double _usdAmount;
        public double UsdAmount
        {
            get { return _usdAmount; }
            set { _usdAmount = value; OnPropertyChanged("UsdAmount"); }
        }
        private void getSummaryAmount()
        {
            UsdAmount = InventoryDtoList.Where(x => x.currency == "USD").Sum(s => s.total_amount);
            PhpAmount = InventoryDtoList.Where(x => x.currency == "PHP").Sum(s => s.total_amount);
            if (selectedCurrency.currency == "PHP")
            {
                //double _conversion = 56.00;
                double _conversion = Convert.ToDouble(Properties.Settings.Default["Forex"].ToString());
                double temp = InventoryDtoList.Where(x => x.currency == "USD").Sum(s => s.total_amount);
                double UsdConverion = temp * _conversion;

                SummaryAmount = InventoryDtoList.Where(x => x.currency == "PHP").Sum(s => s.total_amount);
                SummaryAmount += UsdConverion;
                //MessageBox.Show(Properties.Settings.Default["Forex"].ToString());
            }
            else if (selectedCurrency.currency == "USD")
            {
                //double _conversion = 56.00;
                //double tempAmount = 0.00;
                SummaryAmount = InventoryDtoList.Where(x => x.currency == "USD").Sum(s => s.total_amount);
                //SummaryAmount = tempAmount * _conversion;
                //MessageBox.Show(Properties.Settings.Default["Forex"].ToString());
            }
        }
        private void FetchCurrency()
        {
            CurrencyItem = new ObservableCollection<Currency>();
            //var itemCol = InventoryDtoList.Select(ci => new Currency { currency = ci.currency}).Distinct();
            var itemCol = _repository.All<ItemMaster>().Select(ci => new Currency { currency = ci.currency}).Distinct().ToList();
            Console.WriteLine(itemCol);
            foreach (var item in itemCol)
            {
                //Console.WriteLine(item);
                CurrencyItem.Add(item);
            }
        }
        private void FetchItems()
        {
            InventoryDtoList = new ObservableCollection<InventoryDto>();
            var inventoryList = _context.Receiving
                .Join(_context.ItemMasters, r => r.item_no, im => im.item_no,
                (r, im) => new { r, im }).Where(x => x.r.status == 1 && x.im.status == 1)
                .Join(_context.Categories, m => m.im.cat_id, c => c.Id,
                (m, c) => new { m, c })
                .Join(_context.ItemGroups, fm => fm.m.im.group_id, g => g.Id,
                (fm, g) => new InventoryDto
                {
                    poNumber = fm.m.r.officePoNumber,
                    item_no = fm.m.im.item_no,
                    item_desc = fm.m.im.item_desc,
                    serial_no = fm.m.r.serialNo,
                    category = fm.c.cat_desc,
                    group = g.group_name,
                    uom = fm.m.im.uom,
                    currency = fm.m.im.currency,
                    image_source = fm.m.im.image_src,
                    qty_in = fm.m.r.rec_qty,
                    current_qty = fm.m.r.current_qty,
                    amount = fm.m.r.price,
                    total_amount = (fm.m.r.price * fm.m.r.current_qty),
                    date_received = fm.m.r.date_stored,
                    aging = (int)SqlFunctions.DateDiff("day", fm.m.r.date_stored, DateTime.Now) //System.Data.Entity.DbFunctions(fm.m.r.date_stored, DateTime.Now) 

                })
                .Where(x => x.item_no.Contains(SearchItem)
                
                || x.item_desc.Contains(SearchItem))
                .Where(x => x.aging >= FromDay && x.aging <= ToDay)
                .OrderByDescending(x => x.date_received).Take(NumberOfData).ToList();

            foreach (var item in inventoryList)
            {
                //value = DateTime.Now.Subtract(item.date_received);
                //item.aging = (int)value.TotalDays;
                InventoryDtoList.Add(item);
            }
            getSummaryAmount();
           
        }
    }
}
