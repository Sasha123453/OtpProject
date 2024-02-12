using Microsoft.AspNetCore.Mvc;

namespace OtpProject
{
    public interface IOtpSender
    {
        public Task<bool> Send(string number);
    }
}
