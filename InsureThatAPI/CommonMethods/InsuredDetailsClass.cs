using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
namespace InsureThatAPI.CommonMethods
{
    public class EnumInsuredDetails
    {
        //Enum 
        #region Enum for Insured Details 
       public enum InsuredResult {
            Exception =-1,
            EmailAlreadyExists=-3,
            InsertedSuccess=1,
            UpdatedSuccess=2,
            NoAction=0,
            PhoneNumberLength=10
        };
        #endregion
    }
    public class InsuredDetailsClass
    {

        #region GET INSURED CUSTOMER DETAILS
        /// <summary>
        /// Get Customer Details by passing Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public GetInsuredDetailsRef GetInsuredDetails(string emailid,string name, string phoneno)
        {
            GetInsuredDetailsRef insuredref = new GetInsuredDetailsRef();
            InsuredDetails insureddetailsmodel = new InsuredDetails();
            MasterDataEntities db = new MasterDataEntities();
            insuredref.ErrorMessage = new List<string>();
            try
            {
               
                if(emailid=="")
                {
                    emailid = null;
                }
                if(name=="")
                {
                    name = null;
                }
                if(phoneno=="")
                {
                    phoneno = null;
                }

                    var Insured = db.IT_CC_GET_InsuredDetails(emailid,name,phoneno).FirstOrDefault();
                    if (Insured != null)
                    {

                        insureddetailsmodel.ClientType = Convert.ToInt16(Insured.ClientType);
                        insureddetailsmodel.InsuredID = Insured.InsuredID;
                        insureddetailsmodel.Title = Insured.Title;
                        insureddetailsmodel.FirstName = Insured.FirstName;
                        insureddetailsmodel.LastName = Insured.LastName;
                        insureddetailsmodel.MiddleName = Insured.MiddleName;
                        insureddetailsmodel.CompanyBusinessName = Insured.CompanyBusinessName;
                        insureddetailsmodel.TradingName = Insured.TradingName;
                        insureddetailsmodel.ABN = Insured.ABN;
                        insureddetailsmodel.AddressID = Convert.ToInt16(Insured.AddressID);
                        insureddetailsmodel.PostalAddressID = Convert.ToInt16(Insured.PostalAddressID);
                        insureddetailsmodel.PhoneNo = Insured.PhoneNo;
                        insureddetailsmodel.MobileNo = Insured.MobileNo;
                        insureddetailsmodel.DOB = Convert.ToDateTime(Insured.DOB);
                        insureddetailsmodel.EmailID = Insured.EmailID;
                        insuredref.Insureds.Add(insureddetailsmodel);
                        insuredref.Status = "Success";
                    }
                    else
                    {

                        insuredref.ErrorMessage.Add("No Data Available");
                        insuredref.Status = "Failure";
                    }
                //}
                //else
                //{
                //    insuredref.ErrorMessage.Add("EmailId is mandatory");
                //    insuredref.Status = "Failure";
                //    return insuredref;
                //}
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return insuredref;
        }
        #endregion

        #region INSURT AND UPDATE INSURED CUSTOMER DETAILS
        /// <summary>
        /// Get Customer Details by passing Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public int? InsertUpdateInsuredDetails(int? ID,InsuredDetails insureddetailsmodel)
        {
            int? result = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                if (ID.HasValue && ID>0)
                {//UPDATE the Insured Details
                    result = db.IT_CC_Insert_InsuredDetails(ID, insureddetailsmodel.ClientType, insureddetailsmodel.Title, insureddetailsmodel.FirstName, insureddetailsmodel.LastName, insureddetailsmodel.MiddleName, insureddetailsmodel.CompanyBusinessName, insureddetailsmodel.TradingName, insureddetailsmodel.ABN, insureddetailsmodel.AddressID, insureddetailsmodel.PostalAddressID, insureddetailsmodel.PhoneNo, insureddetailsmodel.MobileNo, insureddetailsmodel.DOB, insureddetailsmodel.EmailID).SingleOrDefault();
                }
                else
                {
                    //INSERT the Insured details
                    result = db.IT_CC_Insert_InsuredDetails(null, insureddetailsmodel.ClientType, insureddetailsmodel.Title, insureddetailsmodel.FirstName, insureddetailsmodel.LastName, insureddetailsmodel.MiddleName, insureddetailsmodel.CompanyBusinessName, insureddetailsmodel.TradingName, insureddetailsmodel.ABN, insureddetailsmodel.AddressID, insureddetailsmodel.PostalAddressID, insureddetailsmodel.PhoneNo, insureddetailsmodel.MobileNo, insureddetailsmodel.DOB, insureddetailsmodel.EmailID).SingleOrDefault();
                }

            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return result;
        }
        #endregion

        #region AAA
        #endregion
    }
}