using Microsoft.Win32;
using Prism.Commands;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
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
using System.Windows.Media.Imaging;

namespace SatoImsV1._1.ViewModel
{
    public class NewItemViewModel : ViewModelBase
    {
        private ItemMaster _itemMaster;
        private BitmapImage _imageSource;
        private Repository _repository;

        public DelegateCommand SaveItem { get; set; }
        public DelegateCommand BrowsePicture { get; set; }
        public DelegateCommand ShowCategory { get; set; }
        public DelegateCommand ShowGroup { get; set; }


        private ObservableCollection<Category> _categories;
        private Category _selectedIndex;
        private int _selectedAction;
        private ObservableCollection<ItemGroup> _groups;
        private ItemGroup _selectedGroupIndex;
        private int _selectedGroupAction;
        private string _selectedCurrency;

        public BitmapImage ImageSource
        {
            get => _imageSource; set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public ItemMaster ItemMaster
        {
            get => _itemMaster; set
            {
                _itemMaster = value;
                OnPropertyChanged();
            }
        }

        private InvoiceCreationViewModel _receiver;
        public NewItemViewModel(InvoiceCreationViewModel dispatcher)
        {
            _receiver = dispatcher;
            _repository = new Repository();
            ItemMaster = new ItemMaster();
        }

        protected override void RegisterCollections()
        {
            _repository = new Repository();
            GetData();
            GetGroupData();
        }

        protected override void RegisterCommands()
        {
            SaveItem = new DelegateCommand(Save);
            BrowsePicture = new DelegateCommand(Browse);
            ShowCategory = new DelegateCommand(ViewCategory);
            ShowGroup = new DelegateCommand(ViewGroup);

        }

        public ObservableCollection<Category> Categories
        {
            get => _categories; set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public Category SelectedIndex
        {
            get => _selectedIndex;
            set
            {

                _selectedIndex = value;
                SelectedAction = _selectedIndex.Id;
                ItemMaster.cat_id = SelectedAction;
            }
        }


        public int SelectedAction
        {
            get => _selectedAction; set
            {
                _selectedAction = value;
                OnPropertyChanged(nameof(SelectedAction));
            }
        }

        public ObservableCollection<ItemGroup> Groups
        {
            get => _groups; set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public ItemGroup SelectedGroupIndex
        {
            get => _selectedGroupIndex;
            set
            {
                _selectedGroupIndex = value;
                SelectedGroupAction = _selectedGroupIndex.Id;
            }
        }

        public int SelectedGroupAction
        {
            get => _selectedGroupAction; set
            {
                _selectedGroupAction = value;

                OnPropertyChanged(nameof(SelectedGroupAction));
            }
        }

        public string SelectedCurrency
        {
            get => _selectedCurrency; set
            {
                _selectedCurrency = value;
                OnPropertyChanged(SelectedCurrency);
                ItemMaster.currency = _selectedCurrency;
            }
        }


        public void GetData()
        {
            Categories = new ObservableCollection<Category>();
            var cat = _repository.All<Category>().ToList();
            foreach (var items in cat)
            {
                Categories.Add(items);
            }
        }

        public void GetGroupData()
        {
            Groups = new ObservableCollection<ItemGroup>();
            var group = _repository.All<ItemGroup>().ToList();
            foreach (var item in group)
            {
                Groups.Add(item);
            }
        }

        private void ViewGroup()
        {
            new ItemGroupView(this).ShowDialog();
        }
        private void ViewCategory()
        {
            new AddCategoryView(this).ShowDialog();
        }
        private void Browse()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string src = openFileDialog.FileName;
                ImageSource = new BitmapImage(new Uri(src, UriKind.Absolute));
                ItemMaster.image_src = src;
            }
        }
        private async void Save()
        {
            if (ValidateFields())
            {
                try
                {
                    ItemMaster.group_id = SelectedGroupAction;
                    ItemMaster.status = 1;
                    try
                    {
                        if (await Task.Run(() => _repository.Save(ItemMaster)) > 0)
                        {
                            ClearFields();
                            _receiver.FetchItems();
                        }

                        else
                            MessageBox.Show("No item saved");
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
            else
            {
                MessageBox.Show("Complete all fields");
            }
        }

        private void ClearFields()
        {
            ItemMaster = new ItemMaster();
            ImageSource = null;
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(ItemMaster.item_no) && !string.IsNullOrWhiteSpace(ItemMaster.item_desc)
                && !string.IsNullOrEmpty(ItemMaster.group_id.ToString()) && !string.IsNullOrEmpty(ItemMaster.cat_id.ToString())
                && !string.IsNullOrWhiteSpace(ItemMaster.currency) && !string.IsNullOrWhiteSpace(ItemMaster.price.ToString());
        }
    }
}
