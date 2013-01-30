using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class AppointmentsApi : ApiBase {
		private const string AppointmentsController = "/api/students/appointments/";

		public AppointmentsApi(string baseUrl, string accessToken) 
			: base(baseUrl, accessToken) {
		}
		
		public IEnumerable<AppointmentModel> GetAppointments() {
			var result = this.GetResource<IEnumerable<AppointmentModel>>(AppointmentsController);
			return result;
		}
	}

	public enum AppointmentTypeFormat {
		FaceToFace = 1,
		Phone = 2,
		FaceToFaceOrPhone = 3,
		DropIn = 4
	}

	public class AppointmentModel {
		
		public int ID { get; set; }

		public string Title { get; set; }
		public IEnumerable<string> Topics { get; set; }

		public AppointmentTypeFormat Format { get; set; }

		public string Campus { get; set; }
		public string Location { get; set; }
		public string Counsellor { get; set; }

		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		public bool CanBook { get; set; }

		public DateTime BookingsOpen { get; set; }
		public DateTime BookingsClose { get; set; }
		public DateTime CancellationsLocked { get; set; }

		public TimeSpan Duration { get; set; }
		public int? JobSeekerID { get; set; }
		public bool Booked { get; set; }

	}
}