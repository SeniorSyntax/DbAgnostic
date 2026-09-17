using System.Data.SqlClient;
using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test;

public class CreateConnectionTest
{
	[Test]
	public void ShouldCreateSqlConnection()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var result = connectionString.CreateConnection();

		result.GetType().Name.ShouldBe("SqlConnection");
	}

	[Test]
	public void ShouldCreateNpgsqlConnection()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var result = connectionString.CreateConnection();

		result.ShouldBeOfType<NpgsqlConnection>();
	}
}
