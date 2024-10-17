using AutoMapper;
using Fiap.Web.SmartWasteManagement.Data.Contexts;
using Fiap.Web.SmartWasteManagement.Data.Repository;
using Fiap.Web.SmartWasteManagement.Models;
using Fiap.Web.SmartWasteManagement.Services;
using Fiap.Web.SmartWasteManagement.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.Web.SmartWasteManagemente.WebApi.Tests
{
    public class CaminhaoControllerTests
    {
        private readonly HttpClient _client;

        public CaminhaoControllerTests()
        {
           _client = new HttpClient
           {
               BaseAddress = new Uri("http://localhost:5038/")
           };
        }

        [Fact]
        public async Task GetCaminhoes_RetornarStatusOK()
        {
            //Arrange
            var requestUri = "/api/Caminhao";

            //Act
            var response = await _client.GetAsync(requestUri);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCaminhaoDeId5_RetornarStatusOK()
        {
            //Arrange
            var requestUri = "/api/Caminhao/5";

            //Act
            var response = await _client.GetAsync(requestUri);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}