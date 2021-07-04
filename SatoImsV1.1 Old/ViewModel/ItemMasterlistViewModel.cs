using Prism.Commands;
using SatoImsV1._1.Data;
using SatoImsV1._1.Data.Dtos;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace SatoImsV1._1.ViewModel
{
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


        protected override void RegisterCommands()
        {
            FilterDataGrid = new DelegateCommand(FetchDatagridItems);
            SearchDataGrid = new DelegateCommand(SearchDatagridItems);
            AgingCommand = new DelegateCommand(Aging);
            PlusCommand = new DelegateCommand(Plus);
            MinusCommand = new DelegateCommand(Minus);
        }

        private void Aging()
        {
            FetchItems();
        }

        protected override void RegisterCollections()
        {
            _context = new IMSContext();
            _repository = new Repository();

            NumberOfData = 10;
            FromDay = 0;
            ToDay = 1000;

            SearchItem = string.Empty;

            FetchSuppliers();
            FetchItems();
        }

        private void SearchDatagridItems()
        {
            FetchItems();
        }

        private void Minus()
        {
            NumberOfData -= 10;
        }

        private void Plus()
        {
            NumberOfData += 10;
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

        private void FetchSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>();
            var sup = _repository.All<Supplier>(x => x.status == 1).ToList();
            foreach (var item in sup)
            {
                Suppliers.Add(item);
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
        }
    }
}
