using GalacticEmpire.Client.Shared;
using GalacticEmpire.Shared.Dto.Attack;
using GalacticEmpire.Shared.Dto.Drone;
using GalacticEmpire.Shared.Dto.Empire;
using GalacticEmpire.Shared.Dto.Unit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;

namespace GalacticEmpire.Client.States
{
    public class EmpireState : IAsyncDisposable
    {
        public EmpireDetailsDto Empire { get; set; }
        public HubConnection Connection { get; set; }

        public event Action OnChange;

        public EmpireState(NavigationManager uriHelper, IHttpClientFactory HttpClientFactory, ISnackbar snackbar)
        {
            Connection = new HubConnectionBuilder()
                   .WithUrl(uriHelper.ToAbsoluteUri("/gamehub"))
                   .WithAutomaticReconnect()
                   .Build();

            Connection.On<string>("FinishedJob", action =>
            {
                snackbar.Add(action, Severity.Info);

                CallLoadData(uriHelper, HttpClientFactory);

                NotifyStateChanged();
            });

            Task.Run(async () =>
            {
                await Connection.StartAsync();
            });
        }

        public async Task InitializeAsync(NavigationManager uriHelper, IHttpClientFactory HttpClientFactory, ISnackbar snackbar)
        {
            if (Empire == null)
            {
                try
                {
                    var http = HttpClientFactory.CreateClient("blazorWASM");
                    Empire = await http.GetFromJsonAsync<EmpireDetailsDto>($"api/Empires/details");
                }
                catch (AccessTokenNotAvailableException)
                {
                    uriHelper.NavigateTo("/");
                }

            }

            if(Connection.State == HubConnectionState.Disconnected)
            {
                Connection = new HubConnectionBuilder()
                    .WithUrl(uriHelper.ToAbsoluteUri("/gamehub"))
                    .WithAutomaticReconnect()
                    .Build();

                Connection.On<string>("FinishedJob", action =>
                {
                    snackbar.Add(action, Severity.Info);

                    CallLoadData(uriHelper, HttpClientFactory);

                    NotifyStateChanged();
                });

                await Connection.StartAsync();
            }
        }

        private void CallLoadData(NavigationManager uriHelper, IHttpClientFactory HttpClientFactory)
        {
            Task.Run(async () =>
            {
                await WebsocketInvoke(uriHelper, HttpClientFactory);
            });
        }

        public async Task WebsocketInvoke(NavigationManager uriHelper, IHttpClientFactory HttpClientFactory)
        {
            try
            {
                var http = HttpClientFactory.CreateClient("blazorWASM");
                Empire = await http.GetFromJsonAsync<EmpireDetailsDto>($"api/Empires/details");
            }
            catch (AccessTokenNotAvailableException)
            {
                uriHelper.NavigateTo("/");
            }

            NotifyStateChanged();
        }

        public async Task BuyPlanet(int planetId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Planets/buy-planet/{planetId}?connectionId={Connection.ConnectionId}", new { });

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("A bolygó foglalás alatt.", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory, Snackbar);
            NotifyStateChanged();
        }

        public async Task BuyUpgrade(Guid empirePlanetId,int upgradeId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Upgrades/{empirePlanetId}/add-upgrade/{upgradeId}?connectionId={Connection.ConnectionId}", new { });

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("A bolygó fejlesztés alatt.", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory, Snackbar);
            NotifyStateChanged();
        }

        public async Task AttackPlayer(SendAttackDto SendAttackDto, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Attack/sendattack?connectionId={Connection.ConnectionId}", SendAttackDto);

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("Sikeresen megtámadtad a kiválasztott játékost!", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory, Snackbar);
            NotifyStateChanged();
        }

        public async Task DronePlayer(SendDroneDto SendDroneDto, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Drone/senddrone?connectionId={Connection.ConnectionId}", SendDroneDto);

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("Sikeresen elküldtek a kémeket a kiválasztott játékoshoz!", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory, Snackbar);
            NotifyStateChanged();
        }

        public async Task BuyUnits(List<BuyUnitDetailsDto> buyUnits, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var BuyUnitCollection = new BuyUnitsCollectionDto
            {
                Units = buyUnits
            };

            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Units/buy-units?connectionId={Connection.ConnectionId}", BuyUnitCollection);

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("Sikeresen elkezdted képezni az egységeket!", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory, Snackbar);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private async Task<bool> HandleError(HttpResponseMessage responseMessage, ISnackbar Snackbar)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadFromJsonAsync<ErrorHandling>();
                Snackbar.Add(error.Title, Severity.Error);
                return true;
            }

            return false;
        }

        public async ValueTask DisposeAsync()
        {
            if (Connection is not null)
            {
                await Connection.DisposeAsync();
            }
        }

    }
}
