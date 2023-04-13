using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Network
{
    public class BankingHttpServer
    {
        #region Private properties
        /// <summary>
        /// Web site's file system delimiter characters 
        /// </summary>
        private char[] WebSiteFSDelimiterChars { get; } = { '/', '\\' }; 

        /// <summary>
        /// Path of bin directory 
        /// </summary>
        private string BinPath { get; } = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); 

        /// <summary>
        /// Allowed web site's paths 
        /// </summary>
        private List<string> WebPaths = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        private Banking.Common.CoreServerSettings Settings { get; set; }
        #endregion  // Private properties

        #region Constructors
        /// <summary>
        /// Default constructor 
        /// </summary>
        public BankingHttpServer(string configFile)
        {
            Settings = (new Banking.Common.Configurator()).GetCoreServerConfigSettings(configFile); 
            AddWebPaths(); 
        }
        #endregion  // Constructors

        #region Public methods
        /// <summary>
        /// Create web server as HttpListener
        /// </summary>
        public void CreateWebServer()
        {
            HttpListener listener = new HttpListener();
            AddPrefixes(listener); 
            listener.Start();

            System.Console.WriteLine("Start to listen...");

            new Thread(() =>
                {
                    while (true)
                    {
                        HttpListenerContext ctx = listener.GetContext();
                        ThreadPool.QueueUserWorkItem((_) => ProcessRequest(ctx));
                    }
                }
            ).Start();
        }
        #endregion  // Public methods

        #region Request processing 
        /// <summary>
        /// Processes request and sends response back 
        /// </summary>
        /// <param name="ctx"></param>
        private void ProcessRequest(HttpListenerContext ctx)
        {
            string responseText = GetResponseText(ctx.Request.Url.ToString());
            byte[] buf = Encoding.UTF8.GetBytes(responseText);

            System.Console.WriteLine(ctx.Response.StatusCode + " " + ctx.Response.StatusDescription + ": " + ctx.Request.Url);

            System.IO.StreamReader reader = new System.IO.StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding);
            System.Console.WriteLine(reader.ReadToEnd()); 

            ctx.Response.ContentEncoding = Encoding.UTF8;
            ctx.Response.ContentType = "text/html";
            ctx.Response.ContentLength64 = buf.Length;
            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
            ctx.Response.Close();
        }

        /// <summary>
        /// Gets response text depending on a given URL 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetResponseText(string url)
        {
            if (!IsPathValid(url)) return "Path is not valid"; 
            // if (IsPathValid(url, WebPaths["atm/pin/enter"]))
            // {
            //     return "<html><head><title>atm/pin/enter</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>atm/pin/enter</body></html>"; 
            // }
            // else if (IsPathValid(url, WebPaths["test"]))
            // {
            //     return "<html><head><title>test</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>test</body></html>"; 
            // }
            // else if (IsPathValid(url, WebPaths["dbg"]))
            // {
            //     return "<html><head><title>Debug</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>Debug</body></html>";
            // }
            return "Page is not found";
        }
        #endregion  // Request processing 

        #region Web site's folder structure
        /// <summary>
        /// Adds all the possible paths into WebPaths dictionary 
        /// </summary>
        private void AddWebPaths()
        {
            foreach (string atm in Settings.Atm) foreach (string path in Settings.HttpPathsAtm) WebPaths.Add("/atm/" + atm + path);
            foreach (string eftpos in Settings.Eftpos) WebPaths.Add("/eftpos/" + eftpos + "/pin/enter/");
            WebPaths.Add("/test/");
            WebPaths.Add("/dbg/");
        }

        /// <summary>
        /// Adds all the possible paths from WebPaths dictionary into HttpListener.Prefixes 
        /// </summary>
        /// <param name="listener"></param>
        private void AddPrefixes(HttpListener listener)
        {
            string purePath = string.Empty; 
            foreach (string path in WebPaths)
            {
                purePath = path.TrimStart(WebSiteFSDelimiterChars).TrimEnd(WebSiteFSDelimiterChars); 
                if (!string.IsNullOrEmpty(purePath)) listener.Prefixes.Add("http://localhost:8080/" + purePath + "/");
            }
        }

        /// <summary>
        /// Checks if web site path is valid 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool IsPathValid(string url)
        {
            string pureUrl = url.TrimStart(WebSiteFSDelimiterChars).TrimEnd(WebSiteFSDelimiterChars); 
            string purePath = string.Empty; 
            foreach (string path in WebPaths) 
            {
                purePath = path.TrimStart(WebSiteFSDelimiterChars).TrimEnd(WebSiteFSDelimiterChars); 
                if (pureUrl.Contains(purePath)) return true; 
            }
            return false;
        }
        #endregion  // Web site's folder structure

        #region Getting files 
        /// <summary>
        /// Reads all text from specified file located inside bin directory 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string ReadAllTextFromBinDir(string filepath)
        {
            string pureBin = BinPath.TrimEnd(WebSiteFSDelimiterChars); 
            string purePath = filepath.TrimStart(WebSiteFSDelimiterChars); 
            return System.IO.File.ReadAllText(pureBin + @"\" + purePath); 
        }
        #endregion  // Getting files 
    }
}
