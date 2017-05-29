using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Domains
{
    public interface IEntity
    {
    }
    public interface IEntity<T> : IEntity
    {
        T ID { get; set; }
    }
}
