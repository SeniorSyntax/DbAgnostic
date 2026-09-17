using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class SelectorConnectionStringSqlServerTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var result = connectionString.PickDialect("sql server", "postgres");

		result.ShouldBe("sql server");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var sqlserver = new object();
		var postgres = new object();
		var result = connectionString.PickDialect(sqlserver, postgres);

		result.ShouldBeSameAs(sqlserver);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		var result = connectionString.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.ShouldBe(true);
		sqlserver.ShouldBe(true);
		postgres.ShouldBe(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		connectionString.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.ShouldBe(true);
		postgres.ShouldBe(false);
	}
}
