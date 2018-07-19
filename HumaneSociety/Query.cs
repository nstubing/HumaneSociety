﻿using System;
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
            throw new NotImplementedException();
        }

        internal static IQueryable<Client> RetrieveClients()
        {
            var myClients = db.Clients.Select(c => c);
            return myClients;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            throw new NotImplementedException();
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            throw new NotImplementedException();
        }

        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static object GetAnimalByID(int iD)
        {
            throw new NotImplementedException();
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
