using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetEscapades.Configuration.Validation;

namespace Web.Config
{
    public class TraduoraSecretSettings : IValidatable
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ProjectId { get; set; }

        public void Validate()
        {
            if (ClientId == null) throw new Exception("ClientId cannot be null");
            if (ClientSecret == null) throw new Exception("ClientSecret cannot be null");
            if (ProjectId == null) throw new Exception("ProjectId cannot be null");
        }
    }
}
