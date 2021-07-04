using CsvHelper;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Teleperformance.Model;
using Teleperformance.Repository.Generics;
using Teleperformance.Repository.InboundRepository;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class InboundViewModel : ViewModelBase
    {
        public Inbound Inbound { get; set; }
        public string DatenTime { get; set; } = DateTime.Now.ToShortDateString();
        public DelegateCommand Proceed { get; set; }
        public DelegateCommand ExportCommand { get; set; }

        private readonly InboundRepo inboundRepo;
        private readonly Repos repos;

        private ObservableCollection<Inbound> _registeredTagList;
        public ObservableCollection<Inbound> RegisteredTagList
        {
            get => _registeredTagList;
            set { _registeredTagList = value; OnPropertyChanged(); }
        }


        protected override void RegisterCommands()
        {
            Proceed = new DelegateCommand(RegisterInbound);
            ExportCommand = new DelegateCommand(Export);
            Inbound = new Inbound();

        }

        private void RegisterInbound()
        {
            Inbound.DatenTime = DateTime.Now.ToString("yyyy-MM-dd");
            Inbound.Status = 1;

            if (inboundRepo.AddInboundTags(Inbound))
            {
                FetchInboundTags();
                Inbound.TagId = "";
            }
            else
                MessageBox.Show("Error", "Register Inbound", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public InboundViewModel()
        {
            inboundRepo = new InboundRepo();
            repos = new Repos();
            FetchInboundTags();
        }


        public void FetchInboundTags()
        {
            RegisteredTagList = new ObservableCollection<Inbound>();
            var list = repos.All<Inbound>().Where(i => i.Status == 1);
            foreach (var tags in list)
            {
                RegisteredTagList.Add(tags);
            }
        }

        private void Export()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                var list = repos.All<Inbound>().ToList();
                using (var writer = new StreamWriter(dialog.FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Inbound>();
                    csv.NextRecord();
                    foreach (var record in list)
                    {
                        csv.WriteRecord(record);
                        csv.NextRecord();
                    }
                }
            }



        }
    }
}
