using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFT.Core.Domains
{
    public abstract class EntityBase
    {
    }

    public abstract class Entity<T> : EntityBase, IEntity<T>
    {
        [Key]
        public T ID { get; set; }
    }
}
