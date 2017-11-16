﻿using System;
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
    public class BoatController : Controller
    {
        // GET: Boat
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BoatDetails(int? cid)
        {
            NewPolicyDetailsClass BoatDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> TypeBoatList = new List<SelectListItem>();
            TypeBoatList = BoatDetailsmodel.TypeOfBoatList();
            List<SelectListItem> HullMeterialsList = new List<SelectListItem>();

            List<SelectListItem> Addresslist = new List<SelectListItem>();
            Addresslist.Add(new SelectListItem { Text = "ABC", Value = "1" });
            Addresslist.Add(new SelectListItem { Text = "XYZ", Value = "2" });
            HullMeterialsList = BoatDetailsmodel.HullMeterialList();
            BoatDetails BoatDetails = new BoatDetails();
            BoatDetails.BoatnameObj = new BoatNames();
            BoatDetails.BoatnameObj.EiId = 61103;
            BoatDetails.RegistrationdetailObj = new RegistrationDetails();
            BoatDetails.RegistrationdetailObj.EiId = 61105;
            BoatDetails.MakeObj = new Makes();
            BoatDetails.MakeObj.EiId = 61107;
            BoatDetails.ModelbObj = new ModelsB();
            BoatDetails.ModelbObj.EiId = 61109;
            BoatDetails.YearmanufactureObj = new YearOfManufacture();
            BoatDetails.YearmanufactureObj.EiId = 61111;
            BoatDetails.LengthmetreObj = new LengthInMetres();
            BoatDetails.LengthmetreObj.EiId = 61113;
            BoatDetails.TypeboatObj = new TypeOfBoat();
            BoatDetails.TypeboatObj.TypeList = TypeBoatList;
            BoatDetails.TypeboatObj.EiId = 61117;
            BoatDetails.HullmeterialObj = new HullMeterials();
            BoatDetails.HullmeterialObj.MeterialList = HullMeterialsList;
            BoatDetails.HullmeterialObj.EiId = 61119;
            BoatDetails.SpeedObj = new Speeds();
            BoatDetails.SpeedObj.EiId = 61129;
            BoatDetails.DetectorObj = new Detectors();
            BoatDetails.DetectorObj.EiId = 61131;
            BoatDetails.MooringstorageObj = new TypeOfMooringStorage();
            BoatDetails.MooringstorageObj.EiId = 61133;
            BoatDetails.otherpleasedetailObj = new OtherPleaseDetails();
            BoatDetails.otherpleasedetailObj.EiId = 0;
            BoatDetails.AddressObj = new AddressesBD();
            BoatDetails.AddressObj.AddressList = Addresslist;
            BoatDetails.AddressObj.EiId = 0;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                BoatDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0"; ;
                BoatDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == BoatDetails.BoatnameObj.EiId))
                {
                    BoatDetails.BoatnameObj.Name = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.BoatnameObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.RegistrationdetailObj.EiId))
                {
                    BoatDetails.RegistrationdetailObj.Registration = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.RegistrationdetailObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.MakeObj.EiId))
                {
                    BoatDetails.MakeObj.Make = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.MakeObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.ModelbObj.EiId))
                {
                    BoatDetails.ModelbObj.Modelb = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.ModelbObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.YearmanufactureObj.EiId))
                {
                    BoatDetails.YearmanufactureObj.Year = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.YearmanufactureObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.LengthmetreObj.EiId))
                {
                    BoatDetails.LengthmetreObj.Metres = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.LengthmetreObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.TypeboatObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == BoatDetails.TypeboatObj.EiId).FirstOrDefault();
                    BoatDetails.TypeboatObj.Type = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.HullmeterialObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == BoatDetails.HullmeterialObj.EiId).FirstOrDefault();
                    BoatDetails.HullmeterialObj.Meterials = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.SpeedObj.EiId))
                {
                    BoatDetails.SpeedObj.Speed = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.SpeedObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.DetectorObj.EiId))
                {
                    BoatDetails.DetectorObj.Detector = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.DetectorObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.MooringstorageObj.EiId))
                {
                    BoatDetails.MooringstorageObj.Mooringorstorage = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.MooringstorageObj.EiId).FirstOrDefault().Answer);
                }

                if (details.Exists(q => q.QuestionId == BoatDetails.otherpleasedetailObj.EiId))
                {
                    BoatDetails.otherpleasedetailObj.Other = Convert.ToString(details.Where(q => q.QuestionId == BoatDetails.otherpleasedetailObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatDetails.HullmeterialObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == BoatDetails.AddressObj.EiId).FirstOrDefault();
                    BoatDetails.AddressObj.Address = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
            }
            return View(BoatDetails);
        }
        [HttpPost]
        public ActionResult BoatDetails(int? cid, BoatDetails BoatDetails)
        {
            NewPolicyDetailsClass BoatDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> TypeBoatList = new List<SelectListItem>();
            TypeBoatList = BoatDetailsmodel.TypeOfBoatList();
            List<SelectListItem> HullMeterialsList = new List<SelectListItem>();
            HullMeterialsList = BoatDetailsmodel.HullMeterialList();
            BoatDetails.TypeboatObj.TypeList = TypeBoatList;
            BoatDetails.HullmeterialObj.MeterialList = HullMeterialsList;

            List<SelectListItem> Addresslist = new List<SelectListItem>();
            Addresslist.Add(new SelectListItem { Text = "ABC", Value = "1" });
            Addresslist.Add(new SelectListItem { Text = "XYZ", Value = "2" });
            BoatDetails.AddressObj.AddressList = Addresslist;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (BoatDetails.BoatnameObj.EiId > 0 && BoatDetails.BoatnameObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.BoatnameObj.EiId, BoatDetails.BoatnameObj.Name.ToString());
                }
                if (BoatDetails.RegistrationdetailObj.EiId > 0 && BoatDetails.RegistrationdetailObj.Registration != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.RegistrationdetailObj.EiId, BoatDetails.RegistrationdetailObj.Registration.ToString());
                }
                if (BoatDetails.MakeObj.EiId > 0 && BoatDetails.MakeObj.Make != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.MakeObj.EiId, BoatDetails.MakeObj.Make.ToString());
                }
                if (BoatDetails.ModelbObj.EiId > 0 && BoatDetails.ModelbObj.Modelb != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.ModelbObj.EiId, BoatDetails.ModelbObj.Modelb.ToString());
                }
                if (BoatDetails.YearmanufactureObj.EiId > 0 && BoatDetails.YearmanufactureObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.YearmanufactureObj.EiId, BoatDetails.YearmanufactureObj.Year.ToString());
                }
                if (BoatDetails.LengthmetreObj.EiId > 0 && BoatDetails.LengthmetreObj.Metres != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.LengthmetreObj.EiId, BoatDetails.LengthmetreObj.Metres.ToString());
                }
                if (BoatDetails.TypeboatObj.EiId > 0 && BoatDetails.TypeboatObj.Type != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.TypeboatObj.EiId, BoatDetails.TypeboatObj.Type.ToString());
                }
                if (BoatDetails.HullmeterialObj.EiId > 0 && BoatDetails.HullmeterialObj.Meterials != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.HullmeterialObj.EiId, BoatDetails.HullmeterialObj.Meterials.ToString());
                }
                if (BoatDetails.SpeedObj.EiId > 0 && BoatDetails.SpeedObj.Speed != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.SpeedObj.EiId, BoatDetails.SpeedObj.Speed.ToString());
                }
                if (BoatDetails.DetectorObj.EiId > 0 && BoatDetails.DetectorObj.Detector != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.DetectorObj.EiId, BoatDetails.DetectorObj.Detector.ToString());
                }
                if (BoatDetails.MooringstorageObj.EiId > 0 && BoatDetails.MooringstorageObj.Mooringorstorage != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.MooringstorageObj.EiId, BoatDetails.MooringstorageObj.Mooringorstorage.ToString());
                }
                if (BoatDetails.otherpleasedetailObj.EiId > 0 && BoatDetails.otherpleasedetailObj.Other != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.otherpleasedetailObj.EiId, BoatDetails.otherpleasedetailObj.Other.ToString());
                }
                if (BoatDetails.AddressObj.EiId > 0 && BoatDetails.AddressObj.Address != null)
                {
                    db.IT_InsertCustomerQnsData(BoatDetails.CustomerId, 1, BoatDetails.AddressObj.EiId, BoatDetails.AddressObj.Address.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    BoatDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (BoatDetails.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = BoatDetails.CompletionTrackB.ToCharArray();

                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 0)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        BoatDetails.CompletionTrackB = Completionstring;
                    }

                }
                else
                {
                    Session["completionTrackB"] = "1-0-0-0-0-0"; ;
                    BoatDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("MotorDetails", new { cid = cid });
            }
            return View(BoatDetails);
        }
        [HttpGet]
        public ActionResult MotorDetails(int? cid)
        {
            NewPolicyDetailsClass MotorDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> FuelTypeList = new List<SelectListItem>();
            FuelTypeList = MotorDetailsmodel.FuelType();
            List<SelectListItem> MotorPositionList = new List<SelectListItem>();
            MotorPositionList = MotorDetailsmodel.MotorPosition();
            List<SelectListItem> DriveTypeList = new List<SelectListItem>();
            DriveTypeList = MotorDetailsmodel.DriveType();
            MotorDetails MotorDetails = new MotorDetails();
            MotorDetails.YearmanufactureObj = new YearOfManufacture();
            MotorDetails.YearmanufactureObj.EiId = 61149;
            MotorDetails.MakemodelObj = new MakeAndModel();
            MotorDetails.MakemodelObj.EiId = 61151;
            MotorDetails.SerialnumberObj = new SerialNumbersMD();
            MotorDetails.SerialnumberObj.EiId = 61153;
            MotorDetails.FueltypeObj = new FuelType();
            MotorDetails.FueltypeObj.FualList = FuelTypeList;
            MotorDetails.FueltypeObj.EiId = 61155;
            MotorDetails.MotorpositionObj = new MotorPosition();
            MotorDetails.MotorpositionObj.MotorList = MotorPositionList;
            MotorDetails.MotorpositionObj.EiId = 61157;
            MotorDetails.DetectorObj = new Detectors();
            MotorDetails.DetectorObj.EiId = 0;
            MotorDetails.DrivetypeObj = new DriveType();
            MotorDetails.DrivetypeObj.DriveList = DriveTypeList;
            MotorDetails.DrivetypeObj.EiId = 61159;
            MotorDetails.PowerObj = new Powers();
            MotorDetails.PowerObj.EiId = 61161;
            MotorDetails.MarketvalueObj = new MarketValues();
            MotorDetails.MarketvalueObj.EiId = 0;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0"; ;
                MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == MotorDetails.YearmanufactureObj.EiId))
                {
                    MotorDetails.YearmanufactureObj.Year = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.YearmanufactureObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.MakemodelObj.EiId))
                {
                    MotorDetails.MakemodelObj.Makemodel = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.MakemodelObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.SerialnumberObj.EiId))
                {
                    MotorDetails.SerialnumberObj.Serialnumber = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.SerialnumberObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.FueltypeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MotorDetails.FueltypeObj.EiId).FirstOrDefault();
                    MotorDetails.FueltypeObj.Type = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.MotorpositionObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MotorDetails.MotorpositionObj.EiId).FirstOrDefault();
                    MotorDetails.MotorpositionObj.Position = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.DetectorObj.EiId))
                {
                    MotorDetails.DetectorObj.Detector = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.DetectorObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.DrivetypeObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == MotorDetails.DrivetypeObj.EiId).FirstOrDefault();
                    MotorDetails.DrivetypeObj.Drivetype = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.PowerObj.EiId))
                {
                    MotorDetails.PowerObj.Power = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.PowerObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == MotorDetails.MarketvalueObj.EiId))
                {
                    MotorDetails.MarketvalueObj.Marketvalue = Convert.ToString(details.Where(q => q.QuestionId == MotorDetails.MarketvalueObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(MotorDetails);
        }
        [HttpPost]
        public ActionResult MotorDetails(int? cid, MotorDetails MotorDetails)
        {
            NewPolicyDetailsClass MotorDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> FuelTypeList = new List<SelectListItem>();
            FuelTypeList = MotorDetailsmodel.FuelType();
            List<SelectListItem> MotorPositionList = new List<SelectListItem>();
            MotorPositionList = MotorDetailsmodel.MotorPosition();
            List<SelectListItem> DriveTypeList = new List<SelectListItem>();
            DriveTypeList = MotorDetailsmodel.DriveType();
            MotorDetails.FueltypeObj.FualList = FuelTypeList;
            MotorDetails.MotorpositionObj.MotorList = MotorPositionList;
            MotorDetails.DrivetypeObj.DriveList = DriveTypeList;

            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (MotorDetails.YearmanufactureObj.EiId > 0 && MotorDetails.YearmanufactureObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.YearmanufactureObj.EiId, MotorDetails.YearmanufactureObj.Year.ToString());
                }
                if (MotorDetails.MakemodelObj.EiId > 0 && MotorDetails.MakemodelObj.Makemodel != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.MakemodelObj.EiId, MotorDetails.MakemodelObj.Makemodel.ToString());
                }
                if (MotorDetails.SerialnumberObj.EiId > 0 && MotorDetails.SerialnumberObj.Serialnumber != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.SerialnumberObj.EiId, MotorDetails.SerialnumberObj.Serialnumber.ToString());
                }
                if (MotorDetails.FueltypeObj.EiId > 0 && MotorDetails.FueltypeObj.Type != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.FueltypeObj.EiId, MotorDetails.FueltypeObj.Type.ToString());
                }
                if (MotorDetails.MotorpositionObj.EiId > 0 && MotorDetails.MotorpositionObj.Position != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.MotorpositionObj.EiId, MotorDetails.MotorpositionObj.Position.ToString());
                }
                if (MotorDetails.DetectorObj.EiId > 0 && MotorDetails.DetectorObj.Detector != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.DetectorObj.EiId, MotorDetails.DetectorObj.Detector.ToString());
                }
                if (MotorDetails.DrivetypeObj.EiId > 0 && MotorDetails.DrivetypeObj.Drivetype != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.DrivetypeObj.EiId, MotorDetails.DrivetypeObj.Drivetype.ToString());
                }
                if (MotorDetails.PowerObj.EiId > 0 && MotorDetails.PowerObj.Power != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.PowerObj.EiId, MotorDetails.PowerObj.Power.ToString());
                }
                if (MotorDetails.MarketvalueObj.EiId > 0 && MotorDetails.MarketvalueObj.Marketvalue != null)
                {
                    db.IT_InsertCustomerQnsData(MotorDetails.CustomerId, 1, MotorDetails.MarketvalueObj.EiId, MotorDetails.MarketvalueObj.Marketvalue.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (MotorDetails.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = MotorDetails.CompletionTrackB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 2)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        MotorDetails.CompletionTrackB = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackB"] = "0-1-0-0-0-0"; ;
                    MotorDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("BoatOperator", new { cid = cid });
            }
            return View(MotorDetails);
        }
        [HttpGet]
        public ActionResult BoatOperator(int? cid)
        {
            NewPolicyDetailsClass BoatOperatorListmodel = new NewPolicyDetailsClass();
            List<SelectListItem> NameBOLists = new List<SelectListItem>();
            NameBOLists = BoatOperatorListmodel.BoatOperatorLists();
            BoatOperator BoatOperator = new BoatOperator();
            BoatOperator.NameboObj = new NameBOs();
            BoatOperator.NameboObj.NameBOList = NameBOLists;
            BoatOperator.NameboObj.EiId = 61187;
            BoatOperator.YearsexperienceObj = new YearsExperienced();
            BoatOperator.YearsexperienceObj.EiId = 61189;
            BoatOperator.TypesboatObj = new TypesofBoat();
            BoatOperator.TypesboatObj.EiId = 61191;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0"; ;
                BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == BoatOperator.NameboObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == BoatOperator.NameboObj.EiId).FirstOrDefault();
                    BoatOperator.NameboObj.Name = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == BoatOperator.YearsexperienceObj.EiId))
                {
                    BoatOperator.YearsexperienceObj.Year = Convert.ToString(details.Where(q => q.QuestionId == BoatOperator.YearsexperienceObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == BoatOperator.TypesboatObj.EiId))
                {
                    BoatOperator.TypesboatObj.Type = Convert.ToString(details.Where(q => q.QuestionId == BoatOperator.TypesboatObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(BoatOperator);
        }
        [HttpPost]
        public ActionResult BoatOperator(int? cid, BoatOperator BoatOperator)
        {
            NewPolicyDetailsClass BoatOperatorListmodel = new NewPolicyDetailsClass();
            List<SelectListItem> NameBOLists = new List<SelectListItem>();
            NameBOLists = BoatOperatorListmodel.BoatOperatorLists();
            BoatOperator.NameboObj.NameBOList = NameBOLists;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (BoatOperator.NameboObj.EiId > 0 && BoatOperator.NameboObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, 1, BoatOperator.NameboObj.EiId, BoatOperator.NameboObj.Name.ToString());
                }
                if (BoatOperator.YearsexperienceObj.EiId > 0 && BoatOperator.YearsexperienceObj.Year != null)
                {
                    db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, 1, BoatOperator.YearsexperienceObj.EiId, BoatOperator.YearsexperienceObj.Year.ToString());
                }
                if (BoatOperator.YearsexperienceObj.EiId > 0 && BoatOperator.TypesboatObj.Type != null)
                {
                    db.IT_InsertCustomerQnsData(BoatOperator.CustomerId, 1, BoatOperator.TypesboatObj.EiId, BoatOperator.TypesboatObj.Type.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (BoatOperator.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = BoatOperator.CompletionTrackB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 4)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        BoatOperator.CompletionTrackB = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackB"] = "0-0-1-0-0-0"; ;
                    BoatOperator.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("CoverDetails", new { cid = cid });
            }
            return View(BoatOperator);
        }
        [HttpGet]
        public ActionResult CoverDetails(int? cid)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessList = new List<SelectListItem>();
            ExcessList = CoverDetailsmodel.excessRate();
            CoverDetails CoverDetails = new CoverDetails();
            CoverDetails.MarketvalueObj = new MarketValues();
            CoverDetails.MarketvalueObj.EiId = 61207;
            CoverDetails.MotorvalueObj = new MotorValues();
            CoverDetails.MotorvalueObj.EiId = 61209;
            CoverDetails.AccessorydescriptionObj = new AccessoryDescription();
            CoverDetails.AccessorydescriptionObj.EiId = 61213;
            CoverDetails.AccessorysuminsureObj = new AccessorySumInsured();
            CoverDetails.AccessorysuminsureObj.EiId = 61215;
            CoverDetails.Totalsumassured = "0";
            CoverDetails.Coverforaccessories = "";
            CoverDetails.Totalcoverboat = "";
            CoverDetails.LiabilityObj = new LiabilityCD();
            CoverDetails.LiabilityObj.EiId = 61223;
            CoverDetails.ExcesscdObj = new ExcessCD();
            CoverDetails.ExcesscdObj.ExcessList = ExcessList;
            CoverDetails.ExcesscdObj.EiId = 61233;
            CoverDetails.FreeperiodObj = new ClaimFreePeriod();
            CoverDetails.FreeperiodObj.EiId = 61235;
            CoverDetails.NodiscountObj = new NoClaimDiscount();
            CoverDetails.NodiscountObj.EiId = 61237;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0"; ;
                CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == CoverDetails.MarketvalueObj.EiId))
                {
                    CoverDetails.MarketvalueObj.Marketvalue = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.MarketvalueObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.MotorvalueObj.EiId))
                {
                    CoverDetails.MotorvalueObj.Motorvalue = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.MotorvalueObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.AccessorydescriptionObj.EiId))
                {
                    CoverDetails.AccessorydescriptionObj.Description = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.AccessorydescriptionObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.AccessorysuminsureObj.EiId))
                {
                    CoverDetails.AccessorysuminsureObj.Suminsured = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.AccessorysuminsureObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.LiabilityObj.EiId))
                {
                    CoverDetails.LiabilityObj.Liability = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.LiabilityObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.ExcesscdObj.EiId))
                {
                    var loc = details.Where(q => q.QuestionId == CoverDetails.ExcesscdObj.EiId).FirstOrDefault();
                    CoverDetails.ExcesscdObj.Excess = !string.IsNullOrEmpty(loc.Answer) ? (loc.Answer) : null;
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.FreeperiodObj.EiId))
                {
                    CoverDetails.FreeperiodObj.Freeperiod = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.FreeperiodObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == CoverDetails.NodiscountObj.EiId))
                {
                    CoverDetails.NodiscountObj.Nodiscount = Convert.ToString(details.Where(q => q.QuestionId == CoverDetails.NodiscountObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(CoverDetails);
        }
        [HttpPost]
        public ActionResult CoverDetails(int? cid, CoverDetails CoverDetails)
        {
            NewPolicyDetailsClass CoverDetailsmodel = new NewPolicyDetailsClass();
            List<SelectListItem> ExcessList = new List<SelectListItem>();
            ExcessList = CoverDetailsmodel.excessRate();
            CoverDetails.ExcesscdObj.ExcessList = ExcessList;
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (CoverDetails.MarketvalueObj.EiId > 0 && CoverDetails.MarketvalueObj.Marketvalue != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.MarketvalueObj.EiId, CoverDetails.MarketvalueObj.Marketvalue.ToString());
                }
                if (CoverDetails.MotorvalueObj.EiId > 0 && CoverDetails.MotorvalueObj.Motorvalue != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.MotorvalueObj.EiId, CoverDetails.MotorvalueObj.Motorvalue.ToString());
                }
                if (CoverDetails.AccessorydescriptionObj.EiId > 0 && CoverDetails.AccessorydescriptionObj.Description != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.AccessorydescriptionObj.EiId, CoverDetails.AccessorydescriptionObj.Description.ToString());
                }
                if (CoverDetails.AccessorysuminsureObj.EiId > 0 && CoverDetails.AccessorysuminsureObj.Suminsured != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.AccessorysuminsureObj.EiId, CoverDetails.AccessorysuminsureObj.Suminsured.ToString());
                }
                if (CoverDetails.LiabilityObj.EiId > 0 && CoverDetails.LiabilityObj.Liability != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.LiabilityObj.EiId, CoverDetails.LiabilityObj.Liability.ToString());
                }
                if (CoverDetails.ExcesscdObj.EiId > 0 && CoverDetails.ExcesscdObj.Excess != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.ExcesscdObj.EiId, CoverDetails.ExcesscdObj.Excess.ToString());
                }
                if (CoverDetails.FreeperiodObj.EiId > 0 && CoverDetails.FreeperiodObj.Freeperiod != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.FreeperiodObj.EiId, CoverDetails.FreeperiodObj.Freeperiod.ToString());
                }
                if (CoverDetails.NodiscountObj.EiId > 0 && CoverDetails.NodiscountObj.Nodiscount != null)
                {
                    db.IT_InsertCustomerQnsData(CoverDetails.CustomerId, 1, CoverDetails.NodiscountObj.EiId, CoverDetails.NodiscountObj.Nodiscount.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (CoverDetails.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = CoverDetails.CompletionTrackB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 6)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        CoverDetails.CompletionTrackB = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackB"] = "0-0-0-1-0-0"; ;
                    CoverDetails.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("Options", new { cid = cid });
            }
            return View(CoverDetails);
        }
        [HttpGet]
        public ActionResult Options(int? cid)
        {
            Options Options = new Options();
            Options.WaterwayObj = new Waterways();
            Options.WaterwayObj.EiId = 61253;
            Options.LimitseawardObj = new LimitSeawardTravel();
            Options.LimitseawardObj.EiId = 61255;
            Options.SailboatObj = new SailBoats();
            Options.SailboatObj.EiId = 61259;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                Options.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0"; ;
                Options.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == Options.WaterwayObj.EiId))
                {
                    Options.WaterwayObj.Waterway = Convert.ToString(details.Where(q => q.QuestionId == Options.WaterwayObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Options.LimitseawardObj.EiId))
                {
                    Options.LimitseawardObj.Seaward = Convert.ToString(details.Where(q => q.QuestionId == Options.LimitseawardObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == Options.SailboatObj.EiId))
                {
                    Options.SailboatObj.Sailboat = Convert.ToString(details.Where(q => q.QuestionId == Options.SailboatObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(Options);
        }
        [HttpPost]
        public ActionResult Options(int? cid, Options Options)
        {
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (Options.WaterwayObj.EiId > 0 && Options.WaterwayObj.Waterway != null)
                {
                    db.IT_InsertCustomerQnsData(Options.CustomerId, 1, Options.WaterwayObj.EiId, Options.WaterwayObj.Waterway.ToString());
                }
                if (Options.LimitseawardObj.EiId > 0 && Options.LimitseawardObj.Seaward != null)
                {
                    db.IT_InsertCustomerQnsData(Options.CustomerId, 1, Options.WaterwayObj.EiId, Options.WaterwayObj.Waterway.ToString());
                }
                if (Options.SailboatObj.EiId > 0 && Options.SailboatObj.Sailboat != null)
                {
                    db.IT_InsertCustomerQnsData(Options.CustomerId, 1, Options.SailboatObj.EiId, Options.SailboatObj.Sailboat.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    Options.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (Options.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = Options.CompletionTrackB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 8)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        Options.CompletionTrackB = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackB"] = "0-0-0-0-1-0"; ;
                    Options.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("InterestedPartiesBoat", new { cid = cid });
            }
            return View(Options);
        }
        [HttpGet]
        public ActionResult InterestedPartiesBoat(int? cid)
        {
            InterestedPartiesBoat InterestedPartiesBoat = new InterestedPartiesBoat();
            InterestedPartiesBoat.InstitutionsObj = new NameOfInstitutions();
            InterestedPartiesBoat.InstitutionsObj.EiId = 61277;
            InterestedPartiesBoat.LocationObj = new LocationsIPB();
            InterestedPartiesBoat.LocationObj.EiId = 61279;
            if (Session["completionTrackB"] != null)
            {
                Session["completionTrackB"] = Session["completionTrackB"];
                InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            else
            {
                Session["completionTrackB"] = "0-0-0-0-0-0";
                InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
            }
            var db = new MasterDataEntities();
            var details = db.IT_GetCustomerQnsDetails(cid, 1).ToList();
            if (details != null && details.Any())
            {
                if (details.Exists(q => q.QuestionId == InterestedPartiesBoat.InstitutionsObj.EiId))
                {
                    InterestedPartiesBoat.InstitutionsObj.Name = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesBoat.InstitutionsObj.EiId).FirstOrDefault().Answer);
                }
                if (details.Exists(q => q.QuestionId == InterestedPartiesBoat.LocationObj.EiId))
                {
                    InterestedPartiesBoat.LocationObj.Location = Convert.ToString(details.Where(q => q.QuestionId == InterestedPartiesBoat.LocationObj.EiId).FirstOrDefault().Answer);
                }
            }
            return View(InterestedPartiesBoat);
        }
        [HttpPost]
        public ActionResult InterestedPartiesBoat(int? cid, InterestedPartiesBoat InterestedPartiesBoat)
        {
            var db = new MasterDataEntities();
            if (cid.HasValue && cid > 0)
            {
                if (InterestedPartiesBoat.InstitutionsObj.EiId > 0 && InterestedPartiesBoat.InstitutionsObj.Name != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartiesBoat.CustomerId, 1, InterestedPartiesBoat.InstitutionsObj.EiId, InterestedPartiesBoat.InstitutionsObj.Name.ToString());
                }
                if (InterestedPartiesBoat.LocationObj.EiId > 0 && InterestedPartiesBoat.LocationObj.Location != null)
                {
                    db.IT_InsertCustomerQnsData(InterestedPartiesBoat.CustomerId, 1, InterestedPartiesBoat.LocationObj.EiId, InterestedPartiesBoat.LocationObj.Location.ToString());
                }
                if (Session["completionTrackB"] != null)
                {
                    Session["completionTrackB"] = Session["completionTrackB"];
                    InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
                    if (InterestedPartiesBoat.CompletionTrackB != null)
                    {
                        string Completionstring = string.Empty;
                        char[] arr = InterestedPartiesBoat.CompletionTrackB.ToCharArray();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            char a = arr[i];
                            if (i == 10)
                            {
                                a = '1';
                            }
                            Completionstring = Completionstring + a;
                        }
                        Session["completionTrackB"] = Completionstring;
                        InterestedPartiesBoat.CompletionTrackB = Completionstring;
                    }
                }
                else
                {
                    Session["completionTrackB"] = "0-0-0-0-0-1"; ;
                    InterestedPartiesBoat.CompletionTrackB = Session["completionTrackB"].ToString();
                }
                return RedirectToAction("BoatDetails", new { cid = cid });
            }
            return View(InterestedPartiesBoat);
        }
    }    
}