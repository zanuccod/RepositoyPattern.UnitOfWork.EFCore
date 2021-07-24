using System;
using System.Text.Json;

namespace RepositoyPattern.UnitOfWork.EFCore.Models
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
