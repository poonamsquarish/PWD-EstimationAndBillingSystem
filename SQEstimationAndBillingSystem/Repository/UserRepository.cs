using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Interfaces;
using SQEstimationAndBillingSystem.Models;
using SquarishSQEstimationAndBillingSystem.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SQEstimationAndBillingSystem.Repository
{
    public class UserRepository: IUserRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public UserRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public UserModel ValidateUser(string UserName, string Password)
        {
            return _dbContext.Database.SqlQuery<UserModel>("Validate_User @Username, @Password",
                    new SqlParameter("Username", DbNullIfNull(UserName)),
                    new SqlParameter("Password", DbNullIfNull(Password))
                    ).FirstOrDefault();
        }


        public UserModel GetUserById(int ID)
        {
            return _dbContext.Database.SqlQuery<UserModel>("SQSPGetUserById @ID", new SqlParameter("ID", ID)).FirstOrDefault();
        }

        public object DbNullIfNull(object obj)
        {
            return obj != null ? obj : DBNull.Value;
        }

        public string AddEditUser(UserModel ObjUserModel)
        {
         

            return  _dbContext.Database.SqlQuery<string>("SQSPAddEditUser @ID, @FirstName, @MiddleName, @LastName, @PhoneNumber, @Email, @IsActive, @RoleID, @UserName, @Password, @Department",
                new SqlParameter("ID", ObjUserModel.ID),
                new SqlParameter("FirstName", ObjUserModel.FirstName),
                new SqlParameter("MiddleName", ObjUserModel.MiddleName),
                new SqlParameter("LastName", ObjUserModel.LastName),
                new SqlParameter("PhoneNumber", ObjUserModel.PhoneNumber),
                new SqlParameter("Email", DbNullIfNull(ObjUserModel.Email)),
                new SqlParameter("IsActive", ObjUserModel.IsActive),
                new SqlParameter("RoleID", ObjUserModel.RoleID),
                new SqlParameter("UserName", ObjUserModel.UserName),
                new SqlParameter("Password", SaltPasswordHelper.ComputeHash(ObjUserModel.Password, "SHA512", null)),
                new SqlParameter("Department", DbNullIfNull(ObjUserModel.Department))

                  ).FirstOrDefault();

        }

        public List<UserModel> GetAllUser()
        {

            return _dbContext.Database.SqlQuery<UserModel>("SQSPGetAllUser").ToList();
        }

        public string DeleteUserById(int ID)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteUserById @ID", new SqlParameter("ID", ID)).FirstOrDefault();

        }
    }
}