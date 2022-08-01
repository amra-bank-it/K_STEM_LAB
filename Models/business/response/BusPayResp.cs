namespace K_STEM_LAB.Models.business.response
{
  public class BusPayResp
  {
    public bool Success { get; set; }

    public int idRecepient { get; set; }    

    public int income { get; set; }

    public string note { get; set; }

    public bool is_confirmed { get; set; }

  }
}
