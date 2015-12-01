using System;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using ConnectWiseSDK;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWiseCollector
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
        private static Boolean sendToServer = true;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo(args[0]));
            Boolean success = true;
            string type = null;
            string filter = "";
          
            type = args[1];
            filter = args[2];
            if (args.Length > 3)
            {
                sendToServer = false;
            }
            TheCollector theCollector = new TheCollector();
            theCollector.setSite(SoftbladeParameters.getParameter(SoftbladeParameters.SITE));
            theCollector.setCompany(SoftbladeParameters.getParameter(SoftbladeParameters.COMPANY));
            theCollector.setUsername(SoftbladeParameters.getParameter(SoftbladeParameters.USERNAME));
            theCollector.setPassword(SoftbladeParameters.getParameter(SoftbladeParameters.PASSWORD));

 //           logger.Info("SoftbladeParameters: " + SoftbladeParameters.DebugOutput());
            try
            {
                string message = null;
                logger.Info("Getting " + type);
                switch (type.ToLower())
                {
                    case "servicetickets":
                        message = JsonConvert.SerializeObject(theCollector.CollectServiceTickets(filter));
                        break;
                    case "agreements":
                        message = JsonConvert.SerializeObject(theCollector.CollectAgreements(filter));
                        break;
                    case "agreementtypes":
                        message = JsonConvert.SerializeObject(theCollector.CollectAgreementTypes(filter));
                        break;
                    case "agreementadditions":
                        message = JsonConvert.SerializeObject(theCollector.CollectAgreementAdditions(filter));
                        break;
                    case "companies":
                        message = JsonConvert.SerializeObject(theCollector.CollectCompanyInfo(filter));
                        break;
                    case "company":
                        int companyId = Int32.Parse(filter);
                        message = JsonConvert.SerializeObject(theCollector.getCompany(companyId));
                        break;
                    case "serviceticket":
                        int ticketId = Int32.Parse(filter);
                        message = JsonConvert.SerializeObject(theCollector.getTicket(ticketId));
                        break;
                    case "projects":
                        message = JsonConvert.SerializeObject(theCollector.CollectProjects(filter));
                        break;
                    case "products":
                        message = JsonConvert.SerializeObject(theCollector.CollectProducts(filter));
                        break;
                    case "invoices":
                        message = JsonConvert.SerializeObject(theCollector.CollectInvoices(filter));
                        break;
                    case "purchaseorders":
                        message = JsonConvert.SerializeObject(theCollector.CollectPurchaseOrders(filter));
                        break;
                    case "configurations":
                        message = JsonConvert.SerializeObject(theCollector.CollectConfigurations(filter));
                        break;
                    case "manageddevices":
                        message = JsonConvert.SerializeObject(theCollector.CollectManagedDevices(filter));
                        break;
                    default:
                        logger.Error("You need to specify a type on the command line");
                        success = false;
                        break;
                }
                if(message != null)
                    writeMessage(type.ToLower(), message);

            }
            catch (Exception e)
            {
                logger.Error("Error getting " + type + ": " + e.Message);
                success = false;
            }

            if (!success)
                System.Environment.Exit(-1);
        }

        private static void writeMessage(string type, string message)
        {
            string source = SoftbladeParameters.getParameter(SoftbladeParameters.SOURCE);
            string customerId = SoftbladeParameters.getParameter(SoftbladeParameters.CUSTOMER_ID_PARAMETER);

            RestServer rs = new RestServer();

            CustomerMessage output = new CustomerMessage();
            output.setMessage(message);
            output.setCustomerId(customerId);
            output.setSource(source);
            output.setType(type);
            logger.Debug("Writing message: " + output.ToString());
            if (sendToServer)
            {
                rs.writeCustomerMessage(output);
            }
        }
    }
}
