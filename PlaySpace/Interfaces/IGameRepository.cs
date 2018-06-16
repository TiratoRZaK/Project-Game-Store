using PlaySpace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaySpace.Interfaces
{
    public interface IGameRepository : IDisposable
    {
        IEnumerable<Game> GetGameList(); //получение всех игр
        Game GetGame(int gameId);        //получение игры по id
        void Create(Game item);          //создание игры 
        void Update(Game item);          //обновление данных игры
        Game Delete(int gameId);         //удаление игры по id
        void Save();                     //сохранение игры
    }
}
