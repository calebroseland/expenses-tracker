using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Domain.Entities
{
    public class AccountEntry : BaseEntity
    {
        public virtual Account Account { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Value { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Category Category { get; set; }
    }
}
