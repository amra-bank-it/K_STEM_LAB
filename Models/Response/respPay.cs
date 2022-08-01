namespace K_STEM_LAB.Models.Response.respPay
{
  public class Root
  {
    public bool success { get; set; }
    public List<object> errors { get; set; }
    public Model model { get; set; }
  }

  public class Model
    {
        public int id { get; set; }
        public string branch_id { get; set; }
        public object location_id { get; set; }
        public string customer_id { get; set; }
        public string pay_type_id { get; set; }
        public string pay_account_id { get; set; }
        public string pay_item_id { get; set; }
        public object teacher_id { get; set; }
        public object commodity_id { get; set; }
        public object ctt_id { get; set; }
        public string document_date { get; set; }
        public int income { get; set; }
        public string payer_name { get; set; }
        public string note { get; set; }
        public bool is_confirmed { get; set; }

    }
}