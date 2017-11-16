using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using InsureThatAPI.CommonMethods;
namespace InsureThatAPI.Controllers
{
    public class TravelController : Controller
    {
        // GET: Travel
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TravelCover(int? cid)
        {
            NewPolicyDetailsClass Tmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcTcList = new List<SelectListItem>();
            ExcTcList = Tmodel.excessRate();
            TravelCover TravelCover = new TravelCover();
            TravelCover.TravellerscoveredObj = new TravellersToBeCovered();
            TravelCover.TravellerscoveredObj.EiId = 61429;
            TravelCover.DataofbirthObj = new DataOfBirthsTC();
            TravelCover.DataofbirthObj.EiId = 61431;
            TravelCover.NumbertravelersObj = new NumberOfTravelers();
            TravelCover.NumbertravelersObj.EiId = 61433;
            TravelCover.YourtripObj = new YourTrips();
            TravelCover.YourtripObj.EiId = 61437;
            TravelCover.WintersportObj = new WinterSports();
            TravelCover.WintersportObj.EiId = 61441;
            TravelCover.ExcessObj = new ExcessesTC();
            TravelCover.ExcessObj.EiId = 61443;
            TravelCover.ExcessObj.ExcessList = ExcTcList;
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId))
                {
                    TravelCover.TravellerscoveredObj.Travellerscovered = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.TravellerscoveredObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.DataofbirthObj.EiId))
                {
                    TravelCover.DataofbirthObj.Dataofbirth = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.DataofbirthObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId))
                {
                    TravelCover.NumbertravelersObj.Numbertravelers = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.NumbertravelersObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.YourtripObj.EiId))
                {
                    TravelCover.YourtripObj.Yourtrip = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.YourtripObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.WintersportObj.EiId))
                {
                    TravelCover.WintersportObj.Wintersport = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.WintersportObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == TravelCover.ExcessObj.EiId))
                {
                    TravelCover.ExcessObj.Excess = Convert.ToString(details.Where(q => q.QuestionId == TravelCover.ExcessObj.EiId).FirstOrDefault().Answer);
                }
            }
                return View(TravelCover);
        }
        [HttpPost]
        public ActionResult TravelCover(TravelCover TravelCover, int? cid)
        {
            NewPolicyDetailsClass Tmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcTcList = new List<SelectListItem>();
            ExcTcList = Tmodel.excessRate();
            TravelCover.ExcessObj.ExcessList = ExcTcList;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (TravelCover.TravellerscoveredObj.EiId > 0 && TravelCover.TravellerscoveredObj.Travellerscovered != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.TravellerscoveredObj.EiId, TravelCover.TravellerscoveredObj.Travellerscovered.ToString());
                }
                if (TravelCover.DataofbirthObj.EiId > 0 && TravelCover.DataofbirthObj.Dataofbirth != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.DataofbirthObj.EiId, TravelCover.DataofbirthObj.Dataofbirth.ToString());
                }
                if (TravelCover.NumbertravelersObj.EiId > 0 && TravelCover.NumbertravelersObj.Numbertravelers != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.NumbertravelersObj.EiId, TravelCover.NumbertravelersObj.Numbertravelers.ToString());
                }
                if (TravelCover.YourtripObj.EiId > 0 && TravelCover.YourtripObj.Yourtrip != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.YourtripObj.EiId, TravelCover.YourtripObj.Yourtrip.ToString());
                }
                if (TravelCover.WintersportObj.EiId > 0 && TravelCover.WintersportObj.Wintersport != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.WintersportObj.EiId, TravelCover.WintersportObj.Wintersport.ToString());
                }
                if (TravelCover.ExcessObj.EiId > 0 && TravelCover.ExcessObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(TravelCover.CustomerId, 1, TravelCover.ExcessObj.EiId, TravelCover.ExcessObj.Excess.ToString());
                }
            }
            return View(TravelCover);
        }
    }
}