using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectWiseSDK;
using log4net;

namespace ConnectWiseCollector
{
    class TheCollector
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(TheCollector));
        private const string cookie = "SoftBlade Collector";

        private string site;
        private string company;
        private string username;
        private string password;
        private List<string> returnFields = new List<string>();

        public void ConnectWiseCollector()
        {
            returnFields.Add("Id");
        }

        public void setSite(String tmp)
        {
            site = tmp;
        }

        public void setCompany(String tmp)
        {
            company = tmp;
        }

        public void setUsername(String tmp)
        {
            username = tmp;
        }

        public void setPassword(String tmp)
        {
            password = tmp;
        }

        public List<ServiceTicket> CollectServiceTickets()
        {
            ServiceTicketApi serviceTicketApi = new ServiceTicketApi(site, company, username, password, cookie);
            List<ServiceTicket> tickets = new List<ServiceTicket>();

            List<TicketFindResult> results = serviceTicketApi.FindServiceTickets("", null, null, null, null, returnFields);
            logger.Info("Found " + results.Count + " Service tickets");

            foreach (TicketFindResult result in results)
            {
                logger.Info("Getting ticket details for: " + result.Id);
                tickets.Add(serviceTicketApi.GetServiceTicket(result.Id));
            }
            return tickets;
        }

        public List<Company> CollectCompanyInfo()
        {
            CompanyApi companyApi = new CompanyApi(site, company, username, password, cookie);

            List<Company> companies = new List<Company>();

            List<CompanyFindResult> results = companyApi.FindCompanies("", null, null, null, returnFields);
            logger.Info("Found " + results.Count + " Companies");
            foreach (CompanyFindResult result in results)
            {
                logger.Info("Getting company details for: " + result.Id);
                companies.Add(companyApi.GetCompany(result.Id));
            }
            return companies;
        }

        public List<Agreement> CollectAgreements()
        {
            AgreementApi api = new AgreementApi(site, company, username, password, cookie);

            List<Agreement> returnList = new List<Agreement>();
            List<AgreementFindResult> results = api.FindAgreements("", "AgreementName", null, null, true, returnFields);
            logger.Info("Found " + results.Count + " agreements");
            foreach (AgreementFindResult result in results)
            {
                logger.Info("Getting agreement details for: " + result.Id);
                returnList.Add(api.GetAgreement(result.Id));
            }
            return returnList;
        }

        public List<Project> CollectProjects()
        {
            ProjectApi api = new ProjectApi(site, company, username, password, cookie);

            List<Project> returnList = new List<Project>();

            List<ProjectFindResult> results = api.FindProjects("", null, null, null, null, returnFields);
            logger.Info("Found " + results.Count + " projects");
            foreach (ProjectFindResult result in results)
            {
                logger.Info("Getting project details for: " + result.Id);
                returnList.Add(api.GetProject(result.Id));
            }
            return returnList;
        }

        public List<Invoice> CollectInvoices()
        {
            InvoiceApi api = new InvoiceApi(site, company, username, password, cookie);

            List<Invoice> returnList = new List<Invoice>();

            List<InvoiceFindResult> results = api.FindInvoices("", null, null, null, null, returnFields);
            logger.Info("Found " + results.Count + " invoices");
            foreach (InvoiceFindResult result in results)
            {
                logger.Info("Getting agreement details for: " + result.Id);
                returnList.Add(api.GetInvoice(result.Id));
            }
            return returnList;
        }

        public List<PurchaseOrder> CollectPurchaseOrders()
        {
            PurchasingApi api = new PurchasingApi(site, company, username, password, cookie);

            List<PurchaseOrder> returnList = new List<PurchaseOrder>();

            List<PurchaseOrderFindResult> results = api.FindPurchaseOrders("", null, null, null, returnFields);
            logger.Info("Found " + results.Count + " purchase orders");
            foreach (PurchaseOrderFindResult result in results)
            {
                logger.Info("Getting purchase order details for: " + result.Id);
                returnList.Add(api.GetPurchaseOrder(result.Id));
            }
            return returnList;
        }


        public List<Configuration> CollectConfigurations()
        {
            ConfigurationApi api = new ConfigurationApi(site, company, username, password, cookie);

            List<Configuration> returnList = new List<Configuration>();

            List<ConfigurationFindResult> results = api.FindConfigurations("", null, null, null, null, returnFields);
            logger.Info("Found " + results.Count + " Configurations");
            foreach (ConfigurationFindResult result in results)
            {
                logger.Info("Getting Configuration details for: " + result.Id);
                returnList.Add(api.GetConfiguration(result.Id));
            }
            return returnList;
        }

        public List<ManagedMachineData> CollectManagedDevices(string managedSolutionName)
        {
 
            ManagedDeviceApi api = new ManagedDeviceApi(site, company, username, password, cookie);

            List<ManagedMachineData> results = api.GetManagedServers(managedSolutionName, null);
            results.AddRange(api.GetManagedWorkstations(managedSolutionName, null));
            logger.Info("Found " + results.Count + " Managed Devices");
            return results;
        }
    }
}
