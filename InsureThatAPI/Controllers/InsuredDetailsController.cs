using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
using static InsureThatAPI.CommonMethods.EnumInsuredDetails;
using System.Text.RegularExpressions;

namespace InsureThatAPI.Controllers
{
    public class InsuredDetailsController : ApiController
    {
        // GET: api/InsuredDetails
        public IEnumerable<string> Get()
        {
            return new string[] { "Enter valid URL", "Enter search parameters emailId,name,Phoneno" };
        }
        /// <summary>
        /// Get customer details by searching through email id
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        // GET: api/InsuredDetails/5
        #region GET CUSTOMER DETAILS BY SEARCHING THROUGH EMAILID
        [HttpGet]
        public GetInsuredDetailsRef Get(string emailId, string name, string phoneno)
        {

            GetInsuredDetailsRef insuredref = new GetInsuredDetailsRef();
            InsuredDetailsClass insureddetails = new InsuredDetailsClass();
            insuredref = insureddetails.GetInsuredDetails(emailId, name, phoneno);
            return insuredref;
        }
        #endregion

        // POST: api/InsuredDetails
        public InsuredDetailsRef Post([FromBody]InsuredDetails value)
        {
            InsuredDetailsClass insureddetails = new InsuredDetailsClass();
            EnumInsuredDetails.InsuredResult resultEnum = new EnumInsuredDetails.InsuredResult();
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            InsuredDetailsRef insuredref = new InsuredDetailsRef();
            MasterDataEntities db = new MasterDataEntities();
            List<string> Errors = new List<string>();
            insuredref.ErrorMessage = new List<string>();
            if (value != null)
            {

                if (value.ABN == null || value.ABN == string.Empty || string.IsNullOrWhiteSpace(value.ABN.Trim()))
                {
                    Errors.Add("ABN is required");
                }
                else
                {

                    string justStrings = new String(value.ABN.Trim().Where(Char.IsLetter).ToArray());
                    if (value.ABN.Length > 14)
                    {
                        Errors.Add("ABN should not be greater than 14 characters.");
                    }
                    if (!regexItem.IsMatch(value.ABN.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in ABN");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("ABN allows only numerc values.");
                    }
                }

                if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
                {
                    Errors.Add("EmailID is required");
                }
                if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
                {
                    Errors.Add("EmailID is not valid");
                }
                if (value.EmailID != null && value.EmailID.Length > 50)
                {
                    Errors.Add("Length of EmailId should not exceed 50 characters.");
                }
                if (value.ClientType == null || value.ClientType <= 0)
                {
                    Errors.Add("Client Type is required");
                }

                if (value.Title == null || value.Title == string.Empty || string.IsNullOrWhiteSpace(value.Title.Trim()))
                {
                    Errors.Add("Title is required");
                }
                else
                {
                    string justNumber = new String(value.Title.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.Title == "MR" || value.Title == "Mr" || value.Title == "Mrs" || value.Title == "MRS" || value.Title == "Miss" || value.Title == "MISS")
                    {

                    }
                    else
                    {
                        Errors.Add("Title allows only MR,Miss,MRS");
                    }
                    if (!regexItem.IsMatch(value.Title.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Title");

                    }
                    if (value.Title.Length > 20)
                    {
                        Errors.Add("Title should not be greater than 20 characters.");
                    }
                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Title does not allow numerc values.");
                    }
                }
                if (value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim()))
                {
                    Errors.Add("First Name is required");
                }
                else
                {
                    string justNumber = new String(value.FirstName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.FirstName.Length > 20)
                    {
                        Errors.Add("First Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.FirstName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in First Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("First Name does not allow numerc values.");
                    }
                }
                if (value.Lastname == null || value.Lastname == string.Empty || string.IsNullOrWhiteSpace(value.Lastname.Trim()))
                {
                    Errors.Add("Last Name is required");
                }
                else
                {
                    string justNumber = new String(value.Lastname.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.Lastname.Length > 20)
                    {
                        Errors.Add("Last Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.Lastname.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Last Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Last Name does not allow numerc values.");
                    }
                }
                if (value.MiddleName == null || value.MiddleName == string.Empty || string.IsNullOrWhiteSpace(value.MiddleName.Trim()))
                {
                    Errors.Add("Middle Name is required");
                }
                else
                {
                    string justNumber = new String(value.MiddleName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.MiddleName.Length > 20)
                    {
                        Errors.Add("Middle Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.MiddleName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Middle Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Middle Name does not allow numerc values.");
                    }
                }
                if (value.AddressID == null || value.AddressID <= 0)
                {
                    Errors.Add("AddressID is required");
                }
                else
                {

                    if (!regexItem.IsMatch(value.AddressID.ToString()))
                    {
                        Errors.Add("Special characters are not allowed in AddressID ");
                    }
                }
                if (value.PostalAddressID == null || value.PostalAddressID <= 0)
                {
                    Errors.Add("Postal AddressID is required");
                }
                else
                {

                    if (!regexItem.IsMatch(value.PostalAddressID.ToString()))
                    {
                        Errors.Add("Special characters are not allowed in Postal AddressID ");
                    }
                }
                if (value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
                {
                    Errors.Add("Phone Number is required");

                }
                else
                {

                    string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != 9 && value.PhoneNo.Length != 9)
                    {
                        Errors.Add("Phone number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                    }

                    if (!regexItem.IsMatch(value.PhoneNo.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Phone number");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("Phone number allows only numerc values.");
                    }

                    if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                    }
                }


                if (value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
                {

                    Errors.Add("Mobile Number is required");


                }
                else
                {
                    string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("mobile number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                    }

                    if (!regexItem.IsMatch(value.MobileNo.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Mobile number");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("Mobile number allows only numerc values.");
                    }
                    if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                    }
                }
                if (value.DOB == null)
                {
                    Errors.Add("DOB is required");
                }
                else
                {
                    string justStrings = value.DOB.ToString();
                    if (justStrings == default(DateTime).ToString())
                    {
                        Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                    }
                    else
                    {
                        string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
                        DateTime dDate;
                        if (DateTime.TryParse(inputString, out dDate))
                        {
                            //valid
                        }
                        else
                        {
                            Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                        }
                    }

                }
                if (Errors != null && Errors.Count() > 0)
                {
                    insuredref.Status = "Failure";
                    insuredref.ErrorMessage = Errors;
                    return insuredref;
                }
                else
                {
                    int? result = insureddetails.InsertUpdateInsuredDetails(null, value);
                    if (result.HasValue && result > 0)
                    {

                        insuredref.Status = "Success";
                        insuredref.InsuredID = result.Value;
                    }
                    if (result.HasValue && result == 2)
                    {

                        insuredref.Status = "Success";
                        insuredref.InsuredID = value.InsuredID;
                    }
                    else if (result.HasValue && result == -4)
                    {

                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage.Add("Email Id and Phone number already exist.");

                    }

                    else if (result.HasValue && result == (int)InsuredResult.Exception)
                    {

                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage.Add("Failed to insert.");

                    }

                    else if (result.HasValue && result == (int)InsuredResult.EmailAlreadyExists)
                    {

                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage.Add("Email Id already exists.");

                    }
                }

            }

            else
            {
                insuredref.Status = "Failure";
            }
            return insuredref;
        }

        // PUT: api/InsuredDetails/5
        public InsuredDetailsRef Put(int id, [FromBody]InsuredDetails value)
        {
            int? result = 0;
            InsuredDetailsClass insureddetails = new InsuredDetailsClass();
            InsuredDetailsRef insuredref = new InsuredDetailsRef();
            List<string> Errors = new List<string>();
            Regex regemail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$"); // for confirm special charectors or not
            insuredref.ErrorMessage = new List<string>();
            if (value != null && id > 0)
            {

                if (value.ABN == null || value.ABN == string.Empty || string.IsNullOrWhiteSpace(value.ABN.Trim()))
                {
                    Errors.Add("ABN is required");
                }
                else
                {

                    string justStrings = new String(value.ABN.Trim().Where(Char.IsLetter).ToArray());
                    if (value.ABN.Length > 14)
                    {
                        Errors.Add("ABN should not be greater than 14 characters.");
                    }
                    if (!regexItem.IsMatch(value.ABN.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in ABN");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("ABN allows only numerc values.");
                    }
                }

                if (value.EmailID == null || value.EmailID == string.Empty || string.IsNullOrWhiteSpace(value.EmailID.Trim()))
                {
                    Errors.Add("EmailID is required");
                }
                if (value.EmailID != null && !regemail.IsMatch(value.EmailID))
                {
                    Errors.Add("EmailID is not valid");
                }
                if (value.EmailID != null && value.EmailID.Length > 50)
                {
                    Errors.Add("Length of EmailId should not exceed 50 characters.");
                }

                if (value.Title == null || value.Title == string.Empty || string.IsNullOrWhiteSpace(value.Title.Trim()))
                {
                    Errors.Add("Title is required");
                }
                else
                {
                    string justNumber = new String(value.Title.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());

                    if (value.Title == "MR" || value.Title == "Mr" || value.Title == "Mrs" || value.Title == "MRS" || value.Title == "Miss" || value.Title == "MISS")
                    {

                    }
                    else
                    {
                        Errors.Add("Title allows only MR, Miss,MRS");
                    }
                    if (!regexItem.IsMatch(value.Title.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Title");

                    }
                    if (value.Title.Length > 20)
                    {
                        Errors.Add("Title should not be greater than 20 characters.");
                    }
                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Title does not allow numeric values.");
                    }
                }
                if (value.FirstName == null || value.FirstName == string.Empty || string.IsNullOrWhiteSpace(value.FirstName.Trim()))
                {
                    Errors.Add("First Name is required");
                }
                else
                {
                    string justNumber = new String(value.FirstName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.FirstName.Length > 20)
                    {
                        Errors.Add("First Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.FirstName.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in First Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("First Name does not allow numerc values.");
                    }
                }
                if (value.Lastname == null || value.Lastname == string.Empty || string.IsNullOrWhiteSpace(value.Lastname.Trim()))
                {
                    Errors.Add("Last Name is required");
                }
                else
                {
                    string justNumber = new String(value.Lastname.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.Lastname.Length > 20)
                    {
                        Errors.Add("Last Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.Lastname.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Last Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Last Name does not allow numerc values.");
                    }
                }
                if (value.MiddleName == null || value.MiddleName == string.Empty || string.IsNullOrWhiteSpace(value.MiddleName.Trim()))
                {
                    Errors.Add("Middle Name is required");
                }
                else
                {
                    string justNumber = new String(value.MiddleName.Trim().Where(Char.IsDigit).ToArray());
                    // string justStrings = new String(value.Title.Trim().Where(Char.IsLetter).ToArray());
                    if (value.MiddleName.Length > 20)
                    {
                        Errors.Add("Middle Name should not be greater than 20 characters.");
                    }
                    if (!regexItem.IsMatch(value.Lastname.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Middle Name");

                    }

                    if (justNumber != null && justNumber != string.Empty)
                    {
                        Errors.Add("Middle Name does not allow numerc values.");
                    }
                }
                if (value.AddressID == null || value.AddressID <= 0)
                {
                    Errors.Add("AddressID is required");
                }
                else
                {

                    if (!regexItem.IsMatch(value.AddressID.ToString()))
                    {
                        Errors.Add("Special characters are not allowed in AddressID ");
                    }
                }
                if (value.PostalAddressID == null || value.PostalAddressID <= 0)
                {
                    Errors.Add("Postal AddressID is required");
                }
                else
                {

                    if (!regexItem.IsMatch(value.AddressID.ToString()))
                    {
                        Errors.Add("Special characters are not allowed in Postal AddressID ");
                    }
                }
                if (value.PhoneNo == null || value.PhoneNo == string.Empty || string.IsNullOrWhiteSpace(value.PhoneNo.Trim()))
                {
                    Errors.Add("Phone Number is required");

                }
                else
                {

                    string justNumber = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.PhoneNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != 9 && value.PhoneNo.Length != 9)
                    {
                        Errors.Add("Phone number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                    }

                    if (!regexItem.IsMatch(value.PhoneNo.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Phone number");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("Phone number allows only numerc values.");
                    }

                    if (value.PhoneNo.Count() > (int)InsuredResult.PhoneNumberLength || value.PhoneNo.Count() < (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("Phone Number is required, must not be more than 9 digits and less than 9 digits.");
                    }
                }
                string justNumbers = new String(value.PhoneNo.Trim().Where(Char.IsDigit).ToArray());// for getting digits from string

                if (value.MobileNo == null || value.MobileNo == string.Empty || string.IsNullOrWhiteSpace(value.MobileNo.Trim()))
                {

                    Errors.Add("Mobile Number is required");


                }
                else
                {
                    string justNumber = new String(value.MobileNo.Trim().Where(Char.IsDigit).ToArray());
                    string justStrings = new String(value.MobileNo.Trim().Where(Char.IsLetter).ToArray());
                    if (justStrings != null && justNumber.Length != (int)InsuredResult.PhoneNumberLength && value.MobileNo.Length != (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("mobile number allows only numerc values, must not be more than 9 digits and less than 9 digits.");
                    }

                    if (!regexItem.IsMatch(value.MobileNo.Trim()))
                    {
                        Errors.Add("Special characters are not allowed in Mobile number");
                    }
                    if (justStrings != "")
                    {
                        Errors.Add("Mobile number allows only numerc values.");
                    }
                    if (value.MobileNo.Count() > (int)InsuredResult.PhoneNumberLength || value.MobileNo.Count() < (int)InsuredResult.PhoneNumberLength)
                    {
                        Errors.Add("Mobile Number is required, must not be more than 9 digits and less than 9 digits.");
                    }
                }
                if (value.DOB == null)
                {
                    Errors.Add("DOB is required");
                }
                else
                {
                    string justStrings = value.DOB.ToString();
                    if (justStrings == default(DateTime).ToString())
                    {
                        Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                    }
                    else
                    {
                        string inputString = justStrings; //value.DOB.ToString("MM/dd/yyyy");
                        DateTime dDate;
                        if (DateTime.TryParse(inputString, out dDate))
                        {
                            //valid
                        }
                        else
                        {
                            Errors.Add("DOB is not valid. DOB format is MM/dd/yyyy");
                        }
                    }

                }
                if (Errors != null && Errors.Count() > 0)
                {
                    insuredref.Status = "Failure";
                    insuredref.ErrorMessage = Errors;

                }
                else
                {
                    if (id > 0)
                    {
                        result = insureddetails.InsertUpdateInsuredDetails(id, value);
                        if (result == (int)InsuredResult.UpdatedSuccess)
                        {
                            insuredref.Status = "Success";
                        }
                        if (result == (int)InsuredResult.Exception)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Failed to insert.");
                        }
                        if (result == -5)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("EmailId is already exist.");
                        }
                        if (result == -6)
                        {
                            insuredref.Status = "Failure";
                            insuredref.ErrorMessage.Add("Insured Id is not valid.");
                        }
                    }
                    else
                    {
                        insuredref.Status = "Failure";
                        insuredref.ErrorMessage.Add("Insured ID is required.");
                    }
                }
            }

            else
            {

                insuredref.Status = "Failure";
            }
            return insuredref;

        }

        // DELETE: api/InsuredDetails/5
        public void Delete(int id)
        {
        }
    }
}
