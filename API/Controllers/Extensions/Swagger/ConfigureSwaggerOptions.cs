﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions.Startup.Swagger
{
    /// <summary>
    ///     Configures the Swagger generation options.
    /// </summary>
    /// <remarks>
    ///     This allows API versioning to define a Swagger document per API version after the
    ///     <see cref="IApiVersionDescriptionProvider" /> service has been resolved from the service container.
    /// </remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
        /// </summary>
        /// <param name="provider">
        ///     The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
        ///     documents.
        /// </param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Title = "Orders Demo",
                        Version = description.ApiVersion.ToString(),
                        Description = "API"
                    });
            }
        }
    }
}
