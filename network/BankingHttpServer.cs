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
        /// 
        /// </summary>
        private string ServerAddress { get; } 
        /// <summary>
        /// Path of bin directory 
        /// </summary>
        private string BinPath { get; } = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); 
        /// <summary>
        /// Web site's file system delimiter characters 
        /// </summary>
        private char[] WebSiteFSDelimiterChars { get; } = { '/', '\\' }; 
        /// <summary>
        /// Allowed web site's paths 
        /// </summary>
        private List<string> WebPaths = new List<string>();
        /// <summary>
        /// Confidurations related to the core server
        /// </summary>
        private Banking.Common.Models.CoreServerSettings Settings { get; set; }
        #endregion  // Private properties

        #region Constructors
        /// <summary>
        /// Default constructor 
        /// </summary>
        public BankingHttpServer(string configFile)
        {
            Settings = (new Banking.Common.Configurator()).GetCoreServerConfigSettings(configFile); 
            ServerAddress = Settings.ServerAddress; 
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
            System.Console.WriteLine("Environment: " + Settings.Environment.ToLower()); 

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
            string url = ctx.Request.Url.ToString(); 
            string body = (new System.IO.StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding)).ReadToEnd(); 
            byte[] buf = Encoding.UTF8.GetBytes(GetResponseText(url, body));

            System.Console.WriteLine("body: " + body); 
            System.Console.WriteLine(ctx.Response.StatusCode + " " + ctx.Response.StatusDescription + ": " + ctx.Request.Url);

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
        private string GetResponseText(string url, string body)
        {
            if (!IsPathValid(url)) return "Path is not valid"; 
            if (url.Contains("/atm/")) foreach (string path in Settings.HttpPathsAtm) if (url.Contains(path)) return ProcessAtm(url, body); 
            if (url.Contains("/eftpos/")) foreach (string path in Settings.HttpPathsEftpos) if (url.Contains(path)) return ProcessEfpos(url, body); 
            if (Settings.Environment.ToLower() == "test") foreach (string path in Settings.HttpPathsDbg) if (url.Contains(path)) return ProcessDbg(url);
            return "Page is not found";
        }

        private string ProcessAtm(string url, string body)
        {
            return "ProcessAtm: " + body; 
        }
        private string ProcessEfpos(string url, string body)
        {
            return "ProcessEfpos: " + body; 
        }
        private string ProcessDbg(string url)
        {
            return "ProcessDbg"; 
        }
        #endregion  // Request processing 

        #region Web site's folder structure
        /// <summary>
        /// Adds all the possible paths into WebPaths dictionary 
        /// </summary>
        private void AddWebPaths()
        {
            foreach (string atmUid in Settings.AtmUid) foreach (string path in Settings.HttpPathsAtm) WebPaths.Add("/atm/" + atmUid + path);
            foreach (string eftposUid in Settings.EftposUid) foreach (string path in Settings.HttpPathsEftpos) WebPaths.Add("/eftpos/" + eftposUid + path);
            if (Settings.Environment.ToLower() == "test") foreach (string path in Settings.HttpPathsDbg) WebPaths.Add(path);
            if (Settings.PrintWebPaths) foreach (string path in WebPaths) System.Console.WriteLine("webpath: " + path); 
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
                if (!string.IsNullOrEmpty(purePath)) listener.Prefixes.Add(ServerAddress + purePath + "/");
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
                if (!string.IsNullOrEmpty(purePath) && pureUrl.Contains(purePath)) return true; 
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
