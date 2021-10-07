using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.MediatorExtension
{
    public class MediatorHangfireBridge
    {
        private readonly IMediator _mediator;

        public MediatorHangfireBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(INotification command)
        {
            await _mediator.Publish(command);
        }

        [DisplayName("{0}")]
        public async Task Publish(string jobName, INotification command)
        {
            await _mediator.Publish(command);
        }
    }
}
