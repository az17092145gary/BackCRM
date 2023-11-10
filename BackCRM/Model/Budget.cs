namespace BackCRM.Model
{
    public class Budget
    {
        public int id { get; set; }
        public string? empid { get; set; }
        public int budget { get; set; }
        public DateTime? a_sysdt { get; set; }
        public string? a_user { get; set; }
        public DateTime? u_sysdt { get; set; }
        public string? u_user { get; set; }
    }
}
