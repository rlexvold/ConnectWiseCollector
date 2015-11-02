using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectWiseCollector
{
    class CustomerMessage
    {
        private string customerId;
        private string type;
        private DateTime messageTime = new DateTime();
        private string source;
        private string message;
        private string uriPath = "customermessage";
        private string uuid = Guid.NewGuid().ToString();

        public string getUriPath()
        {
            return uriPath;
        }

        public void setUriPath(String path)
        {
            this.uriPath = path;
        }

        public string getMessage()
        {
            return message;
        }

        public void setMessage(String message)
        {
            this.message = message;
        }

        public string getCustomerId()
        {
            return customerId;
        }
        public void setCustomerId(string id)
        {
            this.customerId = id;
        }
        public void setType(String type)
        {
            this.type = type;
        }
        public string getType()
        {
            return type;
        }
        public void setSource(string source)
        {
            this.source = source;
        }
        public string getSource()
        {
            return this.source;
        }
        public string getUuid()
        {
            return uuid;
        }

        public override string ToString()
        {
            string output = "UUID: " + uuid;
            output += "\ncustomerId: " + customerId;
            output += "\nType: " + type;
            output += "\nSource: " + source;
            output += "\nTime: " + messageTime;
            output += "\nMessage: " + message;
            output += "\n";
            return output;
        }
    }
}
