using Impinj.OctaneSdk;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Teleperformance.Data;
using Teleperformance.Model;
using Teleperformance.Repository.Generics;
using Teleperformance.Repository.OutboundRepository;
using Teleperformance.View;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class OutboundMonitoringViewModel : ViewModelBase
    {
        public static ImpinjReader reader { get; set; }
        private readonly Repos repos;
        private readonly OutboundRepo oRepo;
        private ObservableCollection<RedTagsDto> dto;

        private ObservableCollection<OutboundDto> _tagId;

        public ObservableCollection<OutboundDto> RedTags
        {
            get { return _tagId; }
            set { _tagId = value; OnPropertyChanged(); }
        }


        private string _status;
        private ObservableCollection<OutboundDto> _registeredList;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public DelegateCommand Stop { get; set; }
        public DelegateCommand Start { get; set; }

        public ObservableCollection<OutboundDto> RegisteredList
        {
            get => _registeredList;
            set { _registeredList = value; OnPropertyChanged(); }
        }

        private string backcolor;

        public string BackgroundColor
        {
            get { return backcolor; }
            set { backcolor = value; OnPropertyChanged(); }
        }

        private string isFound;

        public string IsFound
        {
            get { return isFound; }
            set { isFound = value; OnPropertyChanged(); }
        }

        private string tagId;

        public string TagId
        {
            get { return tagId; }
            set { tagId = value; OnPropertyChanged(); }
        }


        public OutboundMonitoringViewModel()
        {
            repos = new Repos();
            oRepo = new OutboundRepo();
            RedTags = new ObservableCollection<OutboundDto>();
            dto = new ObservableCollection<RedTagsDto>();
            if (!reader.IsConnected)
            {
                RunReader();
                InitList();
            }
        }

        private void InitList()
        {
            RegisteredList = new ObservableCollection<OutboundDto>();
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
                RegisteredList.Add(tags);
            }
        }

        protected override void RegisterCommands()
        {
            reader = new ImpinjReader();
            Stop = new DelegateCommand(StopReading);
            Start = new DelegateCommand(RunReader);
        }

        public void StopReading()
        {
            if (reader.IsConnected)
            {
                reader.Disconnect();
                Console.WriteLine("Disconnected");
                Status = "Disconnected";
            }

        }

        private void RunReader()
        {
            try
            {
                reader.Connect("speedwayr-11-de-89.local");

                Settings settings = reader.QueryDefaultSettings();
                settings.Antennas.GetAntenna(2).RxSensitivityInDbm = -30;
                settings.Antennas.GetAntenna(2).TxPowerInDbm = 10.00;

                settings.SearchMode = SearchMode.SingleTargetReset;
                settings.Session = 2;
                settings.Report.IncludeAntennaPortNumber = true;

                settings.Report.Mode = ReportMode.Individual;
                reader.ApplySettings(settings);
                reader.TagsReported += OnTagsReported;
                reader.Start();

                Status = "Connected";
                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.
                    WriteLine("An Octane SDK exception has occurred : {0}", ex.Message);
                Status = "Disconnected";
            }
        }
        private void OnTagsReported(Object sender, TagReport args)
        {
            Action action = delegate ()
            {
                foreach (Tag tag in args)
                {
                    string tagId = tag.Epc.ToString().Replace(" ", "");
                    RedTags.Add(new OutboundDto { TagId = tagId });

                    Console.WriteLine(tag.Epc);
                    Console.WriteLine(tagId);
                    foreach (var item in RegisteredList)
                    {
                        if (item.TagId == tagId)
                        {
                            BackgroundColor = "Coral";
                            IsFound = "Tag Found";
                            TagId = tagId;


                            dto.Add(new RedTagsDto()
                            {
                                TagNo = item.TagId,
                                AssetDesc = item.AssetDescription,
                                AssetNum = item.AssetNumber,
                                DatenTime = DateTime.Now.ToString("MM-dd-yyyy")
                            });

                            var dialog = new AlertWindowView(new AlertWindowViewModel()
                            {
                                RedTags = dto,
                                TotalRead = dto.Count,
                                GateNo = 2
                            });

                            dialog.Show();

                            oRepo.UpdateOutbound(
                               new Outbound
                               {
                                   OutboundId = item.OutboundId,
                                   InboundId = item.InboundId,
                                   DatenTime = item.DatenTime,
                                   Status = 2
                               }
                               );

                            InitList();
                        }
                    }

                }
            };
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
        }
    }
}
