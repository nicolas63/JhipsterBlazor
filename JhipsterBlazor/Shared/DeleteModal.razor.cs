using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace JhipsterBlazor.Shared
{
    public partial class DeleteModal
    {
        [CascadingParameter]
        BlazoredModalInstance BlazoredModal { get; set; }
        
        private void Delete()
        {
            BlazoredModal.Close();
        }
        
        private void Close()
        {
            BlazoredModal.Cancel();
        }
    }
}
