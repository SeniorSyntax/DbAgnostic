using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class SelectorDbConnectionSqlServerTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var result = connection.PickDialect("sql server", "postgres");

		result.ShouldBe("sql server");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = new object();
		var postgres = new object();
		var result = connection.PickDialect(sqlserver, postgres);

		result.ShouldBeSameAs(sqlserver);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		var result = connection.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.ShouldBe(true);
		sqlserver.ShouldBe(true);
		postgres.ShouldBe(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		connection.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.ShouldBe(true);
		postgres.ShouldBe(false);
	}
}
