using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
    //  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
    //  Maciej Chlebny

    /* Decorator Patern
     * 
     * ICodec :
     * Code - do some special operation
     * Handle - Coding or Decoding given key
     * AddNextInChain it is used to chaining Decorator - potentially dangerous!! 
     */
    interface ICodec
    {
        string Code(string key);
        string Handle(string key);
        ICodec AddNextInChain(ICodec codec);
    }

    abstract class DecCodec : ICodec
    {
        protected ICodec NextInChain = null;

        public ICodec AddNextInChain(ICodec codec)
        {
            this.NextInChain = codec;
            return NextInChain;
        }

        public abstract string Code(string key);

        public string Handle(string key)
        {

            string CodedString = Code(key);
            if (NextInChain != null)
                return this.NextInChain.Handle(CodedString);

            return CodedString;
        }
    }

    class FrameCodec : DecCodec
    {
        private int frameSize { get; set; }
        private String left, right;

        public FrameCodec(int n)
        {
            this.frameSize = n;
            if (this.frameSize > 0 && this.frameSize < 10)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= this.frameSize; i++)
                    sb.Append(i.ToString());

                left = sb.ToString();
                char[] arr = left.ToCharArray();
                Array.Reverse(arr);
                right = new string(arr);
            }

        }
        public override string Code(string key)
        {
            if (this.frameSize < 1)
                return key;

            StringBuilder sb = new StringBuilder(left);
            sb.Append(key);
            sb.Append(right);

            return sb.ToString();
        }

    }
    class FrameDeCodec : DecCodec
    {
        private int frameSize { get; set; }

        public FrameDeCodec(int n)
        {
            if (n < 0 || n > 9) throw new Exception("Invalid Argument");
            this.frameSize = n;
        }
        public override string Code(string key)
        {
            if (this.frameSize < 1)
                return key;

            string tmp = key.Substring(frameSize, key.Length - 2 * frameSize);
            return tmp;
        }
    }
    class ReverseCodec : DecCodec
    {
        public override string Code(string key)
        {
            char[] arr = key.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }

    class PushCodec : DecCodec
    {
        public int shift { get; private set; }
        public PushCodec(int n)
        {
            this.shift = n;
        }
        
        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
        public override string Code(string key)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < key.Length; i++)
            {
                sb.Append(key[mod((i - this.shift),key.Length)]);
            }
            
            return sb.ToString();

        }
    }

    class CezarCodec : DecCodec
    {
        public int shift { get; private set; }
        public CezarCodec(int n)
        {
            this.shift = n%10;
        }

        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
        public override string Code(string key)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in key)
            {
                sb.Append(
                    (mod((int.Parse(c.ToString()) + shift), 10))
                    .ToString());
            }

            return sb.ToString();
        }
    }

    class SwapCodec : DecCodec
    {
        public override string Code(string key)
        {
            StringBuilder sb = new StringBuilder();
            int len = key.Length%2==0 ? key.Length: (key.Length-1);

            for (int i = 0; i < len; i += 2)
            {
                sb.Append(key[i + 1]);
                sb.Append(key[i]);
            }
            if (key.Length % 2 == 1)
            {
                sb.Append(key.Last());
            }

            return sb.ToString();
        }
    }
}
