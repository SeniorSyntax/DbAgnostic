using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class SetIntegratedSecuritySqlServerTest
{
    [Test]
    public void ShouldSetIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

        var result = connectionString.SetIntegratedSecurity();

        result.IntegratedSecurity().ShouldBeTrue();
    }
    
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
         var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user", Password = "pass"}.ToString();
    
         var result = connectionString.SetIntegratedSecurity();
    
         result.ShouldNotContain("Password", Case.Sensitive);
         result.ShouldNotContain("User Id", Case.Sensitive);
    }
}