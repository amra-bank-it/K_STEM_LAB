namespace K_STEM_LAB.Models.Response.respCheck

{
  public class Root
  {
    public int total { get; set; }
    public int count { get; set; }
    public int page { get; set; }
    public List<Item> items { get; set; }
  }

  public class Item
    {
        public int id { get; set; }
        public List<int> branch_ids { get; set; }
        public List<string> teacher_ids { get; set; }
        public string name { get; set; }
        public object color { get; set; }
        public int is_study { get; set; }
        public int study_status_id { get; set; }
        public object lead_status_id { get; set; }
        public object lead_reject_id { get; set; }
        public object lead_source_id { get; set; }
        public object assigned_id { get; set; }
        public int legal_type { get; set; }
        public string legal_name { get; set; }
        public object company_id { get; set; }
        public string dob { get; set; }
        public string balance { get; set; }
        public string balance_base { get; set; }
        public int balance_bonus { get; set; }
        public int paid_count { get; set; }
        public object next_lesson_date { get; set; }
        public object paid_till { get; set; }
        public object last_attend_date { get; set; }
        public string b_date { get; set; }
        public string e_date { get; set; }
        public string note { get; set; }
        public int paid_lesson_count { get; set; }
        public object paid_lesson_date { get; set; }
        public List<string> phone { get; set; }
        public List<object> email { get; set; }
        public List<object> web { get; set; }
        public List<object> addr { get; set; }
        public string custom_status_ { get; set; }
        public string custom_gdezhivut { get; set; }
        public string custom_shkola { get; set; }
        public string custom_klass { get; set; }
        public string custom_nomerdogovora { get; set; }
        public string custom_instaname { get; set; }
        public string custom_rejection_reason { get; set; }
        public string custom_prichinaotkazadop { get; set; }

    }
}

