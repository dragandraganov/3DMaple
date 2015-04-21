using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DMapleSystem.Data.Models
{
    public class PayPalTransaction
    {
        [Key]
        public int Id { get; set; }

        public string Cmd { get; set; }
        public string Business { get; set; }
        public string NoShipping { get; set; }
        public string Redirect { get; set; }
        public string CancelReturn { get; set; }
        public string NotifyUrl { get; set; }
        public string CurrencyCode { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
        public string ActionUrl { get; set; }

        public PayPalTransaction(bool useSandbox)
        {
            this.Cmd = "_xclick";
            this.Business = ConfigurationManager.AppSettings["business"];
            this.CancelReturn = ConfigurationManager.AppSettings["cancel_return"];
            this.Redirect = ConfigurationManager.AppSettings["return"];
            if (useSandbox)
            {
                this.ActionUrl = ConfigurationManager.AppSettings["test_url"];
            }
            else
            {
                this.ActionUrl = ConfigurationManager.AppSettings["Prod_url"];
            }
            // We can add parameters here, for example OrderId, CustomerId, etc....
            this.NotifyUrl = ConfigurationManager.AppSettings["notify_url"];
            // We can add parameters here, for example OrderId, CustomerId, etc....
            this.CurrencyCode = ConfigurationManager.AppSettings["currency_code"];
        }
    }
}
