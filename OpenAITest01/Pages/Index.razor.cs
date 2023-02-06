using Microsoft.AspNetCore.Components;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using Material.Blazor;

namespace OpenAITest01.Pages;

public partial class Index
{
    [Inject] private IOpenAIService OpenAIService { get; set; }


    private string Question { get; set; } = "";
    private CompletionCreateResponse Response { get; set; } = new();
    private MBCircularProgress? CircularProgress { get; set; }
    private MBCircularProgressType ProgressType { get; set; } = MBCircularProgressType.Closed;
    private int MaxTokens { get; set; } = 100;
    private decimal Temperature { get; set; } = 0.1m;

    private async Task ExecuteQuery()
    {
        ProgressType = MBCircularProgressType.Indeterminate;
        await InvokeAsync(StateHasChanged);
        
        Response = await OpenAIService.Completions.CreateCompletion(new CompletionCreateRequest()
        {
            Prompt = Question,
            Model = Models.TextDavinciV3,
            Temperature = (float)Temperature,
            MaxTokens = MaxTokens,
        });

        ProgressType = MBCircularProgressType.Closed;
        await InvokeAsync(StateHasChanged);
    }
}
