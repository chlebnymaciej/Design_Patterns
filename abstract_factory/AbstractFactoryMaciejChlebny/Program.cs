using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiPlatform.Interfaces;
namespace MultiPlatform
{
	class Program
	{

        private static void BuildUI(UIFactory factory)
		{
            IGrid grid = factory.createGrid();
            // zrobilem konstruktor ktory przyjmuje content bo nie chce mi sie pisac
            /*
            IButton but = factory.createButton("");
            but.Content = "dupa";
            grid.AddButton(but);
            */

            grid.AddButton(factory.createButton("BigPurpleButton"));
            grid.AddButton(factory.createButton("SmallButton"));
            grid.AddButton(factory.createButton("Baton"));

            grid.AddTextBox(factory.createTextBox(""));
            grid.AddTextBox(factory.createTextBox("EmptyTextBox"));
            grid.AddTextBox(factory.createTextBox("xoBtxeT"));

            foreach(var i in grid.GetButtons())
            {
                i.ButtonPressed();
                i.DrawContent();
            }

            foreach(var i in grid.GetTextBoxes())
            {
                i.DrawContent();
            }

        }
        
		static void Main(string[] args)
		{

			Console.WriteLine("<---------------------iOS--------------------->");
            UIFactory ios = new iosFactory();
            BuildUI(ios);


			Console.WriteLine("<---------------------Windows--------------------->");
            UIFactory win = new winFactory();
            BuildUI(win);
			

			Console.WriteLine("<---------------------Android--------------------->");
            UIFactory and = new aidFactory();
            BuildUI(and);
			
		}
	}
}
