using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsureThatAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using InsureThatAPI.CommonMethods;
using Newtonsoft.Json.Linq;
using System.IO;

namespace InsureThatAPI.Controllers
{

    public class CustomerController : Controller
    {

        #region PARTIAL VIEW TO STRING
        [NonAction]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion



        // GET: Customer
        [HttpGet]
        public ActionResult CustomerSearch()
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CustomerSearch(CustomerSearch customersearch, string term)
        {
            var db = new MasterDataEntities();
           if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
                HttpClient hclient = new HttpClient();
            if (Session["IyId"] != null && Session["IyId"] != "")
            {
                customersearch.iyId = Session["IyId"].ToString();
            }
            InsuredDetails insuredetails = new InsuredDetails();
            customersearch.iyId = 9262.ToString();//testing should remove
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + term + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);

            if (Res.IsSuccessStatusCode)
            {
                GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                if(EmpResponse!=null)
                insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                if (insureddetails.Insureds != null && insureddetails.Insureds.Count()>0)
                {
                   // int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                  //  return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = insureddetails.Insureds.Select(p=>p.InsuredID).FirstOrDefault(), CustomerId = customerid });

                  //  return RedirectToAction("InsuredPolicys", "Customer", customersearch);
                    return Json(new { results = insureddetails.Insureds.Select(p => new InsuredListDDL() { id = p.InsuredID, text = p.CompanyBusinessName + p.FirstName + " " + p.MiddleName + " " + p.LastName }).ToList() });
                }
                else if(insureddetails.ErrorMessage!=null && insureddetails.ErrorMessage.Count()>0)
                {
                    ViewBag.Status = "Failure";
                    return View(customersearch);
                }
                //if (insureddetails.Insureds != null)
                //{
                //    Session["InsuredId"] = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                //}

            }
            else
            {
                ViewBag.Status = "Failure";

            }
            if (term != null || customersearch.insuredName != null || customersearch.policyNo != null || customersearch.phoneNo != null)
            {
                TempData["ErrorMessage"] = "No data found.";
                return RedirectToAction("AdvancedCustomerSearch", "Customer", customersearch);
            }
            return View(customersearch);
        }
        [HttpGet]
        public ActionResult AdvancedCustomerSearch(CustomerSearch customersearch)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            return View(customersearch);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AdvCustomerSearch(CustomerSearch customersearch, string InsuredName, string PolicyNo, string PhoneNo, string EmailId)
        {
            //return RedirectToAction("PolicyList", "Customer", new { InsuredId = ViewBag.InsuredId, CustomerId = 1 });
            if(((customersearch.policyNo==null || string.IsNullOrWhiteSpace(customersearch.policyNo)) && (customersearch.emailId==null || string.IsNullOrWhiteSpace(customersearch.emailId)) && (customersearch.phoneNo==null || string.IsNullOrWhiteSpace(customersearch.phoneNo)))&&((PolicyNo == null || String.IsNullOrWhiteSpace(PolicyNo)) && (InsuredName == null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId == null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo == null || String.IsNullOrWhiteSpace(PhoneNo))))
            {
                TempData["ErrorMsg"] = "Enter any details to search";
                return RedirectToAction("AdvancedCustomerSearch", "Customer");

            }
            //else if ((PolicyNo ==null || String.IsNullOrWhiteSpace(PolicyNo))  && (InsuredName==null || String.IsNullOrWhiteSpace(InsuredName)) && (EmailId==null || String.IsNullOrWhiteSpace(EmailId)) && (PhoneNo==null || String.IsNullOrWhiteSpace(PhoneNo)))
            //{
            //    TempData["ErrorMsg"] = "Enter any details to search";
            //    return RedirectToAction("AdvancedCustomerSearch", "Customer");

            //}
            HttpClient hclient = new HttpClient();
            if (Session["IyId"] != null && Session["IyId"] != "")
            {
                customersearch.iyId = Session["IyId"].ToString();
            }
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            InsuredDetails insuredetails = new InsuredDetails();
            MasterDataEntities db = new MasterDataEntities();
            customersearch.iyId = 9262.ToString();//testing should remove//9262 is raci
                                                  //  StringContent content = new StringContent(JsonConvert.SerializeObject(customersearch), Encoding.UTF8, "application/json");
            var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
            hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          
                if (customersearch != null && (customersearch.emailId != null || customersearch.phoneNo != null))
                {
                    //On Submit
                    HttpResponseMessage Ress = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + customersearch.policyNo + "&insuredName=" + null + "&emailId=" + customersearch.emailId + "&phoneNo=" + customersearch.phoneNo);
                    if (Ress.IsSuccessStatusCode)
                    {
                        GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                        var EmpResponse = Ress.Content.ReadAsStringAsync().Result;
                        insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                        if (insureddetails.Insureds != null)
                        {
                            ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                            int? customerid = db.IT_InsertCustomerMaster(customersearch.emailId, customersearch.InsuredId, customersearch.policyNo, null, customersearch.insuredName, null).SingleOrDefault();
                            return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = ViewBag.InsuredId, CustomerId = customerid });

                        }

                    }
                }
            
            if (Request.IsAjaxRequest())
            {
                //On Auto Search
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/InsuredDetails?iyId=" + customersearch.iyId + "&policyNo=" + PolicyNo + "&insuredName=" + InsuredName + "&emailId=" + EmailId + "&phoneNo=" + PhoneNo);

                if (Res.IsSuccessStatusCode)
                {
                    GetInsuredDetailsRef insureddetails = new GetInsuredDetailsRef();
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    insureddetails = JsonConvert.DeserializeObject<GetInsuredDetailsRef>(EmpResponse);
                    if (insureddetails.Insureds != null)
                    {
                        ViewBag.InsuredId = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                        db.IT_InsertCustomerMaster(insureddetails.Insureds.Select(p => p.EmailID).FirstOrDefault(), ViewBag.InsuredId, PolicyNo, null, InsuredName, null);
                        return Json(new { results = insureddetails.Insureds });
                    }
                    //if (insureddetails.Insureds != null)
                    //{
                    //    Session["InsuredId"] = insureddetails.Insureds.Select(p => p.InsuredID).FirstOrDefault();
                    //}

                }
                else
                {
                    ViewBag.Status = "Failure";

                }
                if (customersearch.insuredName != null || customersearch.policyNo != null || customersearch.phoneNo != null)
                {
                    TempData["ErrorMessage"] = "No data found.";
                    return RedirectToAction("AdvancedCustomerSearch", "Customer", customersearch);
                }
            }
            return View(customersearch);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (Session["UserName"] != null)
            {

            }
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ChangePassword(ChangePasswordDetails changepassword)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            LogInDetailsClass logincls = new LogInDetailsClass();
            LogInDetails logindetailsmodel = new LogInDetails();
            ChangePasswordDetailsRef changepasswordref = new ChangePasswordDetailsRef();
            string strEncrypt = string.Empty;
            if (Session["UserName"] != null && Session["UserName"] != "")
            {
                string strDecrypt = string.Empty;
                string UserName = string.Empty;
                UserName = Session["UserName"].ToString();
                string PlainTextEncrpted = string.Empty;
                string NewPassword = string.Empty;
                string loginKey = string.Empty;
                int IyId = 9262;
                string EncrptForLogin = String.Format("{0:ddddyyyyMMdd}", DateTime.Now);
                PlainTextEncrpted = IyId + "|" + UserName + "|InsureThatDirect";
                loginKey = LogInDetailsClass.Encrypt(PlainTextEncrpted, EncrptForLogin);
                HttpClient hclient = new HttpClient();
                hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await hclient.GetAsync("Api/Login?loginKey=" + loginKey + "");//change controller name and field name
                changepassword.Email = Session["Email"].ToString();                                                                               //   LogInDetails loginmodel = new LogInDetails();
                if (Res.IsSuccessStatusCode)
                {
                    //  Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    logindetailsmodel = JsonConvert.DeserializeObject<LogInDetails>(EmpResponse);
                    strEncrypt = LogInDetailsClass.Encrypt(changepassword.Password, "TimsFirstEncryptionKey");//encrypt password method
                                                                                                              // strDecrypt = Decrypt(strEncrypt, "TimsFirstEncryptionKey");//decrypt password method

                    if (logindetailsmodel.EncryptedPassword != null && strEncrypt == logindetailsmodel.EncryptedPassword.ToString())
                    {
                        NewPassword = LogInDetailsClass.Encrypt(changepassword.NewPassword, "TimsFirstEncryptionKey");//encrypt new password method
                        HttpResponseMessage Resp = await hclient.GetAsync("https://api.insurethat.com.au/Api/Login?UserName=" + changepassword.UserName + "&EmailID=" + changepassword.Email + "&EncryptedPassword=" + NewPassword);
                        if (Res.IsSuccessStatusCode)
                        {
                            return RedirectToAction("AgentLogin", "Login");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Failed to update New password, try later.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Old password does not match.";

                    }

                }

            }
            return View();

        }
        #region Display Policy List Based on Insured Id
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> PolicyList(int? InsuredId, int? CustomerId)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            GetNewPolicyDetailsRef policydetails = new GetNewPolicyDetailsRef();
          //  return View(policydetails);
            PolicyList policylist = new PolicyList();
            policydetails.PolicyData = new List<PolicyDetails>();
            InsuredId = 108454;
            if (InsuredId.HasValue && InsuredId > 0)
            {
                //  insureddetails.InsuredID = Convert.ToInt32(Session["InsuredId"]);


                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await hclient.GetAsync(" https://api.insurethat.com.au/Api/PolicyDetails?iyId=9262&paId=" + InsuredId + "");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                policydetails = JsonConvert.DeserializeObject<GetNewPolicyDetailsRef>(EmpResponse);
                ViewBag.CustomerId = CustomerId;
                if (policydetails.PolicyData != null && policydetails.PolicyData.Count() > 0)
                {
                    return View(policydetails);
                }
                else
                {
                    return RedirectToAction("NewPolicy", "Customer", new { CustomerId = 1 });
                }
            }
            else
            { return RedirectToAction("AdvancedCustomerSearch", "Customer"); }


          
        }





        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredPolicys(int? InsuredId, int? CustomerId)
        {
            if (Session["apiKey"] != null)
            {
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            GetNewPolicyDetailsRef policydetails = new GetNewPolicyDetailsRef();
            PolicyList policylist = new PolicyList();
            policydetails.PolicyData = new List<PolicyDetails>();
            //InsuredId = 108454;
            if (InsuredId.HasValue && InsuredId > 0)
            {
                //  insureddetails.InsuredID = Convert.ToInt32(Session["InsuredId"]);


                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await hclient.GetAsync(" https://api.insurethat.com.au/Api/PolicyDetails?iyId=9262&paId=" + InsuredId + "");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                policydetails = JsonConvert.DeserializeObject<GetNewPolicyDetailsRef>(EmpResponse);
                ViewBag.CustomerId = CustomerId;
                if (policydetails.PolicyData != null && policydetails.PolicyData.Count() > 0)
                {
                    return View(policydetails);
                }
                else
                {
                    return RedirectToAction("NewPolicy", "Customer", new { CustomerId = 1 });
                }
            }
            //else if(CustomerId.HasValue && CustomerId>0)
            //{
            //    return View(policydetails);
            //}
            else
            { return RedirectToAction("AdvancedCustomerSearch", "Customer"); }
        }
        #endregion

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails(int? CustomerId, int? PcId, int? step)
        {
            // PolicyNo = "ITRD00075330";
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            PcId = 54611;
            if (PcId != null && PcId > 0)
            {
                ViewEditPolicyDetails model = new ViewEditPolicyDetails();
                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string PlainTextEncrpted = string.Empty;
                string ApiKey = string.Empty;

                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + ApiKey + "&action=Retrieve&pcId=" + PcId + "&trId=0&effectiveDate=1900-01-01&reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);
                if(model.ErrorMessage!=null && model.ErrorMessage.Count>0 && model.ErrorMessage.Contains("API Session Expired")){
                    return RedirectToAction("AgentLogin", "Login");
                }

                if (model.RiskData != null && model.RiskData.Count>0)
                {
                    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step==1 )
                    {
                        return View(model);
                    }
                     if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 2)
                    {
                        return PartialView("ViewOccupancyIPHBuilding", model);
                        
                    }
                    if (model.RiskData.Exists(o=>o.Name=="Home Contents") && step == 3)
                    {
                        //model.RiskData = model.RiskData.Where(p => p.Name == "Home Contents").Select(o => o.Elements).ToList();
                        return PartialView("ViewHomeContent", model);
                       
                    }
                    if (model.RiskData.Exists(o => o.Name == "Valuables") && step == 4)
                    {
                        return PartialView("ViewValuables", model);

                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 5)
                    {
                        return PartialView("ViewFarmProperty", model);

                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 6)
                    {
                        return PartialView("ViewFarmMachinery", model);

                    }
                    if (model.PremiumData!=null && model.PremiumData.Count>0 && step==8)
                    {
                        return PartialView("ViewQuotation", model);

                    }
                    if (model.RiskData.Select(p => p.Name).First() == "Home Building")
                    {
                        // return Json(new { content = RenderRazorViewToString("_UpdateEditRolePermissionList", manageusers), rolesddl = manageusers.RoleList, rolenames = manageusers.RoleName, ViewBag.messages, status = true, msg = "Role assigned successfully." });
                    }
                    return View(model);
                }


            }

            return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = 108454, CustomerId = 1 });
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ViewUpdatePolicyDetails( int? PcId, int? step)        {
            // PolicyNo = "ITRD00075330";
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            PcId = 54611;
            if (PcId != null && PcId > 0)
            {
                ViewEditPolicyDetails model = new ViewEditPolicyDetails();
                HttpClient hclient = new HttpClient();
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string PlainTextEncrpted = string.Empty;
                string ApiKey = string.Empty;

                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/Api/PolicyDetails?apiKey=" + ApiKey + "&action=Retrieve&pcId=" + PcId + "&trId=0&effectiveDate=1900-01-01&reason=");/*insureddetails.InsuredID */
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword

                model = JsonConvert.DeserializeObject<ViewEditPolicyDetails>(EmpResponse);

                if (model.RiskData != null && model.RiskData.Count > 0)
                {
                    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 1)
                    {
                        return View(model);
                    }
                    if (model.RiskData.Select(o => o.Name).FirstOrDefault() == "Home" && step == 2)
                    {
                        return PartialView("ViewOccupancyIPHBuilding", model);
                        
                    }
                    if (model.RiskData.Exists(o=>o.Name=="Home Contents") && step == 3)
                    {
                        //model.RiskData = model.RiskData.Where(p => p.Name == "Home Contents").Select(o => o.Elements).ToList();
                        return PartialView("ViewHomeContent", model);
                       
                    }
                    if (model.RiskData.Exists(o => o.Name == "Valuables") && step == 4)
                    {
                        return PartialView("ViewValuables", model);

                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 5)
                    {
                        return PartialView("ViewFarmProperty", model);

                    }
                    if (model.RiskData.Exists(o => o.Name == "Farm Property") && step == 6)
                    {
                        return PartialView("ViewFarmMachinery", model);

                    }
                    if (model.PremiumData!=null && model.PremiumData.Count>0)
                    {
                        return PartialView("ViewQuotation", model);

                    }
                    if (model.RiskData.Select(p => p.Name).First() == "Home Building")
                    {
                        // return Json(new { content = RenderRazorViewToString("_UpdateEditRolePermissionList", manageusers), rolesddl = manageusers.RoleList, rolenames = manageusers.RoleName, ViewBag.messages, status = true, msg = "Role assigned successfully." });
                    }
                    return View(model);
                }


            }

            return RedirectToAction("InsuredPolicys", "Customer", new { InsuredId = 108454, CustomerId = 1 });
        }

        [HttpGet]
        public ActionResult NewPolicy(int? CustomerId)
        {

            return View();
        }
        [HttpGet]
        public ActionResult PolicyInclustions(int? CustomerId)
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult PolicyHistory(int? CustomerId, string PolicyNo)
        //{

        //    return View();
        //}
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> InsuredList(string PolicyNumber)
        {
            if (Session["apiKey"] != null)
            {
                Session["apiKey"] = Session["apiKey"];
            }
            else
            {
                return RedirectToAction("AgentLogin", "Login");
            }
            if (PolicyNumber != null && !string.IsNullOrEmpty(PolicyNumber))
            {
                PolicyHistory model = new PolicyHistory();
                HttpClient hclient = new HttpClient();
                var response = hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string ApiKey = string.Empty;

                ApiKey = Session["apiKey"].ToString();
                HttpResponseMessage Res = await hclient.GetAsync("https://api.insurethat.com.au/api/PolicyHistory?apiKey=" + ApiKey + "&policyNumber=" + PolicyNumber);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<PolicyHistory>(EmpResponse);


                }

                return View(model);
            }
            return View();
        }
    }


}