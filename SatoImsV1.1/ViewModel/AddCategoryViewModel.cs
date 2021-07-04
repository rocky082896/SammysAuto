using Prism.Commands;
using SatoImsV1._1.Model;
using SatoImsV1._1.Repositories;
using SatoImsV1._1.ViewModel.Base;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace SatoImsV1._1.ViewModel
{
    public class AddCategoryViewModel : ViewModelBase
    {
        private Category _category;
        public DelegateCommand SaveCategory { get; set; }
        protected override void RegisterCommands()
        {
            SaveCategory = new DelegateCommand(Add);
        }
        public Category Category
        {
            get => _category; set
            {

                _category = value;
                OnPropertyChanged();
            }
        }

        private NewItemViewModel _receiver;
        private Repository _repository;
        public AddCategoryViewModel(NewItemViewModel dispatcher)
        {
            _receiver = dispatcher;
            _repository = new Repository();
            Category = new Category();
        }

        public async void Add()
        {
            if (ValidateFields())
            {
                try
                {
                    try
                    {
                        Category.status = 1;
                        if (await Task.Run(() => _repository.Save(Category)) > 0)
                        {
                            ClearField();
                            _receiver.GetData();
                        }
                        else
                            MessageBox.Show("An error occured");

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
                MessageBox.Show("Enter category name");
            }
        }
        private void ClearField()
        {
            Category = new Category();
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Category.cat_desc);
        }

    }
}
