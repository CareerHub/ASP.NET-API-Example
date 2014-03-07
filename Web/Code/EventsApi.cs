using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class EventsApi : ApiBase {
        private const string EventsApiBase = "api/students/vAlpha/events";

		public EventsApi(string baseUrl, string accessToken)
            : base(baseUrl + EventsApiBase, accessToken) {
		}
		
		public Task<GetResult<IEnumerable<EventModel>>> GetEvents() {
			return this.GetResource<IEnumerable<EventModel>>("");
		}

        public Task<GetResult<IEnumerable<EventModel>>> SearchEvents(string text) {
            return this.GetResource<IEnumerable<EventModel>>("search?text=" + text);
        }

        public Task<GetResult<EventModel>> GetEvent(int id) {
            return this.GetResource<EventModel>(id.ToString());
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
        public bool IsBooked { get; set; }
		public bool BookingsEnabled { get; set; }
		public EventBookingSettingsModel BookingSettings { get; set; }
	}

	public class EventBookingSettingsModel {
		public int? BookingLimit { get; set; }
		public int? PlacesRemaining { get; set; }
		public DateTime? BookingsOpenUtc { get; set; }
		public DateTime BookingsCloseUtc { get; set; }

        public bool CanBook() {
            if (this.BookingsOpenUtc.HasValue && this.BookingsOpenUtc.Value > DateTime.UtcNow) {
                return false;
            }

            if (this.BookingsCloseUtc < DateTime.UtcNow) {
                return false;
            }

            if (this.PlacesRemaining.HasValue && this.PlacesRemaining.Value <= 0) {
                return false;
            }

            return true;
        }
	}
}