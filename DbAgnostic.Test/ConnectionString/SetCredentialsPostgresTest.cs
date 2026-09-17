using Npgsql;
using NUnit.Framework;
using Shouldly;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsPostgresTest
{
    [Test]
    public void ShouldSetUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo"}.ToString();

        var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials("user", "pass"));

        result.Username.ShouldBe("user");
        result.Password.ShouldBe("pass");
    }

    [Test]
    public void ShouldNotSetIntegratedSecurity()
    {
        var connectionString = "Host=foo";

        var result = connectionString.SetCredentials("user", "pass");

        result.IntegratedSecurity().ShouldBeFalse();
        result.UserName().ShouldBe("user");
        result.Password().ShouldBe("pass");
        result.ShouldNotContain("Integrated Security", Case.Sensitive);
    }
	
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", Username = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(null, null);

        result.ShouldNotContain("UserName", Case.Sensitive);
        result.ShouldNotContain("Password", Case.Sensitive);
    }
    
    [Test]
    public void ShouldRemoveCredentials()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "u", Password = "p"}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().ShouldBeFalse();
        result.UserName().ShouldBeNull();
        result.Password().ShouldBeNull();
    }
    
    [Test]
    public void ShouldRemoveCredentials2()
    {
        var connectionString = "Host=foo;Integrated Security=true";

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().ShouldBeFalse();
        result.UserName().ShouldBeNull();
        result.Password().ShouldBeNull();
    }
}