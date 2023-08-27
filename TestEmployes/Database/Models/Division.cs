using System.Collections.ObjectModel;
using TestEmployes.Utils;

namespace TestEmployes.Database.Models
{
    public class Division:  BaseVM
    {
        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }
        
        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }
        
        private ObservableCollection<Employe> employes;

        public ObservableCollection<Employe> Employes { get => employes; set => SetProperty(ref employes, value); }
    }
}
