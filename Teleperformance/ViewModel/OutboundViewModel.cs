using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Teleperformance.Data;
using Teleperformance.Model;
using Teleperformance.Repository.Generics;
using Teleperformance.Repository.InboundRepository;
using Teleperformance.Repository.OutboundRepository;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class OutboundViewModel : ViewModelBase
    {
        private ObservableCollection<Inbound> _registeredTagList;
        private ObservableCollection<OutboundDto> _outboundList;

        private readonly InboundRepo iRepo;
        private readonly Repos repos;
        private readonly OutboundRepo outboundRepo;



        public DelegateCommand AddCommand { get; set; }
        public Outbound Outbound { get; set; }
        public Inbound SelectedItem { get; set; }

        public ObservableCollection<OutboundDto> OutboundList { get => _outboundList; set { _outboundList = value; OnPropertyChanged(); } }

        protected override void RegisterCommands()
        {
            AddCommand = new DelegateCommand(Add);
        }

        public OutboundViewModel()
        {
            repos = new Repos();
            Outbound = new Outbound();
            outboundRepo = new OutboundRepo();
            iRepo = new InboundRepo();
            FetchInbound();
            FetchOutbound();
        }

        private void Add()
        {
            Outbound.InboundId = SelectedItem.InboundId;
            Outbound.DatenTime = DateTime.Now.ToString("yyyy-MM-dd");
            Outbound.Status = 1;

            if (outboundRepo.AddOutboundTags(Outbound))
            {
                UpdateInbound();
                FetchOutbound();
            }
            else
                MessageBox.Show("Error", "Register Outboind", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void FetchOutbound()
        {
            OutboundList = new ObservableCollection<OutboundDto>();
            var samp = repos.context.Outbounds.Where(x => x.Status == 1)
                .Join(repos.context.Inbounds, o => o.InboundId, i => i.InboundId,
                (o, i) => new OutboundDto
                {
                    OutboundId = o.OutboundId,
                    InboundId = i.InboundId,
                    TagId = i.TagId,
                    AssetNumber = i.AssetNumber,
                    AssetDescription = i.AssetDescription,
                    DatenTime = o.DatenTime

                });

           
            foreach (var tags in samp)
            {
                OutboundList.Add(tags);
            }

        }

        public ObservableCollection<Inbound> TagList
        {
            get => _registeredTagList;
            set { _registeredTagList = value; OnPropertyChanged(); }
        }

        private void FetchInbound()
        {
            TagList = new ObservableCollection<Inbound>();
            var list = repos.All<Inbound>().Where(i => i.Status == 2);
            foreach (var tags in list)
            {
                TagList.Add(tags);
            }
        }

        private void UpdateInbound()
        {
            iRepo.UpdateInbound(
                new Inbound
                {
                    InboundId = SelectedItem.InboundId,
                    TagId = SelectedItem.TagId,
                    AssetDescription = SelectedItem.AssetDescription,
                    AssetNumber = SelectedItem.AssetNumber,
                    DatenTime = SelectedItem.DatenTime,
                    Status = 3
                });

            FetchInbound();
        }
    }
}
