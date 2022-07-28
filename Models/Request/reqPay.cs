namespace K_STEM_LAB.Models.Request
{
    public class reqPay
    {

        public string branch_id { get; set; }
        public int customer_id { get; set; }
        public int pay_type_id { get; set; }
        public int pay_account_id { get; set; }
        public int pay_item_id { get; set; }
        public string document_date { get; set; }
        public decimal income { get; set; }
        public string payer_name { get; set; }
        public string note { get; set; }
        public int is_confirmed { get; set; }
        
    }
}