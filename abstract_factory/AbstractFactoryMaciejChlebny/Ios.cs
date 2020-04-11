using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Interfaces
{
    public class iosButton : IButton
    {
        private string _content;
        public iosButton(string con = "Button")
        {
            this.Content = con;
            Console.WriteLine("IOS Button created");
        }
        public string Content
        {
            set => _content = value;
        }

        public void ButtonPressed()
        {
            Console.WriteLine($"IOS Button pressed, content - {_content}");
        }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }

    public class iosTextBox : ITextBox
    {
        private string _content;
        public iosTextBox(string con = "TextBox")
        {
            this.Content = con;
            Console.WriteLine("IOS TextBox created");
        }
        public string Content { set => _content = value; }

        public void DrawContent()
        {
            Console.WriteLine(_content);
        }
    }
    public class iosGrid : IGrid
    {
        private List<IButton> buttons = new List<IButton>();
        private List<ITextBox> textBoxes = new List<ITextBox>();
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
            return textBoxes.AsEnumerable<ITextBox>();
        }
    }
}
