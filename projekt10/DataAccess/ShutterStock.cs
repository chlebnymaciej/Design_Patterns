using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
	public class PhotMetadata
	{
		public string Name { get; set; }
		public string Camera { get; set; }
		public double[] CameraSettings { get; set; }
		public DateTime Date { get; set; }
		public string WidthPx { get; set; }//Encrypted
		public string HeightPx { get; set; }//Encrypted
		public double Longitude { get; set; }
		public double Latitude { get; set; }

		public PhotMetadata() { }
		public PhotMetadata(PhotMetadata p)
		{
			this.Name = p.Name;
			this.Camera = p.Camera;
			this.CameraSettings = p.CameraSettings;
			this.Date = p.Date;
			this.WidthPx = p.WidthPx;
			this.HeightPx = p.HeightPx;
			this.Longitude = p.Longitude;
			this.Latitude = p.Latitude;
		}
	}
	class ShutterStockDatabase 
	{
		public PhotMetadata[][][] Photos;
		public IIterator<PhotMetadata> GetIterator()
		{
			return new ShutterStockIterator(Photos);
		}
	}
}
