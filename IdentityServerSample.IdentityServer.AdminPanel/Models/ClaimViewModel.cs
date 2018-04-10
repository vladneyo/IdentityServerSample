namespace IdentityServerSample.IdentityServer.AdminPanel.Models
{
    public class ClaimViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public ClaimViewModel(int id, string type, string value)
        {
            Id = id;
            Type = type;
            Value = value;
        }
    }
}
