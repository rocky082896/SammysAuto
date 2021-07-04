using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Data.Entity.Validation;
using System.Windows.Media.Imaging;
using TrinityAttenTrace.Data;
using TrinityAttenTrace.Model;
using TrinityAttenTrace.ViewModel.Base;

namespace TrinityAttenTrace.ViewModel
{
    public class AddStudentViewModel : ViewModelBase
    {
        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public DateTime DateOfBirth { get; set; }
        private StudentsModel _student;
        public DelegateCommand SaveCommand { get; set; }

        public StudentsModel Student
        {
            get => _student;
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

        private readonly AttenTraceContext _context;

        public DelegateCommand BrowseCommand
        {
            get; set;
        }

        public AddStudentViewModel(AttenTraceContext context)
        {
            _context = context;
            Student = new StudentsModel();
        }


        protected override void RegisterCommands()
        {
            BrowseCommand = new DelegateCommand(BrowsePicture);
            SaveCommand = new DelegateCommand(Save);
        }

        private async void Save()
        {
            try
            {
                _context.Students.Add(Student);
                await _context.SaveChangesAsync();
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

        private void BrowsePicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }
    }
}
