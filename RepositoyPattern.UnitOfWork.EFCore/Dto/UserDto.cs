using System;
using System.Text.Json;

namespace RepositoyPattern.UnitOfWork.EFCore.Dto
{
    public class UserDto : IEquatable<UserDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public bool Equals(UserDto other)
        {
            var result = true;

            result &= FirstName.Equals(other.FirstName);
            result &= LastName.Equals(other.LastName);
            result &= Email.Equals(other.Email);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !ReferenceEquals(obj, this))
            {
                return false;
            }

            return Equals(obj as UserDto);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email);
        }
    }
}
