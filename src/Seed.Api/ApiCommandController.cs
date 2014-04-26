﻿using System;
using System.Web.Http;
using Seed.Infrastructure.Messaging;

namespace Seed.Api
{
    public abstract class ApiCommandController : ApiController
    {
        public IHttpActionResult CommandResult(ICommandResult result)
        {
            if (result.Success)
            {
                return Ok();
            }

            return InternalServerError(result.Error);
        }
    }
}