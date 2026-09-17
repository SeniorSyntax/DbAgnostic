using System.Data.SqlClient;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsSqlServerTest
{
    [Test]
    public void ShouldSetUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo"}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials("user", "pass"));

        result.UserID.ShouldBe("user");
        result.Password.ShouldBe("pass");
    }

    [Test]
    public void ShouldTurnOffIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials("user", "pass"));

        result.IntegratedSecurity.ShouldBeFalse();
    }

    [Test]
    public void ShouldClearIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials("user", "pass");

        result.ShouldNotContain("Integrated Security", Case.Sensitive);
    }
	
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(null, null);

        result.ShouldNotContain("User ID", Case.Sensitive);
        result.ShouldNotContain("Password", Case.Sensitive);
    }
    
    [Test]
    public void ShouldRemoveCredentials()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().ShouldBeFalse();
        result.UserName().ShouldBeNull();
        result.Password().ShouldBeNull();
    }
    
    [Test]
    public void ShouldRemoveCredentials2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().ShouldBeFalse();
        result.UserName().ShouldBeNull();
        result.Password().ShouldBeNull();
    }
}