using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestEmployes.Database.Models;
using TestEmployes.Database.Services;
using TestEmployes.Utils;

namespace TestEmployes.ViewModel
{
    public class AddEmployeVM : BaseVM, IDialogChildVM
    {
        public AddEmployeVM() 
        {
            Title = "Создание сотрудника";

            Divisions = new ObservableCollection<Division>(Services.DivisionService.GetAllDivisions());
            SelectedDivision = Divisions.FirstOrDefault();
        }

        private string title;
        public string Title { get => title; set => SetProperty(ref title, value); }

        private bool? dialogResult;
        public bool? DialogResult { get => dialogResult; set => SetProperty(ref dialogResult, value); }

        public delegate void EmployeAdded(Division division);
        public event EmployeAdded EmployeAddedEvent; 
                
        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                Save.OnCanExecuteChanged();
            }
        }

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                SetProperty(ref surname, value);
                Save.OnCanExecuteChanged();
            }
        }

        private string birthDay;
        public string BirthDay
        {
            get => birthDay;
            set
            {
                SetProperty(ref birthDay, value);
                Save.OnCanExecuteChanged();
            }
        }

        private int division;
        public int Division
        {
            get => division;
            set
            {
                SetProperty(ref division, value);
                Save.OnCanExecuteChanged();
            }
        }

        private ObservableCollection<Division> divisions;
        public ObservableCollection<Division> Divisions { get => divisions; set => SetProperty(ref divisions, value); }

        private Division selectedDivision;

        public Division SelectedDivision { get => selectedDivision; set=>SetProperty(ref selectedDivision, value); }

        private Command save;
        public Command Save => save ?? (save = new Command(_Save, _CanSave));

        private void _Save()
        {
            Services.EmployeService.Add(new Employe()
            {
                Name = Name,
                Surname= Surname,
                BirthDay = BirthDay,
                DivisionId = SelectedDivision.Id
            });

            EmployeAddedEvent?.Invoke(SelectedDivision);

            DialogResult = true;           
        }

        private bool _CanSave()
        {
            return !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Surname) &&
                !string.IsNullOrEmpty(BirthDay) &&
                SelectedDivision != null;
        }

        private Command close;
        public Command Close => close ?? (close = new Command(() => DialogResult = false, () => true));
    }
}
