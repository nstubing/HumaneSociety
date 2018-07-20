using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.Linq;

namespace HumaneSociety
{
    public static class Query
    {
        public delegate void EmployeeDelegate(Employee employee);
        public static HumaneSocietyDataContext db = new HumaneSocietyDataContext();
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

        internal static Species GetSpecies(string species)
        {
            var mySpecies = db.Species.Where(s => s.Name == species).FirstOrDefault();
            if (mySpecies==null)
            {
                Species newSpecies = new Species();
                newSpecies.Name = species;
                db.Species.InsertOnSubmit(newSpecies);
                TryDBChanges();
                GetSpecies(species);
            }
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
            bool userExists = false;
            var employeeName = db.Employees.Where(e => e.UserName == username).FirstOrDefault();
            if(employeeName!=null)
            {
                userExists = true;
            }
            return userExists;
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
            foreach(KeyValuePair<int,string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        var thisSpecies = db.Species.Where(s=> s.Name==update.Value).FirstOrDefault();
                        thisAnimal.Species = thisSpecies;
                        break;
                    case 2:
                        thisAnimal.Name=update.Value;
                        break;
                    case 3:
                        thisAnimal.Age = Int32.Parse(update.Value);
                        break;
                    case 4:
                        thisAnimal.Demeanor = update.Value;
                        break;
                    case 5:
                        thisAnimal.KidFriendly = UserInterface.GetBitData(update.Value);
                        break;
                    case 6:
                        thisAnimal.PetFriendly = UserInterface.GetBitData(update.Value);
                        break;
                    case 7:
                        thisAnimal.Weight = Int32.Parse(update.Value);
                        break;
                    case 8:
                        Query.UpdateRoom(thisAnimal.AnimalId, Int32.Parse(update.Value));
                        break;

                }
                TryDBChanges();

            }


        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "not approved");
            return pendingAdoptions;
        }

        internal static IQueryable<Adoption> GetUserAdoptionStatus(Client client)
        {
            var userAdoption = db.Adoptions.Where(a => a.ClientId == client.ClientId);
            return userAdoption;

        }

        internal static Room GetRoom(int animalId)
        {
            var myRoom = db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
            return myRoom;
        }

        internal static List<AnimalShot> GetShots(Animal animal)
        {
            var myAnimalShots = db.Animals.Where(a => a.AnimalId == animal.AnimalId).Select(a=>a.AnimalShots).FirstOrDefault().ToList();
            return myAnimalShots;

        }

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            EmployeeDelegate employeeDelegate;
            switch (v)
            {
                case "create":
                    employeeDelegate =CreateNewEmployee;
                    break;
                case "read":
                    employeeDelegate = ReadEmployee;
                    break;
                case "update":
                    employeeDelegate = UpdateEmployee;
                    break;
                case "delete":
                    employeeDelegate = DeleteEmployee;
                    break;
                default:
                    employeeDelegate = CreateNewEmployee;
                    break;
            }
            employeeDelegate(employee);

        }
        internal static void CreateNewEmployee(Employee employee)
        {
            db.Employees.InsertOnSubmit(employee);
            TryDBChanges();
        }
        internal static void ReadEmployee(Employee employee)
        {
            var thisEmployee = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
            UserInterface.DisplayEmployeeInfo(thisEmployee);
            TryDBChanges();
        }
        internal static void UpdateEmployee(Employee employee)
        {
            var thisEmployee = db.Employees.Where(s => s.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
            thisEmployee.FirstName = employee.FirstName;
            thisEmployee.LastName = employee.LastName;
            thisEmployee.Email = employee.Email;
            TryDBChanges();
        }
        internal static void DeleteEmployee(Employee employee)
        {
            var myDelete = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
            db.Employees.DeleteOnSubmit(myDelete);
            TryDBChanges();

        }

        internal static void UpdateShot(string v, Animal animal)
        {
            AnimalShot thisShot = new AnimalShot();
            thisShot.AnimalId = animal.AnimalId;
            var thisShotId= db.Shots.Where(s => s.Name == v).FirstOrDefault();
            thisShot.ShotId = thisShotId.ShotId;
            db.AnimalShots.InsertOnSubmit(thisShot);
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

            var thisAdoption = db.Adoptions.Where(a => a.AdoptionId == adoption.AdoptionId).FirstOrDefault();
            if(v)
            {
                adoption.ApprovalStatus = "approved";
            }
            TryDBChanges();

        }

        internal static void UpdateAddress(Client client)
        {

            var newAddress = db.Clients.Where(c => c.ClientId == client.ClientId).Where(a => a.Address == client.Address).FirstOrDefault();
            db.Clients.InsertOnSubmit(newAddress);
            TryDBChanges();
        }

        internal static void updateClient(Client client)
        {
            var newClient = db.Clients.Where(c => c.ClientId == client.ClientId).FirstOrDefault();
            db.Clients.InsertOnSubmit(newClient);
            TryDBChanges();
        }

        internal static void UpdateRoom(int animalId, int v)
        {
            Room newRoom = new Room();
            newRoom.AnimalId = animalId;
            var thisRoom = db.Rooms.Where(r => r._RoomNumber == v).FirstOrDefault();
            if (thisRoom ==null)
            {
                newRoom._RoomNumber = v;

            }
            else if (thisRoom!=null && thisRoom.AnimalId==null)
            {
                newRoom._RoomNumber = v;
            }
            else
            {
                Console.WriteLine("This room is full.");
                UpdateRoom(animalId, UserInterface.GetIntegerData("room number", "the animal's"));
            }
            db.Rooms.InsertOnSubmit(newRoom);
            TryDBChanges();
        }

        internal static void UpdateEmail(Client client)
        {
            var newEmail = db.Clients.Where(e=>e.ClientId == client.ClientId).Where(e => e.Email == client.Email).FirstOrDefault();
            db.Clients.InsertOnSubmit(newEmail);
            TryDBChanges();
        }

        internal static void UpdateFirstName(Client client)
        {
            var newFirstName = db.Clients.Where(c=>c.ClientId == client.ClientId).Where(c => c.FirstName == client.FirstName).FirstOrDefault();
            db.Clients.InsertOnSubmit(newFirstName);
            TryDBChanges();
        }

        internal static void UpdateLastName(Client client)
        {
            var newLastName = db.Clients.Where(c=>c.ClientId == client.ClientId).Where(c => c.LastName == client.LastName).FirstOrDefault();
            db.Clients.InsertOnSubmit(newLastName);
            TryDBChanges();
        }

        internal static void UpdateUsername(Client client)
        {
            var newUsername =db.Clients.Where(c =>c.ClientId == client.ClientId).Where(c => c.UserName == client.UserName).FirstOrDefault();
            db.Clients.InsertOnSubmit(newUsername);
            TryDBChanges();
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
