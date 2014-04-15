using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TargetServerCommunicator
{
    /// <summary>
    /// Communicates with a SAD server.
    /// </summary>
    public class TargetServerInterface : ITargetServer
    {
        private const string ROUTE_START = "start";
        private const string ROUTE_STOP  = "stop";
        private const string ROUTE_GAMES = "games";

        private string m_teamName;

        public TargetServerInterface(string teamName, string ipAddress, int port)
        {
            IpAddress   = ipAddress;
            Port        = port;
            m_teamName  = teamName;
        }

        public TargetServerInterface(string teamName)
            : this(teamName, "10.0.0.8", 3000)
        {
        }

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Gets or sets the port 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Writes data to the uri and returns the response as a string.
        /// </summary>
        /// <param name="route"></param>
        private string Pull(string route, string routeData)
        {
            string request = "";

            using (var client = new WebClient())
            {
                string baseHost = string.Format("http://{0}:{1}/{2}", IpAddress, Port, route);
                request         = client.DownloadString(baseHost);
            }

            return request;
        }

        public void StartGame(string gameName)
        {
            using (var client = new WebClient())
            {
                string baseHost = string.Format("http://{0}:{1}/{2}", IpAddress, Port, ROUTE_START);
                var nameValue   = new NameValueCollection();
                nameValue.Add("teamname", m_teamName);
                nameValue.Add("gamename", gameName);
                client.UploadValues(baseHost, nameValue);
            }
        }
        public void StopRunningGame()
        {
            using (var client = new WebClient())
            {
                string baseHost = string.Format("http://{0}:{1}/{2}", IpAddress, Port, ROUTE_STOP);
                var nameValue = new NameValueCollection();
                nameValue.Add("teamname", m_teamName);                
                client.UploadValues(baseHost, nameValue);
            }
        }        
        public  ICollection<Target> RetrieveTargetList(string game)
        {
            var data        = new Collection<Target>();
            var response    = Pull(ROUTE_GAMES, "");
            data.Add(new Target() { Name = response});
            
            //TODO: parse for target data

            return data;
        }
        /// <summary>
        /// Retrieves a list of game names
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public ICollection<string> RetrieveGameList()
        {
            var games = new Collection<string>();
            var data = Pull(ROUTE_GAMES, "");
            games.Add(data);

            //TODO: Parse game list

            return games;
        }
    }
}
