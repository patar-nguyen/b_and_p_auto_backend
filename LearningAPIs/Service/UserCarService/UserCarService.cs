using Microsoft.Data.SqlClient;
using LearningAPIs.Model;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Dapper;

namespace LearningAPIs.Service.UserCarService
{
    public class UserCarService : IUserCarService
    {

        public UserCar CreateUserCar(UserCarRequest request)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserCar(Guid userId, int carId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCar> GetUserCars()
        {
            throw new NotImplementedException();
        }
    }
}
