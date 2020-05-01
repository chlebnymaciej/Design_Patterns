using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;

namespace TravelAgencies.Agencies
{
    //  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
    //  Maciej Chlebny

    /* 
     *  Decorator Pattern
     *  
     */
    public interface IPhoto
    {
        PhotMetadata plain { get; }
        string Decorated();
    }
    public class PlainPhoto : IPhoto
    {
    public PhotMetadata plain { get; private set; }
    public string Decorated()
    {
        return null;
    }
    public PlainPhoto(PhotMetadata phot)
    {
        this.plain = phot;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(plain.Name);
        sb.Append(" (");
        sb.Append(plain.WidthPx);
        sb.Append(" x ");
        sb.Append(plain.HeightPx);
        sb.Append(" )");
        return sb.ToString();
    }
}
public abstract class DecPhoto : IPhoto
{
    public virtual PhotMetadata plain { get; }
    protected IPhoto photo;

    public DecPhoto(IPhoto photo)
    {
        this.photo = photo;
    }
    public abstract string Decorated();
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(plain.Name);
        sb.Append(" (");
        sb.Append(plain.WidthPx);
        sb.Append(" x ");
        sb.Append(plain.HeightPx);
        sb.Append(" )");
        return sb.ToString();
    }
}

public class PolandPhoto : DecPhoto
{
    public override PhotMetadata plain 
    { 
        get 
        {
            PhotMetadata tmp = new PhotMetadata(photo.plain);
            tmp.Name = Decorated();
            return tmp;
        }
    }

    public PolandPhoto(IPhoto photo) : base(photo) { }

    public override string Decorated()
    {
        string name = photo.plain.Name;
        StringBuilder sb = new StringBuilder();
        foreach(char c in name)
        {
            switch (c)
            {
                case 'c':
                    sb.Append('ć');
                    break;
                case 's':
                    sb.Append('ś');
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }
        return sb.ToString();
    }
}

public class FrancePhoto : DecPhoto
{
    public override PhotMetadata plain
    {
        get
        {
            PhotMetadata tmp = new PhotMetadata(photo.plain);
            return tmp;
        }
    }

    public FrancePhoto(IPhoto photo) : base(photo) { }

    public override string Decorated()
    {
        return null;
    }
}

public class ItalyPhoto : DecPhoto
{
    public override PhotMetadata plain
    {
        get
        {
            PhotMetadata tmp = new PhotMetadata(photo.plain);
            return tmp;
        }
    }

    public ItalyPhoto(IPhoto photo) : base(photo) { }

    public override string Decorated()
    {
        string name = photo.plain.Name;
        StringBuilder sb = new StringBuilder("Dello_");
        sb.Append(name);
        return sb.ToString();
    }
}
}
