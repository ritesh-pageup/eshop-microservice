using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;    
        public static CustomerId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);
            if(guid == Guid.Empty)
            {
                throw new DomainException("Customer Id can not be empty.");
            }
            return new CustomerId(guid);
        }
    }
}
