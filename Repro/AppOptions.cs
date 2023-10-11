// Copyright (c) Martin Costello, 2023. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace Repro;

public sealed class SiteOptions
{
    public Geolocation Single { get; set; }

    public IDictionary<string, Geolocation> Keyed { get; set; } = new Dictionary<string, Geolocation>();
}
