using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code {
    public class DeleteResult : IApiResult {
        public DeleteResult() {}

        public DeleteResult(string error) {
            this.Error = error;
        }

        public bool Success {
            get { return String.IsNullOrWhiteSpace(this.Error); }
        }

        public string Error { get; private set; }
    }
}