﻿using Core.Application.Abstractions.Storage;
using Core.Application.Abstractions.Tokens;
using Infrastructure.Infrastructure.Enums;
using Infrastructure.Infrastructure.Services.Storage;
using Infrastructure.Infrastructure.Services.Storage.Azure;
using Infrastructure.Infrastructure.Services.Storage.Local;
using Infrastructure.Infrastructure.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }

    public static void AddStorage(this IServiceCollection services, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local: services.AddScoped<IStorage, LocalStorage>(); break;
            case StorageType.Azure: services.AddScoped<IStorage, AzureStorage>(); break;
            default: services.AddScoped<IStorage, LocalStorage>(); break;
        }
    }
}
