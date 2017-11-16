using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json;

namespace InsureThatAPI.Controllers
{
    public class FarmPolicyPropertyController : Controller
    {
        // GET: FarmPolicyProperty
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FarmLocationDetails(int? cid)
        {
            FarmLocationDetails FarmLocationDetails = new FarmLocationDetails();
            if (Session["completionTrackPFP"] != null)
            {
                Session["completionTrackPFP"] = Session["completionTrackPFP"];
                FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            else
            {
                Session["completionTrackPFP"] = "0-0-0-0-0"; ;
                FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
            }
            return View(FarmLocationDetails);
        }
        [HttpPost]
        public ActionResult FarmLocationDetails(int? cid, FarmLocationDetails FarmLocationDetails)
        {
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (Session["completionTrackPFP"] != null)
                {
                    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                    FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                    if (FarmLocationDetails.completionTrackPFP != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = FarmLocationDetails.completionTrackPFP.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackPFP"] = Completionstring;
                        FarmLocationDetails.completionTrackPFP = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackPFP"] = "1-0-0-0-0"; ;
                    FarmLocationDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                }
                return RedirectToAction("FarmDetails", new { cid = cid });
            }
            return View(FarmLocationDetails);
        }
        [HttpGet]
        public ActionResult FarmDetails(int? cid)
        {
            NewPolicyDetailsClass FarmDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> constructionTypeList = new List<SelectListItem>();
            constructionTypeList = FarmDetailsmodel.constructionType();
            FarmDetails FarmDetails = new FarmDetails();
            FarmDetails.DescriptionFBObj = new DetailedDescription();
            FarmDetails.DescriptionFBObj.EiId = 62255;
            FarmDetails.YearconstructedFBObj = new YearConstructedFB();
            FarmDetails.YearconstructedFBObj.EiId = 62257;
            FarmDetails.ConstructionFBObj = new ConstructionFB();
            FarmDetails.ConstructionFBObj.ConstructionHCList = constructionTypeList;
            FarmDetails.ConstructionFBObj.EiId = 62259;
            FarmDetails.ContaincoolroomObj = new ContainCoolroomFB();
            FarmDetails.ContaincoolroomObj.EiId = 62261;
            FarmDetails.SuminsuredFBObj = new SumInsuredsFB();
            FarmDetails.SuminsuredFBObj.EiId = 62263;
            FarmDetails.UnrepaireddamageObj = new UnrepairedDamageFS();
            FarmDetails.UnrepaireddamageObj.EiId = 62309;
            if (Session["completionTrackPFP"] != null)
            {
                Session["completionTrackPFP"] = Session["completionTrackPFP"];
                FarmDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            else
            {
                Session["completionTrackPFP"] = "0-0-0-0-0"; ;
                FarmDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FarmDetails.DescriptionFBObj.EiId))
                {
                    FarmDetails.DescriptionFBObj.Description = Convert.ToString(details.Where(q => q.QuestionId == FarmDetails.DescriptionFBObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmDetails.YearconstructedFBObj.EiId))
                {
                    FarmDetails.YearconstructedFBObj.Year = Convert.ToString(details.Where(q => q.QuestionId == FarmDetails.YearconstructedFBObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmDetails.ConstructionFBObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmDetails.ConstructionFBObj.EiId).FirstOrDefault();
                    FarmDetails.ConstructionFBObj.Construction = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == FarmDetails.ContaincoolroomObj.EiId))
                {
                    FarmDetails.ContaincoolroomObj.Coolroom = Convert.ToString(details.Where(q => q.QuestionId == FarmDetails.ConstructionFBObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmDetails.SuminsuredFBObj.EiId))
                {
                    FarmDetails.SuminsuredFBObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == FarmDetails.SuminsuredFBObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmDetails.UnrepaireddamageObj.EiId))
                {
                    FarmDetails.UnrepaireddamageObj.Unrepaireddamage = Convert.ToString(details.Where(q => q.QuestionId == FarmDetails.UnrepaireddamageObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(FarmDetails);
        }
        [HttpPost]
        public ActionResult FarmDetails(int? cid, FarmDetails FarmDetails)
        {
            NewPolicyDetailsClass FarmDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> constructionTypeList = new List<SelectListItem>();
            constructionTypeList = FarmDetailsmodel.constructionType();
            FarmDetails.ConstructionFBObj.ConstructionHCList = constructionTypeList;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FarmDetails.DescriptionFBObj.EiId > 0 && FarmDetails.DescriptionFBObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.DescriptionFBObj.EiId, FarmDetails.DescriptionFBObj.Description.ToString());
                }
                if (FarmDetails.YearconstructedFBObj.EiId > 0 && FarmDetails.YearconstructedFBObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.YearconstructedFBObj.EiId, FarmDetails.YearconstructedFBObj.Year.ToString());
                }
                if (FarmDetails.ConstructionFBObj.EiId > 0 && FarmDetails.ConstructionFBObj.Construction != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.ConstructionFBObj.EiId, FarmDetails.ConstructionFBObj.Construction.ToString());
                }
                if (FarmDetails.ContaincoolroomObj.EiId > 0 && FarmDetails.ContaincoolroomObj.Coolroom != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.ContaincoolroomObj.EiId, FarmDetails.ContaincoolroomObj.Coolroom.ToString());
                }
                if (FarmDetails.SuminsuredFBObj.EiId > 0 && FarmDetails.SuminsuredFBObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.SuminsuredFBObj.EiId, FarmDetails.SuminsuredFBObj.Suminsured.ToString());
                }
                if (FarmDetails.UnrepaireddamageObj.EiId > 0 && FarmDetails.UnrepaireddamageObj.Unrepaireddamage != null)
                {
                    db.IT_InsertCustomerQnsData(FarmDetails.CustomerId, 1, FarmDetails.YearconstructedFBObj.EiId, FarmDetails.UnrepaireddamageObj.Unrepaireddamage.ToString());
                }
                if (Session["completionTrackPFP"] != null)
                {
                    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                    FarmDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                    if (FarmDetails.completionTrackPFP != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = FarmDetails.completionTrackPFP.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackPFP"] = Completionstring;
                        FarmDetails.completionTrackPFP = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackPFP"] = "0-1-0-0-0"; ;
                    FarmDetails.completionTrackPFP = Session["completionTrackPFP"].ToString();
                }
                return RedirectToAction("FarmStructures", new { cid = cid });
            }
            return View(FarmDetails);
        }
        [HttpGet]
        public ActionResult FarmStructures(int? cid)
        {
            NewPolicyDetailsClass FarmStructuresmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessRateList = new List<SelectListItem>();
            ExcessRateList = FarmStructuresmodel.ExcessRateFS();
            FarmStructures FarmStructures = new FarmStructures();
            FarmStructures.SublimitObj = new SubLimitFarmFencing();
            FarmStructures.SublimitObj.EiId = 62283;
            FarmStructures.TotalcoverObj = new TotalCoverFarmFencing();
            FarmStructures.TotalcoverObj.EiId = 62287;
            FarmStructures.OtherstructurefcObj = new OtherFarmStructuresFC();
            FarmStructures.OtherstructurefcObj.EiId = 62291;
            FarmStructures.RoofwallsObj = new RoofAndWallsFS();
            FarmStructures.RoofwallsObj.EiId = 62297;
            //FarmStructures.UnrepaireddamageObj = new UnrepairedDamageFS();
            //FarmStructures.UnrepaireddamageObj.EiId = 62309;
            FarmStructures.ExcesshcObj = new HarvestedCropsExcess();
            FarmStructures.ExcesshcObj.ExcessHCList = ExcessRateList;
            FarmStructures.ExcesshcObj.EiId = 62315;
            if (Session["completionTrackPFP"] != null)
            {
                Session["completionTrackPFP"] = Session["completionTrackPFP"];
                FarmStructures.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            else
            {
                Session["completionTrackPFP"] = "0-0-0-0-0"; ;
                FarmStructures.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == FarmStructures.SublimitObj.EiId))
                {
                    FarmStructures.SublimitObj.Sublimit = Convert.ToString(details.Where(q => q.QuestionId == FarmStructures.SublimitObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmStructures.TotalcoverObj.EiId))
                {
                    FarmStructures.TotalcoverObj.Totalcover = Convert.ToString(details.Where(q => q.QuestionId == FarmStructures.TotalcoverObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmStructures.OtherstructurefcObj.EiId))
                {
                    FarmStructures.OtherstructurefcObj.Otherstructure = Convert.ToString(details.Where(q => q.QuestionId == FarmStructures.OtherstructurefcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == FarmStructures.RoofwallsObj.EiId))
                {
                    FarmStructures.RoofwallsObj.Roofwalls = Convert.ToString(details.Where(q => q.QuestionId == FarmStructures.RoofwallsObj.EiId).FirstOrDefault().Answer);
                }
                //if (details.Exists(q => q.QuestionId == FarmStructures.UnrepaireddamageObj.EiId))
                //{
                //    FarmStructures.UnrepaireddamageObj.Unrepaireddamage = Convert.ToString(details.Where(q => q.QuestionId == FarmStructures.UnrepaireddamageObj.EiId).FirstOrDefault().Answer);
                //}
                if (details.Exists(q => q.QuestionId == FarmStructures.ExcesshcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == FarmStructures.ExcesshcObj.EiId).FirstOrDefault();
                    FarmStructures.ExcesshcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(FarmStructures);
        }
        [HttpPost]
        public ActionResult FarmStructures(int? cid, FarmStructures FarmStructures)
        {
            NewPolicyDetailsClass FarmStructuresmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessRateList = new List<SelectListItem>();
            ExcessRateList = FarmStructuresmodel.ExcessRateFS();
            FarmStructures.ExcesshcObj.ExcessHCList = ExcessRateList;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (FarmStructures.SublimitObj.EiId > 0 && FarmStructures.SublimitObj.Sublimit != null)
                {
                    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.SublimitObj.EiId, FarmStructures.SublimitObj.Sublimit.ToString());
                }
                if (FarmStructures.TotalcoverObj.EiId > 0 && FarmStructures.TotalcoverObj.Totalcover != null)
                {
                    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.TotalcoverObj.EiId, FarmStructures.TotalcoverObj.Totalcover.ToString());
                }
                if (FarmStructures.OtherstructurefcObj.EiId > 0 && FarmStructures.OtherstructurefcObj.Otherstructure != null)
                {
                    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.TotalcoverObj.EiId, FarmStructures.OtherstructurefcObj.Otherstructure.ToString());
                }
                if (FarmStructures.RoofwallsObj.EiId > 0 && FarmStructures.RoofwallsObj.Roofwalls != null)
                {
                    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.RoofwallsObj.EiId, FarmStructures.RoofwallsObj.Roofwalls.ToString());
                }
                //if (FarmStructures.UnrepaireddamageObj.EiId > 0 && FarmStructures.UnrepaireddamageObj.Unrepaireddamage != null)
                //{
                //    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.UnrepaireddamageObj.EiId, FarmStructures.UnrepaireddamageObj.Unrepaireddamage.ToString());
                //}
                if (FarmStructures.ExcesshcObj.EiId > 0 && FarmStructures.ExcesshcObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(FarmStructures.CustomerId, 1, FarmStructures.ExcesshcObj.EiId, FarmStructures.ExcesshcObj.Excess.ToString());
                }
                if (Session["completionTrackPFP"] != null)
                {
                    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                    FarmStructures.completionTrackPFP = Session["completionTrackPFP"].ToString();
                    if (FarmStructures.completionTrackPFP != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = FarmStructures.completionTrackPFP.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackPFP"] = Completionstring;
                        FarmStructures.completionTrackPFP = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackPFP"] = "0-0-1-0-0"; ;
                    FarmStructures.completionTrackPFP = Session["completionTrackPFP"].ToString();
                }
                return RedirectToAction("HarvestedCrops", new { cid = cid });
            }
            return View(FarmStructures);
        }
        [HttpGet]
        public ActionResult HarvestedCrops(int? cid)
        {
            NewPolicyDetailsClass HarvestedCropsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessRateList = new List<SelectListItem>();
            ExcessRateList = HarvestedCropsmodel.excessRate();
            HarvestedCrops HarvestedCrops = new HarvestedCrops();
            HarvestedCrops.SuminsuredhcObj = new HarvestedCropsSumInsured();
            HarvestedCrops.SuminsuredhcObj.EiId = 62329;
            HarvestedCrops.ExcesshcObj = new HarvestedCropsExcess();
            HarvestedCrops.ExcesshcObj.ExcessHCList = ExcessRateList;
            HarvestedCrops.ExcesshcObj.EiId = 62331;
            if (Session["completionTrackPFP"] != null)
            {
                Session["completionTrackPFP"] = Session["completionTrackPFP"];
                HarvestedCrops.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            else
            {
                Session["completionTrackPFP"] = "0-0-0-0-0"; ;
                HarvestedCrops.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == HarvestedCrops.SuminsuredhcObj.EiId))
                {
                    HarvestedCrops.SuminsuredhcObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == HarvestedCrops.SuminsuredhcObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == HarvestedCrops.ExcesshcObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == HarvestedCrops.ExcesshcObj.EiId).FirstOrDefault();
                    HarvestedCrops.ExcesshcObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(HarvestedCrops);
        }
        [HttpPost]
        public ActionResult HarvestedCrops(int? cid, HarvestedCrops HarvestedCrops)
        {
            NewPolicyDetailsClass HarvestedCropsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessRateList = new List<SelectListItem>();
            ExcessRateList = HarvestedCropsmodel.excessRate();
            HarvestedCrops.ExcesshcObj.ExcessHCList = ExcessRateList;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (Session["completionTrackPFP"] != null)
                {
                    if (HarvestedCrops.SuminsuredhcObj.EiId > 0 && HarvestedCrops.SuminsuredhcObj.Suminsured != null)
                    {
                        db.IT_InsertCustomerQnsData(HarvestedCrops.CustomerId, 1, HarvestedCrops.SuminsuredhcObj.EiId, HarvestedCrops.SuminsuredhcObj.Suminsured.ToString());
                    }
                    if (HarvestedCrops.ExcesshcObj.EiId > 0 && HarvestedCrops.ExcesshcObj.Excess != null)
                    {
                        db.IT_InsertCustomerQnsData(HarvestedCrops.CustomerId, 1, HarvestedCrops.ExcesshcObj.EiId, HarvestedCrops.ExcesshcObj.Excess.ToString());
                    }
                    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                    HarvestedCrops.completionTrackPFP = Session["completionTrackPFP"].ToString();
                    if (HarvestedCrops.completionTrackPFP != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = HarvestedCrops.completionTrackPFP.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackPFP"] = Completionstring;
                        HarvestedCrops.completionTrackPFP = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrackPFP"] = "0-0-0-1-0"; ;
                    HarvestedCrops.completionTrackPFP = Session["completionTrackPFP"].ToString();
                }
                return RedirectToAction("InterestedParties", new { cid = cid });
            }
            return View(HarvestedCrops);
        }
        [HttpGet]
        public ActionResult InterestedParties(int? cid)
        {
            InterestedPartiesHC InterestedPartiesHC = new InterestedPartiesHC();
            InterestedPartiesHC.PartynameObj = new InterestedPartyName();
            InterestedPartiesHC.PartynameObj.EiId = 62345;
            InterestedPartiesHC.PartylocationObj = new InterestedPartyLocation();
            InterestedPartiesHC.PartylocationObj.EiId = 62347;
            InterestedPartiesHC.TotalsuminsuredObj = new InterestedTotalSumInsured();
            InterestedPartiesHC.TotalsuminsuredObj.EiId = 62349;
            if (Session["completionTrackPFP"] != null)
            {
                Session["completionTrackPFP"] = Session["completionTrackPFP"];
                InterestedPartiesHC.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            else
            {
                Session["completionTrackPFP"] = "0-0-0-0-0"; ;
                InterestedPartiesHC.completionTrackPFP = Session["completionTrackPFP"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == InterestedPartiesHC.PartynameObj.EiId))
                {
                    InterestedPartiesHC.PartynameObj.Name = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesHC.PartynameObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == InterestedPartiesHC.PartylocationObj.EiId))
                {
                    InterestedPartiesHC.PartylocationObj.Location = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesHC.PartylocationObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == InterestedPartiesHC.TotalsuminsuredObj.EiId))
                {
                    InterestedPartiesHC.TotalsuminsuredObj.Totalsuminsured = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesHC.TotalsuminsuredObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(InterestedPartiesHC);
        }
        [HttpPost]
        public ActionResult InterestedParties(int? cid, InterestedPartiesHC InterestedPartiesHC)
        {
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (InterestedPartiesHC.PartynameObj.EiId > 0 && InterestedPartiesHC.PartynameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartiesHC.CustomerId, 1, InterestedPartiesHC.PartynameObj.EiId, InterestedPartiesHC.PartynameObj.Name.ToString());
                }
                if (InterestedPartiesHC.PartylocationObj.EiId > 0 && InterestedPartiesHC.PartylocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartiesHC.CustomerId, 1, InterestedPartiesHC.PartylocationObj.EiId, InterestedPartiesHC.PartylocationObj.Location.ToString());
                }
                if (InterestedPartiesHC.TotalsuminsuredObj.EiId > 0 && InterestedPartiesHC.TotalsuminsuredObj.Totalsuminsured != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartiesHC.CustomerId, 1, InterestedPartiesHC.TotalsuminsuredObj.EiId, InterestedPartiesHC.TotalsuminsuredObj.Totalsuminsured.ToString());
                }
                if (Session["completionTrackPFP"] != null)
                {
                    Session["completionTrackPFP"] = Session["completionTrackPFP"];
                    InterestedPartiesHC.completionTrackPFP = Session["completionTrackPFP"].ToString();
                    if (InterestedPartiesHC.completionTrackPFP != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = InterestedPartiesHC.completionTrackPFP.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackPFP"] = Completionstring;
                        InterestedPartiesHC.completionTrackPFP = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrackPFP"] = "0-0-0-0-1"; ;
                    InterestedPartiesHC.completionTrackPFP = Session["completionTrackPFP"].ToString();
                }
                return RedirectToAction("FarmLocationDetails", new { cid = cid });
            }
            return View(InterestedPartiesHC);
        }
    }
}