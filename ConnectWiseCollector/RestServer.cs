using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWiseCollector
{
    class RestServer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RestServer));

        public void writeCustomerMessage(CustomerMessage message)
        {
            RestClient client = new RestClient(SoftbladeParameters.getParameter(SoftbladeParameters.REST_SERVER));
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(message.getUriPath()+"/put", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            logger.Info("Writing message, raw: " + message);
            request.AddJsonBody(message);
            logger.Info("JSON: " + request);
            // execute the request
            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
            {
                logger.Error(response.StatusCode + ": Error writing REST message: " + response.ResponseStatus + response.StatusDescription + response.Content);
                throw new Exception("Error writing message");
            }
            else
            {
                logger.Debug("Wrote message, response: " + response.Content);
            }
            logger.Debug("Exiting");
      }
   }
}
