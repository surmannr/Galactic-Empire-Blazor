using GalacticEmpire.Client.Shared;
using GalacticEmpire.Shared.Dto.Empire;
using GalacticEmpire.Shared.Dto.Unit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GalacticEmpire.Client.States
{
    public class EmpireState
    {
        public EmpireDetailsDto Empire { get; set; }

        public event Action OnChange;

        public async Task InitializeAsync(NavigationManager uriHelper, IHttpClientFactory HttpClientFactory)
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

            NotifyStateChanged();
        }

        public async Task BuyPlanet(int planetId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Planets/buy-planet/{planetId}", new { });

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("A bolygó foglalás alatt.", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
            NotifyStateChanged();
        }

        public async Task BuyUpgrade(Guid empirePlanetId,int upgradeId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Upgrades/{empirePlanetId}/add-upgrade/{upgradeId}", new { });

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("A bolygó fejlesztés alatt.", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
            NotifyStateChanged();
        }

        public async Task BuyUnits(List<BuyUnitDetailsDto> buyUnits, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper, ISnackbar Snackbar)
        {
            var BuyUnitCollection = new BuyUnitsCollectionDto
            {
                Units = buyUnits
            };

            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Units/buy-units", BuyUnitCollection);

            var error = await HandleError(result, Snackbar);
            if (error) return;

            Snackbar.Add("Sikeresen elkezdted képezni az egységeket!", Severity.Success);

            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
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
    }
}
