using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
    public class winButton : IButton
    {
        private string _content;
        public winButton(string con = "Button")
        {
            this.Content = con;
            Console.WriteLine("Windows Button created");
        }
        public string Content
        {
            set
            {
                string s = value;
                _content = s.ToUpper();
            }
        }

        public void ButtonPressed()
        {
            Console.WriteLine("Windows button pressed");
        }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }

    public class winTextBox : ITextBox
    {
        private string _content;
        public winTextBox()
        {
            Console.WriteLine("Windows TextBox created");
        }
        public winTextBox(string con = "TextBox")
        {
            this.Content = con;
            Console.WriteLine("Windows TextBox created");
        }

        public string Content
        {
            set
            {
                _content = value.Substring(value.Length / 2) + " by .Net Core";
            }
        }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }
    public class winGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();
        private ITextBox first = null;
        public winGrid()
        {
            Console.WriteLine("windows grid created");
        }
        public void AddButton(IButton button)
        {
            buttons.Add(button);
        }

        public void AddTextBox(ITextBox textBox)
        {
            if (first == null)
            {
                first = textBox;
                return;
            }
            textBoxes.Add(textBox);
        }

        public IEnumerable<IButton> GetButtons()
        {
            return buttons.AsEnumerable<IButton>().Reverse();
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            List<ITextBox> tmp = new List<ITextBox>(textBoxes);
            tmp.Reverse();
            tmp.Insert(0, first);
            return tmp.AsEnumerable();
        }
    }
}
