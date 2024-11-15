using SGONGA.WebAPI.Business.Shared.Responses;
using System.Text.Json;

namespace SGONGA.WebAPI.Business.Tests.Shared.Responses;

[Trait("Shared", "Exceptions")]
public class BasePagedResponseTests
{
    [Fact]
    public void Constructor_Should_InitializeWithDefaults()
    {
        // Act
        var response = new BasePagedResponse<string>();

        // Assert
        Assert.Equal(0, response.PageIndex);
        Assert.Equal(0, response.PageSize);
        Assert.Equal(0, response.TotalResults);
        Assert.Null(response.Query);
        Assert.False(response.ReturnAll);
        Assert.Empty(response.List);
    }

    [Fact]
    public void Constructor_WithParameters_Should_InitializeProperties()
    {
        // Arrange
        var data = new List<string> { "Item1", "Item2" };
        int pageIndex = 2;
        int pageSize = 10;
        int totalResults = 25;
        string query = "test";
        bool returnAll = true;

        // Act
        var response = new BasePagedResponse<string>(data, totalResults, pageIndex, pageSize, query, returnAll);

        // Assert
        Assert.Equal(pageIndex, response.PageIndex);
        Assert.Equal(pageSize, response.PageSize);
        Assert.Equal(totalResults, response.TotalResults);
        Assert.Equal(query, response.Query);
        Assert.True(response.ReturnAll);
        Assert.Equal(data, response.List);
        Assert.Equal(3, response.TotalPages); // 25 items with page size 10 => 3 pages
    }

    [Fact]
    public void TotalPages_Should_CalculateCorrectly()
    {
        // Arrange
        var response = new BasePagedResponse<string>
        {
            PageSize = 10,
            TotalResults = 55 // 55 items with page size 10 => 6 pages
        };

        // Act
        var totalPages = response.TotalPages;

        // Assert
        Assert.Equal(6, totalPages);
    }

    [Fact]
    public void List_Should_BeSettable()
    {
        // Arrange
        var response = new BasePagedResponse<string>();
        var data = new List<string> { "Item1", "Item2", "Item3" };

        // Act
        response.List = data;

        // Assert
        Assert.Equal(data, response.List);
        Assert.Equal(3, response.List.Count());
    }

    [Fact]
    public void JsonConstructor_Should_DeserializeCorrectly_UsingSystemTextJson()
    {
        // Arrange
        string json = @"
        {
            ""list"": [""Item1"", ""Item2""],
            ""totalResults"": 20,
            ""pageIndex"": 1,
            ""pageSize"": 10,
            ""query"": ""test"",
            ""returnAll"": true
        }";

        // Act
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var response = JsonSerializer.Deserialize<BasePagedResponse<string>>(json, options);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(1, response.PageIndex);
        Assert.Equal(10, response.PageSize);
        Assert.Equal(20, response.TotalResults);
        Assert.Equal("test", response.Query);
        Assert.True(response.ReturnAll);
        Assert.Equal(new[] { "Item1", "Item2" }, response.List);
    }

}
