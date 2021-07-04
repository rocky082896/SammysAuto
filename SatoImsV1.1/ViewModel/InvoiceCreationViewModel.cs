using Prism.Commands;
using SatoImsV1._1.Data;
using SatoImsV1._1.Data.Dtos;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.Repositories.PORepo;
using SatoImsV1._1.View;
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
    public class InvoiceCreationViewModel : ViewModelBase
    {
        private IMSContext _context;
        private Repository _repository;
        private PORepo _poRepo;
        private OfficePurchaseOrder _officePurchaseOrder;
        private ObservableCollection<OfficePurchaseOrderItems> _officePurchaseOrderItems;

        private ItemMaster _selectedItem;
        private ItemMaster _itemMaster;

        private ObservableCollection<ItemMaster> _itemList;
        private ObservableCollection<POItemDto> _pOItemDto;
        private ObservableCollection<Supplier> _supplierList;
        private Supplier _selectedSupplier;
        private Supplier _supplier;

        private int _itemQuantity;
        private double _amount;
        private double _totalAmount;
        private POItemDto _pOItemDtoSelected;

        public DelegateCommand AddNewSupplier { get; set; }
        public DelegateCommand AddItem { get; set; }
        public DelegateCommand ProceedCommand { get; set; }
        public DelegateCommand CreatePO { get; set; }
        public DelegateCommand RemoveCommand { get; set; }

        public InvoiceCreationViewModel(IMSContext context)
        {
            _context = context;
            OfficePurchaseOrder = new OfficePurchaseOrder();
            OfficePurchaseOrderItems = new ObservableCollection<OfficePurchaseOrderItems>();
            POItemDto = new ObservableCollection<POItemDto>();
            TotalAmount = 0;
        }
        protected override void RegisterCollections()
        {
            _repository = new Repository();
            _poRepo = new PORepo();
            FetchItems();
            FetchSuppliers();
        }

        protected override void RegisterCommands()
        {
            AddNewSupplier = new DelegateCommand(ShowSupplier);
            AddItem = new DelegateCommand(ShowNewItem);
            ProceedCommand = new DelegateCommand(Proceed);
            CreatePO = new DelegateCommand(CreateAsync);
            RemoveCommand = new DelegateCommand(SelectionChanged);
        }

        private void SelectionChanged()
        {
            if (POItemDtoSelected != null)
            {
                var itemToRemove = POItemDto.Where(x => x.item_no == POItemDtoSelected.item_no).ToList();
                foreach (var item in itemToRemove)
                {
                    POItemDto.Remove(item);
                }

                TotalAmount = POItemDto.Sum(x => x.amount);
            }
            else
            {
                MessageBox.Show("Select an item first");
            }
        }

        private void ShowNewItem()
        {
            new NewItemView(this).ShowDialog();
        }

        public ItemMaster ItemMaster
        {
            get => _itemMaster; set
            {
                _itemMaster = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OfficePurchaseOrderItems> OfficePurchaseOrderItems
        {
            get => _officePurchaseOrderItems;
            set
            {
                _officePurchaseOrderItems = value;
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

        private void ShowSupplier()
        {
            new SupplierView(this).ShowDialog();
        }

        public ItemMaster SelectedItem
        {
            get => _selectedItem; set
            {
                _selectedItem = value;
                OnPropertyChanged();
                ItemMaster = _selectedItem;
            }
        }

        public int ItemQuantity
        {
            get => _itemQuantity; set
            {
                _itemQuantity = value;
                Amount = ItemMaster.price * _itemQuantity;
                OnPropertyChanged(nameof(ItemQuantity));
            }
        }

        public double Amount
        {
            get => _amount; set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));

            }
        }

        public ObservableCollection<ItemMaster> ItemList
        {
            get => _itemList; set
            {
                _itemList = value;
                OnPropertyChanged();
            }
        }

        public Supplier Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Supplier> SupplierList
        {
            get => _supplierList; set
            {
                _supplierList = value;
                OnPropertyChanged();
            }
        }

        public Supplier SelectedSupplier
        {
            get => _selectedSupplier; set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
                Supplier = _selectedSupplier;
            }
        }
        public void FetchSuppliers()
        {
            SupplierList = new ObservableCollection<Supplier>();
            var list = _repository.All<Supplier>().ToList();
            foreach (var item in list)
            {
                SupplierList.Add(item);
            }
        }
        public void FetchItems()
        {
            ItemList = new ObservableCollection<ItemMaster>();
            var list = _repository.All<ItemMaster>().Select(i => new
            {
                i
            }).Where(c => c.i.status == 1).OrderBy(o => o.i.item_desc);

            foreach (var item in list)
            {
                ItemList.Add(item.i);
            }
        }

        public ObservableCollection<POItemDto> POItemDto
        {
            get => _pOItemDto; set
            {
                _pOItemDto = value;
                OnPropertyChanged("POItemDto");
            }
        }

        public double TotalAmount
        {
            get => _totalAmount; set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        public POItemDto POItemDtoSelected
        {
            get => _pOItemDtoSelected; set
            {
                _pOItemDtoSelected = value;
                OnPropertyChanged();
            }
        }
        private void Proceed()
        {

            bool isExist = POItemDto.Any(i => i.item_no == ItemMaster.item_no);
            if (ItemQuantity > 0 ||
                string.IsNullOrWhiteSpace(ItemQuantity.ToString()))
            {
                if (!isExist)
                {
                    POItemDto.Add(new POItemDto
                    {
                        item_no = ItemMaster.item_no,
                        item_desc = ItemMaster.item_desc,
                        unit = ItemMaster.uom,
                        qty = ItemQuantity,
                        price = ItemMaster.price,
                        amount = ItemMaster.price * ItemQuantity,
                        imagesource = ItemMaster.image_src
                    });
                    Amount = ItemMaster.price * ItemQuantity;
                }
                else
                {
                    int quantity = 0;
                    double price = 0;
                    double amount = 0;

                    foreach (var item in POItemDto.Where(i => i.item_no == ItemMaster.item_no))
                    {
                        price = item.price;
                        quantity = item.qty;
                        amount = item.amount;
                    }

                    amount = price * (quantity + ItemQuantity);

                    POItemDto.Remove(POItemDto.Where(x => x.item_no == ItemMaster.item_no).Single());
                    POItemDto.Add(new POItemDto
                    {
                        item_no = ItemMaster.item_no,
                        item_desc = ItemMaster.item_desc,
                        unit = ItemMaster.uom,
                        qty = quantity + ItemQuantity,
                        price = ItemMaster.price,
                        amount = amount,
                        imagesource = ItemMaster.image_src
                    });
                }

                TotalAmount = POItemDto.Sum(x => x.amount);
            }
            else
            {
                MessageBox.Show("Please enter valid quantity");
            }
        }
        private async void CreateAsync()
        {
            if (ValidateFields())
            {
                try
                {
                    OfficePurchaseOrderItems = new ObservableCollection<OfficePurchaseOrderItems>();
                    foreach (var po in POItemDto)
                    {
                        OfficePurchaseOrderItems.Add(new OfficePurchaseOrderItems
                        {
                            item_no = po.item_no,
                            officePoNumber = OfficePurchaseOrder.officePoNumber,
                            officeUnit = po.unit,
                            officeQuantity = po.qty,
                            officePrice = po.price,
                            officeAmount = po.amount,
                            status = 1
                        });
                    }


                    Console.WriteLine(OfficePurchaseOrder.officePoNumber);
                    foreach (var item in OfficePurchaseOrderItems)
                    {
                        Console.WriteLine(item.officePoNumber);
                        Console.WriteLine(item.item_no);
                    }

                    OfficePurchaseOrder.supp_id = Supplier.supp_id;
                    OfficePurchaseOrder.officeTotalAmount = TotalAmount;
                    OfficePurchaseOrder.status = 1;


                    _context.OfficePurchaseOrders.Add(OfficePurchaseOrder);
                    _context.OfficePurchaseOrdersItems.AddRange(OfficePurchaseOrderItems);

                    //if (await Task.Run(() => _context.SaveChangesAsync()) > 0)
                    //{
                    //    dbContextTransaction.Commit();
                    //    ClearFields();
                    //}
                    //else
                    //    MessageBox.Show("An error occured");


                    //_poRepo.AddPO(OfficePurchaseOrder);
                    //_poRepo.AddPOItems(OfficePurchaseOrderItems);

                    if (await Task.Run(() => _poRepo.TransactItemsAsync(OfficePurchaseOrder, OfficePurchaseOrderItems)))
                        ClearFields();
                    else
                        MessageBox.Show("An error occured");

                }
                catch (DbUpdateException ex) when (ex.InnerException?.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    MessageBox.Show("Duplicate entry");
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
            else
            {
                MessageBox.Show("Please complete required fields");
            }
        }
        private void ClearFields()
        {
            TotalAmount = 0;
            ItemQuantity = 0;
            OfficePurchaseOrder = new OfficePurchaseOrder();
            OfficePurchaseOrderItems = new ObservableCollection<OfficePurchaseOrderItems>();
            POItemDto = new ObservableCollection<POItemDto>();
        }
        private bool ValidateFields()
        {
            return POItemDto.Count > 0 && !string.IsNullOrWhiteSpace(OfficePurchaseOrder.officePoNumber)
                && OfficePurchaseOrder.officeCreditTerms > 0 && !string.IsNullOrEmpty(Supplier.supp_id)
                && OfficePurchaseOrder.officePoValidity > 0 && OfficePurchaseOrder.officeDeliveryDate != null;
        }
    }
}
