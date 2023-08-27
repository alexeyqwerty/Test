using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using TestEmployes.Database.Models;

namespace TestEmployes.Database.Services
{
    public class EmployeService : IEmployeService
    {
        
        public List<Employe> GetEmployes(Division division)
        {
            string query = "SELECT * FROM Employes " +
                "WHERE Division = '" + division.Id + "'";

            List<string> data = Connection.Connection.GetData(query);

            List<Employe> result = new List<Employe>();

            for (int i = 0; i < data.Count; i += 5)
            {
                result.Add(new Employe()
                {
                    Id = int.Parse(data[i]),
                    Name = data[i + 1],
                    Surname = data[i + 2],
                    BirthDay = DateTime.Parse(data[i + 3]).ToShortDateString(),
                    DivisionId = int.Parse(data[i + 4]),
                    DivisionName = division.Name
                });
            }

            return result;
        }

        public int GetEmployesCount()
        {
            string query = "SELECT COUNT(*) FROM Employes";

            List<string> data = Connection.Connection.GetData(query);

            return int.Parse(data[0]);
        }

        public int GetEmployesCount(Division division)
        {
            string query = "SELECT COUNT(*) FROM Employes " +
                "WHERE Division = '" + division.Id + "'";

            List<string> data = Connection.Connection.GetData(query);

            return int.Parse(data[0]);
        }

        public void Add(Employe employe)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    { "@Name", employe.Name },
                    { "@Surname", employe.Surname },
                    { "@BirthDay", Convert.ToDateTime(DateTime.Parse(employe.BirthDay)).ToString("yyyyMMdd") },
                    { "@Division", employe.DivisionId }
                };

            string query = "INSERT INTO Employes (Name, Surname, BirthDay, Division) " +
                "VALUES (@Name, @Surname, @BirthDay, @Division)";

            Connection.Connection.Save(data, query);
        }

        //Сохранение
        public void Edit(List<Employe> employes)
        {
            foreach(Employe emp in employes)
            {
                Dictionary<string, object> data = new Dictionary<string, object>()
                {                    
                    { "@Name", emp.Name },
                    { "@Surname", emp.Surname },
                    { "@BirthDay", Convert.ToDateTime(DateTime.Parse(emp.BirthDay)).ToString("yyyyMMdd") },
                    { "@Division", emp.DivisionId }
                };

                string query = "UPDATE Employes " +
                    "SET Name = @Name, " +
                    "Surname = @Surname, " +
                    "BirthDay = @BirthDay, " +
                    "Division = @Division " +
                    "WHERE Id = '" + emp.Id + "'";

                Connection.Connection.Save(data, query);
            }
        }

        public void Delete(Employe employe)
        {
            string query = "DELETE FROM Employes " +
                "WHERE Id ='" + employe.Id + "'";

            Connection.Connection.Remove(query);
        }
    }
}
