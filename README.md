# SmartWaste — API de gestão de resíduos

Projeto acadêmico de backend em **C# e ASP.NET Core 8**, relacionado ao domínio de gestão de resíduos e coleta.

## Componentes identificados

O projeto `Fiap.Web.SmartWasteManagement.WebApi` contém:

- `AuthController` — endpoints de autenticação;
- `CaminhaoController` — endpoints relacionados a caminhões;
- `WeatherForecastController` — endpoint de exemplo presente no projeto.

## Tecnologias presentes

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core e provedor Oracle
- Autenticação JWT Bearer
- AutoMapper
- Swagger
- Dockerfile

## Estrutura

```text
Fiap.Web.SmartWasteManagement.WebApi/
  Controllers/
  Program.cs
  Dockerfile
  Fiap.Web.SmartWasteManagement.WebApi.csproj
```

Para explorar o projeto, utilize o **SDK .NET 8** e revise primeiro as configurações de dependências, banco de dados e autenticação.

```bash
dotnet restore Fiap.Web.SmartWasteManagement.WebApi/Fiap.Web.SmartWasteManagement.WebApi.csproj
```

A solução pode depender de outros projetos ou serviços externos. Este README documenta o código encontrado, **não uma execução ou implantação testada**.

## Sobre o portfólio

Este repositório apresenta o estudo da construção de APIs com autenticação, persistência e organização de endpoints.
