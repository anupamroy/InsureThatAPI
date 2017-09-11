using InsureThatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.CommonMethods
{
    public class NewPolicyDetailsClass
    {
        /// <summary>
        /// GET NEW POLICY DETAILS
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        #region GET METHOD FOR NEW POLICY DETAILS

        public GetNewPolicyDetailsRef GetPolicyDetails(int ID)
        {
            GetNewPolicyDetailsRef policyDetailsRef = new GetNewPolicyDetailsRef();
            try
            {
                NewPolicyDetails policyDetailsModel = new NewPolicyDetails();
                MasterDataEntities db = new MasterDataEntities();
                policyDetailsRef.ErrorMessage = new List<string>();

                var item = db.IT_CC_GET_NewPolicyDetails(ID).FirstOrDefault();
                if (item!=null)
                {                 
                    
                        policyDetailsModel = new NewPolicyDetails();
                        policyDetailsModel.PcId = item.PcId;
                        policyDetailsModel.TrId = item.TrId;
                        policyDetailsModel.ProductID = item.ProductID;
                        policyDetailsModel.Timestamp = item.TimeStamp;
                        policyDetailsModel.RemoveStampDuty = item.RemoveStampDuty;
                        policyDetailsModel.PolicyStatus = item.PolicyStatus;
                        policyDetailsModel.PolicyNumber = item.PolicyNumber;
                        policyDetailsModel.InceptionDate = item.InceptionDate;
                        policyDetailsModel.FloodCover = item.FloodCover;
                        policyDetailsModel.ExpiryDate = item.ExpiryDate;
                        policyDetailsModel.EffectiveDate = item.EffectiveDate;
                        policyDetailsModel.CreatedByUserID = item.createdByUserID;
                        policyDetailsModel.CoverPeriodUnit = item.CoverPeriodUnit;
                        policyDetailsModel.CoverPeriod = item.CoverPeriod;
                        policyDetailsModel.IsClaimed = item.Is_claimed;
                        policyDetailsModel.AccountManagerID = item.AccountManagerID;
                        policyDetailsModel.Reason = item.Reason_for_cancelletion;
                        policyDetailsModel.Broker = item.Broker;
                        policyDetailsModel.IsClaimed = item.Is_claimed;
                        policyDetailsModel.PolicyDetailsID = item.PolicyDetailsID;
                        policyDetailsRef.PolicyDetailsData = policyDetailsModel;
                        policyDetailsRef.Status = "Success";
                    
                }
                else
                {
                    policyDetailsRef.ErrorMessage.Add("No Data Available");
                    policyDetailsRef.Status = "Failure";
                }
            }
            catch (Exception xp)
            {
                policyDetailsRef.ErrorMessage.Add(xp.Message);
                policyDetailsRef.Status = "Failure";
            }
            finally
            {

            }
            return policyDetailsRef;
        }

        #endregion

        #region UPDATE METHOD FOR NEW POLICY DETAILS
        public int UpdatePolicyDetails(int ID, NewPolicyDetails policyDetailsModel)
        {
            int icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_UPDATE_NewPolicyDetails(ID, policyDetailsModel.EffectiveDate, policyDetailsModel.Reason, policyDetailsModel.FloodCover);
            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount;
        }

        #endregion

        #region INSERT METHOD FOR NEW POLICY DETAILS

        public int InsertPolicyDetails(NewPolicyDetails policyDetailsModel)
        {
            int? icount = 0;
            try
            {
                MasterDataEntities db = new MasterDataEntities();
                icount = db.IT_CC_InsertNewPolicyDetails(policyDetailsModel.PcId, policyDetailsModel.TrId, policyDetailsModel.PolicyNumber, policyDetailsModel.Broker, policyDetailsModel.AccountManagerID, policyDetailsModel.PolicyStatus, policyDetailsModel.CoverPeriod, policyDetailsModel.CoverPeriodUnit, policyDetailsModel.InceptionDate, policyDetailsModel.ExpiryDate, policyDetailsModel.EffectiveDate, policyDetailsModel.ProductID, policyDetailsModel.FloodCover, policyDetailsModel.IsClaimed, policyDetailsModel.RemoveStampDuty, policyDetailsModel.Reason, policyDetailsModel.CreatedByUserID, policyDetailsModel.Timestamp, policyDetailsModel.PolicyDetailsID).FirstOrDefault();

            }
            catch (Exception xp)
            {

            }
            finally
            {

            }
            return icount.Value;
        }

        #endregion

    }
}