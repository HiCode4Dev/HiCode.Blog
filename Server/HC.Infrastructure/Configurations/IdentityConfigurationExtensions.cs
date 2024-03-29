﻿using System.Text;
using HC.Common.Settings;
using HC.Data.Context;
using HC.Data.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HC.Infrastructure.Configurations;

public static class IdentityConfigurationExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(identityOptions =>
        {
            //Password Settings
            identityOptions.Password.RequireDigit = IdentitySettings.Get().PasswordRequireDigit;
            identityOptions.Password.RequiredLength = IdentitySettings.Get().PasswordRequiredLength;
            identityOptions.Password.RequireNonAlphanumeric = IdentitySettings.Get().PasswordRequireNonAlphanumeric; //#@!
            identityOptions.Password.RequireUppercase = IdentitySettings.Get().PasswordRequireUppercase;
            identityOptions.Password.RequireLowercase = IdentitySettings.Get().PasswordRequireLowercase;

            //UserName Settings
            identityOptions.User.RequireUniqueEmail = IdentitySettings.Get().RequireUniqueEmail;

            //Singin Settings
            identityOptions.SignIn.RequireConfirmedEmail = IdentitySettings.Get().RequireConfirmedEmail;
            identityOptions.SignIn.RequireConfirmedPhoneNumber = IdentitySettings.Get().RequireConfirmedPhoneNumber;

            //Lockout Settings
            //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //identityOptions.Lockout.AllowedForNewUsers = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, //default : false
                ValidIssuer = JwtSettings.Get().Issuer,

                ValidateAudience = true, //default : false
                ValidAudience = JwtSettings.Get().Audience,

                ValidateIssuerSigningKey = true, //default : false
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };
        });
    }

}
