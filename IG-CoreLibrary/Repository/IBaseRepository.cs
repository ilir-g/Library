using IG_CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG_CoreLibrary.Repository
{
    public interface IBaseRepository
    {
        Task<ResponseModel<PhoneBookBase>> CreatePhoneBook(PhoneBookBase phoneBookModel);
        Task<ResponseModel<PhoneBookBase>> UpdatePhoneBook(PhoneBookBase phoneBookModel);
        Task<ResponseModel<PhoneBookBase>> DeletePhoneBook(PhoneBookBase phoneBookModel);
        Task<IEnumerable<PhoneBookBase>> ReadAllPhoneBooks();

        Task<IEnumerable<PhoneBookBase>> OrderByFirtLastNamePhoneBooks(bool firstNameOrder = false, bool lastNameOrder = false);
    }
}
