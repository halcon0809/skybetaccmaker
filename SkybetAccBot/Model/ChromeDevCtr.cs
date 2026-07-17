using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

using Newtonsoft.Json.Linq;
using ChromeDevTools.Protocol.Chrome.Emulation;
using ChromeDevTools.Protocol.Chrome.Input;
using MasterDevs.ChromeDevTools;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Browser;
using MasterDevs.ChromeDevTools.Protocol.Chrome.DOM;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Fetch;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Input;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Network;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Page;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Runtime;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Schema;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Target;
using CloseCommand = MasterDevs.ChromeDevTools.Protocol.Chrome.Page.CloseCommand;
using GetResponseBodyCommand = MasterDevs.ChromeDevTools.Protocol.Chrome.Network.GetResponseBodyCommand;

namespace SkybetAccBot.Model
{
    public class ChromeDevCtr
    {
        long documentNodeId = 1;

        IChromeProcess _browserObj = null;

        public IChromeSession _chromeSession = null;

        ChromeSessionFactory _chromeSessionFactory = null;
        UserAgentMetadata _userAgentMetadata = null;
        object _lockerSession = new object();
        private Point cur_point = new Point(0, 0);

        public string _token = "";

        public string PageContent = "";
        public bool bPageLoad = false;

        public bool bIsLogin = false;
        public string m_responseURL = "";
        public enum MoveMethod
        {
            SQRT,
            BEZIER
        }

        List<string> _args = new List<string>()
            {
                //"--headless --disable-gpu",
                "--no-first-run","--disable-default-apps","--no-default-browser-check","--disable-breakpad",
                "--disable-crash-reporter","--no-crash-upload","--deny-permission-prompts",
                "--autoplay-policy=no-user-gesture-required","--disable-prompt-on-repost",
                "--disable-search-geolocation-disclosure","--password-store=basic","--use-mock-keychain",
                "--force-color-profile=srgb","--disable-blink-features=AutomationControlled","--disable-infobars",
                "--disable-session-crashed-bubble","--disable-renderer-backgrounding",
                "--disable-backgrounding-occluded-windows","--disable-background-timer-throttling",
                "--disable-ipc-flooding-protection","--disable-hang-monitor","--disable-background-networking",
                "--metrics-recording-only","--disable-sync","--disable-client-side-phishing-detection",
                "--disable-component-update","--disable-features=TranslateUI,enable-webrtc-hide-local-ips-with-mdns,OptimizationGuideModelDownloading,OptimizationHintsFetching",
                "--start-maximized",
                "--proxy-server=gw.dataimpulse.com:823"
            };

        Random random = new Random();
        int mouseSpeed = 20;
        private static readonly NormalDistribution targetDistribution = new NormalDistribution();
        private static readonly NormalDistribution midpointDistribution = new NormalDistribution();

        public ChromeDevCtr(IChromeSession chromeSession)
        {
            _chromeSession = chromeSession;
        }
        public void InitializeBrowser()
        {
            string _chromePath = "";
            if (File.Exists("chromePath.txt"))
                _chromePath = File.ReadAllText("chromePath.txt");

            string user_dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            user_dir = user_dir + "\\Chrome_data\\";

            var chromeProcessFactory = new ChromeProcessFactory(new StubbornDirectoryCleaner(), _chromePath);
            _browserObj = chromeProcessFactory.Create(
                new ChromeBrowserSettings() { UseRandomPort = true, Args = _args.ToArray(), DataDir = user_dir });

            InitializeChromeSession();
        }

        protected void InitializeChromeSession()
        {
            if (_browserObj is null)
            {
                return;
            }

            var session_result = _browserObj.GetSessionInfo().Result;
            var sessionInfo = _browserObj.GetSessionInfo().Result.LastOrDefault(c => c.Type == "page");
            _chromeSessionFactory = new ChromeSessionFactory();

            _chromeSession = _chromeSessionFactory.Create(sessionInfo.WebSocketDebuggerUrl) as ChromeSession;

            var resultUserAgentBrands = _chromeSession.SendAsync(new EvaluateCommand() { Expression = "JSON.stringify(window.navigator.userAgentData.brands)" }).Result;
            if (resultUserAgentBrands.Result.Result.Value == null)
            {
                //Пустая страница почему-то
                NavigateInvoke("chrome://new-tab-page");
                Thread.Sleep(2000);
                resultUserAgentBrands = _chromeSession.SendAsync(new EvaluateCommand() { Expression = "JSON.stringify(window.navigator.userAgentData.brands)" }).Result;
            }

            _userAgentMetadata = new UserAgentMetadata()
            {
                Platform = "Windows",
                PlatformVersion = "",
                Architecture = "",
                Model = "",
                Mobile = false
            };
            InitSession("about:blank");
        }

        private void InitSession(string url, string proxyUsername = null, string proxyPassword = null)
        {
            lock (_lockerSession)
            {
                var targetInfo = _chromeSession.SendAsync(new CreateTargetCommand() { Url = url }).Result;

                var allSessions = _browserObj.GetSessionInfo().Result;
                foreach (var session in allSessions)
                {
                    // Close all other sessions
                    if (session.Id != targetInfo.Result.TargetId)
                    {
                        _chromeSession.SendAsync(new CloseTargetCommand() { TargetId = session.Id }).Wait();
                    }
                    else
                    {
                        _chromeSession.Dispose();
                        _chromeSession = _chromeSessionFactory.Create(session.WebSocketDebuggerUrl) as ChromeSession;

                        string scriptResult = File.ReadAllText("ScriptsMobile.js");
                        var injectResult = _chromeSession.SendAsync(new AddScriptToEvaluateOnNewDocumentCommand() { Source = scriptResult }).Result;

                        var pageEnableResult = _chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.Page.EnableCommand>().Result;
                        var domEnableResult = _chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.DOM.EnableCommand>().Result;
                        var networkEnableResult = _chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.Network.EnableCommand>().Result;

                        _chromeSession.Subscribe<RequestWillBeSentEvent>(sendedRequest =>
                        {
                            try
                            {
                                string requestUrl = sendedRequest.Request.Url.ToLower();
                            }
                            catch { }
                        });

                        var targets = _chromeSession.SendAsync(new SetDiscoverTargetsCommand() { Discover = true }).Result;
                        //finish page load
                        _chromeSession.Subscribe<LoadEventFiredEvent>(loadEvent =>
                        {
                            // we cannot block in event handler, hence the task
                            Task.Run(async () =>
                            {
                                documentNodeId = (await _chromeSession.SendAsync(new GetDocumentCommand())).Result.Root.NodeId;
                                injectResult = (await _chromeSession.SendAsync(new AddScriptToEvaluateOnNewDocumentCommand() { Source = File.ReadAllText("Scripts.js") }));
                            });
                        });



                        _chromeSession.Subscribe<ResponseReceivedEvent>(e =>
                        {
                            Task.Run(async () =>
                            {
                                var resp_url = e.Response.Url;
                                if (resp_url == m_responseURL)
                                {
                                    try
                                    {
                                        var result = (await _chromeSession.SendAsync(new GetResponseBodyCommand() { RequestId = e.RequestId })).Result;
                                        PageContent = result.Body;
                                    }
                                    catch { }
                                }
                            });
                        });
                        _chromeSession.Subscribe<FrameStartedLoadingEvent>(frameStarted =>
                        {

                        });

                        _chromeSession.Subscribe<FrameResizedEvent>(e =>
                        {
                            Task.Run(async () =>
                            {
                                //Console.WriteLine("FrameResizedEvent: ");
                                //Console.WriteLine("Page Loaded");

                            });
                        });
                        //can be FrameStoppedLoadingEvent or LoadEventFiredEvent
                        _chromeSession.Subscribe<FrameStoppedLoadingEvent>(frameStopped =>
                        {

                        });

                        _chromeSession.Subscribe<NavigatedWithinDocumentEvent>(navigatedWithinDocument =>
                        {

                        });

                        _chromeSession.Subscribe<FrameNavigatedEvent>(frameNavigated =>
                        {
                            try
                            {

                            }
                            catch (Exception e)
                            {
                            }
                        });
                        _chromeSession.Subscribe<ExecutionContextCreatedEvent>(executionContext =>
                        {
                            try
                            {

                            }
                            catch (Exception e)
                            {
                            }
                        });
                        _chromeSession.Subscribe<ExecutionContextDestroyedEvent>(contextDestroyed =>
                        {
                            try
                            {

                            }
                            catch (Exception e)
                            {
                            }
                        });

                        _chromeSession.Subscribe<FrameDetachedEvent>(frameDetached =>
                        {

                        });

                        _chromeSession.Subscribe<WebSocketFrameReceivedEvent>(e =>
                        {

                        });
                    }
                }
            }
        }

        public bool bcheckPageLoad()
        {
            bool bflag = true;
            int nTry = 20;
            while (!bPageLoad || string.IsNullOrEmpty(PageContent))
            {
                Thread.Sleep(1000);
                nTry--;
                if (nTry < 0)
                {
                    bflag = false;
                    break;
                }
            }
            return bflag;
        }

        #region Browser Functions
        public bool NavigateInvoke(string visitUrl)
        {
            try
            {
                if (!visitUrl.StartsWith("https://")) visitUrl = "https://" + visitUrl;
                ExecuteScript(string.Format("location.href==='{0}'?0:location.href='{0}'", visitUrl));
            }
            catch (Exception ex)
            {
                int a = 1;
            }
            return true;
        }
        public string ExecuteScript(string jsCode, bool requiredResult = false, bool awaitPromise = false)
        {
            string result = string.Empty;
            try
            {
                if (!requiredResult)
                    _chromeSession.SendAsync(new EvaluateCommand() { Expression = jsCode }).Wait();
                else
                {
                    var script = _chromeSession.SendAsync(new EvaluateCommand() { Expression = jsCode, AwaitPromise = awaitPromise }).Result.Result;
                    if (script.Result.Value == null)
                        return result;

                    result = script.Result.Value.ToString();
                }

            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<bool> ClickOnPoint(string scriptResult, ClickType clickType = ClickType.click, int interval = 1)
        {
            try
            {
                JObject posObject = JObject.Parse(scriptResult);
                decimal x = decimal.Parse(posObject.SelectToken("x").ToString());
                decimal y = decimal.Parse(posObject.SelectToken("y").ToString());
                decimal width = Utils.ParseToDecimal(posObject.SelectToken("width").ToString());
                decimal height = Utils.ParseToDecimal(posObject.SelectToken("height").ToString());
                if (x == 0 && y == 0)
                { }
                else
                {
                    Point point = new Point()
                    {
                        X = (int)x,
                        Y = (int)y
                    };
                    await MouseMovement(point, MoveMethod.SQRT);
                    Thread.Sleep(Utils.GetRandValue(500, 1000));
                    int cnt = 1;
                    if (clickType == ClickType.doubleClick)
                        cnt = 2;
                    else if (clickType == ClickType.TripleClick)
                        cnt = 3;

                    await MouseClick(point, cnt);

                    return true;
                }
            }
            catch (Exception e)
            {
                //m_handlerWriteStatus("ClickOnPoint " + e.ToString());
            }
            return false;
        }
        public async Task<bool> MouseClick(Point point, int clickCnt = 1)
        {
            try
            {
                long button = (long)MouseButton.Left;
                await _chromeSession.SendAsync(new DispatchMouseEventCommand { Type = "mousePressed", Button = "left", ClickCount = clickCnt, Buttons = button, X = point.X, Y = point.Y });
                Thread.Sleep(600);
                await _chromeSession.SendAsync(new DispatchMouseEventCommand { Type = "mouseReleased", Button = "left", ClickCount = clickCnt, Buttons = button, X = point.X, Y = point.Y });
            }
            catch { }
            return true;
        }
        public async Task<bool> CLickOnPoint(int x, int y, ClickType clickType = ClickType.click)
        {
            try
            {
                if (x == 0 && y == 0)
                {
                    return false;
                }
                else
                {
                    Point point = new Point()
                    {
                        X = x,
                        Y = y
                    };
                    await MouseMovement(point, MoveMethod.SQRT);
                    Thread.Sleep(Utils.GetRandValue(500, 1000));
                    await MouseClick(point);
                    return true;
                }
            }
            catch { }

            return true;
        }
        public async Task<bool> CLickElementOn(long documentId, Point point, MoveMethod moveMethod = MoveMethod.BEZIER)
        {
            bool isFound = false;
            try
            {
                await MouseMovement(point, moveMethod);
                await MouseClick(point);
                isFound = true;
            }
            catch { }
            return isFound;
        }
        public async Task<bool> FindAndClickElement(long documentId, string selector, int ClickCnt = 1, MoveMethod moveMethod = MoveMethod.BEZIER)
        {
            bool isFound = false;
            try
            {
                Point cur_point = await GetLocationForElement(documentId, selector);
                if (cur_point.X == 0 && cur_point.Y == 0)
                    return isFound;

                await MouseMovement(cur_point, moveMethod);
                await MouseClick(cur_point, ClickCnt);
                isFound = true;
            }
            catch { }
            return isFound;
        }
        public async Task<bool> FindAndClickElement(long documentId, long bodyId)
        {
            bool isFound = false;
            try
            {
                Point cur_point = await GetLocationForElement(documentId, bodyId);
                if (cur_point.X == 0 && cur_point.Y == 0)
                    return isFound;

                await MouseMovement(cur_point);
                await MouseClick(cur_point, 1);
                isFound = true;
            }
            catch { }
            return isFound;
        }
        public async Task<bool> FindElement(long documentId, string selector)
        {
            bool isFound = false;
            try
            {
                long bodyNodeId = (await _chromeSession.SendAsync(new QuerySelectorCommand
                {
                    NodeId = documentId,
                    Selector = selector
                })).Result.NodeId;

                if (bodyNodeId != 0)
                    isFound = true;

            }
            catch (Exception ex)
            {
            }
            return isFound;
        }
        public async Task<Point> GetLocationForElement(long documentId, string selecter)
        {
            Point point = new Point();
            try
            {
                    long bodyNodeId = (await _chromeSession.SendAsync(new QuerySelectorCommand
                {
                    NodeId = documentId,
                    Selector = selecter
                })).Result.NodeId;

                if (bodyNodeId == 0)
                    return new Point(0, 0);

                var height = (await _chromeSession.SendAsync(new GetBoxModelCommand { NodeId = bodyNodeId })).Result;
                point.X = (int)height.Model.Content[0];
                point.Y = (int)height.Model.Content[1];
            }
            catch { }

            return point;
        }
        public async Task<Point> GetLocationForElement(long documentId, long bodyNodeId)
        {
            Point point = new Point();
            try
            {
                var height = (await _chromeSession.SendAsync(new GetBoxModelCommand { NodeId = bodyNodeId })).Result;
                point.X = (int)height.Model.Content[0];
                point.Y = (int)height.Model.Content[1];
            }
            catch { }
            return point;
        }
        public async Task<bool> MouseMovement(Point target, MoveMethod moveMethod = MoveMethod.BEZIER)
        {
            if (moveMethod == MoveMethod.BEZIER)
                await MoveWithBezier(target);
            else
            {
                Point closeToEndPos = new Point(
                                       target.X + Utils.GetRandValue(5, 30, true),
                                       target.Y + Utils.GetRandValue(5, 30, true)
                                     );
                await MoveWithSqrt(target);
            }

            return true;
        }
        public async Task<bool> MoveWithBezier(Point target)
        {
            try
            {
                int pointerAccuracy = 10; //assume 10 pixels is the accuracy of this particular human's point and click

                //calculate an X-Y offset to mimic the accuracy of a human
                int targetX = target.X + Convert.ToInt32(pointerAccuracy * targetDistribution.NextGaussian());
                int targetY = target.Y + Convert.ToInt32(pointerAccuracy * targetDistribution.NextGaussian());
                //declare the original pointer position
                int originalX = cur_point.X;
                int originalY = cur_point.Y;
                //find a mid point between original and target position
                int midPointX = (target.X - targetX) / 2;
                int midPointY = (target.Y - targetY) / 2;
                //Find a co-ordinate normal to the straight line between start and end point, starting at the midpoint and normally distributed
                //This is reduced by a factor of 4 to model the arc of a right handed user.
                int bezierMidPointX = Convert.ToInt32((midPointX / 4) * (midpointDistribution.NextGaussian()));
                int bezierMidPointY = Convert.ToInt32((midPointY / 4) * (midpointDistribution.NextGaussian()));

                BezierCurve bc = new BezierCurve();
                double[] input = new double[] { originalX, originalY, bezierMidPointX, bezierMidPointY, targetX, targetY };

                int numberOfDataPoints = 1000;
                double[] output = new double[numberOfDataPoints];

                //co-ords are couplets of doubles hence the / 2
                bc.Bezier2D(input, numberOfDataPoints / 2, output);
                int pause = 0;
                List<System.Drawing.Point> points = new List<Point>();
                for (int count = 1; count != numberOfDataPoints - 1; count += 2)
                {
                    Point point = new Point((int)output[count + 1], (int)output[count]);
                    points.Add(point);
                    await _chromeSession.SendAsync(new EmulateTouchFromMouseEventCommand { Type = "mouseMoved", Button = "none", X = point.X, Y = point.Y });
                    if ((count % 10) == 0)
                        pause = 2 + ((count ^ 5) / (count * 2));

                    Thread.Sleep(pause);
                    cur_point = point;
                }
            }
            catch { }
            return true;
        }
        public async Task<bool> MoveWithSqrt(Point ePos)
        {
            try
            {
                int x = ePos.X;
                int y = ePos.Y;
                double randomSpeed = Math.Max((random.Next(mouseSpeed) / 2.0 + mouseSpeed) / 10.0, 0.1);
                await WindMouse((double)cur_point.X, (double)cur_point.Y, (double)x, (double)y, 9.0, 3.0, 10.0 / randomSpeed,
                    15.0 / randomSpeed, 10.0 * randomSpeed, 10.0 * randomSpeed);
            }
            catch { }
            return true;
        }
        public async Task<bool> WindMouse(double xs, double ys, double xe, double ye,
            double gravity, double wind, double minWait, double maxWait,
            double maxStep, double targetArea)
        {

            double dist, windX = 0, windY = 0, veloX = 0, veloY = 0, randomDist, veloMag, step;
            int oldX, oldY, newX = (int)Math.Round(xs), newY = (int)Math.Round(ys);

            double waitDiff = maxWait - minWait;
            double sqrt2 = Math.Sqrt(2.0);
            double sqrt3 = Math.Sqrt(3.0);
            double sqrt5 = Math.Sqrt(5.0);

            dist = Hypot(xe - xs, ye - ys);

            while (dist > 1.0)
            {

                wind = Math.Min(wind, dist);

                if (dist >= targetArea)
                {
                    int w = random.Next((int)Math.Round(wind) * 2 + 1);
                    windX = windX / sqrt3 + (w - wind) / sqrt5;
                    windY = windY / sqrt3 + (w - wind) / sqrt5;
                }
                else
                {
                    windX = windX / sqrt2;
                    windY = windY / sqrt2;
                    if (maxStep < 3)
                        maxStep = random.Next(3) + 3.0;
                    else
                        maxStep = maxStep / sqrt5;
                }

                veloX += windX;
                veloY += windY;
                veloX = veloX + gravity * (xe - xs) / dist;
                veloY = veloY + gravity * (ye - ys) / dist;

                if (Hypot(veloX, veloY) > maxStep)
                {
                    randomDist = maxStep / 2.0 + random.Next((int)Math.Round(maxStep) / 2);
                    veloMag = Hypot(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                xs += veloX;
                ys += veloY;
                dist = Hypot(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);

                if (oldX != newX || oldY != newY)
                {
                    cur_point = new Point(newX, newY);
                    await _chromeSession.SendAsync(new EmulateTouchFromMouseEventCommand { Type = "mouseMoved", Button = "none", X = newX, Y = newY });
                }

                step = Hypot(xs - oldX, ys - oldY);
                int wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);
                Thread.Sleep(wait);
            }

            int endX = (int)Math.Round(xe);
            int endY = (int)Math.Round(ye);
            if (endX != newX || endY != newY)
            {
                cur_point = new Point(endX, endY);
                await _chromeSession.SendAsync(new EmulateTouchFromMouseEventCommand { Type = "mouseMoved", Button = "none", X = endX, Y = endY });
            }

            return true;
        }
        public double Hypot(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public bool InputText(string text)
        {
            try
            {
                _chromeSession.SendAsync(new ImeSetCompositionCommand { Text = text, SelectionStart = 0, SelectionEnd = (long)text.Length });
                Thread.Sleep(800);
            }
            catch { }
            return true;
        }

        public void Close_Browser()
        {
            try
            {
                _chromeSession.SendAsync<CloseCommand>().Wait();

            }
            catch { }
            try
            {
                if (_chromeSession != null)
                {
                    _chromeSession.Dispose();
                    _chromeSession = null;
                }


                if (_browserObj != null)
                {
                    _browserObj.Dispose();
                    _browserObj = null;
                }

            }
            catch (Exception e) { }
        }
        #endregion
        public void SetResponseURL(string url)
        {
            m_responseURL = url;
            PageContent = "";
        }
        public string GetResponse()
        {
            return PageContent;
        }
    }
}
