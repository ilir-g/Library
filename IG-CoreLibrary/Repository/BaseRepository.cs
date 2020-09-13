using IG_CoreLibrary.Models;
using IG_CoreLibrary.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_CoreLibrary
{
    public class BaseRepository : IBaseRepository
    {
      
        protected NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This method creates the phone book entity
        /// </summary>
        /// <param name="PhoneBookBase">PhoneBookBase is the object that get as parameter.</param>
        /// <returns>Returns the response as ResponseModel that is an object that return the item saved, and if there is any error returns the error as a Messagge.</returns>
        public async Task<ResponseModel<PhoneBookBase>> CreatePhoneBook(PhoneBookBase phoneBookModel)
        {
            var response = new ResponseModel<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::CreatePhoneBook");
                List<PhoneBookBase> list = await ReadPhoneBooks() as List<PhoneBookBase>;
                var Exists = list.FirstOrDefault(i => i.FirstName == phoneBookModel.FirstName && i.LastName == phoneBookModel.LastName);
                if (Exists != null)
                {
                    response.HasError = true;
                    response.Messagge = "This item already exist, go to update it!";
                    response.item = phoneBookModel;
                    return response;
                }
                list.Add(phoneBookModel);
                var res = await WritePhoneBooksToFile(list);
                Logger.Info("End method::CreatePhoneBook");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::CreatePhoneBook: {0}", ex.ToString());
                response.HasError = true;
                response.Messagge = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// This method updates the phone book entity
        /// </summary>
        /// <param name="PhoneBookBase">PhoneBookBase is the object that get as parameter.</param>
        /// <returns>Returns the response as ResponseModel that is an object that return the item saved, and if there is any error returns the error as a Messagge.</returns>
        public async Task<ResponseModel<PhoneBookBase>> UpdatePhoneBook(PhoneBookBase phoneBookModel)
        {
            var response = new ResponseModel<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::UpdatePhoneBook");
                List<PhoneBookBase> list = await ReadPhoneBooks() as List<PhoneBookBase>;
                var index = list.FindIndex(p => p.FirstName == phoneBookModel.FirstName && p.LastName == p.LastName);
                list.ElementAt(index).FirstName=phoneBookModel.FirstName;
                list.ElementAt(index).LastName=phoneBookModel.LastName;
                list.ElementAt(index).L_PhoneBook=phoneBookModel.L_PhoneBook;
                response = await WritePhoneBooksToFile(list);
                Logger.Info("End method::UpdatePhoneBook");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::UpdatePhoneBook : {0}", ex.ToString());
                response.HasError = true;
                response.Messagge = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// This method delete the phone book entry
        /// </summary>
        /// <param name="PhoneBookBase">PhoneBookBase is the object that get as parameter.</param>
        /// <returns>Returns the response as ResponseModel that is an object that return the item saved, and if there is any error returns the error as a Messagge.</returns>
        public async Task<ResponseModel<PhoneBookBase>> DeletePhoneBook(PhoneBookBase phoneBookModel)
        {
            var response = new ResponseModel<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::DeletePhoneBook");
                List<PhoneBookBase> list = await ReadPhoneBooks() as List<PhoneBookBase>;
                var index = list.FindIndex(p => p.FirstName == phoneBookModel.FirstName && p.LastName == p.LastName);
                list.RemoveAt(index);
                response = await WritePhoneBooksToFile(list);
                Logger.Info("End method::DeletePhoneBook");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::UpdatePhoneBook : {0}", ex.ToString());
                response.HasError = true;
                response.Messagge = ex.Message;
                return response;
            }
        }

        /// <summary>
        /// This method read all the list of entities
        /// </summary>
        /// <param></param>
        /// <returns>Returns a list of the model PhoneBookBase.</returns>
        public async Task<IEnumerable<PhoneBookBase>> ReadAllPhoneBooks()
        {
            var response = new List<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::ReadAllPhoneBooks");
                response = await ReadPhoneBooks() as List<PhoneBookBase>;               
                Logger.Info("End method::ReadAllPhoneBooks");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::CreatePhoneBook: {0}", ex.ToString());               
                return response;
            }
        }

        /// <summary>
        /// This method order all the list of entities by first name or last name
        /// </summary>
        /// <param name="firstNameOrder">boolean input, default false, for ordering the list by first name</param>
        /// <param name="lastNameOrder">boolean input, default false, for ordering the list by last name</param>
        /// <returns>Returns a list ordered by FirstName or LastName of the model PhoneBookBase.</returns>
        public async Task<IEnumerable<PhoneBookBase>> OrderByFirtLastNamePhoneBooks(bool firstNameOrder=false,bool lastNameOrder=false)
        {
            var response = new List<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::ReadAllPhoneBooks");
                response = await ReadPhoneBooks() as List<PhoneBookBase>;
                if (firstNameOrder && lastNameOrder)
                    response = response.OrderBy(i => i.FirstName).ThenBy(i => i.LastName).ToList();
                else if (firstNameOrder)
                    response  = response.OrderBy(i => i.FirstName).ToList();
                else if (lastNameOrder)
                    response=  response.OrderBy(i => i.LastName).ToList();
                Logger.Info("End method::ReadAllPhoneBooks");
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::CreatePhoneBook: {0}", ex.ToString());
                return response;
            }
        }

        /// <summary>
        /// This a private method writes all the list of entities into the file
        /// </summary>
        /// <param name="List<PhoneBookBase>">the list input to be saved to the file</param>
        /// <param name="FileName">opsional input for the fileName that will be saved the list of entities, by default has a value</param>
        /// <returns>Returns the response as ResponseModel that is an object that return the item saved, and if there is any error returns the error as a Messagge.</returns>
        private async Task<ResponseModel<PhoneBookBase>> WritePhoneBooksToFile(List<PhoneBookBase> phoneBooks,string FileName = "Test.json")
        {
            var response = new ResponseModel<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::WritePhoneBooksToFile");

                string path = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)), FileName);
                if (!File.Exists(path))
                {
                    Logger.Info("Method::WritePhoneBooksToFile:: Creating the file json");
                    FileStream fs = File.Create(path);
                    fs.Close();
                    
                }
                File.WriteAllText(path,string.Empty);
              
                using (StreamWriter sr = new StreamWriter(path))
                {
                    
                    await sr.WriteAsync(JsonConvert.SerializeObject(phoneBooks));
                }
               
                Logger.Info("End method::WritePhoneBooksToFile");
                response.HasError = false;
                response.Messagge = string.Empty;
                return await Task.FromResult(response); ;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::WritePhoneBooksToFile : {0}", ex.ToString());
                response.HasError = true;
                response.Messagge = ex.Message;
                
                return response;
            }
        }
        
        /// <summary>
        /// This a private method reads all the list of entities from the file
        /// </summary>      
        /// <param name="FileName">opsional input for the fileName that will be saved the list of entities, by default has a value</param>
        /// <returns>Returns as response as List of PhoneBookBase model</returns>
        private async Task<IEnumerable<PhoneBookBase>> ReadPhoneBooks(string FileName="Test.json")
        {
            var response = new List<PhoneBookBase>();
            try
            {
                Logger.Info("Init method::ReadPhoneBooks");

                string path = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)), FileName);
                if (!File.Exists(path))
                {
                    Logger.Info("Method::ReadPhoneBooks:: Creating the file json");
                    FileStream fs = File.Create(path);
                    fs.Close();
                    return response;
                }
                using (StreamReader sr = new StreamReader(path))
                {
                    string json = sr.ReadToEnd();
                    response = JsonConvert.DeserializeObject<List<PhoneBookBase>>(json)??new List<PhoneBookBase>();
                   
                }
                Logger.Info("End method::ReadPhoneBooks");
                return await Task.FromResult<List<PhoneBookBase>>(response); ;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception method::ReadPhoneBooks: {0}", ex.ToString());
             
                return response;
            }
        }
    }
}
