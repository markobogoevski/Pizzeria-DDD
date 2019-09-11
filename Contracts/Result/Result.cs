namespace Contracts.Result
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;

    public class Result : ResultCommonLogic
    {
        private Result() : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
        }

        internal Result(ResultType resultType, string message) : base(resultType, isFailure: true, message: message)
        {
        }

        public static Result ValidationFailed(string message)
        {
            return new Result(ResultType.BadRequest, message);
        }

        public static Result<T> ValidationFailed<T>(string message)
        {
            return new Result<T>(ResultType.BadRequest, message);
        }

        public static Result Conflicted(string message)
        {
            return new Result(ResultType.Conflicted, message);
        }

        public static Result<T> Conflicted<T>(string message)
        {
            return new Result<T>(ResultType.Conflicted, message);
        }

        public static Result Failed(string message)
        {
            return new Result(ResultType.InternalError, message);
        }

        public static Result<T> Failed<T>(string message)
        {
            return new Result<T>(ResultType.InternalError, message);
        }

        public static Result Forbidden(string message)
        {
            return new Result(ResultType.Forbidden, message);
        }

        public static Result FirstFailureOrOk(params Result[] results)
        {
            if (results.Any(f => f.IsFailure))
            {
                return results.First(f => f.IsFailure);
            }

            return Ok();
        }

        public static Result FirstFailureOrOk<T>(IEnumerable<Result<T>> results)
        {
            if (results.Any(x => x.IsFailure))
            {
                return results.First(x => x.IsFailure);
            }

            return Ok();
        }

        // Generics
        public static Result<T> Forbidden<T>(string message)
        {
            return new Result<T>(ResultType.Forbidden, message);
        }

        public static Result Invalid(string message)
        {
            return new Result(ResultType.Invalid, message);
        }

        public static Result<T> Invalid<T>(string message)
        {
            return new Result<T>(ResultType.Invalid, message);
        }

        public static Result NotFound(string message)
        {
            return new Result(ResultType.NotFound, message);
        }

        public static Result<T> NotFound<T>(string message)
        {
            return new Result<T>(ResultType.NotFound, message);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> FromError<T>(ResultCommonLogic result)
        {
            return new Result<T>(result.ResultType, result.Message);
        }

        public static Result Unauthorized(string message)
        {
            return new Result(ResultType.Unauthorized, message);
        }

        public static Result<T> Unauthorized<T>(string message)
        {
            return new Result<T>(ResultType.Unauthorized, message);
        }
    }

    public class Result<T> : ResultCommonLogic
    {
        public bool IsEmpty => Value?.Equals(Empty) ?? true;

        public T Value { get; }

        private static T Empty => default(T);

        internal Result(ResultType resultType, string message) : base(resultType, isFailure: true, message: message)
        {
            Value = Empty;
        }

        internal Result(T value) : base(ResultType.Ok, isFailure: false, message: string.Empty)
        {
            Value = value;
        }


        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Result.Ok();
            }

            return new Result(result.ResultType, result.Message);
        }
    }

    [DebuggerStepThrough]
    public abstract class ResultCommonLogic
    {
        private readonly string _message;

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public string Message => _message;

        public ResultType ResultType { get; }

        public HttpStatusCode HttpStatusCode
        {
            get
            {
                HttpStatusCode statusCode;

                switch (ResultType)
                {
                    case ResultType.Ok:
                        statusCode = HttpStatusCode.OK;
                        break;
                    case ResultType.NotFound:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    case ResultType.Forbidden:
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    case ResultType.Conflicted:
                        statusCode = HttpStatusCode.Conflict;
                        break;
                    case ResultType.Invalid:
                        statusCode = HttpStatusCode.NotAcceptable;
                        break;
                    case ResultType.Unauthorized:
                        statusCode = HttpStatusCode.Unauthorized;
                        break;
                    case ResultType.BadRequest:
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                return statusCode;
            }
        }

        protected ResultCommonLogic(ResultType resultType, bool isFailure, string message)
        {
            if (isFailure)
            {
                if (String.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(message), "There must be error message for failure.");
                }

                if (resultType == ResultType.Ok)
                {
                    throw new ArgumentException("There should be error type for failure.", nameof(resultType));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(message))
                {
                    throw new ArgumentException("There should be no error message for success.", nameof(message));
                }

                if (resultType != ResultType.Ok)
                {
                    throw new ArgumentException("There should be no error type for success.", nameof(resultType));
                }
            }

            ResultType = resultType;
            IsFailure = isFailure;
            _message = message;
        }
    }
}
