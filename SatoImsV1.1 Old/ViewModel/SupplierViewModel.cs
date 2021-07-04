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
    public class SupplierViewModel : ViewModelBase
    {
        private Supplier _supplier;
        public DelegateCommand SaveSupplier { get; set; }

        private Repository _repository;
        private InvoiceCreationViewModel _receiver;
        public SupplierViewModel(InvoiceCreationViewModel dispatcher)
        {
            _repository = new Repository();
            _receiver = dispatcher;
            Supplier = new Supplier();
        }

        public Supplier Supplier
        {
            get => _supplier; set
            {
                _supplier = value;
                OnPropertyChanged();
            }
        }

        protected override void RegisterCommands()
        {
            SaveSupplier = new DelegateCommand(ExecuteInsert);
        }

        private async void ExecuteInsert()
        {
            if (ValidateFields())
            {
                try
                {
                    try
                    {
                        Supplier.status = 1;
                        if (await Task.Run(() => _repository.Save(Supplier)) > 0)
                        {
                            ClearFields();
                            _receiver.FetchSuppliers();
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
                MessageBox.Show("Fill up required fields");
            }

        }
        private void ClearFields()
        {
            Supplier = new Supplier();
        }
        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(Supplier.supp_id) && !string.IsNullOrWhiteSpace(Supplier.supplier_name)
                && !string.IsNullOrWhiteSpace(Supplier.address) && !string.IsNullOrWhiteSpace(Supplier.contact_1);
        }
    }
}
