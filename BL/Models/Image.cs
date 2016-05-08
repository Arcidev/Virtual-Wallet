
namespace BL.Models
{
    public class Image : IDto
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Image return false.
            Image i = obj as Image;
            if (i == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Id == i.Id) && (this.Path.Equals(i.Path));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Id.GetHashCode();
            hash = (hash * 7) + Path?.GetHashCode() ?? 0;
            return hash;
        }
    }
}
