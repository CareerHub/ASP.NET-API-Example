using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Code {
    public interface IApiResult {

        bool Success { get; }
        string Error { get; }
    }
}
