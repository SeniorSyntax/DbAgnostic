using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class ReadingSqlServerTest
{
	[Test]
	public void ShouldParseDatabaseName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

		connectionString.DatabaseName().ShouldBe("db");
	}
	
	[Test]
	public void ShouldParseDatabaseNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		connectionString.DatabaseName().ShouldBe(null);
	}
	
	[Test]
	public void ShouldParseServerName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "server"}.ToString();

		connectionString.ServerName().ShouldBe("server");
	}

	[Test]
	public void ShouldParseServerNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {UserID = "user"}.ToString();

		connectionString.ServerName().ShouldBe(null);
	}
	
	[Test]
	public void ShouldParseApplicationName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		connectionString.ApplicationName().ShouldBe("app");
	}

	[Test]
	public void ShouldParseApplicationNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		connectionString.ApplicationName().ShouldBe(null);
	}

	[Test]
	public void ShouldParsePassword()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", Password = "pass"}.ToString();

		connectionString.Password().ShouldBe("pass");
	}

	[Test]
	public void ShouldParseUserName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user"}.ToString();

		connectionString.UserName().ShouldBe("user");
	}

	[Test]
	public void ShouldParseIntegratedSecurity()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

		connectionString.IntegratedSecurity().ShouldBe(true);
	}

	[Test]
	public void ShouldParseConnectionTimeout()
	{
		var connectionString = new SqlConnectionStringBuilder {ConnectTimeout = 47}.ToString();

		connectionString.ConnectionTimeout().ShouldBe(47);
	}
}