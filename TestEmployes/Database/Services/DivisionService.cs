using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEmployes.Database.Models;

namespace TestEmployes.Database.Services
{
    public class DivisionService : IDivisionService
    {
        public List<Division> GetAllDivisions()
        {
            string query = "SELECT Id, Name FROM Divisions";

            List<string> data = Connection.Connection.GetData(query);

            List<Division> result = new List<Division>();

            for (int i = 0; i < data.Count; i += 2)
            {
                result.Add(new Division()
                {
                    Id = int.Parse(data[i]),
                    Name = data[i + 1]
                });
            }

            return result;
        }

        public bool Exist(string divisionName)
        {
            string query = "SELECT * FROM Divisions " +
                "WHERE Name = N'" + divisionName + "'";

            return Connection.Connection.GetData(query).Count > 0;
        }

        public void Add(Division division)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "@Name", division.Name }
            };

            string query = "INSERT INTO Divisions (Name) VALUES (@Name)";

            Connection.Connection.Save(data, query);
        }

        public void Edit(Division division)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "@Name", division.Name }
            };

            string query = "UPDATE Divisions " +
                "SET Name = @Name " +
                "WHERE Id = '" + division.Id + "'";

            Connection.Connection.Save(data, query);
        }

        public void Delete(Division division)
        {            
            string query = "DELETE FROM Divisions " +
                "WHERE Id ='" + division.Id + "'";

            Connection.Connection.Remove(query);
        }
    }
}
