using GalacticEmpire.Shared.Dto.Empire;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
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

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
