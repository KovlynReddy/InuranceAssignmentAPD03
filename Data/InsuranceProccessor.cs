using InsuranceDLL.DataAccess.DomainModels;
using InsuranceDLL.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InuranceAssignmentAPD03.Data
{
    public class InsuranceProccessor : IInsuranceDB
    {
        private readonly ApplicationDbContext db;

        public InsuranceProccessor(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Account AddAccount(Account entity)
        {
            db.Accounts.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public Feature AddFeature(Feature entity)
        {
            db.Features.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public Policy AddPolicy(Policy entity)
        {
            db.Policies.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public Profile AddProfile(Profile entity)
        {
            db.Profiles.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public Transaction AddTransaction(Transaction entity)
        {
            db.Transactions.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public User AddUser(User entity)
        {
            db.MyUsers.Add(entity);

            db.SaveChanges();

            return entity;
        }

        public Account DeleteAccount(Account entity)
        {
            db.Accounts.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public Feature DeleteFeature(Feature entity)
        {
            db.Features.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public Policy DeletePolicy(Policy entity)
        {
            db.Policies.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public Profile DeleteProfile(Profile entity)
        {
            db.Profiles.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public Transaction DeleteTransaction(Transaction entity)
        {
            db.Transactions.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public User DeleteUser(User entity)
        {
            db.MyUsers.Remove(entity);

            db.SaveChanges();

            return entity;
        }

        public List<Account> FilterAccounts(string query)
        {
            throw new NotImplementedException();
        }

        public List<Feature> FilterFeatures(string query)
        {
            throw new NotImplementedException();
        }

        public List<Policy> FilterPolicys(string query)
        {
            throw new NotImplementedException();
        }

        public List<Profile> FilterProfiles(string query)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> FilterTransactions(string query)
        {
            throw new NotImplementedException();
        }

        public List<User> FilterUsers(string query)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(Account entity)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(string entity)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllAccounts()
        {
            return db.Accounts.ToList();
        }

        public List<Feature> GetAllFeatures()
        {
            return db.Features.ToList();
        }

        public List<Policy> GetAllPolicys()
        {
            return db.Policies.ToList();
        }

        public List<Profile> GetAllProfiles()
        {
            return db.Profiles.ToList();
        }

        public List<Transaction> GetAllTransactions()
        {
            return db.Transactions.ToList();
        }

        public List<User> GetAllUsers()
        {
            return db.MyUsers.ToList();
        }

        public Feature GetFeature(Feature entity)
        {
            return db.Features.FirstOrDefault(m=>m.FeatureId == entity.FeatureId);
        }

        public Feature GetFeature(string entity)
        { return db.Features.FirstOrDefault(m => m.FeatureId == entity); }

        public Policy GetPolicy(Policy entity)
        { return db.Policies.FirstOrDefault(m => m.PolicyId == entity.PolicyId); }

        public Policy GetPolicy(string entity)
        { return db.Policies.FirstOrDefault(m => m.PolicyId == entity); }

        public Profile GetProfile(Profile entity)
        { return db.Profiles.FirstOrDefault(m => m.ProfileId == entity.ProfileId); }

        public Profile GetProfile(string entity)
        { return db.Profiles.FirstOrDefault(m => m.ProfileId == entity); }

        public Transaction GetTransaction(Transaction entity)
        {
            return db.Transactions.FirstOrDefault(m => m.TransactionId == entity.TransactionId);
        }

        public Transaction GetTransaction(string entity)
        {
            return db.Transactions.FirstOrDefault(m => m.TransactionId == entity);
        }

        public User GetUser(User entity)
        { return db.MyUsers.FirstOrDefault(m => m.UserId == entity.UserId); }

        public User GetUser(string entity)
        { return db.MyUsers.FirstOrDefault(m => m.UserId == entity); }

        public Account UpdateAccount(Account entity)
        {
            var selectedentity = db.Accounts.FirstOrDefault(m => m.AccountId == entity.AccountId);
            selectedentity.AccountId = entity.AccountId;
            selectedentity.Balance = entity.Balance;
            selectedentity.UserId = entity.UserId;

            //selectedentity.Remarks = entity.EmployeeName;

            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }

        public Feature UpdateFeature(Feature entity)
        {
            var selectedentity = db.Features.FirstOrDefault(m => m.FeatureId == entity.FeatureId);
            selectedentity.FeatureId = entity.FeatureId;
         

            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }

        public Policy UpdatePolicy(Policy entity)
        {
            var selectedentity = db.Policies.FirstOrDefault(m => m.PolicyId == entity.PolicyId);
            selectedentity.PolicyId = entity.PolicyId;


            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }

        public Profile UpdateProfile(Profile entity)
        {
            var selectedentity = db.Profiles.FirstOrDefault(m => m.ProfileId == entity.ProfileId);
            selectedentity.ProfileId = entity.ProfileId;


            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }

        public Transaction UpdateTransaction(Transaction entity)
        {
            var selectedentity = db.Transactions.FirstOrDefault(m => m.TransactionId == entity.TransactionId);
            selectedentity.Notes = entity.Notes;


            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }

        public User UpdateUser(User entity)
        {
            var selectedentity = db.MyUsers.FirstOrDefault(m => m.UserId == entity.UserId);
            selectedentity.UserId = entity.UserId;

            db.Entry(selectedentity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();

            return selectedentity;
        }
    }
}
