﻿using System;
using System.Web.Http.ModelBinding;

namespace Seed.Api.Security
{
    public class SignInFailedResult : SignInResult
    {
        private readonly ModelStateDictionary _modelState;

        public SignInFailedResult(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public string Message
        {
            get { return "Invalid user name or password."; }
        }

        public ModelStateDictionary ModelState
        {
            get { return _modelState; }
        }
    }
}