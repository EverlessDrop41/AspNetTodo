using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.EF.Model
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool? Completed { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public Todo() { }

        public Todo(string name, bool completed)
        {
            Name = name;
            Completed = completed;
        }
    }
}
