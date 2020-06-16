using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Problems;
using BigTask2.Ui;
using projekt10_2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

namespace BigTask2
{
	class Program
	{
        static IEnumerable<Route> ServeRequest(Request request)
        {
            // Chain of responsibility walicdacji requestu
            IChain validatingChain = new FromValidate();
            validatingChain.AddNext(new ToValidate())
                           .AddNext(new FilterPopulationvalidate())
                           .AddNext(new Vehiclesvalidate());
            
            if (!validatingChain.Handle(request))
                return null;

            (IGraphDatabase cars, IGraphDatabase trains) = MockData.InitDatabases();
            // unia bazy danych taki mini dekorator
            IGraphDatabase database = new GraphDatabaseUnion(cars, trains);
            // Chain of responsibility tworzenia dekoratora bazy danych
            IFilterDBChain filterDBChain = new RestaurantDBChainDecorator();
            filterDBChain.AddNext(new AllowedVehiclesDBChainDecorator())
                         .AddNext(new MinPopulationDBChainDecorator());

            IGraphDatabase filteredDatabase = filterDBChain.Handle(request.Filter, database);

            // lista solverów tutaj dodajemy nowy solver -- jedyna zmiana!!
            List<IExtendedSolver> listOfSolvers = new List<IExtendedSolver>();
            listOfSolvers.Add(new DijkstraCostSolverExtended());
            listOfSolvers.Add(new DijkstraTimeSolverExtended());
            listOfSolvers.Add(new DFSSolverExtended());
            listOfSolvers.Add(new BFSSolverExtended());

            // tworzenie chain do tworznie problemów
            IChainCreatingProblem creatingProblem = new CostCreateProblem();
            creatingProblem.AddNext(new TimeCreateProblem());

            // uzupełnianie i tworznie problemu 
            IRouteProblem prob = creatingProblem.Handle(request);
            if (prob == null) return null;
            prob.Graph = filteredDatabase;

            // odwiedzanie solverów
            foreach (var i in listOfSolvers)
            {
                if (i.IsNameEquals(request.Solver))
                {
                    var res = prob.AcceptVisitor(i);
                    if (res != null)
                        return res;
                }
            }
            return null;
		}
		static void Main(string[] args)
		{
            Console.WriteLine("---- Xml Interface ----");

            // tworznie systemów
            ISystemFactory xmlFactory = new XMLFactory();
            ISystem xmlSystem = CreateSystem(xmlFactory);

            Execute(xmlSystem, "xml_input.txt");
            Console.WriteLine();
            Console.WriteLine("---- KeyValue Interface ----");

            // tworznie systemów
            ISystemFactory keyvalueFactory = new KeyValueFactory();
            ISystem keyValueSystem = CreateSystem(keyvalueFactory);
            
            Execute(keyValueSystem, "key_value_input.txt");
            Console.WriteLine();
        }

        /* Prepare method Create System here (add return, arguments and body)*/
        static ISystem CreateSystem(ISystemFactory factory)
        {
            // tworze tylko jedną konkrecję system generaal
            //bo nie ma według mnie sensu rozkładać tego na dwie klasy różniące się tylko nazwą
            // ale wrazie co konkrecje są stworzone w Interfaces.cs wystarczy odkomentować
            return new SystemGeneral(factory.CreateForm(), factory.CreateDisplay());
        }

    static void Execute(ISystem system, string path)
        {
            IEnumerable<IEnumerable<string>> allInputs = ReadInputs(path);
            foreach (var inputs in allInputs)
            {
                foreach (string input in inputs)
                {
                    system.Form.Insert(input);
                }
                var request = RequestMapper.Map(system.Form);
                var result = ServeRequest(request);
                system.Display.Print(result);
                Console.WriteLine("==============================================================");
            }
        }

        private static IEnumerable<IEnumerable<string>> ReadInputs(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                List<List<string>> allInputs = new List<List<string>>();
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    List<string> inputs = new List<string>();
                    while (!string.IsNullOrEmpty(line))
                    {
                        inputs.Add(line);
                        line = file.ReadLine();
                    }
                    if (inputs.Count > 0)
                    {
                        allInputs.Add(inputs);
                    }
                }
                return allInputs;
            }
        }
    }
}
