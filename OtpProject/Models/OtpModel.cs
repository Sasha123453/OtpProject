namespace OtpProject.Models
{
    public class OtpModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Attempts { get; set; }
        public int Status { get; set; }
        public OtpModel(string number, string code)
        {
            Attempts = 0;
            Status = 0;
            RegistrationDate = DateTime.Now;
            Number = number;
            Code = code;
        }
    }
}
