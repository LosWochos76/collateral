using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class EMail
    {
        private List<EMailAdress> to = new List<EMailAdress>();
        private List<EMailAdress> cc = new List<EMailAdress>();
        private List<Attachment> attachments = new List<Attachment>();

        public List<EMailAdress> TO
        {
            get { return to; }
        }

        public List<EMailAdress> CC
        {
            get { return cc; }
        }

        public List<Attachment> Attachments
        {
            get { return attachments; }
        }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
