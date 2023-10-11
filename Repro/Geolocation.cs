// Copyright (c) Martin Costello, 2023. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Repro;

[TypeConverter(typeof(GeolocationTypeConverter))]
public struct Geolocation : IEquatable<Geolocation>, IParsable<Geolocation>
{
    public static readonly Geolocation Zero = new(0, 0);

    private const double MaximumLatitude = 90;
    private const double MaximumLongitude = 180;

    public Geolocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public static bool operator ==(Geolocation left, Geolocation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Geolocation left, Geolocation right)
    {
        return !(left == right);
    }

    public static double Distance(Geolocation x, Geolocation y)
    {
        static double ToRadians(double degrees) => degrees * Math.PI / 180;

        double R = 6371e3;
        double φ1 = ToRadians(x.Latitude);
        double φ2 = ToRadians(y.Latitude);
        double Δφ = ToRadians(y.Latitude - x.Latitude);
        double Δλ = ToRadians(y.Longitude - x.Longitude);

        double a = (Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2)) +
                   (Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2));

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double d = R * c;

        return d;
    }

    public static Geolocation Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException($"The string '{s}' is not a valid geolocation.");
        }

        return result;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Geolocation result)
    {
        result = Zero;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        string separator = ",";

        if (provider is CultureInfo culture)
        {
            separator = culture.NumberFormat.NumberGroupSeparator;
        }

        var parts = s.Split(separator);

        if (parts.Length != 2)
        {
            return false;
        }

        if (!double.TryParse(parts[0], provider, out var latitude) ||
            !double.TryParse(parts[1], provider, out var longitude))
        {
            return false;
        }

        if (latitude > MaximumLatitude ||
            latitude < -MaximumLatitude)
        {
            return false;
        }

        if (longitude > MaximumLongitude ||
            longitude < -MaximumLongitude)
        {
            return false;
        }

        result = new(latitude, longitude);
        return true;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Geolocation other)
        {
            return Equals(other);
        }

        return false;
    }

    public bool Equals(Geolocation other)
        => Latitude == other.Latitude && Longitude == other.Longitude;

    public override int GetHashCode()
        => HashCode.Combine(Latitude, Longitude);

    public override string ToString()
        => $"{Latitude},{Longitude}";

    private sealed class GeolocationTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(Geolocation))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string s)
            {
                return Parse(s, culture);
            }
            else if (value is Geolocation geolocation)
            {
                return geolocation;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
