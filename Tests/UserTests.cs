namespace Tests;

using Models;

public class UserTests
{
    [Fact]
    public void UserCreated()
    {
        User test = new User("test", "test", true);
        Assert.NotNull(test);
    }

    [Theory]
    [InlineData("test:tes2t")]
    [InlineData("tes3t:test")]
    [InlineData("test:3test")]
    [InlineData("tesst:test")]
    [InlineData("test:test")]
    [InlineData("test:t$$est")]
    [InlineData("tes000t:test")]
    [InlineData("test:taest")]
    [InlineData("tesat:test")]
    public void checkCredentialsWork(string input)
    {
        string[] up = input.Split(':');
        User temp = new User(up[0], up[1], true);
        Assert.True(temp.checkCredentials(up[0], up[1]));
        Assert.False(temp.checkCredentials("not uname", "not password"));
    }
}