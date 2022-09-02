using System.Collections;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}