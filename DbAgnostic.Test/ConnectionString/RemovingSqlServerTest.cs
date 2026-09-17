using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class RemovingSqlServerTest
{
    [Test]
    public void ShouldRemoveDatabase()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

        var result = connectionString.ChangeDatabase(null);

        result.ShouldBe("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveDatabase2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

        var result = connectionString.RemoveDatabase();

        result.ShouldBe("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveServer()
    {
        var connectionString = new SqlConnectionStringBuilder {InitialCatalog = "db", DataSource = "server"}.ToString();

        var result = connectionString.ChangeServer(null);

        result.ShouldBe("Initial Catalog=db");
    }

    [Test]
    public void ShouldRemoveServer2()
    {
        var connectionString = new SqlConnectionStringBuilder {InitialCatalog = "db", DataSource = "server"}.ToString();

        var result = connectionString.RemoveServer();

        result.ShouldBe("Initial Catalog=db");
    }

    [Test]
    public void ShouldRemoveApplicationName()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.ChangeApplicationName(null);

        result.ShouldBe("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveApplicationName2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.RemoveApplicationName();

        result.ShouldBe("Data Source=foo");
    }
}