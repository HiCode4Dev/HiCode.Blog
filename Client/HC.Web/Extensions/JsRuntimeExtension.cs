﻿using Microsoft.JSInterop;

namespace HC.Web.Extensions;

public static class JsRuntimeExtension
{
    /// <summary>
    /// To disable the scrollbar of the body when showing the modal, so the modal can be always shown in the viewport without being scrolled out.
    /// </summary>
    public static async Task SetBodyOverflow(this IJSRuntime jsRuntime, bool hidden)
    {
        await jsRuntime.InvokeVoidAsync("App.setBodyOverflow", hidden);
    }

    public static async Task GoBack(this IJSRuntime jsRuntime)
    {
        await jsRuntime.InvokeVoidAsync("App.goBack");
    }
}
