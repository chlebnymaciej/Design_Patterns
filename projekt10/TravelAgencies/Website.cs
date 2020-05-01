using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;

namespace projekt10.TravelAgencies
{
    interface IWebsite
    {
        void Present();
    }

    class Website : IWebsite
    {
        private int Temporary;
        private int Pernament;
        private IEnumerable<IOffer> offers;
        public Website(int Temp, int Perm, IEnumerable<IOffer> offers)
        {
            this.Temporary = Temp;
            this.Pernament = Perm;
            this.offers = offers;
        }
        public void Present()
        {
            int perm = 0;
            int temp = 0;
            foreach(IOffer o in offers)
            {
                if (o.IsTemporary() && temp!=Temporary)
                {
                    o.Post();
                    temp++;
                }

                if (!o.IsTemporary() && perm != Pernament)
                {
                    o.Post();
                    perm++;
                }
                if (temp == Temporary && perm == Pernament)
                    return;
            }
            return;                
        }
    }
}
