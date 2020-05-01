using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
    interface IDBCoders
    {
        ICodec Coder { get;}
        ICodec Decoder { get;}
    }
    class BookingCoder : IDBCoders
    {
        public ICodec Coder { get; private set; }
        public ICodec Decoder { get; private set; }

        private static BookingCoder instance=null;
        private BookingCoder()
        {

            DecCodec fra = new FrameCodec(2);
            DecCodec rev = new ReverseCodec();
            DecCodec cez = new CezarCodec(-1);
            DecCodec swa = new SwapCodec();


            cez.AddNextInChain(swa);
            rev.AddNextInChain(cez);
            fra.AddNextInChain(rev);

            Coder = new FrameCodec(2);
            Coder.AddNextInChain(new ReverseCodec())
                .AddNextInChain(new CezarCodec(-1))
                .AddNextInChain(new SwapCodec());

            Decoder = new SwapCodec();
            Decoder.AddNextInChain(new CezarCodec(1))
                .AddNextInChain(new ReverseCodec())
                .AddNextInChain(new FrameDeCodec(2));
        }

        public static BookingCoder GetCoder()
        {
            if (instance == null)
            {
                instance = new BookingCoder();
            }
            return instance;
        }
    }

    class ShutterStockCoder : IDBCoders
    {
        public ICodec Coder { get; private set; }
        public ICodec Decoder { get; private set; }


        private ShutterStockCoder()
        {
            Decoder = new ReverseCodec();
            Decoder.AddNextInChain(new PushCodec(3))
                   .AddNextInChain(new FrameDeCodec(1))
                   .AddNextInChain(new CezarCodec(-4));


            DecCodec fra = new FrameCodec(1);
            DecCodec rev = new ReverseCodec();
            DecCodec cez = new CezarCodec(4);
            DecCodec pus = new PushCodec(3);

            pus.AddNextInChain(rev);
            fra.AddNextInChain(pus);
            cez.AddNextInChain(fra);


            Coder = cez;
        }

        private static ShutterStockCoder instance = null;
        public static ShutterStockCoder GetCoder()
        {
            if (instance == null)
            {
                instance = new ShutterStockCoder();
            }
            return instance;
        }
    }

    class TripAdvisorCoder : IDBCoders
    {
        public ICodec Coder { get; private set; }
        public ICodec Decoder { get; private set; }


        private TripAdvisorCoder()
        {
            Coder = new PushCodec(3);
            Coder.AddNextInChain(new FrameCodec(2))
                 .AddNextInChain(new SwapCodec())
                 .AddNextInChain(new PushCodec(3));

            Decoder = new PushCodec(-3);
            Decoder.AddNextInChain(new SwapCodec())
                   .AddNextInChain(new FrameDeCodec(2))
                   .AddNextInChain(new PushCodec(-3));
        }

        private static TripAdvisorCoder instance = null;
        public static TripAdvisorCoder GetCoder()
        {
            if (instance == null)
            {
                instance = new TripAdvisorCoder();
            }
            return instance;
        }
    }
}
