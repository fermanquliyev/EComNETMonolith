using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Shared.CQRS.Behaviors
{
    public class LoggingBehaviour<TCommand, TCommandResponse> : IPipelineBehavior<TCommand, TCommandResponse>
    where TCommand : IRequest<TCommandResponse>
    where TCommandResponse: notnull
    {
        private readonly ILogger<LoggingBehaviour<TCommand, TCommandResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TCommand, TCommandResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TCommandResponse> Handle(TCommand request, RequestHandlerDelegate<TCommandResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[BEGIN] Handling {RequestName} ({Request}) with {Response} Type", request.GetType().Name, request, typeof(TCommandResponse).Name);
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var elapsedMilliseconds = timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 3000)
            {
                _logger.LogWarning("[SLOW] {RequestName} ({Request}) took {ElapsedMilliseconds} ms", request.GetType().Name, request, elapsedMilliseconds);
            }

            _logger.LogInformation("[END] Handled {RequestName} with {ResponseType} Type", request.GetType().Name, response.GetType().Name);
            return response;
        }
    }
}
