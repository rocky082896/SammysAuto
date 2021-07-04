using Prism.Commands;
using SatoImsV1._1.Data;
using SatoImsV1._1.Data.Dtos;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.Repositories.ReceivingRepo;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class ReceivingViewModel : ViewModelBase
    {
        private ObservableCollection<OfficePurchaseOrder> _pOList;
        private ObservableCollection<OfficePurchaseOrderItems> _purchaseOrderItemList;
        private ObservableCollection<POItemDto> _pOItemDto;

        private OfficePurchaseOrder _officePurchaseOrder;
        private OfficePurchaseOrder _selectedOfficePO;

        private PurchaseOrderItems _purchaseOrderItems;
        private POItemDto _selectedItem;

        private double _price;
        private Receiving _receiving;
        private string _pONumber;
        private readonly ReceivingRepository _receivingRepo;
        private readonly Repository _repository;
        private readonly IMSContext _context;

        public DelegateCommand ExportData { get; set; }
        public DelegateCommand CreateReport { get; set; }
        public DelegateCommand ReceiveCommand { get; set; }
        public DelegateCommand SelectionChangedCommand { get; set; }

        public ReceivingViewModel(IMSContext context)
        {
            _receivingRepo = new ReceivingRepository();
            _repository = new Repository();
            _context = context;
            POList = new ObservableCollection<OfficePurchaseOrder>();
            SelectedItem = new POItemDto();
            Receiving = new Receiving();
            FetchPO();
        }

        protected override void RegisterCommands()
        {
            ExportData = new DelegateCommand(Export);
            CreateReport = new DelegateCommand(Create);
            ReceiveCommand = new DelegateCommand(Receive);
            SelectionChangedCommand = new DelegateCommand(SelectionChanged);
        }




        private void Receive()
        {
            if (ValidateFields())
            {
                if (SelectedItem.item_no != null)
                {
                    if (Receiving.rec_qty != SelectedItem.qty)
                    {
                        var result = MessageBox.Show("Receive quantity doesn't match item quantity, " +
                             "Do you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            receiveItem();
                        }
                    }
                    else
                    {
                        receiveItem();
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item");
                }

            }
            else
            {
                MessageBox.Show("Fill up all fields");
            }
        }

        private void Create()
        {
            MessageBox.Show(OfficePurchaseOrder.officeTotalAmount + "");
        }

        private void Export()
        {
        }

        public double Price
        {
            get => _price; set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public string PONumber
        {
            get => _pONumber; set
            {
                _pONumber = value;
                OnPropertyChanged();
            }
        }

        public Receiving Receiving
        {
            get => _receiving; set
            {
                _receiving = value;
                OnPropertyChanged();
            }
        }

        public OfficePurchaseOrder OfficePurchaseOrder
        {
            get => _officePurchaseOrder; set
            {
                _officePurchaseOrder = value;
                OnPropertyChanged();

            }
        }

        public OfficePurchaseOrder SelectedOfficePO
        {
            get => _selectedOfficePO; set
            {
                _selectedOfficePO = value;
                OnPropertyChanged();
                try
                {
                    OfficePurchaseOrder = _selectedOfficePO;
                    PONumber = _selectedOfficePO.officePoNumber;
                    FetchPOItems();
                }
                catch { }
            }
        }

        public ObservableCollection<OfficePurchaseOrder> POList
        {
            get => _pOList; set
            {
                _pOList = value;
                OnPropertyChanged();
            }
        }

        public PurchaseOrderItems PurchaseOrderItems
        {
            get => _purchaseOrderItems; set
            {
                _purchaseOrderItems = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OfficePurchaseOrderItems> PurchaseOrderItemList
        {
            get => _purchaseOrderItemList; set
            {
                _purchaseOrderItemList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<POItemDto> POItemDto
        {
            get => _pOItemDto; set
            {
                _pOItemDto = value;
                OnPropertyChanged();
            }
        }

        public POItemDto SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private void SelectionChanged()
        {
            try
            {
                Price = SelectedItem.price != 0 ? SelectedItem.price : 0;
                Receiving.item_no = SelectedItem.item_no;
            }
            catch { }
        }

        public void FetchPOItems()
        {
            POItemDto = new ObservableCollection<POItemDto>();
            var list = _context.OfficePurchaseOrders
                .Join(_context.OfficePurchaseOrdersItems,
                op => op.officePoNumber,
                opi => opi.officePoNumber,
                (op, opi) => new { op, opi }).Where(a => a.op.status == 1
                && a.opi.status == 1
                && a.op.officePoNumber == SelectedOfficePO.officePoNumber
                && a.opi.officePoNumber == SelectedOfficePO.officePoNumber)
                .Join(_context.ItemMasters,
                    im => im.opi.item_no,
                    p => p.item_no,
                    (im, p) => new POItemDto
                    {
                        imagesource = p.image_src,
                        item_no = im.opi.item_no,
                        item_desc = p.item_desc,
                        amount = im.opi.officeAmount,
                        price = im.opi.officePrice,
                        qty = im.opi.officeQuantity,
                        unit = im.opi.officeUnit
                    });

            foreach (var item in list)
            {
                POItemDto.Add(item);
            }

        }
        public void FetchPO()
        {
            POList = new ObservableCollection<OfficePurchaseOrder>();
            var list = _repository.All<OfficePurchaseOrder>()
                .Select(o => new { o })
                .Where(i => i.o.status == 1);

            foreach (var item in list)
            {
                POList.Add(item.o);
            }
        }

        private async void receiveItem()
        {
            try
            {
                try
                {
                    Receiving.officePoNumber = SelectedOfficePO.officePoNumber;
                    Receiving.date_stored = DateTime.Now;
                    Receiving.price = Price;
                    Receiving.current_qty = Receiving.rec_qty;
                    Receiving.status = 1;
                    Receiving.user_id = "1111";
                    _receivingRepo.AddReceiving(Receiving);
                    _receivingRepo.UpdateOfficeItem(SelectedOfficePO.officePoNumber);
                    
                    if (POItemDto.Count == 1)
                        _receivingRepo.UpdateOffice(SelectedOfficePO.officePoNumber);

                    if (await Task.Run(() => _receivingRepo.TransactItems()) > 0)
                    {
                        FetchPOItems();
                        ClearFields();
                        if (POItemDto.Count < 1)
                            FetchPO();
                    }
                    else
                    {
                        MessageBox.Show("No item saved");
                    }
                }
                catch (DbUpdateException ex) when (ex.InnerException?.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    MessageBox.Show("Duplicate entry");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        private void ClearFields()
        {
            Receiving = new Receiving();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Receiving.serialNo) &&
                Receiving.rec_qty > 0;
        }
    }
}
