using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class EventsApi : ApiBase {
		private const string URI = "/api/students/events/";

		public EventsApi(string baseUrl, string accessToken) 
			: base(baseUrl, accessToken) {
		}
		
		public IEnumerable<EventModel> GetEvents() {
			var result = this.GetResource<IEnumerable<EventModel>>(URI);
			return result;
		}
	}
	
	public class EventModel {
		public int ID { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Details { get; set; }
		public string Venue { get; set; }
		public DateTime StartUtc { get; set; }
		public DateTime EndUtc { get; set; }
		public bool BookingsEnabled { get; set; }
		public EventBookingSettingsModel BookingSettings { get; set; }
	}

	public class EventBookingSettingsModel {
		public int? BookingLimit { get; set; }
		public int? PlacesRemaining { get; set; }
		public DateTime? BookingsOpenUtc { get; set; }
		public DateTime BookingsCloseUtc { get; set; }
	}
}