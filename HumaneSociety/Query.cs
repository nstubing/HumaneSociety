using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace HumaneSociety
{
    public static class Query
    {
        public static HumaneSocietyDataContext db = new HumaneSocietyDataContext(@"C:\Users\Chelsea\Desktop\C# projects\HumaneSocietyStarter\HumaneSociety_DBCreation.sql");
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            TryDBChanges();
        }

        internal static DietPlan GetDietPlan(string dietPlan)
        {
            var myDietPlan = db.DietPlans.Where(d => d.Name == dietPlan).FirstOrDefault();
            return myDietPlan;
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            TryDBChanges();
        }

        internal static species GetSpecies(string species)
        {
            var mySpecies = db.Species.Where(s => s.Name == species).FirstOrDefault();
            return mySpecies;
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            var isEmployee = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();
            return isEmployee;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Address myAddress = new Address();
            myAddress.AddressLine1 = streetAddress;
            myAddress.Zipcode = zipCode;
            myAddress.USStateId = state;
            db.Addresses.InsertOnSubmit(myAddress);
            TryDBChanges();
            Client myClient = new Client();
            myClient.FirstName = firstName;
            myClient.LastName = lastName;
            myClient.UserName = username;
            myClient.Password = password;
            myClient.Email = email;
            myClient.AddressId = GetAddressId(streetAddress);
            db.Clients.InsertOnSubmit(myClient);
            TryDBChanges();
        }
        internal static int GetAddressId(string streetAddress)
        {
            var thisAddress = db.Addresses.Where(a => a.AddressLine1 == streetAddress || a.AddressLine2 == streetAddress).FirstOrDefault();
            return thisAddress.AddressId;
        }
        internal static Client GetClient(string userName, string password)
        {
            var thisClient = db.Clients.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
            return thisClient;
        }

        internal static IQueryable<Client> RetrieveClients()
        {
            var myClients = db.Clients.Select(c => c);
            return myClients;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            var addUsername = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            addUsername.UserName = employee.UserName;
            addUsername.Password = employee.Password;
            TryDBChanges();
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            var employeeName = db.Employees.Select(e => e.UserName == username).FirstOrDefault();
            return employeeName;
        }

        internal static void Adopt(Animal animal, Client client)
        {
            Adoption myAdoption = new Adoption();
            myAdoption.ClientId = client.ClientId;
            myAdoption.AnimalId = animal.AnimalId;
            myAdoption.ApprovalStatus = "not approved";
            myAdoption.AdoptionFee = 75;
            myAdoption.PaymentCollected = false;
            db.Adoptions.InsertOnSubmit(myAdoption);
            TryDBChanges();
        }

        internal static Animal GetAnimalByID(int iD)
        {
            var myAnimal = db.Animals.Where(d => d.AnimalId == iD).FirstOrDefault();
            return myAnimal;
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            var thisAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();

        }

        internal static object GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static object GetUserAdoptionStatus(Client client)
        {
            throw new NotImplementedException();
        }

        internal static Room GetRoom(int animalId)
        {
            throw new NotImplementedException();
        }

        internal static object GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<USState> GetStates()
        {
            var myState = db.USStates.Select(s => s);
            return myState;
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            var thisEmployee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();
            return thisEmployee;
        }

        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAddress(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void updateClient(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateEmail(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateFirstName(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateLastName(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }

        public static void TryDBChanges()
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
