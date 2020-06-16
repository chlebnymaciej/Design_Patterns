//This file contains fragments that You have to fulfill

using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Problems;
using System;
using System.Collections.Generic;


//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Dodane przejścia iteratorów i dziedziczenie po ISolver jeśli zabronione to proszę o wybaczenie,
// jednak jego brak delikatnie zmienia budowe kodu, ale też nie bardzo. Aby zrozumieć o co mi chodzi proszę zajrzeć do Solver.cs
namespace BigTask2.Algorithms
{
	abstract class Dijkstra : ISolver
	{

		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, (double dist, Route last)> distances = new Dictionary<City, (double dist, Route last)>();
			HashSet<City> visitedCitites = new HashSet<City>();
			distances[from] = (0, null);
			City minCity = from;
			while (minCity != to)
			{
				var it = graph.GetRoutesFrom(minCity);
				while (it.Get() != null)
				{
					Route route = it.Get(); /* Change to current Route*/
					it.Next();			   // my line

					if (visitedCitites.Contains(route.To))
					{
						continue;
					}
					double dist = distances[minCity].dist + OptimizingValueFunc(route);
					if (!distances.ContainsKey(route.To))
					{
						distances[route.To] = (dist, route);
					}
					else
					{
						if (dist < distances[route.To].dist)
						{
							distances[route.To] = (dist, route);
						}
					}
				}
				visitedCitites.Add(minCity);
				minCity = null;
				foreach (var (city, (dist, route)) in distances)
				{
					if (visitedCitites.Contains(city))
					{
						continue;
					}
					if (minCity == null || dist < distances[city].dist)
					{
						minCity = city;
					}
				}
				if (minCity == null)
				{
					return null;
				}
			}
			List<Route> result = new List<Route>();
			for (Route route = distances[to].last; route != null; route = distances[route.From].last)
			{
				result.Add(route);
			}
			result.Reverse();
			return result;
		}

        protected abstract double OptimizingValueFunc(Route route);
    }

    class DijkstraCost : Dijkstra
    {

		protected override double OptimizingValueFunc(Route route)
        {
            return route.Cost;
        }
    }

    class DijkstraTime : Dijkstra
    {
		protected override double OptimizingValueFunc(Route route)
        {
            return route.TravelTime;
        }
    }
}
