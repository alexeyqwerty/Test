using System.Collections.Generic;
using TestEmployes.Database.Connection;
using TestEmployes.Database.Models;

namespace TestEmployes.Database.Services
{
    public interface IEmployeService
    {
        List<Employe> GetEmployes(Division division);

        int GetEmployesCount();

        int GetEmployesCount(Division division);

        void Add(Employe employe);

        void Edit(List<Employe> employes);

        void Delete(Employe employe);
    }

    public interface IDivisionService
    {
        List<Division> GetAllDivisions();

        bool Exist(string divisionId);

        void Add(Division division);    

        void Edit(Division division);
        
        void Delete(Division division);
    }

    public class Services
    {
        public static IEmployeService EmployeService { get; private set; }
        public static IDivisionService DivisionService { get; private set; }

        static Services() 
        {
            EmployeService = new EmployeService();

            DivisionService = new DivisionService();

            Connection.Connection.Open();
        }

        
    }
}
