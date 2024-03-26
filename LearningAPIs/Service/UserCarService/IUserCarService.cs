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
    public interface IUserCarService
    {
        IEnumerable<UserCar> GetUserCars();
        UserCar CreateUserCar(UserCarRequest request);
        bool DeleteUserCar(Guid userId, int carId);
    }
}
