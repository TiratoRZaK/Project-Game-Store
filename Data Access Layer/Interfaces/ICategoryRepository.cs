using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> GetCategoryList(); //получение всех
        Category GetCategory(int id);        //получение  по id
        Category Create(Category item);          //создание  
        void Update(Category item);          //обновление данных 
        Category Delete(int categoryId, IGameRepository repository);   //удаление  по id
        void Save();                     //сохранение 
    }
}
