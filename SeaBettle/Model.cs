using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBettle
{
    public enum ShotStatus  // Статус выстрела
    { 
        Miss, // Промах
        Wounded, // Ранил
        Kill, // Убил
        EndBattle // Конец игры
    }
    public enum CoordStatus // Статус координат
    {
        None, // Пусто
        Ship, // Корабль
        Shot, // Выстрел
        Got // Попал
    }
    public enum TypeShips // Типы кораблей
    {
        x1, x2, x3, x4
    }
    public enum Direction // Направление размещения корабля
    {
        Horizontal, Vertical
    }
    public class Model
    {
        public CoordStatus[,] PlayerShips = new CoordStatus[10, 10]; // Поле координат своих кораблей (игрока)
        public CoordStatus[,] EnemyShips = new CoordStatus[10, 10]; // Поле координат кораблей противника
        public int UndiscoverCells = 20; // Необнаруженные клетки кораблей противника
        public ShotStatus LastShot; // Поле статуса последнего выстрела
        public bool WoundedStatus; // Поле статуса ранения
        public string? LastShotCoord; // Поле координат последнего выстрела (? позволяет переменной быть null)
        public Model() // Констуктор. Инициализация полей модели
        {
            LastShot = ShotStatus.Miss;
            WoundedStatus = false; // Вообще не попал в клетку корабля
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    PlayerShips[i, j] = CoordStatus.None;
                    EnemyShips[i, j] = CoordStatus.None;
                }
        }
        public ShotStatus Shot(string ShotCoord) // Метод выстрела игрока. Входящий параметр - строка из двух цифр
        {
            ShotStatus result = ShotStatus.Miss;
            int x, y; // Координаты строки и столбца выстрела соответственно
            x = int.Parse(ShotCoord.Substring(0,1));
            y = int.Parse(ShotCoord.Substring(1,1));
            if (PlayerShips[x, y] == CoordStatus.None)
                result = ShotStatus.Miss;
            else
            {
                result = ShotStatus.Kill;
                if ((x != 9 && PlayerShips[x+1,y] == CoordStatus.Ship) || (x != 0 && PlayerShips[x-1,y] == CoordStatus.Ship) || (y != 9 && PlayerShips[x,y+1] == CoordStatus.Ship) || (y != 0 && PlayerShips[x,y-1] == CoordStatus.Ship))
                    result = ShotStatus.Wounded;
                PlayerShips[x, y] = CoordStatus.Got; // Координате клетки прописываем статус "Попал"
                UndiscoverCells--; // Вычитаем клетку
                if (UndiscoverCells == 0)
                    result = ShotStatus.EndBattle;
            }
            return result;
        }
        public string ShotGen() // Метод выстрела ПК (генерация случайных координат)
        {
            string result = "00";
            int x, y; // Координаты строки и столбца выстрела соответственно
            Random rand = new Random();
            if (LastShot == ShotStatus.Kill)
                WoundedStatus = false;
            if ((LastShot == ShotStatus.Kill || LastShot == ShotStatus.Miss) && !WoundedStatus)
            {
                x = rand.Next(0, 9);
                y = rand.Next(0, 9);
            }
            else
            {
                x = int.Parse(LastShotCoord.Substring(0, 1));
                y = int.Parse(LastShotCoord.Substring(1, 1));
                if (LastShot == ShotStatus.Wounded)
                {                    
                    if (x != 9 && EnemyShips[x + 1, y] == CoordStatus.Got) x = x - 1;
                    if (y != 9 && EnemyShips[x, y + 1] == CoordStatus.Got) y = y - 1;
                    if (x != 0 && EnemyShips[x - 1, y] == CoordStatus.Got) x = x + 1;
                    if (y != 0 && EnemyShips[x, y - 1] == CoordStatus.Got) y = y + 1;
                    {

                    }
                }
            }
            result = x.ToString() + y.ToString();
            return result;
        }
    }
}
