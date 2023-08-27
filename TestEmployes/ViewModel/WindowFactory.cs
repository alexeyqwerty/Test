using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestEmployes.Utils;
using TestEmployes.View;

namespace TestEmployes.ViewModel
{
    public enum WindowMode
    {
        Add,
        Edit
    }

    public interface IDialogChildVM
    {
        string Title { get; set; }   

        bool? DialogResult { get; set; }

        Command Save { get; }

        Command Close { get; }
    }

    public class WindowFactory
    {
        public static void ShowDialog(IDialogChildVM childVM)
        {
            Window window = null;

            if(childVM is AddEditDivisionVM) window = new AddDivisionWindow();

            if (childVM is AddEmployeVM) window = new AddEmploye();

            if (window != null)
            {
                window.Owner = Application.Current.MainWindow;
                window.DataContext = childVM;
                window.ShowDialog();

                window.Owner.Activate();
            }
        }
    }
}
