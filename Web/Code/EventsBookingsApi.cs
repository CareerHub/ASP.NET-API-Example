using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class EventBookingsApi : ApiBase {
        private const string ApiBase = "/api/students/vAlpha/events";

        public EventBookingsApi(string baseUrl, string accessToken)
            : base(baseUrl + ApiBase, accessToken) {
		}
		
		public Task<GetResult<IEnumerable<EventModel>>> GetUpcomingEvents() {
            return this.GetResource<IEnumerable<EventModel>>("bookings/upcoming");
		}

        public Task<PostResult<EventBookingModel>> BookEvent(int eventId) {
            return this.PostResource<EventBookingModel>(eventId + "/bookings");
        }

        public Task<DeleteResult> CancelBooking(int eventId) {
            return this.DeleteResource(eventId + "/bookings");
        }
	}

    public class EventBookingModel {
        public int ID { get; set; }
        public int EventID { get; set; }
    }
}