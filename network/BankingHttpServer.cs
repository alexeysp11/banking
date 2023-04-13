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
        private Dictionary<string, string> WebPaths = new Dictionary<string, string>();
        #endregion  // Private properties

        #region Constructors
        /// <summary>
        /// Default constructor 
        /// </summary>
        public BankingHttpServer()
        {
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
            if (IsPathValid(url, WebPaths["atm/pin/enter"]))
            {
                return "<html><head><title>atm/pin/enter</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>atm/pin/enter</body></html>"; 
            }
            else if (IsPathValid(url, WebPaths["test"]))
            {
                return "<html><head><title>test</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>test</body></html>"; 
            }
            else if (IsPathValid(url, WebPaths["dbg"]))
            {
                return "<html><head><title>Debug</title></head><body>Hello, this is a custom BankingCoreHttpServer.<br>Debug</body></html>";
            }
            return "Page is not found";
        }
        #endregion  // Request processing 

        #region Web site's folder structure
        /// <summary>
        /// Adds all the possible paths into WebPaths dictionary 
        /// </summary>
        private void AddWebPaths()
        {
            WebPaths.Add("atm/pin/enter", "/atm/pin/enter/");
            WebPaths.Add("test", "/test/");
            WebPaths.Add("dbg", "/dbg/");
        }

        /// <summary>
        /// Adds all the possible paths from WebPaths dictionary into HttpListener.Prefixes 
        /// </summary>
        /// <param name="listener"></param>
        private void AddPrefixes(HttpListener listener)
        {
            foreach (string key in WebPaths.Keys)
            {
                listener.Prefixes.Add("http://localhost:8080/" + WebPaths[key].TrimStart(WebSiteFSDelimiterChars).TrimEnd(WebSiteFSDelimiterChars) + "/");
            }
        }

        /// <summary>
        /// Checks if web site path is valid 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsPathValid(string url, string path)
        {
            return url.Contains(path.TrimStart(WebSiteFSDelimiterChars).TrimEnd(WebSiteFSDelimiterChars));
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
            return System.IO.File.ReadAllText(BinPath.TrimEnd(WebSiteFSDelimiterChars) + @"\" + filepath.TrimStart(WebSiteFSDelimiterChars)); 
        }
        #endregion  // Getting files 
    }
}
