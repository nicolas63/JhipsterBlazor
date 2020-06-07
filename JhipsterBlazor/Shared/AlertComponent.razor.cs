using JhipsterBlazor.Services;

namespace JhipsterBlazor.Shared
{
    public partial class AlertComponent
    {
        private readonly IAlertService _alertService;

        public AlertComponent(IAlertService alertService)
        {
            _alertService = alertService;
        }
    }
}
