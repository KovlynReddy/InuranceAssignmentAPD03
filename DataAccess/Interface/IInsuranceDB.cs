using InsuranceDLL.DataAccess.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceDLL.DataAccess.Interface
{
    public interface IInsuranceDB
    {
        #region User
        User AddUser(User entity);
        User UpdateUser(User entity);
        User GetUser(User entity);
        User GetUser(string entity);
        User DeleteUser(User entity);
        List<User> GetAllUsers();
        List<User> FilterUsers(string query);
        #endregion

        #region Transaction
        Claim AddClaim(Claim entity);
        Claim UpdateClaim(Claim entity);
        Claim GetClaim(Claim entity);
        Claim GetClaim(string entity);
        Claim DeleteClaim(Claim entity);
        List<Claim> GetAllClaims();
        List<Claim> FilterClaims(string query);
        #endregion
    #region Transaction
        Transaction AddTransaction(Transaction entity);
        Transaction UpdateTransaction(Transaction entity);
        Transaction GetTransaction(Transaction entity);
        Transaction GetTransaction(string entity);
        Transaction DeleteTransaction(Transaction entity);
        List<Transaction> GetAllTransactions();
        List<Transaction> FilterTransactions(string query);
        #endregion

        #region Account
        Account AddAccount(Account entity);
        Account UpdateAccount(Account entity);
        Account GetAccount(Account entity);
        Account GetAccount(string entity);
        Account DeleteAccount(Account entity);
        List<Account> GetAllAccounts();
        List<Account> FilterAccounts(string query);
        #endregion

        #region Feature

        Feature AddFeature(Feature entity);
        Feature UpdateFeature(Feature entity);
        Feature GetFeature(Feature entity);
        Feature GetFeature(string entity);
        Feature DeleteFeature(Feature entity);
        List<Feature> GetAllFeatures();
        List<Feature> FilterFeatures(string query);
        #endregion

        #region Policy
        Policy AddPolicy(Policy entity);
        Policy UpdatePolicy(Policy entity);
        Policy GetPolicy(Policy entity);
        Policy GetPolicy(string entity);
        Policy DeletePolicy(Policy entity);
        List<Policy> GetAllPolicys();
        List<Policy> FilterPolicys(string query);
        #endregion

        #region Profile
        Profile AddProfile(Profile entity);
        Profile UpdateProfile(Profile entity);
        Profile GetProfile(Profile entity);
        Profile GetProfile(string entity);
        Profile DeleteProfile(Profile entity);
        List<Profile> GetAllProfiles();
        List<Profile> FilterProfiles(string query);
        #endregion

    }
}
