using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestEmployes.Utils;

namespace TestEmployes.Database.Models
{
    public class Employe : BaseVM
    {
		private int id;
        public int Id {	get => id; set => SetProperty(ref id, value); }
        
        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }
        
        private string surname;
        public string Surname { get => surname; set => SetProperty(ref surname, value); }
        
        private string birthDay;
        public string BirthDay { get => birthDay; set => SetProperty(ref birthDay, value); }        

        private int divisionId;
        public int DivisionId { get => divisionId; set => SetProperty(ref divisionId, value); }

        private string divisionName;
        public string DivisionName { get => divisionName; set => SetProperty(ref divisionName, value); }
    }
}
