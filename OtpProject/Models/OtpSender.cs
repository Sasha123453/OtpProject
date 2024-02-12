using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OtpProject.Models
{
    public class OtpSender : IOtpSender
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptionsMonitor<SmsAero> _keyMonitor;
        private readonly IDbEditor _dbEditor;
        public OtpSender(IHttpClientFactory clientFactory, IOptionsMonitor<SmsAero> keyMonitor, IDbEditor dbEditor)
        {
            _clientFactory = clientFactory;
            _keyMonitor = keyMonitor;
            _dbEditor = dbEditor;
        }
        public async Task<bool> Send(string number)
        {
            var code = GenerateCode();
            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_keyMonitor.CurrentValue.Email}:{_keyMonitor.CurrentValue.Key}")));

            var url = $"https://gate.smsaero.ru/v2/sms/send?number={number}&text=Ваш код: {code}&sign=SMS Aero";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("Unable to send code");
            await _dbEditor.CreateAsync(number, code);
            return true;
        }
        //Заглушка
        private string GenerateCode()
        {
            return Random.Shared.Next(100000, 999999).ToString();
        }
    }
}
