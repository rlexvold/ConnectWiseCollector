using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWiseCollector
{
    public class SoftbladeParameters
    {
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(SoftbladeParameters));
	    public static string CUSTOMER_ID_PARAMETER	= "customer.id";
	    public static string REST_SERVER = "restServer.url";
        public static string SITE = "connectWise.site";
        public static string COMPANY = "connectWise.company";
        public static string USERNAME = "connectWise.username";
        public static string PASSWORD = "connectWise.password";
        public static string SOURCE = "message.source";
        public static string TYPE = "message.type";
	    private IDictionary<string, string> valueDictionary = new Dictionary<string, string>();
        private static SoftbladeParameters	instance;

        public static string DebugOutput()
        {
            return get().ToString();
        }

        public override string ToString()
        {
            string output = "SoftbladeParameters contents\n";
            foreach (KeyValuePair<string, string> entry in valueDictionary)
            {
                output += entry.Key + "=" + entry.Value + "\n";
            }
            return output;
        }

        private static SoftbladeParameters get()
        {
            if (instance == null)
                instance = new SoftbladeParameters();
            return instance;
        }

        public SoftbladeParameters()
        {
            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                setParam(de.Key.ToString(),de.Value.ToString());
            }
        }

        public static void setParameter(string param, string value)
        {
            get().setParam(param, value);
        }

        private void setParam(string param, string value)
        {
            valueDictionary[param] = value;
        }

        public static string getParameter(string param)
        {
            return get().getParam(param);
        }

        private string getParam(string param)
        {
            string value = "";
            try
            {
                value = valueDictionary[param];
                if (value == null)
                {
                    LOGGER.Warn("Parameter not found: " + param);
                }
            }
            catch (Exception e)
            {
                LOGGER.Error(e);
            }
            return value;
        }
    }
}
