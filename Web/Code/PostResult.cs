﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code {

    public class PostResult<T> : IApiResult {
        public PostResult(T result) {
            this.Content = result;
        }

        public PostResult(string error) {
            this.Error = error;
        }

        public T Content { get; private set; }

        public bool Success {
            get { return String.IsNullOrWhiteSpace(this.Error); }
        }

        public string Error { get; private set; }
    }
}