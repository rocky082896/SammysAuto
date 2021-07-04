using Impinj.OctaneSdk;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Teleperformance.Data;
using Teleperformance.Model;
using Teleperformance.Repository.Generics;
using Teleperformance.Repository.InboundRepository;
using Teleperformance.View;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class InboundMonitoringViewModel : ViewModelBase
    {
        public static ImpinjReader reader { get; set; }


        private readonly Repos repos;
        private readonly InboundRepo iRepo;
        private ObservableCollection<RedTagsDto> dto;

        private ObservableCollection<InboundDto> _tagId;

        public ObservableCollection<InboundDto> RedTags
        {
            get { return _tagId; }
            set { _tagId = value; OnPropertyChanged(); }
        }


        private string _status;
        private ObservableCollection<Inbound> _registeredList;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public DelegateCommand Stop { get; set; }
        public DelegateCommand Start { get; set; }

        public ObservableCollection<Inbound> RegisteredList
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


        public InboundMonitoringViewModel()
        {
            repos = new Repos();
            iRepo = new InboundRepo();
            RedTags = new ObservableCollection<InboundDto>();
            dto = new ObservableCollection<RedTagsDto>();
            if (!reader.IsConnected)
            {
                RunReader();
                InitList();
            }
        }

        private void InitList()
        {
            Action action = delegate ()
            {
                dto.Add(new RedTagsDto()
                {
                    TagNo = "100025912105",
                    AssetDesc = "ASSET DESCRIPTION #1",
                    AssetNum = "1000-2124",
                    DatenTime = DateTime.Now.ToString("MM-dd-yyyy")
                });

                Console.WriteLine(dto.Count + "");
                var dialog = new AlertWindowView(new AlertWindowViewModel()
                {
                    RedTags = dto,
                    TotalRead = dto.Count,
                    GateNo = 1
                });
                dialog.Show();
            };
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);


            RegisteredList = new ObservableCollection<Inbound>();
            var list = repos.All<Inbound>();
            foreach (var tags in list)
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
                settings.Antennas.GetAntenna(1).MaxRxSensitivity = true; //RxSensitivityInDbm = -30;
                settings.Antennas.GetAntenna(1).MaxTxPower = true;//10.00;
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
                    Console.WriteLine(tag.Epc.ToString().Replace(" ", ""));

                    string tagId = tag.Epc.ToString().Replace(" ", "");
                    RedTags.Add(new InboundDto { TagId = tagId });

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

                            Console.WriteLine(dto.Count + "");
                            var dialog = new AlertWindowView(new AlertWindowViewModel()
                            {
                                RedTags = dto,
                                TotalRead = dto.Count,
                                GateNo = 1
                            });

                            dialog.Show();

                            if (item.Status == 1)
                            {
                                iRepo.UpdateInbound(
                                new Inbound
                                {
                                    InboundId = item.InboundId,
                                    TagId = item.TagId,
                                    DatenTime = item.DatenTime,
                                    AssetNumber = item.AssetNumber,
                                    AssetDescription = item.AssetDescription,
                                    Status = 2
                                }
                              );
                            }

                            InitList();
                        }
                    }

                }
            };
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, action);
        }
    }
}
