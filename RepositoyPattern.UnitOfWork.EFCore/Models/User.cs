using System;

namespace RepositoyPattern.UnitOfWork.EFCore.Models
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
