using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class ReadingPostgresTest
{
	[Test]
	public void ShouldParseDatabaseName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = "db"}.ToString();

		connectionString.DatabaseName().ShouldBe("db");
	}

	[Test]
	public void ShouldParseDatabaseNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		connectionString.DatabaseName().ShouldBe(null);
	}

	[Test]
	public void ShouldParseDatabaseNameNull2()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = null}.ToString();

		connectionString.DatabaseName().ShouldBe(null);
	}

	[Test]
	public void ShouldParseServerName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "server"}.ToString();

		connectionString.ServerName().ShouldBe("server");
	}

	[Test]
	public void ShouldParseServerNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = null, Username = "user"}.ToString();

		connectionString.ServerName().ShouldBe(null);
	}
	
	[Test]
	public void ShouldParseApplicationName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", ApplicationName = "app"}.ToString();

		connectionString.ApplicationName().ShouldBe("app");
	}

	[Test]
	public void ShouldParseApplicationNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		connectionString.ApplicationName().ShouldBe(null);
	}

	[Test]
	public void ShouldParsePassword()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Password = "pass"}.ToString();

		connectionString.Password().ShouldBe("pass");
	}

	[Test]
	public void ShouldParseUserName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "user"}.ToString();

		connectionString.UserName().ShouldBe("user");
	}

	[Test]
	public void ShouldNotSupportIntegratedSecurity()
	{
		var connectionString = "Host=foo;Database=bar";

		connectionString.ServerName().ShouldBe("foo");
		connectionString.DatabaseName().ShouldBe("bar");
		connectionString.IntegratedSecurity().ShouldBeFalse();
	}

	[Test]
	public void ShouldParseConnectionTimeout()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Timeout = 42}.ToString();

		connectionString.ConnectionTimeout().ShouldBe(42);
	}
}