using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.SignalR
{
    public class GameHubService : IGameHubService
    {
        private readonly IHubContext<GameHub> hubContext;

        public GameHubService(IHubContext<GameHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task FinishJob(string connectionId, string action)
        {
            await hubContext.Clients.Client(connectionId).SendAsync("FinishedJob", action);
        }
    }

    public interface IGameHubService
    {
        Task FinishJob(string userId, string action);
    }
}
