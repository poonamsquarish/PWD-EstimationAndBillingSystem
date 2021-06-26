using SQEstimationAndBillingSystem.Models;
using System.Collections.Generic;

namespace SQEstimationAndBillingSystem.Interfaces
{
    public interface IUserRepository
    {
        UserModel ValidateUser(string UserName, string Password);
        UserModel GetUserById(int ID);
        string AddEditUser(UserModel ObjUserModel);
        List<UserModel> GetAllUser();
        string DeleteUserById(int ID);
    }
}
