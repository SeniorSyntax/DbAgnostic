using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class SelectorConnectionStringPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var result = connectionString.PickDialect("sql server", "postgres");

		result.ShouldBe("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = new object();
		var postgres = new object();
		var result = connectionString.PickDialect(sqlserver, postgres);

		result.ShouldBeSameAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		var result = connectionString.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.ShouldBe(true);
		sqlserver.ShouldBe(false);
		postgres.ShouldBe(true);
	}

	[Test]
	public void ShouldExecute()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		connectionString.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.ShouldBe(false);
		postgres.ShouldBe(true);
	}
}
