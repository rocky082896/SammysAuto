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
    public class ItemGroupViewModel : ViewModelBase
    {
        private ItemGroup _group;
        public DelegateCommand SaveGroup { get; set; }
        protected override void RegisterCommands()
        {
            SaveGroup = new DelegateCommand(Save);
        }
        public ItemGroup ItemGroup
        {
            get => _group; set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private NewItemViewModel _receiver;
        private Repository _repository;

        public ItemGroupViewModel(NewItemViewModel dispatcher)
        {
            _receiver = dispatcher;
            _repository = new Repository();
            ItemGroup = new ItemGroup();
        }

        private async void Save()
        {
            if (ValidateField())
            {
                try
                {
                    try
                    {
                        if (await Task.Run(() => _repository.Save(ItemGroup)) > 0)
                        {
                            ClearField();
                            _receiver.GetGroupData();
                        }
                        else
                        {
                            MessageBox.Show("Item no save");
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
            else
            {
                MessageBox.Show("Enter group name");
            }

        }

        private void ClearField()
        {
            ItemGroup = new ItemGroup();
        }

        private bool ValidateField()
        {
            return !string.IsNullOrWhiteSpace(ItemGroup.group_name);
        }
    }
}
