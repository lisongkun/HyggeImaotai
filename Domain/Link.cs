﻿using System.Diagnostics;
using System.Runtime.InteropServices;

namespace hygge_imaotai.Domain
{
    public static class Link
    {
        public static void OpenInBrowser(string? url)
        {
            if (url is not null && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }
}
