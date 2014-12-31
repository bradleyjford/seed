using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Seed.Api.Infrastructure.ActionResults
{
    public class ValidationFailureResponse : IHttpActionResult
    {
        private readonly HttpActionContext _context;
        private readonly IEnumerable<ValidationResultResponse> _validationResults;

        public ValidationFailureResponse(HttpActionContext context, IEnumerable<ValidationResult> validationResults)
        {
            _context = context;
            _validationResults = validationResults.Select(r => new ValidationResultResponse(r.ErrorMessage, r.MemberNames));
        }
        
        public ValidationFailureResponse(HttpActionContext context, ModelStateDictionary modelState)
        {
            _context = context;

            _validationResults =
                from keyValuePair in modelState
                from error in keyValuePair.Value.Errors
                select new ValidationResultResponse(error.ErrorMessage, new[] { StripModelPrefix(keyValuePair.Key) });
        }

        private string StripModelPrefix(string propertyName)
        {
            var lastSeperator = propertyName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase);

            if (lastSeperator == -1)
            {
                return propertyName;
            }

            return propertyName.Substring(lastSeperator + 1, propertyName.Length - (lastSeperator + 1));
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var config = _context.ControllerContext.Configuration;

            var contentNegotiator = config.Services.GetContentNegotiator();
            var content = contentNegotiator.Negotiate(_validationResults.GetType(), _context.Request, config.Formatters);

            var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                ReasonPhrase = "Validation failure",
                Content = new ObjectContent(_validationResults.GetType(), _validationResults, content.Formatter)
            };

            return Task.FromResult(response);
        }

        public class ValidationResultResponse
        {
            public ValidationResultResponse(string errorMessage, IEnumerable<string> memberNames)
            {
                ErrorMessage = errorMessage;
                MemberNames = memberNames;
            }

            public string ErrorMessage { get; private set; }
            public IEnumerable<string> MemberNames { get; private set; }
        }
    }
}