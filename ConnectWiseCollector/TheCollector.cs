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
        private List<string> returnFields =null;

        public void ConnectWiseCollector()
        {
//            returnFields.Add("Id");
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

        public List<ServiceTicket> CollectServiceTickets(string filter)
        {
            List<string> fields = new List<string>();
            fields.Add("Id");
            ServiceTicketApi serviceTicketApi = new ServiceTicketApi(site, company, username, password, cookie);
            List<TicketFindResult> results = serviceTicketApi.FindServiceTickets(filter, "Id", null, null, null, fields);
            logger.Info("Found " + results.Count + " Service tickets. Getting detailed ticket info...");
            List<ServiceTicket> fullTickets = new List<ServiceTicket>();
            int i = 0;
            foreach (TicketFindResult ticket in results)
            {
                i++;
                if ((i % 100) == 0)
                    logger.Info(i);
            
                try
                {
                    ServiceTicket ticketDetail = serviceTicketApi.GetServiceTicket(ticket.Id);

                    if (ticketDetail != null)
                        fullTickets.Add(ticketDetail);
                    else
                        logger.Info("Ticket details not found for: " + ticket.Id);
                }
                catch (Exception e)
                {
                    logger.Error("Error getting ticket: " + ticket.Id + " - " + e.Message);
                }
            }
            return fullTickets;
        }

        public ServiceTicket getTicket(int ticketId)
        {
            ServiceTicketApi serviceTicketApi = new ServiceTicketApi(site, company, username, password, cookie);
            return serviceTicketApi.GetServiceTicket(ticketId);
        }

        public List<CompanyFindResult> CollectCompanyInfo(string filter)
        {
            CompanyApi companyApi = new CompanyApi(site, company, username, password, cookie);

            List<CompanyFindResult> results = companyApi.FindCompanies(filter, "Id", null, null, null);
            logger.Info("Found " + results.Count + " Companies");
            return results;
        }

        public List<AgreementFindResult> CollectAgreements(string filter)
        {
            AgreementApi api = new AgreementApi(site, company, username, password, cookie);

            List<AgreementFindResult> results = api.FindAgreements(filter, "Id", null, null, true, null);
            logger.Info("Found " + results.Count + " agreements");
            return results;
        }

        public List<AgreementAdditionFindResult> CollectAgreementAdditions(string filter)
        {
            AgreementApi api = new AgreementApi(site, company, username, password, cookie);

            List<AgreementAdditionFindResult> results = api.FindAgreementAdditions(filter, "Id", null, null, true, null);
            logger.Info("Found " + results.Count + " agreement additions");
            return results;
        }

        public List<AgreementWorkTypeFindResult> CollectAgreementTypes(string filter)
        {
            AgreementApi api = new AgreementApi(site, company, username, password, cookie);

            List<AgreementWorkTypeFindResult> results = api.FindAgreementWorkTypes(filter, "Id", null, null, true, null);
            logger.Info("Found " + results.Count + " agreement work types");
            return results;
        }

        public Company getCompany(int id)
        {
            CompanyApi api = new CompanyApi(site, company, username, password, cookie);
            return api.GetCompany(id);
        }

        public List<ProjectFindResult> CollectProjects(string filter)
        {
            ProjectApi api = new ProjectApi(site, company, username, password, cookie);

            List<ProjectFindResult> results = api.FindProjects(filter, "Id", null, null, null, returnFields);
            logger.Info("Found " + results.Count + " projects");
            return results;
        }

        public List<InvoiceFindResult> CollectInvoices(string filter)
        {
            InvoiceApi api = new InvoiceApi(site, company, username, password, cookie);

            List<InvoiceFindResult> results = api.FindInvoices(filter, "Id", null, null, null, returnFields);
            logger.Info("Found " + results.Count + " invoices");
            return results;
        }

        public List<PurchaseOrderFindResult> CollectPurchaseOrders(string filter)
        {
            PurchasingApi api = new PurchasingApi(site, company, username, password, cookie);

            List<PurchaseOrderFindResult> results = api.FindPurchaseOrders(filter, "Id", null, null, returnFields);
            logger.Info("Found " + results.Count + " purchase orders");
            return results;
        }


        public List<ProductFindResult> CollectProducts(string filter)
        {
            ProductApi api = new ProductApi(site, company, username, password, cookie);

            List<ProductFindResult> results = api.FindProducts(filter, "Id", null, null, returnFields);
            logger.Info("Found " + results.Count + " products");
            return results;
        }


        public List<ConfigurationFindResult> CollectConfigurations(string filter)
        {
            ConfigurationApi api = new ConfigurationApi(site, company, username, password, cookie);

            List<ConfigurationFindResult> results = api.FindConfigurations(filter, "Id", null, null, null, returnFields);
            logger.Info("Found " + results.Count + " Configurations");
            return results;
        }

        public List<TicketProduct> CollectTicketProductList(int ticketId)
        {
            ServiceTicketApi api = new ServiceTicketApi(site, company, username, password, cookie);

            List<TicketProduct> results = api.GetTicketProductList(ticketId);
            if (results == null)
                results = new List<TicketProduct>();
            logger.Info("Found " + results.Count + " Ticket Products");
            return results;
        }

        public List<DocumentInfo> CollectTicketDocuments(int ticketId)
        {
            ServiceTicketApi api = new ServiceTicketApi(site, company, username, password, cookie);

            List<DocumentInfo> results = api.GetTicketDocuments(ticketId);
            if (results == null)
                results = new List<DocumentInfo>();
            logger.Info("Found " + results.Count + " Ticket documents");
            return results;
        }

        public List<ManagedMachineData> CollectManagedDevices(string managedSolutionName)
        {
 
            ManagedDeviceApi api = new ManagedDeviceApi(site, company, username, password, cookie);

            logger.Info("Getting managed servers for solutionName/GroupID: " + managedSolutionName);
            List<ManagedMachineData> results = api.GetManagedServers(managedSolutionName, managedSolutionName);
            logger.Info("Got " + results.Count + " managed servers, getting workstations now...");
            results.AddRange(api.GetManagedWorkstations(managedSolutionName, managedSolutionName));
            logger.Info("Found " + results.Count + " total managed Managed Devices");
            return results;
        }
    }
}
