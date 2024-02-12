using OtpProject.Models;

namespace OtpProject
{
    public interface IDbEditor
    {
        public Task CreateAsync(string number, string code);
        public bool Verify(string number, string code);

    }
}
