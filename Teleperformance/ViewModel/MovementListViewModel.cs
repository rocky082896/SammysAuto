using System.Collections.ObjectModel;
using System.Linq;
using Teleperformance.Data;
using Teleperformance.Model;
using Teleperformance.Repository.Generics;
using Teleperformance.Repository.InboundRepository;
using Teleperformance.Repository.OutboundRepository;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class MovementListViewModel : ViewModelBase
    {
        private readonly InboundRepo iRepo;
        private readonly Repos repos;
        private readonly OutboundRepo outboundRepo;
        public Outbound Outbound { get; set; }

        private ObservableCollection<Inbound> _registeredTagList;
        private ObservableCollection<OutboundDto> _outboundList;


        public ObservableCollection<Inbound> TagList
        {
            get => _registeredTagList;
            set { _registeredTagList = value; OnPropertyChanged(); }
        }

        private void FetchInbound()
        {
            TagList = new ObservableCollection<Inbound>();
            var list = repos.All<Inbound>();
            foreach (var tags in list)
            {
                TagList.Add(tags);
            }
        }

        public ObservableCollection<OutboundDto> OutboundList { get => _outboundList; set { _outboundList = value; OnPropertyChanged(); } }

        public MovementListViewModel()
        {
            repos = new Repos();
            Outbound = new Outbound();
            outboundRepo = new OutboundRepo();
            iRepo = new InboundRepo();
            FetchInbound();
            FetchOutbound();
        }

        private void FetchOutbound()
        {
            OutboundList = new ObservableCollection<OutboundDto>();
            var samp = repos.context.Outbounds
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
    }
}
