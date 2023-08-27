using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestEmployes.Database.Connection;
using TestEmployes.Database.Models;
using TestEmployes.Database.Services;
using TestEmployes.Utils;

namespace TestEmployes.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private string empQuant;
        public string EmpQuant { get => empQuant; set => SetProperty(ref empQuant, value); }

        private string divQuant;
        public string DivQuant { get => divQuant; set => SetProperty(ref divQuant, value); }

        private bool haveEmployesToChange;
        public bool HaveEmployesToChange { get => haveEmployesToChange; set => SetProperty(ref haveEmployesToChange, value); }


        private ObservableCollection<Division> divisions;
        public ObservableCollection<Division> Divisions { get => divisions; set => SetProperty(ref divisions, value); }

        private ObservableCollection<string> divList;
        public ObservableCollection<string> DivList { get => divList; set => SetProperty(ref divList, value); }

        private List<Employe> employesToChange { get; set; }


        public MainWindowVM()
        {  
            Divisions = new ObservableCollection<Division>();

            UpdateDivisionsList();            

            employesToChange = new List<Employe>();

            UpdateEmployesQuantity();
        }

        private void UpdateDivisionsList()
        {
            Divisions?.Clear();
            Services.DivisionService.GetAllDivisions().ForEach(Divisions.Add);

            DivList = new ObservableCollection<string>(Divisions.Select(d => d.Name).ToList());

            DivQuant = Divisions.Count.ToString();
        }

        private void UpdateEmployesQuantity()
        {
            EmpQuant = Services.EmployeService.GetEmployesCount().ToString();
        }

        //Добавление подразделения
        private Command addDivision;

        public Command AddDivision => addDivision ?? (addDivision = new Command(_AddDivision, () => true));

        private void _AddDivision()
        {  
            var addDivision = new AddEditDivisionVM(WindowMode.Add);

            WindowFactory.ShowDialog(addDivision);
            
            if (addDivision.DialogResult == true)
            {
                UpdateDivisionsList();
            }           
        }

        //Редактирование подразделения
        private Command editDivision;

        public Command EditDivision => editDivision ?? (editDivision = new Command(_EditDivision, (object obj) => true));

        private void _EditDivision(object obj)
        {
            Division division = obj as Division;

            if (division != null) 
            {
                var editDivision = new AddEditDivisionVM(WindowMode.Edit, division);

                WindowFactory.ShowDialog(editDivision);

                if(editDivision.DialogResult == true)
                {
                    UpdateDivisionsList();
                }
            }   
        }

        //Удаление подразделеня
        private Command deleteDivision;

        public Command DeleteDivision => deleteDivision ?? (deleteDivision = new Command(_DeleteDivision, (object obj) => true));

        private void _DeleteDivision(object obj)
        {
            Division division = obj as Division;

            if (division != null)
            {
                if(Services.EmployeService.GetEmployesCount(division) > 0) 
                {
                    MessageBox.Show("Так нельзя! Чтоб удалить подразделение, в нем не должно быть сотрудников!");

                    return;
                }

                Services.DivisionService.Delete(division);

                UpdateDivisionsList();
            }
        }

        //Сохранение сотрудников
        private Command save;

        public Command Save => save ?? (save = new Command(_Save, () => true));

        private void _Save()
        {
            if (employesToChange.Count > 0) 
            {
                Services.EmployeService.Edit(employesToChange);
                
                employesToChange.Clear();
                HaveEmployesToChange = false;
                
                UpdateDivisionsList();
            }
        }

        //Команда раскрытия экспандера подразделения
        private Command divisionExpanded;
       
        public Command DivisionExpanded => divisionExpanded ?? (divisionExpanded = new Command(_DivisionExpanded, (object obj) => true));

        void _DivisionExpanded(object obj)
        {
            var division = obj as Division;

            if (division != null) 
            {
                Divisions.Where(d=>d.Id.Equals(division.Id)).FirstOrDefault().Employes =
                    new ObservableCollection<Employe>(Services.EmployeService.GetEmployes(division));                
            }
        }

        //Команда изменения данных у сотрудника
        private Command employeChanged;

        public Command EmployeChanged => employeChanged ?? (employeChanged = new Command(_EmployeChanged, (object obj) => true));

        private void _EmployeChanged(object obj)
        {
            var employe = obj as Employe;

            if(employe != null)
            {
                employe.DivisionId = Divisions
                    .Where(d => d.Name.Equals(employe.DivisionName))
                    .FirstOrDefault()
                    .Id;                            

                if(!employesToChange.Any(e => e.Id.Equals(employe.Id)))
                {
                    employesToChange.Add(employe);

                    HaveEmployesToChange = true;
                }
            }
        }

        private Command addEmploye;

        public Command AddEmploye => addEmploye ?? (addEmploye = new Command(_AddEmploye, () => true));

        private void _AddEmploye()
        {
            var addEmploye = new AddEmployeVM();

            addEmploye.EmployeAddedEvent += _DivisionExpanded;

            WindowFactory.ShowDialog(addEmploye);

            UpdateEmployesQuantity();
        }

        private Command deleteEmploye;

        public Command DeleteEmploye => deleteEmploye ?? (deleteEmploye = new Command(_DeleteEmploye, (object obj) => true));

        private void _DeleteEmploye(object obj)
        {
            Employe employe = obj as Employe;

            if(employe != null) 
            {
                Services.EmployeService.Delete(employe);

                Divisions
                    .Where(d => d.Id.Equals(employe.DivisionId))
                    .FirstOrDefault()
                    .Employes.Remove(employe);

                UpdateEmployesQuantity();
            }
        }

        //Закрытие приложения -> закрытие подключения
        private Command closing;

        public Command Closing=>closing??(closing=new Command(_Closing, () => true));

        private void _Closing()
        {
            Connection.Close();
        }
    }
}
