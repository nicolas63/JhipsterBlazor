using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Blazorise;
using Microsoft.AspNetCore.Components;
using SharedModel.Constants;
using SharedModel.Models;
using Toolbelt.Blazor;

namespace JhipsterBlazor.Shared.Components
{
    public partial class AlertError
    {
        private List<JhiAlert> Alerts { get; set; }

        [Inject] 
        private HttpClientInterceptor Interceptor { get; set; }

        protected override Task OnInitializedAsync()
        {
            Alerts = new List<JhiAlert>();
            Interceptor.AfterSend += HandleErrors;
            return base.OnInitializedAsync();
        }

        public async void AddAlert(JhiAlert alert)
        {
            Alerts.Add(alert);
            if (alert.Timeout != 0)
            {
                RemoveAfter(alert, alert.Timeout);
            }
            await InvokeAsync(StateHasChanged);
        }

        public async void RemoveAlert(JhiAlert alert)
        {
            Alerts.Remove(alert);
            await InvokeAsync(StateHasChanged);
        }

        private void HandleErrors(object s, HttpClientInterceptorEventArgs e)
        {
            if (e.Response?.IsSuccessStatusCode == true)
            {
                return;
            }
            if (e.Response == null)
            {
                AddErrorAlert("Server not reachable");
                return;
            }
            switch (e.Response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    var errorHandler = "";
                    foreach (var httpResponseHeader in e.Response.Headers)
                    {
                        if (httpResponseHeader.Key.EndsWith("app-error"))
                        {
                            errorHandler = httpResponseHeader.Key;
                        }
                    }
                    if (errorHandler != "")
                    {
                        AddErrorAlert(errorHandler);
                    }
                    break;
                case HttpStatusCode.NotFound:
                    AddErrorAlert("Not found");
                    break;
                default:
                    AddErrorAlert(e.Response.Content.ToString());
                    break;
            }
        }

        private void AddErrorAlert(string errorMsg)
        {
            var alert = new JhiAlert
            {
                Type = TypeAlert.Danger,
                Msg = errorMsg,
                Timeout = 3000,
                Scoped = true,
            };
            AddAlert(alert);
        }

        private void RemoveAfter(JhiAlert alert, double timeout)
        {
            var timer = new Timer { Interval = timeout };
            timer.Elapsed += (sender, args) =>
            {
                if (sender is Timer senderTimer)
                {
                    senderTimer.Stop();
                }

                RemoveAlert(alert);
            };
            timer.Start();
        }

        private Color GetColorAlert(JhiAlert alert)
        {
            switch (alert.Type)
            {
                case TypeAlert.Danger:
                    return Color.Danger;
                case TypeAlert.Info:
                    return Color.Info;
                case TypeAlert.Success:
                    return Color.Success;
                case TypeAlert.Warning:
                    return Color.Warning;
                default:
                    return Color.Danger;
            }
        }
    }
}
