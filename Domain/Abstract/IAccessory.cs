using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IAccessory
    {
        IEnumerable<Accessory> Accessories { get; }
    }
}
