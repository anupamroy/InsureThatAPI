using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
namespace InsureThatAPI.Controllers
{
    public class UserDetailsController : ApiController
    {
        // GET: api/UserDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        ///  GET USER DETAILS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        #region  GET METHOD FOR USER DETAILS
        // GET: api/UserDetails/5
        public UserDetailsRef Get(int id)
        {
            UserDetailsRef userDetailsref = new UserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            try
            {
                userDetailsref = userDetails.GetUserDetails(id);
                return userDetailsref;
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return userDetailsref;
        }
        #endregion


        #region INSERT USER DETAILS POST METHOD
        // POST: api/UserDetails
        public UserDetailsRef Post([FromBody]UserDetails value)
        {
            UserDetailsRef userDetailsref = new UserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            List<string> Errors = new List<string>();
            userDetailsref.ErrorMessage = new List<string>();
            if (string.IsNullOrWhiteSpace(value.UserName.Trim()))
            {
                Errors.Add("User Name is required");
            }
            if (string.IsNullOrWhiteSpace(value.FirstName.Trim()))
            {
                Errors.Add("First Name is required");
            }
            if (string.IsNullOrWhiteSpace(value.LastName.Trim()))
            {
                Errors.Add("Last Name is required");
            }
            if (value.AddressID == null || value.AddressID <= 0)
            {
                Errors.Add("AddressID is required");
            }
            if (value.PostalAddressID == null || value.PostalAddressID <= 0)
            {
                Errors.Add("Postal AddressID is required");
            }
            if (string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
            {
                Errors.Add("Phone Number is required");
                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
            {

                Errors.Add("Mobile Number is required");
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (value.DOB == null)
            {
                Errors.Add("DOB is required");
            }
            if (Errors != null && Errors.Count() > 0)
            {
                userDetailsref.Status = "Failure";
                userDetailsref.ErrorMessage = Errors;
                return userDetailsref;
            }
            else
            {
                int? result = userDetails.InsertUpdateUserDetails(null, value);
                if (result.HasValue && result > 0)
                {
                    userDetailsref.UserData = new UserDetails();
                    userDetailsref.Status = "Success";
                    userDetailsref.UserData.UserID = result.Value;
                }
                else if (result.HasValue && result == (int)InsuredResult.Exception)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Failed to insert.");

                }

                else if (result.HasValue && result == (int)InsuredResult.EmailAlreadyExists)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Email Id already exists.");

                }
            }
            return userDetailsref;
        }

        #endregion


        #region UPDATE USER DETAILS PUT METHOD
        // PUT: api/UserDetails/5
        public UserDetailsRef Put(int id, [FromBody]UserDetails value)
        {
            UserDetailsRef userDetailsref = new UserDetailsRef();
            UserDetailsClass userDetails = new UserDetailsClass();
            List<string> Errors = new List<string>();
            userDetailsref.ErrorMessage = new List<string>();
            if (string.IsNullOrWhiteSpace(value.UserName.Trim()))
            {
                Errors.Add("User Name is required");
            }
            if (string.IsNullOrWhiteSpace(value.FirstName.Trim()))
            {
                Errors.Add("First Name is required");
            }
            if (string.IsNullOrWhiteSpace(value.LastName.Trim()))
            {
                Errors.Add("Last Name is required");
            }
            if (value.AddressID == null || value.AddressID <= 0)
            {
                Errors.Add("AddressID is required");
            }
            if (value.PostalAddressID == null || value.PostalAddressID <= 0)
            {
                Errors.Add("Postal AddressID is required");
            }
            if (string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
            {
                Errors.Add("Phone Number is required");
                if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
            {

                Errors.Add("Mobile Number is required");
                if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                {
                    Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                }
            }
            if (value.DOB == null)
            {
                Errors.Add("DOB is required");
            }
            if (Errors != null && Errors.Count() > 0)
            {
                userDetailsref.Status = "Failure";
                userDetailsref.ErrorMessage = Errors;
                return userDetailsref;
            }
            else
            {
                int? result = userDetails.InsertUpdateUserDetails(id, value);
                if (result.HasValue && result > 0)
                {
                    userDetailsref.Status = "Success";
                  
                }
                else if (result.HasValue && result == (int)InsuredResult.Exception)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Failed to insert.");
                }
                else if (result.HasValue && result == (int)InsuredResult.EmailAlreadyExists)
                {
                    userDetailsref.Status = "Failure";
                    userDetailsref.ErrorMessage.Add("Email Id already exists.");

                }
            }
            return userDetailsref;
        }

        #endregion
        // DELETE: api/UserDetails/5
        public void Delete(int id)
        {
        }
    }
}
