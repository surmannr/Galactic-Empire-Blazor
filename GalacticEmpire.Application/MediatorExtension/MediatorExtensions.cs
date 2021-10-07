using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.MediatorExtension
{
    public static class MediatorExtensions
    {
        public static void Schedule(this IMediator mediator, string jobName, INotification request, TimeSpan time)
        {
            var client = new BackgroundJobClient();
            client.Schedule<MediatorHangfireBridge>(bridge => bridge.Publish(jobName, request), time);
        }

        public static void Schedule(this IMediator mediator, INotification request, TimeSpan time)
        {
            var client = new BackgroundJobClient();
            client.Schedule<MediatorHangfireBridge>(bridge => bridge.Publish(request), time);
        }
    }
}
