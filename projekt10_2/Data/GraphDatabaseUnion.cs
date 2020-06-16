
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Klasa łącząca bazy danych w jedną
using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigTask2.Data
{
    class GraphDatabaseUnion : IGraphDatabase
    {
        public List<IGraphDatabase> graphDatabases;

        public GraphDatabaseUnion(params IGraphDatabase[] list)
        {
            graphDatabases = new List<IGraphDatabase>(list);
        }

        public GraphDatabaseUnion(List<IGraphDatabase> list)
        {
            graphDatabases = list;
        }
        public City GetByName(string cityName)
        {
            foreach (IGraphDatabase gb in graphDatabases)
            {
                if (gb.GetByName(cityName) != null)
                    return gb.GetByName(cityName);
            }
            return null;
        }

        public INumerator GetRoutesFrom(City from)
        {
            if (graphDatabases == null || graphDatabases.Count == 0)
                return null;

            FacadeIterator it = new FacadeIterator();
            foreach(IGraphDatabase i in graphDatabases)
            {
                it.AddInumerator(i.GetRoutesFrom(from));
            }
            return it;
        }
    }
}
