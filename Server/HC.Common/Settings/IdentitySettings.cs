﻿using HC.Common.Extensions;

namespace HC.Common.Settings;

public class IdentitySettings
{
    public bool PasswordRequireDigit { get; set; }
    public int PasswordRequiredLength { get; set; }
    public bool PasswordRequireNonAlphanumeric { get; set; }
    public bool PasswordRequireUppercase { get; set; }
    public bool PasswordRequireLowercase { get; set; }
    public bool RequireUniqueEmail { get; set; }
    public bool RequireConfirmedEmail { get; set; }
    public bool RequireConfirmedPhoneNumber { get; set; }

    public static IdentitySettings Get()
    {
        var appSettingPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", "HC.Api", "appsettings.json");
        IdentitySettings settings = ConfigurationExtensions.GetSection<IdentitySettings>(appSettingPath, nameof(IdentitySettings)) ?? new();
        return settings;
    }
}
