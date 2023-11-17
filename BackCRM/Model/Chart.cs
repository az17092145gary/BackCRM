namespace BackCRM.Model
{
    public class Pie
    {
        public string empid { get; set; }
        public string emplname { get; set; }
        public int balance { get; set; }
        public int total { get; set; }
        public Root root { get; set; }
    }
    public class Data
    {
        public int value { get; set; }
        public string name { get; set; }
    }

    public class Series
    {
        public string type { get; set; }
        public Label label { get; set; }
        public List<Data> data { get; set; }
    }

    public class Label
    {
        public bool show { get; set; }
    }

    public class Root
    {
        public List<Series> series { get; set; }
    }
}
