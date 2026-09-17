using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class MutationSqlServerTest
{
	[Test]
	public void ShouldChangeDatabase()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "original"}.ToString();

		var result = connectionString.ChangeDatabase("changed");

		new SqlConnectionStringBuilder(result).InitialCatalog.ShouldBe("changed");
	}

	[Test]
	public void ShouldPointToMasterDatabase()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

		var result = connectionString.PointToMasterDatabase();

		new SqlConnectionStringBuilder(result).InitialCatalog.ShouldBe("master");
	}

	[Test]
	public void ShouldChangeServer()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "server"}.ToString();

		var result = connectionString.ChangeServer("anotherserver");

		new SqlConnectionStringBuilder(result).DataSource.ShouldBe("anotherserver");
	}

	[Test]
	public void ShouldChangeApplicationName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		var result = connectionString.ChangeApplicationName("coolapp");

		new SqlConnectionStringBuilder(result).ApplicationName.ShouldBe("coolapp");
	}
	
	[Test]
	public void ShouldSetConnectionTimeout()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		var result = connectionString.SetConnectionTimeout(17);

		new SqlConnectionStringBuilder(result).ConnectTimeout
			.ShouldBe(17);
	}
}
