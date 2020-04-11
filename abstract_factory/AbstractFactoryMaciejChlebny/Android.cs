using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
    public class andButton : IButton
    {
        private string _content;
        public andButton(string con = "Button")
        {
            this.Content=con;
            Console.WriteLine("Android Button created");
        }

        public string Content
        {
            set => _content = value.Length > 8 ? value.Substring(0, 8) : value;
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"Sweet {_content}!");
        }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }

    public class aidTextBox : ITextBox
    {
        private string _content;

        public aidTextBox(string con = "TextBox")
        {
            this.Content = con;
            Console.WriteLine("Android TextBox created");
        }

        public string Content
        {
            set
            {
                // https://www.dailyrazor.com/blog/csharp-reverse-string/
                char[] myArr = value.ToCharArray();
                Array.Reverse(myArr);
                _content = new string(myArr);
            }
        }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }
    public class aidGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();
        public aidGrid()
        {
            Console.WriteLine("Android Grid created");
        }
        public void AddButton(IButton button)
        {
            buttons.Add(button);
        }

        public void AddTextBox(ITextBox textBox)
        {
            textBoxes.Add(textBox);
        }

        public IEnumerable<IButton> GetButtons()
        {
            return buttons.AsEnumerable<IButton>();
        }

        public IEnumerable<ITextBox> GetTextBoxes()
        {
            List<ITextBox> tmp = new List<ITextBox>();
            return tmp.AsEnumerable();
        }
    }
}
