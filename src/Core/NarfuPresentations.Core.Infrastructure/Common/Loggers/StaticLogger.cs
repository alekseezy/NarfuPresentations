﻿using Serilog;
using Serilog.Core;

namespace NarfuPresentations.Core.Infrastructure.Common.Loggers;

public static class StaticLogger
{
    public static void EnsureInitialized()
    {
        if (Log.Logger is not Logger)
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
    }
}
