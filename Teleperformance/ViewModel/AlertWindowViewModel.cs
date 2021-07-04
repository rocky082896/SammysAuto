using Prism.Commands;
using System.Collections.ObjectModel;
using Teleperformance.Data;
using Teleperformance.ViewModel.Base;

namespace Teleperformance.ViewModel
{
    public class AlertWindowViewModel : ViewModelBase
    {
        public DelegateCommand CloseCommand { get; set; }

        public ObservableCollection<RedTagsDto> _redTags;

        public ObservableCollection<RedTagsDto> RedTags
        {
            get { return _redTags; }
            set
            {
                _redTags = value;
                OnPropertyChanged();
            }
        }

        private int _gateNo;

        public int GateNo
        {
            get { return _gateNo; }
            set { _gateNo = value; OnPropertyChanged(); }
        }



        private int _totalRead;

        public int TotalRead
        {
            get { return _totalRead; }
            set { _totalRead = value; OnPropertyChanged(); }
        }


        private string _tagNo;

        public string TagNo
        {
            get => _tagNo; set { _tagNo = value; OnPropertyChanged(); }

        }
        private string _assetNo;

        public string AssetNo
        {
            get { return _assetNo; }
            set
            {
                _assetNo = value;
                OnPropertyChanged();
            }
        }

        private string _assetDesc;

        public string AssetDesc
        {
            get { return _assetDesc; }
            set
            {
                _assetDesc = value;
                OnPropertyChanged();
            }
        }


    }
}
