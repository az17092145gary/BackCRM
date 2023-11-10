namespace BackCRM.Model
{
    public class Note
    {
        public int id { get; set; }
        public string? emplid { get; set; }
        public string? custid { get; set; }
        public string? custname { get; set; }
        public string? custphone { get; set; }
        public string? brand { get; set; }
        public string? series { get; set; }
        public string? model { get; set; }
        public string? color { get; set; }
        public string? status { get; set; }
        public int km { get; set; }
        public string? equipment { get; set; }
        public string? memo { get; set; }
        public DateTime? a_sysdt { get; set; }
        public string? a_user { get; set; }
        public DateTime? u_sysdt { get; set; }
        public string? u_user { get; set; }
    }
}
