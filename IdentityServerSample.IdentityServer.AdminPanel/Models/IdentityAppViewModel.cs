using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerSample.IdentityServer.AdminPanel.Models
{
    public class IdentityAppViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }

        public IdentityAppViewModel(string id, string name, string displayName, string type)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Type = type;
        }
    }
}
