using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;
namespace TravelAgencies.DataAccess
{
    //  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
    //  Maciej Chlebny


    /*
     * Iterator Pattern
     * 
     * Interface IIterator<T>
     * Contains methods:
     * - bool HasNext() - tells if collection have next element
     * - bool Next() - goes to next element return value does not
     * tells user anything according to my standard
     * - T Get() - returns current element, if element does not exist
     * behavoiur is undiefined
     * 
     */
    interface IIterator<T>
    {
        bool HasNext();
        bool Next();
        T Get();
    }

    class BookingIterator : IIterator<ListNode>
    {
        private int Index = 0;
        private ListNode[] listNodes;

        public BookingIterator(ListNode[] list)
        {
            this.listNodes = (ListNode[])list.Clone();
        }

        public ListNode Get()
        {
            return listNodes[Index];
        }

        public bool HasNext()
        {
            for (int i = 1; i <= listNodes.Length; i++)
            {
                if (listNodes[(Index + i) % listNodes.Length] != null)
                    return true;
            }
            return false;
        }

        public bool Next()
        {
            listNodes[Index] = listNodes[Index].Next;
            for (int i = 1; i <= listNodes.Length; i++)
            {
                if (listNodes[(Index + i) % listNodes.Length] != null)
                {
                    Index = (Index + i) % listNodes.Length;
                    return true;
                }
            }
            return false;
        }
    }

    class OysterIterator : IIterator<PlainReview>
    {
        private int state;
        private OysterIterator iterator;
        private BSTNode node;

        public OysterIterator(BSTNode reviews, OysterIterator parent = null)
        {
            if (reviews == null)
            {
                state = 0;
                if (parent != null)
                    parent.state = (parent.state + 1) % 4;
                return;
            }
            node = reviews;
            state = 1;
            iterator = new OysterIterator(reviews.Left, this);
        }

        public PlainReview Get()
        {
            if (state == 0)
                return null;
            if (state == 2)
                return new PlainReview(node.UserName, node.Review);

            return this.iterator.Get();
        }

        public bool HasNext()
        {
            if (state == 0) return false;
            if (state == 1) return true;
            if (state == 2 && this.node.Right != null) return true;
            return this.iterator.HasNext();
        }

        public bool Next()
        {
            switch (state)
            {
                case 0:
                    return false;
                case 1:
                    if (!this.iterator.Next())
                        this.state++;
                    return true;
                case 2:
                    {
                        state = 3;
                        this.iterator = new OysterIterator(node.Right, this);
                        if (state == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                case 3:
                    if (!this.iterator.Next())
                    {
                        this.state = 0;
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }

    class ShutterStockIterator : IIterator<PhotMetadata>
    {
        private bool next;
        private int i, j, k;
        private int n1, n2, n3;
        private PhotMetadata[][][] db;
        public ShutterStockIterator(PhotMetadata[][][] photMetadatas)
        {
            this.db = photMetadatas;
            i = j = k = 0;
            next = NextI();
        }
        public PhotMetadata Get()
        {
            return db[i][j][k];
        }

        public bool HasNext()
        {
            return next;
        }

        public bool Next()
        {
            bool current = HasNext();
            this.i = n1;
            this.j = n2;
            this.k = n3;
            this.next = NextI();
            return current;
        }

        private bool NextI()
        {
            for (int i1 = i; i1 < db.Length; i1++)
            {
                if (db[i1] == null) continue;
                
                int i2 = (i1 == i) ? j : 0;
                for (; i2 < db[i1].Length; i2++)
                {
                    if (db[i1][i2] == null) continue;
                    
                    int i3 = (i2 == j && i1 == i) ? k+1 : 0;
                    for (; i3 < db[i1][i2].Length; i3++)
                    {
                        if (db[i1][i2][i3] != null) // poprawić
                        {
                            n1 = i1;
                            n2 = i2;
                            n3 = i3;
                            return true;
                        }
                    }

                }

            }
            n1 = -1;
            n2 = -1;
            n3 = -1;
            return false;
        }
    }

    class TripAdvisorIterator : IIterator<TripAdvisorRecord>
    {
        private int current = -1;
        private int cur_dic_no = -1;
        private int next = -1;
        private int dic_no = -1;
        private TripAdvisorDatabase db;

        public TripAdvisorIterator(TripAdvisorDatabase database)
        {
            this.db = database;
            this.HasNext();
            this.Next();
        }
        public TripAdvisorRecord Get()
        {
            return new TripAdvisorRecord(db.Names[cur_dic_no][db.Ids[current]],
                                        db.Prices[db.Ids[current]],
                                        db.Ratings[db.Ids[current]],
                                        db.Countries[db.Ids[current]]);
        }

        public bool HasNext()
        {
            for (int i = current + 1; i < db.Ids.Length; i++)
            {
                Guid g = db.Ids[i];
                if (!db.Prices.ContainsKey(g))
                    continue;
                if (!db.Ratings.ContainsKey(g))
                    continue;
                if (!db.Countries.ContainsKey(g))
                    continue;
                for (int j = 0; j < db.Names.Length; j++)
                {
                    if (db.Names[j].ContainsKey(g))
                    {
                        next = i;
                        dic_no = j;
                        return true;
                    }
                }
            }
            next = -1;
            return false;
        }

        public bool Next()
        {
            if (next == -1)
                return false;

            current = next;
            cur_dic_no = dic_no;
            HasNext();
            return true;

        }
    }
}
