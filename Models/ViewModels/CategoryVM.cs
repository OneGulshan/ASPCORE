using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CategoryVM //Basically jo-2 properties hamein use me leni hoti hai Model ki uske liye ham VM bana lete hain, VM can be diff types of depending on same Model
    {
        public Category Category { get; set; } = new Category();//In VM Model(Category) initialization is required for providing some data like 0/1 type in array
        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }
}
