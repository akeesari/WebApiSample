using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AK.Net.Todo.Data{

    [Table("Todo")]
    public partial class Todo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool IsClosed { get; set; }
    }
}
