using LegacyApp;

namespace LegacyAppTest;

public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        var userService = new UserService();
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoewithoutproperemail";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);
        int clientId = 1;

        //Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.False(result, "User should not be added with an invalid email.");
    }

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Or_LastName_Is_NullOrEmpty()
    {
        //Arrange
        var userService = new UserService();
        string firstName = ""; // Empty first name
        string lastName = "Doe";
        string email = "john@doe.com";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);
        int clientId = 1;

        //Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.False(result, "User should not be added with an empty first name.");
    }

    [Fact]
    public void AddUser_Should_Return_False_For_Underage_User()
    {
        //Arrange
        var userService = new UserService();
        string firstName = "John";
        string lastName = "Doe";
        string email = "john@doe.com";
        DateTime dateOfBirth = DateTime.Now.AddYears(-20); // Age is less than 21
        int clientId = 1;

        //Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.False(result, "User should not be added if under 21 years old.");
    }

    [Fact]
    public void AddUser_Should_Return_False_When_User_Is_Just_Under_21()
    {
        //Arrange
        var userService = new UserService();
        string firstName = "Test";
        string lastName = "User";
        string email = "testuser@example.com";

        DateTime dateOfBirth = DateTime.Now.AddYears(-21).AddDays(1);
        int clientId = 1;

        //Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.False(result, "User should not be added if they are under 21 years of age.");
    }
    
    [Fact]
    public void AddUser_Should_Not_Check_CreditLimit_For_VeryImportantClient()
    {
        // Arrange
        var userService = new UserService();
        string firstName = "FirstName";
        string lastName = "Malewski";
        string email = "malewski@gmail.pl";
        DateTime dateOfBirth = new DateTime(1980, 1, 1);
        int clientId = 2;

        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.True(result, "VeryImportantClient should be added without credit limit check.");
    }
    
    [Fact]
    public void AddUser_Should_Add_NormalClient_With_Sufficient_CreditLimit()
    {
        // Arrange
        var userService = new UserService();
        string firstName = "FirstName";
        string lastName = "Kwiatkowski";
        string email = "kwiatkowski@wp.pl";
        DateTime dateOfBirth = new DateTime(1980, 1, 1);
        int clientId = 5;

        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.True(result, "NormalClient with sufficient credit limit should be added.");
    }
    
    [Fact]
    public void AddUser_Should_Add_VeryImportantClient_Without_CreditLimit_Check()
    {
        // Arrange
        var userService = new UserService();
        string firstName = "FirstName";
        string lastName = "Malewski";
        string email = "malewski@gmail.pl";
        DateTime dateOfBirth = new DateTime(1980, 1, 1);
        int clientId = 2;

        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.True(result, "VeryImportantClient should be added without credit limit check.");
    }

    [Fact]
    public void AddUser_Should_Consider_Doubled_CreditLimit_For_ImportantClient()
    {
        // Arrange
        var userService = new UserService();
        string firstName = "FirstName";
        string lastName = "Smith";
        string email = "smith@gmail.pl";
        DateTime dateOfBirth = new DateTime(1980, 1, 1);
        int clientId = 3;

        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.True(result, "ImportantClient with a sufficient doubled credit limit should be added.");
    }
    
    [Fact]
    public void AddUser_Should_Reject_NormalClient_With_Insufficient_Original_CreditLimit()
    {
        // Arrange
        var userService = new UserService();
        string firstName = "FirstName";
        string lastName = "Kwiatkowski";
        string email = "kwiatkowski@wp.pl";
        DateTime dateOfBirth = new DateTime(1980, 1, 1);
        int clientId = 5;

        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.True(result, "NormalClient with a sufficient credit limit should be added.");
    }


}