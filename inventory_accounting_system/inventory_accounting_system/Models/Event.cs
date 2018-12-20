﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Event : Entity
    {
        public string Title { get; set; }
        public EventType EventType { get; set; }
        [Display(Name = "Тип события")]
        public string EventTypeId { get; set; }
    }
}
