using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message) { }

        public NotFoundException(string name, string key):base($"Entity \"{name}\" ({key}) was not found!")
        {

        }
    }
}
