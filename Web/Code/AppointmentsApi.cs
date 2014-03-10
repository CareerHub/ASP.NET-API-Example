using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class AppointmentBookingsApi : ApiBase {
		private const string AppointmentsApiBase = "api/students/alpha/appointments/bookings";

		public AppointmentBookingsApi(string baseUrl, string accessToken)
            : base(baseUrl + AppointmentsApiBase, accessToken) {
		}

        public Task<GetResult<IEnumerable<BookingModel>>> GetUpcomingAppointments() {
			return this.GetResource<IEnumerable<BookingModel>>("upcoming");
		}
	}

	public enum AppointmentTypeFormat {
		FaceToFace = 1,
		Phone = 2,
		FaceToFaceOrPhone = 3,
		DropIn = 4
	}

    public class BookingModel {
        public int ID { get; set; }
        public string TypeName { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Consultant { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public bool? Attended { get; set; }
        public string Skype { get; set; }
    }
}