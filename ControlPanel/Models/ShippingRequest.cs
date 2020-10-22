﻿using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class ShippingRequest : BasicModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        public Product prodcut { get; set; }

        public int productId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Scheduled Date")]
        public DateTime scheduledDate { get; set; }


        public Payment Payment { get; set; }

        public int PaymentId { get; set; }

        public string ReasonOfCancellation { get; set; }

    }
}