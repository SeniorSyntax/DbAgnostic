using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class SelectorDbConnectionPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var result = connection.PickDialect("sql server", "postgres");

		result.ShouldBe("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = new object();
		var postgres = new object();
		var result = connection.PickDialect(sqlserver, postgres);

		result.ShouldBeSameAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		var result = connection.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.ShouldBe(true);
		sqlserver.ShouldBe(false);
		postgres.ShouldBe(true);
	}

	[Test]
	public void ShouldExecute()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		connection.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.ShouldBe(false);
		postgres.ShouldBe(true);
	}
}
