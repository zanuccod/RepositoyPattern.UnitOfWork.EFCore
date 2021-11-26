using System;
using System.Text.Json;

namespace RepositoyPattern.UnitOfWork.EFCore.Models
{
    public class User : BaseEntity, IEquatable<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public bool Equals(User other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id == other.Id
                && !string.IsNullOrEmpty(this.FirstName) && this.FirstName.Equals(other.FirstName)
                && !string.IsNullOrEmpty(this.LastName) && this.LastName.Equals(other.LastName)
                && !string.IsNullOrEmpty(this.Email) && this.Email.Equals(other.Email);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }
}
