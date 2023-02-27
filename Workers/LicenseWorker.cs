using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using MachManager.Context;
using MachManager.Models.Parameters;

namespace MachManager.Workers{
    public class LicenseWorker : BackgroundService, IDisposable{
        private const int generalDelay = 10 * 1000;
        // private const string licenseServer = "https://otomat.metaganos.com:5059/";
        private const string licenseServer = "http://localhost:5203/";
        protected override async Task ExecuteAsync(CancellationToken stoppingToken){
            while (!stoppingToken.IsCancellationRequested)
            {
                await InvokeLicense();
                await Task.Delay(generalDelay, stoppingToken);
            }
        }
        public virtual async Task StartAsync(CancellationToken cancellationToken){
           await base.StartAsync(cancellationToken);
        }
        public virtual async Task StopAsync(CancellationToken cancellationToken){
            await base.StopAsync(cancellationToken);
        }

        public async Task InvokeLicense(){
            try
            {
                using (MetaGanosSchema _context = SchemaFactory.CreateContext()){
                    var dbDealer = _context.Dealer.FirstOrDefault();
                    if (dbDealer != null){
                        var licPrm = new ValidateLicenseModel{
                            DealerCode = dbDealer.DealerCode,
                            LicenseCode = dbDealer.LicenseKey,
                        };

                        var client = new HttpClient();
                        await client.PostAsync(licenseServer + "LicenseKey/ValidateRemoteLicense", new StringContent(JsonConvert.SerializeObject(licPrm), 
                            System.Text.Encoding.UTF8, "application/json"));

                        dbDealer.LastSelfValidation = DateTime.Now;
                        _context.SaveChanges();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose(){

        }
    }
}