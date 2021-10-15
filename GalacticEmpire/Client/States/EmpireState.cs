using GalacticEmpire.Shared.Dto.Empire;
using GalacticEmpire.Shared.Dto.Unit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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

        public async Task BuyPlanet(int planetId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Planets/buy-planet/{planetId}", new { });
            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
            NotifyStateChanged();
        }

        public async Task BuyUpgrade(Guid empirePlanetId,int upgradeId, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper)
        {
            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Upgrades/{empirePlanetId}/add-upgrade/{upgradeId}", new { });
            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
            NotifyStateChanged();
        }

        public async Task BuyUnits(List<BuyUnitDetailsDto> buyUnits, IHttpClientFactory HttpClientFactory, NavigationManager uriHelper)
        {
            var BuyUnitCollection = new BuyUnitsCollectionDto
            {
                Units = buyUnits
            };

            var http = HttpClientFactory.CreateClient("blazorWASM");
            var result = await http.PostAsJsonAsync($"api/Units/buy-units", BuyUnitCollection);
            Empire = null;
            await InitializeAsync(uriHelper, HttpClientFactory);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
