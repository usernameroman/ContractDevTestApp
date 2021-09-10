using System;
using System.Collections.Generic;
using System.Linq;
using ContractDevTestApp.Application.Common.Exceptions;
using ContractDevTestApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContractDevTestApp.Filters
{
	public class ExceptionFilter : ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

		public ExceptionFilter()
		{
			// Register known exception types and handlers.
			_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{ typeof(ValidationException), HandleValidationException },
				{ typeof(InvalidLoginException), HandleInvalidLoginException },
				{ typeof(EntityNotFoundException), HandleEntityNotFoundException },
			};
		}

		public override void OnException(ExceptionContext context)
		{
			HandleException(context);

			base.OnException(context);
		}
		private void HandleException(ExceptionContext context)
		{
			var type = context.Exception.GetType();
			if (_exceptionHandlers.ContainsKey(type))
			{
				_exceptionHandlers[type].Invoke(context);
				return;
			}

			HandleUnknownException(context);
		}

		private void HandleUnknownException(ExceptionContext context)
		{
			var details = new CommonResponseDto()
			{
				Status = "Internal error",
				Message = context.Exception.Message,
				StackTrace = context.Exception.StackTrace
			};

			context.Result = new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};

			context.ExceptionHandled = true;
		}

		private void HandleValidationException(ExceptionContext context)
		{
			var localizedErrors = (context.Exception as ValidationException)
				.Errors.Select(pair =>
					new KeyValuePair<string, string[]>(
						pair.Key,
						pair.Value.ToArray()
					)
				).ToDictionary(x => x.Key, x => x.Value);

			var details = new CommonResponseDto()
			{
				Status = "Unprocessable entity",
				Errors = localizedErrors,
				Message = "Validation Failed"
			};

			context.Result = new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status422UnprocessableEntity
			};

			context.ExceptionHandled = true;
		}

		private void HandleInvalidLoginException(ExceptionContext context)
		{
			var details = new CommonResponseDto()
			{
				Status = "Login Failed",
				Message = context.Exception.Message
			};

			context.Result = new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status401Unauthorized
			};

			context.ExceptionHandled = true;
		}

		private void HandleEntityNotFoundException(ExceptionContext context)
		{
			var details = new CommonResponseDto()
			{
				Status = "Entity not found",
				Message = context.Exception.Message
			};

			context.Result = new ObjectResult(details)
			{
				StatusCode = StatusCodes.Status400BadRequest
			};

			context.ExceptionHandled = true;
		}
	}
}