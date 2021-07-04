using MvvmValidation;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using TrinityAttenTrace.Model;
using TrinityAttenTrace.View;
using TrinityAttenTrace.ViewModel.Base;

namespace TrinityAttenTrace.ViewModel
{
    public class StudentListViewModel : ViewModelBase
    {
        private string _searchItem;

        public ValidationHelper Validator { get; set; }
        public StudentModel SelectedStudent { get; set; }

        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand ComposeMessageCommand { get; set; }
        public DelegateCommand ShowAddDialog { get; set; }


        private ObservableCollection<StudentModel> _studentList;
        public ObservableCollection<StudentModel> StudentList
        {
            get { return _studentList; }
            set
            {
                _studentList = value;
                this.OnPropertyChanged(nameof(StudentList));
            }
        }

        private string _firstName;
        private string _textWelcome;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }


        public string TextWelcome
        {
            get => _textWelcome;
            set
            {
                _textWelcome = value;
                OnPropertyChanged();
            }
        }

        public string SearchItem
        {
            get => _searchItem; set
            {
                _searchItem = value;
                Console.WriteLine(_searchItem);
                TextWelcome = _searchItem;
                OnPropertyChanged();
            }
        }


        protected override void RegisterCommands()
        {
            ShowAddDialog = new DelegateCommand(LoadRegisterStudent);
            EditCommand = new DelegateCommand(ShowSelectedItem);
            ComposeMessageCommand = new DelegateCommand(ShowComposeMessage);
            InitStudentListValues();
            Validator = new ValidationHelper();
            Validator.AddRequiredRule(() => FirstName, "Keyword is required");

        }

        private void ShowSelectedItem()
        {
            MessageBox.Show(SelectedStudent.FirstName + " " + SelectedStudent.LastNmae);
        }

        private void ShowComposeMessage()
        {
            new ComposeMessageView().ShowDialog();
        }

        private void LoadRegisterStudent()
        {
            new AddStudentView(new AddStudentViewModel(new Data.AttenTraceContext())).ShowDialog();
        }

        private void InitStudentListValues()
        {

            ObservableCollection<StudentModel> studentModels = new ObservableCollection<StudentModel>();
            for (int i = 0; i < 25; i++)
            {
                studentModels.Add(new StudentModel
                {
                    RecordNo = 1,
                    PhotoSource = new BitmapImage(new Uri(@"C:\Users\sps.roland.bitos\Desktop\Sato\doctor.png", UriKind.Absolute)),
                    StudentNo = "2013-0114818",
                    FirstName = "Roland",
                    LastNmae = "Bitos",
                    MiddleName = "Villalobos",
                    GuardianName = "Rolando & Elvira Bitos",
                    MobileNo = "09653425161",
                    Address = "Blk 1 Lot 6 Centennial Homes, Pulo Cabuyao Laguna",
                    Section = "Manuel Roxas",
                    RFIDTag = "14211"
                });
                studentModels.Add(new StudentModel
                {
                    RecordNo = 2,
                    PhotoSource = new BitmapImage(new Uri(@"C:\Users\sps.roland.bitos\Desktop\Sato\me.jpg", UriKind.Absolute)),
                    StudentNo = "2013-0114819",
                    FirstName = "Rocky",
                    LastNmae = "Bitos Jr",
                    MiddleName = "Villalobos",
                    GuardianName = "Rolando & Elvira Bitos",
                    MobileNo = "09653425161",
                    Address = "Brgy. Tibay",
                    Section = "Manuel Roxas",
                    RFIDTag = "69"
                });

            }


            StudentList = studentModels;
        }
    }
}
