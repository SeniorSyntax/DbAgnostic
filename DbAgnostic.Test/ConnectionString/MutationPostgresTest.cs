using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class MutationPostgresTest
{
	[Test]
	public void ShouldChangeDatabase()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", Database = "original"}.ToString();

		var result = connectionString.ChangeDatabase("changed");

		new NpgsqlConnectionStringBuilder(result).Database.ShouldBe("changed");
	}

	[Test]
	public void ShouldPointToMasterDatabase()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", Database = "db"}.ToString();

		var result = connectionString.PointToMasterDatabase();

		new NpgsqlConnectionStringBuilder(result).Database.ShouldBe("postgres");
	}

	[Test]
	public void ShouldChangeServer()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "server"}.ToString();

		var result = connectionString.ChangeServer("anotherserver");

		new NpgsqlConnectionStringBuilder(result).Host.ShouldBe("anotherserver");
	}

	[Test]
	public void ShouldChangeApplicationName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", ApplicationName = "app"}.ToString();

		var result = connectionString.ChangeApplicationName("coolapp");

		new NpgsqlConnectionStringBuilder(result).ApplicationName.ShouldBe("coolapp");
	}

	[Test]
	public void ShouldSetConnectionTimeout()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", ApplicationName = "app"}.ToString();

		var result = connectionString.SetConnectionTimeout(17);

		new NpgsqlConnectionStringBuilder(result).Timeout
			.ShouldBe(17);
	}
}
