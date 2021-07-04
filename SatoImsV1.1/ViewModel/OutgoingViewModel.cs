using Prism.Commands;
using SatoImsV1._1.Data;
using SatoImsV1._1.Data.Dtos;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.Repositories.OutgoingRepo;
using SatoImsV1._1.View;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class OutgoingViewModel: ViewModelBase
    {
        private Outgoing _outgoing;
        private OutgoingDetails _outgoingDetails;

        private Receiving _receiving;
        private Receiving _selectedItem;

        private Supplier _supplier;
        private Supplier _selectedSupplier;

        private OutgoingDataDto _outgoingdto;

        private ObservableCollection<Receiving> _receivingdatas;
        private ObservableCollection<Receiving> _itemno;
        private ObservableCollection<Supplier> _supplierList;
        private ObservableCollection<OutgoingDataDto> _outgoingdatadto;
        private ObservableCollection<DistinctPo> _ObjDistinctPo;


        private string series;
        private int t_amount;
        private string currency;
        
        private Repository _repository;
        private OutgoingRepository _outgoingrepo;
        
        public DelegateCommand createNewSeries { get; set; }
        public DelegateCommand SaveOutgoing { get; set; }
        public DelegateCommand AddItem { get; set; }
        public DelegateCommand RemoveItem { get; set; }
        public DelegateCommand DisplayPoReport { get; set; }
        private  IMSContext _context;

        public OutgoingViewModel(IMSContext context)
        {
            _context = context;
            Outgoing = new Outgoing();
            OutgoingDetails = new OutgoingDetails();
            Supplier = new Supplier();
            getOutgoingDatas = new ObservableCollection<OutgoingDataDto>();
            getReceivingDatas = new ObservableCollection<Receiving>();
            ObjDistinctPO = new ObservableCollection<DistinctPo>();
        }

        protected override void RegisterCommands()
        {
            createNewSeries = new DelegateCommand(GetOutgoingSeries);
            SaveOutgoing = new DelegateCommand(InsertOutgoings);
            AddItem = new DelegateCommand(AddOutgoingItems);
            RemoveItem = new DelegateCommand(SelectionOutgoingChanged);
            DisplayPoReport = new DelegateCommand(showPoReport);
        }

        protected override void RegisterCollections()
        {
            _context = new IMSContext();
            _repository = new Repository();
            _outgoingrepo = new OutgoingRepository();
            FetchSupplierList();
            isFieldEnable = false;
            IsButtonNewEnable = true;
            IsButtonSaveEnable = false;
        }

        #region Object Properties
        public Outgoing Outgoing
        {
            get { return _outgoing; }
            set { _outgoing = value; OnPropertyChanged("Outgoing"); }
        }

        public OutgoingDetails OutgoingDetails
        {
            get { return _outgoingDetails; }
            set { _outgoingDetails = value; OnPropertyChanged("OutgoingDetail"); }
        }

        public Receiving Receiving
        {
            get { return _receiving; }
            set { _receiving = value; OnPropertyChanged(); }
        }

        public Receiving SelectedItemPo
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                Receiving = _selectedItem;
            }
        }

        private DistinctPo _distinct = new DistinctPo();
        public DistinctPo DistinctPo
        {
            get { return _distinct; }
            set { _distinct = value; OnPropertyChanged("DistinctPo"); }
        }
        private DistinctPo _selectedDistinctPo;
        public DistinctPo SelectedDistinctPo
        {
            get { return _selectedDistinctPo; }
            set { _selectedDistinctPo = value; OnPropertyChanged("SelectedDistinctPo");
                DistinctPo = _selectedDistinctPo;
            }
        }


        public Supplier Supplier
        {
            get { return _supplier; }
            set { _supplier = value; OnPropertyChanged("Supplier"); }
        }
        public Supplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set { _selectedSupplier = value; OnPropertyChanged("SelectedSupplier");
                Supplier = _selectedSupplier;
            }
        }
        public OutgoingDataDto OutgoingDataDto
        {
            get { return _outgoingdto; }
            set { _outgoingdto = value; OnPropertyChanged(); }
        }

        private OutgoingDataDto _selectedOutgoingDataDto;
        public OutgoingDataDto SelectedOutgoingDataDto
        {
            get { return _selectedOutgoingDataDto; }
            set { _selectedOutgoingDataDto = value; OnPropertyChanged("SelectedOutgoingDataDto"); }
        }

        public string Series
        {
            get { return series; }
            set { series = value; OnPropertyChanged("Series");
               }
        }

        public int total_Amount
        {
            get { return t_amount; }
            set { t_amount = value;
                    
                    ItemAmount = Receiving.price * t_amount;
                    OnPropertyChanged(nameof(total_Amount));
            }
        }

        private double _itemAmount;
        public double ItemAmount
        {
            get { return _itemAmount; }
            set { _itemAmount = value; OnPropertyChanged("ItemAmount"); }
        }

        private double _totalItemQty;
        public double TotalItemQty
        {
            get { return _totalItemQty; }
            set { _totalItemQty = value; OnPropertyChanged("TotalItemQty"); }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; OnPropertyChanged("Currency"); }
        }


        #region Enable/Disable
        private bool _isFieldEnable;
        public bool isFieldEnable
        {
            get {
               
                return _isFieldEnable; }
            set {

                if (_isFieldEnable != value)
                {
                    _isFieldEnable = value;
                    OnPropertyChanged(nameof(isFieldEnable));
                };
               
            }
        }

        private bool _isButtonNewEnable;
        public bool IsButtonNewEnable
        {
            get { return _isButtonNewEnable; }
            set 
            { 
                if(_isButtonNewEnable != value)
                {
                    _isButtonNewEnable = value; OnPropertyChanged(nameof(IsButtonNewEnable));
                }
            }
        }
        private bool _isButtonSaveEnable;
        public bool IsButtonSaveEnable
        {

            get { return _isButtonSaveEnable; }
            set { _isButtonSaveEnable = value; OnPropertyChanged("IsButtonSaveEnable"); }
        }

        #endregion

        #endregion

        #region Observable Collections
        private ObservableCollection<OutgoingDetails> _OutgoingDetailsList;
        public ObservableCollection<OutgoingDetails> OutgoingDetailsList
        {
            get { return _OutgoingDetailsList; }
            set { _OutgoingDetailsList = value; OnPropertyChanged("OutgoingDetailsList"); }
        }

        //getAll Data in Receiving Table

        public ObservableCollection<Receiving> getReceivingDatas
        {
            get { return _receivingdatas; }
            set { _receivingdatas = value; OnPropertyChanged("getRecievingDatas"); }
        }

        //get The Item Number
        public ObservableCollection<Receiving> getPoItem
        {
            get { return _itemno; }
            set { _itemno = value; OnPropertyChanged(nameof(getPoItem)); }
        }

        //get the Supplier or Customer

        public ObservableCollection<Supplier> getSupplierList
        {
            get { return _supplierList; }
            set { _supplierList = value; OnPropertyChanged("SupplierList"); }
        }

        //get List Of Outgoing from OutgoingDto

        public ObservableCollection<OutgoingDataDto> getOutgoingDatas
        {
            get { return _outgoingdatadto; }
            set { _outgoingdatadto = value; OnPropertyChanged("getOutgoingDatas"); }
        }

        public ObservableCollection<DistinctPo> ObjDistinctPO
        {
            get { return _ObjDistinctPo; }
            set { _ObjDistinctPo = value; OnPropertyChanged("ObjDistinctPO"); }
        }

        #endregion

        #region Process Related
        

       
        //public void showInvoice()
        //{

        //    new InvoiceReportView(this).ShowDialog();

        //}

        public void showPoReport()
        {
            new PoReportView(this).ShowDialog();
        }

        public void FetchPOItem()
        {
           
            

            getPoItem = new ObservableCollection<Receiving>();

            var list = _repository.All<Receiving>().Select(i => new { i });

            foreach (var item in list)
            {
                getPoItem.Add(item.i);
            }
        }
        public void FetchSupplierList()
        {
            getSupplierList = new ObservableCollection<Supplier>();

           
            var list = _repository.All<Supplier>().Select(i => new { i });

            foreach (var item in list)
            {
                getSupplierList.Add(item.i);
            }
        }
        public void fetchOutgoing()
        {
            getOutgoingDatas = new ObservableCollection<OutgoingDataDto>();

            var list = _context.Outgoings.Join(_context.OutgoingDetails, o => o.Outgoing_No, od => od.Outgoing_No_Details,
                (o, od) => new OutgoingDataDto
                {
                    outgoingSeries = o.Outgoing_No,
                    outgoingItemQty = od.Item_Qty,
                    currency = o.Currency,
                    //ponumber = od.Outgoing_PoNo,
                    itemnumber = od.Item_No,
                    receivingid = od.Receiving_ID,
                    price = od.Price,
                    itemamount = od.Item_Amount,
                    customer = od.customer,
                    deliverydate = od.deliverydate
                }) ;
            
            foreach (var item in list)
            {
                getOutgoingDatas.Add(item);
                Console.WriteLine(item.deliverydate.ToString());
            }
        }

        public void GetOutgoingSeries()
        {

            FetchPOItem();

            try
            {
                isFieldEnable = true;
                IsButtonNewEnable = false;
                IsButtonSaveEnable = true;
                Currency = "USD";
                String d = DateTime.Now.Year.ToString();
                String seriesPrefix = "O-";
                String temp_Series = "";
                String zeroSeries = "";
                int maxSeries = _context.Outgoings.Max(mx => (int?)mx.REC_ID) ?? 0;

                int maxSeriesPlusOne = maxSeries + 1;


                if (maxSeriesPlusOne >= 0 && maxSeriesPlusOne <= 9)
                {
                    zeroSeries = "000";
                    temp_Series = seriesPrefix + "" + d + "-" + zeroSeries + maxSeriesPlusOne;
                }
                else if (maxSeriesPlusOne >= 10 && maxSeriesPlusOne <= 99)
                {
                    zeroSeries = "00";
                    temp_Series = seriesPrefix + "" + d + "-" + zeroSeries + maxSeriesPlusOne;
                }
                else if (maxSeriesPlusOne >= 100 && maxSeriesPlusOne <= 999)
                {
                    zeroSeries = "0";
                    temp_Series = seriesPrefix + "" + d + "-" + zeroSeries + maxSeriesPlusOne;
                }
                else
                {
                    temp_Series = seriesPrefix + "" + d + "-" + maxSeriesPlusOne;
                }

                Series = temp_Series;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        public async void InsertOutgoings()
        {

            if(ValidateFields())
            {
                try
                {
                    OutgoingDetailsList = new ObservableCollection<OutgoingDetails>();

                    foreach (var item in getOutgoingDatas)
                    {
                        OutgoingDetailsList.Add(new OutgoingDetails
                        {

                            Receiving_ID = item.receivingid,
                            Outgoing_No_Details = item.outgoingSeries,
                            //Outgoing_PoNo = item.ponumber,
                            Item_No = item.itemnumber,
                            Price = item.price,
                            Item_Amount = item.itemamount,
                            Item_Qty = item.outgoingItemQty,
                            Item_Status = 1,
                            customer = item.customer,
                            deliverydate = item.deliverydate,
                            Current_Qty = 0
                            

                        });
                    }

                    string d = "1900-01-01";

                    Outgoing.Outgoing_No = Series;
                    Outgoing.Outgoing_Status = "1";
                    Outgoing.Total_Item_Amount = TotalItemQty;
                    Outgoing.Currency = Currency;
                    Outgoing.Completion_Date = DateTime.Parse(d);

                    _context.Outgoings.Add(Outgoing);
                    _context.OutgoingDetails.AddRange(OutgoingDetailsList);

                    if (await Task.Run(() => _outgoingrepo.TransactItemsAsync(Outgoing, OutgoingDetailsList)))
                        ClearFields();
                    else
                        MessageBox.Show("An error occured");

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please Complete Fields");
            }
        }
        public void AddOutgoingItems()
        {

            bool isExisting = getOutgoingDatas.Any(o => o.itemnumber == Receiving.item_no);

            if(total_Amount > 0 || string.IsNullOrWhiteSpace(total_Amount.ToString()) && string.IsNullOrWhiteSpace(DistinctPo.PoNumber.ToString()) 
                && string.IsNullOrWhiteSpace(Receiving.item_no.ToString()) && string.IsNullOrWhiteSpace(Supplier.supplier_name.ToString()))
            {
                if (!isExisting)
                {
                    getOutgoingDatas.Add(new OutgoingDataDto
                    {
                        outgoingSeries = Series,
                        //status_header = 1,
                        outgoingItemQty = total_Amount,
                        currency = Currency,
                        //ponumber = DistinctPo.PoNumber,
                        itemnumber = Receiving.item_no,
                        receivingid = Receiving.Id.ToString(),
                        price = Receiving.price,
                        itemamount = ItemAmount,
                        customer = Supplier.supplier_name,
                        //status_details = 1,
                        deliverydate = OutgoingDetails.deliverydate

                    });
                }
                else
                {
                    int itemqty = 0;

                    foreach (var item in getOutgoingDatas.Where(o => o.itemnumber == Receiving.item_no))
                    {
                        itemqty += item.outgoingItemQty;
                       
                    }

                    //getOutgoingDatas.Remove(getOutgoingDatas.Where(x => x.itemnumber == Receiving.item_no).Single());

                    getOutgoingDatas.Add(new OutgoingDataDto
                    {
                        outgoingSeries = Series,

                        outgoingItemQty = itemqty,
                        currency = Currency,
                        //ponumber = DistinctPo.PoNumber,
                        itemnumber = Receiving.item_no,
                        receivingid = Receiving.Id.ToString(),
                        price = Receiving.price,
                        itemamount = ItemAmount,
                        customer = Supplier.supplier_name,

                    });
                }
                TotalItemQty = getOutgoingDatas.Sum(x => x.itemamount);

                if(getOutgoingDatas.Count > 0)
                {
                    IsButtonSaveEnable = true;
                }
                else
                {
                    IsButtonSaveEnable = false;
                }
            }
            else
            {
                MessageBox.Show("Item Quantity Must Be greater than 0");
            }
        }
        private void SelectionOutgoingChanged()
        {
            if (SelectedOutgoingDataDto != null)
            {
                var itemToRemove = getOutgoingDatas.Where(x => x.itemnumber == SelectedOutgoingDataDto.itemnumber).ToList();
                foreach (var item in itemToRemove)
                {
                    getOutgoingDatas.Remove(item);
                }

                TotalItemQty = getOutgoingDatas.Sum(x => x.itemamount);

                if(getOutgoingDatas.Count > 0)
                {
                    IsButtonSaveEnable = true;
                }
                else
                {
                    IsButtonSaveEnable = false;
                }
            }
            else
            {
                MessageBox.Show("Select an item first");
            }
        }
        private bool ValidateFields()
        {
            return getOutgoingDatas.Count > 0;
        }

        public void ClearFields()
        {
            Receiving = new Receiving();
            //DistinctPo = new DistinctPo();
            ObjDistinctPO.Clear();
            getOutgoingDatas.Clear();
            Outgoing = new Outgoing();
            //OutgoingDetails = new OutgoingDetails();
            Series = "";
            Currency = "";
            total_Amount = 0;
            TotalItemQty = 0;
            ItemAmount = 0.00;
            isFieldEnable = false;
            IsButtonNewEnable = true;
            IsButtonSaveEnable = false;
        }
        #endregion
    }
}
