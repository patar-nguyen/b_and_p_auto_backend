﻿using LearningAPIs.Model;

namespace LearningAPIs.Service.UserAccountService
{
    public interface IUserAccountService
    {
        IEnumerable<UserAccount> Get();
        UserAccount GetUserAccount(string username = "", string emailAddress = "");
        UserAccount CreateUserAccount(UserAccountRequest request);
        UserAccount GetUserAccountById(Guid userId);

        UserAccount UpdateUserAccount(Guid userId, UserAccountPatchRequest request);
    }
}
