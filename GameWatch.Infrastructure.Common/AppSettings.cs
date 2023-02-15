using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWatch.Infrastructure.Common;

/// <summary>
/// Application settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// IGDB client ID.
    /// </summary>
    public string IgdbClientId { get; init; }

    /// <summary>
    /// IGDB client secret.
    /// </summary>
    public string IgdbClientSecret { get; init; }
}
