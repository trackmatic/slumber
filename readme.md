[![NuGet Status](https://img.shields.io/nuget/v/Slumber.svg)](https://www.nuget.org/packages/Slumber/)

# Modular Rest Client Library for .Net

Slumber is a modular rest client library which allows you to swap out each component to meet your requirements

## Install With NuGet

    install-package Slumber

## Create a Slumber Client

    var client = new SlumberClient("http://api.fixer.io", slumber =>
    {
        slumber.UseJsonSerialization().UseHttp(http => http.ApplicationJson()).UseConsoleLogger();
    });

## Create and Execute a Request

    var request = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build(); 
    var response = client.Execute(request);