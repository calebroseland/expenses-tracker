﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Domain.Entities
{
    public class Category : BaseEntity
    {
        public virtual string Name { get; set; }
    }
}
