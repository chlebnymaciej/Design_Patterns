using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;

namespace projekt10.TravelAgencies
{
    //  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
    //  Maciej Chlebny

    /* 
     *  Website implementations
     */
    interface IWebsite
    {
        void Present();
        void UpdateOfferList();
    }

    class Website : IWebsite
    {
        private int Temporary;
        private int Pernament;
        public List<IOffer> offers;
        public List<ITravelAgency> travelAgencies;
        public List<IAdAgency> adAgencies;
        public Website(int Temp,
                       int Perm,
                       List<ITravelAgency> travelAgencies,
                       List<IAdAgency> adAgencies,
                       List<IOffer> off = null,
                       int strating_number_of_offers = 10)
        {
            this.Temporary = Temp;
            this.Pernament = Perm;
            this.travelAgencies = travelAgencies;
            this.adAgencies = adAgencies;
            this.offers = off;
            if (off == null)
                this.offers = new List<IOffer>();
            for(int i = 0;i< strating_number_of_offers;i++)
            {
                UpdateOfferList();
            }

        }

        public void UpdateOfferList()
        {
            System.Random random = new Random();
            int ta_len = travelAgencies.Count;
            int aa_len = adAgencies.Count;
            offers.Add(adAgencies[random.Next(0, aa_len)].CreateOffer(travelAgencies[random.Next(0, ta_len)]));

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
