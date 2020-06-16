//This file contains fragments that You have to fulfill

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Dodane przejścia iteratorów i dziedziczenie po ISolver jeśli zabronione to proszę o wybaczenie,
// jednak jego brak delikatnie zmienia budowe kodu, ale też nie bardzo. Aby zrozumieć o co mi chodzi proszę zajrzeć do Solver.cs
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Problems;
using System.Collections.Generic;

namespace BigTask2.Algorithms
{
	class DFS : ISolver
	{ 
		public IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to)
		{
			Dictionary<City, Route> routes = new Dictionary<City, Route>();
			routes[from] = null;
			Stack<City> stack = new Stack<City>();
			stack.Push(from);
			do
			{
				City city = stack.Pop();
				var it = graph.GetRoutesFrom(city);
				while (it.Get() != null)
				{
					Route route = it.Get(); /* Change to current Route*/
					it.Next(); // moja linijka

					if (routes.ContainsKey(route.To))
					{
						continue;
					}
					routes[route.To] = route;
					if (route.To == to)
					{
						break;
					}
					stack.Push(route.To);
				}
			} while (stack.Count > 0);
			if (!routes.ContainsKey(to))
			{
				return null;
			}
			List<Route> result = new List<Route>();
			for (Route route = routes[to]; route != null; route = routes[route.From])
			{
				result.Add(route);
			}
			result.Reverse();
			return result;
		}
	}
}
