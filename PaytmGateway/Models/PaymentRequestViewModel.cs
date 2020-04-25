using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaytmGateway.Models
{
    public class PaymentRequestViewModel
    {
        public PaymentRequestViewModel()
        {
            PhoneNumber = "9892044921";
            Email = "test@mailinator.com";
            Amount = 2.45M;
        }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal Amount { get; set; }

    }
}