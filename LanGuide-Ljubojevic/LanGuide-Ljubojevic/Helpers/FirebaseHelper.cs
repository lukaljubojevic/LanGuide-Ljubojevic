using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using LanGuide_Ljubojevic.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;

namespace LanGuide_Ljubojevic.Helpers
{
    public class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("https://languide-2c6a2-default-rtdb.europe-west1.firebasedatabase.app/");
        public static async Task<List<Users>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Name = item.Object.Name,
                    Surname = item.Object.Surname,
                    Address = item.Object.Address
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                .Child("Users")
                .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        public static async Task<bool> AddUser(string email, string password, string name, string surname, string address)
        {
            try
            {
                await firebase
                .Child("Users")
                .PostAsync(new Users() { Email = email, Password = password, Name = name, Surname = surname, Address = address });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> UpdateUser(string email, string password, string name, string surname, string address)
        {
            try
            {
                var toUpdateUser = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                .Child("Users")
                .Child(toUpdateUser.Key)
                .PutAsync(new Users() { Email = email, Password = password, Name = name, Surname = surname, Address = address });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {
                var toDeletePerson = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

    }
}    