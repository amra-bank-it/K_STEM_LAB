namespace K_STEM_LAB.Models.Response
{
    public class Itemii
    {
        public int id { get; set; }
        public int branch_id { get; set; }
        public int? location_id { get; set; }
        public int? customer_id { get; set; }
        public int pay_type_id { get; set; }
        public int pay_account_id { get; set; }
        public int pay_item_id { get; set; }
        public object teacher_id { get; set; }
        public object commodity_id { get; set; }
        public int ctt_id { get; set; }
        public string document_date { get; set; }
        public object income { get; set; }
        public string payer_name { get; set; } 
        public string note { get; set; }
        public int is_confirmed { get; set; }


        public class Root
        {
            public int total { get; set; }
            public int count { get; set; }
            public int page { get; set; }
            public List<Itemii> items { get; set; }
        }
    }
}