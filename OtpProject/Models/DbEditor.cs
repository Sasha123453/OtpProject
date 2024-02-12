using Microsoft.AspNetCore.Mvc;

namespace OtpProject.Models
{
    public class DbEditor : IDbEditor
    {
        private readonly ApplicationContext _context;
        public DbEditor(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(string number, string code)
        {
            OtpModel model = new OtpModel(number, code);
            _context.Add(model);
            await _context.SaveChangesAsync();
        }
        public bool Verify(string number, string code)
        {
            var model = _context.OtpModels.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Number == number);
            if (model == null || model.Status != 0 || CheckIfExpired(model))
            {
                throw new Exception("Something went wrong");
            }
            if (model.Code != code)
            {
                model.Attempts++;
                throw new Exception("Wrong code");
            }
            model.Status = 1;
            _context.SaveChanges();
            return true;
        }
        public bool CheckIfExpired(OtpModel model)
        {
            var time = (DateTime.Now - model.RegistrationDate).TotalMinutes;
            if (model.Attempts >= 3 || time > 3) return true;
            return false;
        }
    }
}
