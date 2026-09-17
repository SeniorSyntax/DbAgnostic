using System.Data.SqlClient;
using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class SetIntegratedSecurityPostgresTest
{
    [Test]
    public void ShouldNotSetIntegratedSecurityAsItIsNotSupported()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

        var result = connectionString.SetIntegratedSecurity();

        result.IntegratedSecurity().ShouldBeFalse();
    }
    
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "user", Password = "pass"}.ToString();
    
        var result = connectionString.SetIntegratedSecurity();
    
        result.ShouldNotContain("Password", Case.Sensitive);
        result.ShouldNotContain("User Id", Case.Sensitive);
    }
}