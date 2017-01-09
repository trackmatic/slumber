[![NuGet Status](https://img.shields.io/nuget/v/Slumber.svg)](https://www.nuget.org/packages/Slumber/)

# Modular Rest Client Library for .Net

Slumber is a modular rest client library built to provide a flexible framework to create client libraries for any rest api.

Out of the box you get an http implementation built on top of System.Web. Custom implementations can be developed by implmenting the IHttp interface.

## Install With NuGet

    install-package Slumber

## Create a Slumber Client

    var client = new SlumberClient(SlumberConfigurationFactory.Default("http://api.fixer.io"));

## Create a Slumber Client with custom configuration

    var client = new SlumberClient(SlumberConfigurationFactory.Default("http://api.fixer.io", configure: slumber =>
    {
        slumber.UseConsoleLogger();
        slumber.Serialization.Register(ContentTypes.TextHtml, slumber.Serialization.GetFactory(ContentTypes.ApplicationJson));
    }));

## Create and Execute a Request

    var request = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build(); 
    var response = client.Execute(request);

## Multipart content

    var content = new MultipartContent();
    content.Files.Add("file", new MultipartFile("file1.jpg", "image/jpg", File.ReadAllBytes("file1.png")));
    content.FormData.Add("field", "some value");
    var request = HttpRequestBuilder<ExchangeRates>.Get("/latest").QueryParameter("base", "USD").Content(content).Build();
    client.ExecuteAsync(request);

## Dynamic deserialization

    var request = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();
    var response = client.ExecuteAsync(dynamicRequest);

## Typed deserialization

    var request = HttpRequestBuilder<ExchangeRate>.Get("/latest").QueryParameter("base", "USD").Build();
    var response = client.ExecuteAsync(dynamicRequest);