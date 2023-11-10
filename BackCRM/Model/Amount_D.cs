namespace BackCRM.Model
{
    public class Amount_D
    {
        public int id { get; set; }
        public string? empid { get; set; }
        public string? payment { get; set; }
        public int amount { get; set; }
        public string? car { get; set; }
        public string? memo { get; set; }
        public DateTime? a_sysdt { get; set; }
        public string? a_user { get; set; }
        public DateTime? u_sysdt { get; set; }
        public string? u_user { get; set; }
    }
}
