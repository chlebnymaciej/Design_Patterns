using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
    public abstract class UIFactory
    {
        public abstract IButton createButton(string c);

        public abstract IGrid createGrid();
        public abstract ITextBox createTextBox(string c);
    }

    public class iosFactory : UIFactory
    {
        public override ITextBox createTextBox(string c = "TextBox")
        {
            return new iosTextBox(c);
        }

        public override IButton createButton(string c = "Button")
        {
            return new iosButton(c);
        }

        public override IGrid createGrid()
        {
            return new iosGrid();
        }
    }

    public  class winFactory : UIFactory
    {
        public override ITextBox createTextBox(string c = "TextBox")
        {
            return new winTextBox(c);
        }

        public override IButton createButton(string c = "Button")
        {
            return new winButton(c);
        }

        public override IGrid createGrid()
        {
            return new winGrid();
        }
    }

    public class aidFactory : UIFactory
    {
        public override ITextBox createTextBox(string c= "TextBox")
        {
            return new aidTextBox(c);
        }

        public override IButton createButton(string c = "Button")
        {
            return new andButton(c);
        }

        public override IGrid createGrid()
        {
            return new aidGrid();
        }
    }
}
