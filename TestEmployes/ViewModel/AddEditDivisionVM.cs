using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestEmployes.Database.Connection;
using TestEmployes.Database.Models;
using TestEmployes.Database.Services;
using TestEmployes.Utils;

namespace TestEmployes.ViewModel
{
    public class AddEditDivisionVM : BaseVM, IDialogChildVM
    {
        public AddEditDivisionVM(WindowMode windowMode, Division division = null)
        {
            this.windowMode = windowMode;

            switch (windowMode)
            {
                case WindowMode.Add:

                    Title = "Новое подразделение:";

                    break;

                case WindowMode.Edit:

                    Title = "Редактировать подразделение:";

                    Id = division.Id;
                    Name = division.Name;

                    break;
            }

        }

        private WindowMode windowMode;

        private string title;
        public string Title { get => title; set => SetProperty(ref title, value); }

        private bool? dialogResult;
        public bool? DialogResult { get => dialogResult; set => SetProperty(ref dialogResult, value) ; }

        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }

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

        private Command save;
        public Command Save => save ?? (save = new Command(_Save, _CanSave));        

        private void _Save()
        {
            //Находим совпадения

            if(Services.DivisionService.Exist(Name))
            {
                MessageBox.Show("Такое подразделение уже существует");
                return;
            }

            if(windowMode == WindowMode.Edit)
            {
                Services.DivisionService.Edit(new Division() { Id =  Id, Name = Name });
            }

            else if(windowMode == WindowMode.Add)
            {
                Services.DivisionService.Add(new Division() { Id = Id, Name = Name });
            }

            DialogResult = true;

            //Если нет, сохраняем
        }

        private bool _CanSave()
        {
            return !string.IsNullOrEmpty(Name);
        }

        private Command close;
        public Command Close => close ?? (close = new Command(() => DialogResult = false, () => true));        
    }
}
