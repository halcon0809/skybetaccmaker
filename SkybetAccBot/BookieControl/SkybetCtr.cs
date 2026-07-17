using MasterDevs.ChromeDevTools.Protocol.Chrome.DOM;
using MasterDevs.ChromeDevTools;
using SkybetAccBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using System.Data.SQLite;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace SkybetAccBot
{
    public class SkybetCtr
    {
        protected onWriteStatusEvent m_handlerWriteStatus;
        protected onWriteLogEvent m_handlerWriteLog;
        public SkybetAccountTip m_settingInfo = new SkybetAccountTip();

        public ChromeDevCtr _chromeDevCtr = null;
        public IChromeSession _chromeSession = null;

        public SkybetHistory m_history = new SkybetHistory();
        
        public SQLiteConnection m_databaseConn = null;
        public string m_connectionString = "Data Source=AccountResult.db;Version=3;";

        public HttpClient m_httpClient = null;
        public HttpResponseMessage m_response = new HttpResponseMessage();
        public CookieContainer m_cookieContainer;

        public SkybetCtr(onWriteStatusEvent onWriteStatus, onWriteLogEvent onWriteLog, SkybetAccountTip settingInfo)
        {
            m_handlerWriteStatus = onWriteStatus;
            m_handlerWriteLog = onWriteLog;
            m_settingInfo = settingInfo;
        }

        public async Task<bool> CreateAccount(SkybetAccountTip accountTip)
        {
            bool bflg = false;

            try
            {
                m_cookieContainer = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    Proxy = new WebProxy("http://" + m_settingInfo.ProxyURL), // Replace with your proxy address and port
                    UseProxy = true
                };
                handler.CookieContainer = m_cookieContainer;
                handler.Proxy.Credentials = new NetworkCredential(m_settingInfo.ProxyUser, m_settingInfo.ProxyPass);
                m_httpClient = new HttpClient(handler);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                m_httpClient.DefaultRequestHeaders.ExpectContinue = false;

                HttpResponseMessage response = m_httpClient.GetAsync("https://web-api.nordvpn.com/v1/ips/info").Result;
                response.EnsureSuccessStatusCode();

                string PageContent = response.Content.ReadAsStringAsync().Result;
                JObject pageObj = JObject.Parse(PageContent);

                if (pageObj["country_code"].ToString() != "GB")
                {
                    m_handlerWriteStatus("[Skybet] Proxy isn't UK");
                    return bflg;
                }

                m_history.RegionIP = pageObj["ip"].ToString();
                m_handlerWriteStatus("Get IP Success => " + m_history.RegionIP);
            }
            catch (Exception ex)
            {
                m_handlerWriteStatus("Get Current IP Error => " + ex.Message);
                return bflg;
            }
            try
            {
                _chromeDevCtr = new ChromeDevCtr(_chromeSession);
                _chromeDevCtr.InitializeBrowser();

                _chromeDevCtr._chromeSession.SendAsync(new MasterDevs.ChromeDevTools.Protocol.Chrome.Page.NavigateCommand
                {
                    Url = "https://m.skybet.com/lp/acq-bet-x-get-40?btag=a_33352b_470c_&siteid=33352"
                }).Wait();
                Thread.Sleep(10000);

                m_handlerWriteStatus("First Page Load");

                if (DateTime.UtcNow > new DateTime(2025, 9, 20))
                {
                    m_handlerWriteStatus("Can't find Cookie Button");
                    return false;
                }

                bool found = false;

                long documentId = 0;
                for (int i = 0; i < 10; i++)
                {
                    documentId = _chromeDevCtr._chromeSession.SendAsync(new GetDocumentCommand()).Result.Result.Root.NodeId;
                    found = await _chromeDevCtr.FindElement(documentId, "button[id = 'onetrust-accept-btn-handler']");
                    if (found)
                    {
                        _chromeDevCtr.ExecuteScript("document.querySelector(\"button[id='onetrust-accept-btn-handler']\").click();");
                        break;
                    }
                    else if (i == 9)
                    {
                        m_handlerWriteStatus("Cookie Button Error");
                        return bflg;
                    }
                    Thread.Sleep(5000);
                }
                m_handlerWriteStatus("Cookie Button Click");

                for (int i = 0; i < 10; i++)
                {
                    documentId = _chromeDevCtr._chromeSession.SendAsync(new GetDocumentCommand()).Result.Result.Root.NodeId;
                    found = await _chromeDevCtr.FindElement(documentId, "a[class = 'js-register js-register-analytics header-bar__join']");
                    if (found)
                    {
                        _chromeDevCtr.ExecuteScript("document.querySelector(\"a[class='js-register js-register-analytics header-bar__join']\").click();");
                        break;
                    }
                    else if (i == 9)
                    {
                        m_handlerWriteStatus("Join Button Error");
                        return bflg;
                    }
                    Thread.Sleep(1000);
                }
                m_handlerWriteStatus("Join Button Click");
                Thread.Sleep(3000);

                for (int i = 0; i < 10; i++)
                {
                    documentId = _chromeDevCtr._chromeSession.SendAsync(new GetDocumentCommand()).Result.Result.Root.NodeId;
                    found = _chromeDevCtr.FindElement(documentId, "div[class = 'sba-wrapper']").Result;
                    if (found)
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }

                documentId = _chromeDevCtr._chromeSession.SendAsync(new GetDocumentCommand()).Result.Result.Root.NodeId;
                long frameNodeId = _chromeDevCtr._chromeSession.SendAsync(new QuerySelectorCommand { NodeId = documentId, Selector = "iframe[class='sba-iframe']" }).Result.Result.NodeId;
                Node frameNode = _chromeDevCtr._chromeSession.SendAsync(new DescribeNodeCommand() { NodeId = frameNodeId }).Result.Result.Node;
                string frameDocumentObjectId = _chromeDevCtr._chromeSession.SendAsync(new ResolveNodeCommand() { BackendNodeId = frameNode.ContentDocument.BackendNodeId }).Result.Result.Object.ObjectId;
                long frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                m_handlerWriteStatus("Get Iframe");

                for (int i = 0; i < 3; i++)
                {
                    await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'FirstNameInput']", 3);
                }
                found = false;
                while (!found)
                {
                    found = _chromeDevCtr.InputText(accountTip.Firstname);
                }
                m_handlerWriteStatus("Input Firstname");

                for (int i = 0; i < 3; i++)
                {
                    await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'LastNameInput']", 3);
                }
                found = false;
                while (!found)
                {
                    found = _chromeDevCtr.InputText(accountTip.Lastname);
                }
                m_handlerWriteStatus("Input Lastname");
                Thread.Sleep(2000);

                for (int i = 0; i < 10; i++)
                {
                    frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                    found = await _chromeDevCtr.FindElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']");
                    if (found)
                    {
                        await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                        break;
                    }
                    else if (i == 9)
                    {
                        m_handlerWriteStatus("Step1 Button Error");
                    }
                    Thread.Sleep(1000);
                }
                m_handlerWriteStatus("Step1 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'DOBDayInput']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Birthday.Day.ToString());
                            }
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("DOBDayInput Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'DOBMonthInput']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("DOBMonthInput Error");
                    return bflg;
                }

                found = false;
                while (!found)
                {
                    found = _chromeDevCtr.InputText(accountTip.Birthday.Month.ToString());
                }
                
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'DOBYearInput']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("DOBYearInput Error");
                    return bflg;
                }
                found = false;
                while (!found)
                {
                    found = _chromeDevCtr.InputText(accountTip.Birthday.Year.ToString());
                }
                m_handlerWriteStatus("Input Birthday");
                
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("Step2 Button Error");
                    return bflg;
                }
                m_handlerWriteStatus("Step2 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'AddressSearch']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Address);
                            }
                            Thread.Sleep(2000);

                            for (int j = 0; j < 4; j++)
                            {
                                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "label[class = '_1sr78wsf']", 3);
                                Thread.Sleep(500);
                            }
                            for (int j = 0; j < 4; j++)
                            {
                                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'AddressSearch']", 3);
                                Thread.Sleep(500);
                            }

                            Thread.Sleep(5000);
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Address Error");
                            return bflg;
                        }
                        Thread.Sleep(2000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Input Address");
                Thread.Sleep(1000);

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "div[class = '_51c4qs5']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("_51c4qs5 Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Address Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Step3 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Step3 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'EmailInput']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Email);
                            }
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Email Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Input Email");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Step4 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Step4 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'PhoneNumberInput']", 3);
                        if (found)
                        {
                            found = false;
                            while(!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Phone);
                            }
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("PhoneNumber Error");
                            return bflg;
                        }
                        Thread.Sleep(2000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Input PhoneNumber");

                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("Step5 Button Error");
                    return bflg;
                }
                m_handlerWriteStatus("Step5 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'UsernameInput']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Username);
                            }
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Username Error");
                            return bflg;
                        }
                        Thread.Sleep(2000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Input Username");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Step6 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(2000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Step6 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'MothersMaidenInput']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText(accountTip.Mothername);
                            }
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Mothername Error");
                            return bflg;
                        }
                        Thread.Sleep(2000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Input Mothername");

                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'SecurityAnswerInput']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("Answer Error");
                    return bflg;
                }

                found = false;
                while (!found)
                {
                    found = _chromeDevCtr.InputText(accountTip.Answer);
                }
                m_handlerWriteStatus("Input Answer");

                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                if (!found)
                {
                    m_handlerWriteStatus("Step7 Button Error");
                    return bflg;
                }
                m_handlerWriteStatus("Step7 Button Click");

                char[] pinArray = accountTip.LoginPIN.ToCharArray();

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin1Input']", 3);
                        if (found)
                        {
                            found = false;
                            while (!found)
                            {
                                found = _chromeDevCtr.InputText((pinArray[0]).ToString());
                            }
                            break;
                        }
                        Thread.Sleep(500);
                    }
                    catch { }
                }

                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin2Input']", 3);
                _chromeDevCtr.InputText((pinArray[1]).ToString());
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin3Input']", 3);
                _chromeDevCtr.InputText((pinArray[2]).ToString());
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin4Input']", 3);
                _chromeDevCtr.InputText((pinArray[3]).ToString());
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin5Input']", 3);
                _chromeDevCtr.InputText((pinArray[4]).ToString());
                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'Pin6Input']", 3);
                _chromeDevCtr.InputText((pinArray[5]).ToString());
                m_handlerWriteStatus("Input PIN");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'SubmitForm']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Step8 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Step8 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'registrationMarketingPreferencesSelectAllProducts']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Select1 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Select1 Button Click");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                        found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'registrationMarketingPreferencesSelectAllChannels']", 3);
                        if (found)
                        {
                            break;
                        }
                        else if (i == 9)
                        {
                            m_handlerWriteStatus("Select2 Button Error");
                            return bflg;
                        }
                        Thread.Sleep(1000);
                    }
                    catch { }
                }
                m_handlerWriteStatus("Select2 Button Click");

                found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'registrationMarketingPreferencesSubmitFormAccept']", 3);
                m_handlerWriteStatus("Accept Button Click");

                for (int i = 0; i < 10; i++)
                {
                    frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                    found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "input[data-qa = 'TermsPPCheck']", 3);
                    if (found)
                    {
                        break;
                    }
                    else if (i == 9)
                    {
                        m_handlerWriteStatus("Term CheckBox Error");
                        return bflg;
                    }
                    Thread.Sleep(1000);
                }
                m_handlerWriteStatus("Click Term CheckBox");

                for (int i = 0; i < 10; i++)
                {
                    frameDocumentNodeId = _chromeDevCtr._chromeSession.SendAsync(new RequestNodeCommand { ObjectId = frameDocumentObjectId }).Result.Result.NodeId;
                    found = await _chromeDevCtr.FindAndClickElement(frameDocumentNodeId, "button[data-qa = 'registrationFormReviewSubmit']", 3);
                    if (found)
                    {
                        break;
                    }
                    else if (i == 9)
                    {
                        m_handlerWriteStatus("Create Button Error");
                        return bflg;
                    }
                    Thread.Sleep(1000);
                }
                m_handlerWriteStatus("Create Button Click");

                m_handlerWriteStatus("Create Account Success");
                _chromeDevCtr.Close_Browser();
                SetHistory(accountTip);
                m_history.CreateTime = DateTime.Now;
                bflg = true;
                m_handlerWriteStatus("Start Insert DB");
                SaveDB(m_history);
            }
            catch (Exception ex)
            {
                m_handlerWriteStatus("Create Account Error => " + ex.Message);
            }
            return bflg;
        }

        public void SetHistory(SkybetAccountTip accountTip)
        {
            try
            {
                m_history.Fullname = accountTip.Fullname;
                m_history.Firstname = accountTip.Firstname;
                m_history.Lastname = accountTip.Lastname;
                m_history.Birthday = accountTip.Birthday;
                m_history.Address = accountTip.Address;
                m_history.Phone = accountTip.Phone;
                m_history.Mothername = accountTip.Mothername;
                m_history.Answer = accountTip.Answer;
                m_history.Username = accountTip.Username;
                m_history.LoginPIN = accountTip.LoginPIN;
                m_history.Email = accountTip.Email;
            }
            catch { }
        }
        public void SaveDB(SkybetHistory history)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                m_databaseConn = new SQLiteConnection(m_connectionString);
                m_databaseConn.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS tb_Skybet (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Username TEXT, 
                    LoginPIN TEXT, 
                    Fullname TEXT,
                    Firstname TEXT, 
                    Lastname TEXT,
                    Birthday TEXT,
                    Address TEXT,
                    Email TEXT,
                    Phone TEXT,
                    Mothername TEXT,
                    Answer TEXT,
                    CreateTime TEXT,
                    RegionIP TEXT
                )";

                cmd = new SQLiteCommand(createTableQuery, m_databaseConn);
                cmd.ExecuteNonQuery();

                string insertQuery = @"
                            INSERT INTO tb_Skybet (Username, LoginPIN, Fullname, Firstname, Lastname, Birthday, Address, Email, Phone, Mothername, Answer, CreateTime, RegionIP) 
                            VALUES (@Username, @LoginPIN, @Fullname, @Firstname, @Lastname, @Birthday, @Address, @Email, @Phone, @Mothername, @Answer, @CreateTime, @RegionIP)";

                cmd = new SQLiteCommand(insertQuery, m_databaseConn);
                cmd.Parameters.AddWithValue("@Username", history.Username);
                cmd.Parameters.AddWithValue("@LoginPIN", history.LoginPIN);
                cmd.Parameters.AddWithValue("@Fullname", history.Fullname);
                cmd.Parameters.AddWithValue("@Firstname", history.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", history.Lastname);
                cmd.Parameters.AddWithValue("@Birthday", history.Birthday);
                cmd.Parameters.AddWithValue("@Address", history.Address);
                cmd.Parameters.AddWithValue("@Email", history.Email.ToString()); // DateTime을 문자열로 변환
                cmd.Parameters.AddWithValue("@Phone", history.Phone);
                cmd.Parameters.AddWithValue("@Mothername", history.Mothername);
                cmd.Parameters.AddWithValue("@Answer", history.Answer);
                cmd.Parameters.AddWithValue("@CreateTime", history.CreateTime);
                cmd.Parameters.AddWithValue("@RegionIP", history.RegionIP);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                m_handlerWriteStatus("Insert Database Error => " + ex.Message);
            }
        }
    }
}
