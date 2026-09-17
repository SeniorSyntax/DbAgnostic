using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class SelectorPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var result = selector.PickDialect("sql server", "postgres");

		result.ShouldBe("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = new object();
		var postgres = new object();
		var result = selector.PickDialect(sqlserver, postgres);

		result.ShouldBeSameAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		var result = selector.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.ShouldBe(true);
		postgres.ShouldBe(true);
		sqlserver.ShouldBe(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		selector.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		postgres.ShouldBe(true);
		sqlserver.ShouldBe(false);
	}
}
