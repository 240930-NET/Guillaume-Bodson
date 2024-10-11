using P0.APP.Model;

namespace P0.TESTS {
    public class CustomerTest
    {
        [Fact]
        public void TestCustomerCreation()
        {
            //Arrange 
            Customer c = new("username", "password");
            //Act
            string actualUsername = c.Username;
            string actualPassword = c.Password;
            List<Account> accounts = c.Accounts;
            //Assert
            Assert.Equal("username", actualUsername);
            Assert.Equal("password", actualPassword);
            Assert.True(accounts.Count == 0);
        }

        [Fact]
        public void TestAddAccount()
        {
            //Arrange
            Customer c = new("username", "password");
            Account acc = new("owner", "name");
            //Act
            c.AddAccount(acc);
            //Assert
            Assert.Contains(acc, c.Accounts);
        }

        [Fact]
        public void TestRemoveAccount()
        {
            //Arrange
            Customer c = new("username", "password");
            Account acc = new("username", "name");
            c.AddAccount(acc);
            Assert.Contains(acc, c.Accounts);
            //Act
            c.RemoveAccount(acc);
            //Assert
            Assert.DoesNotContain(acc, c.Accounts);
        }

        [Fact]
        public void TestFindAccountByName() {

            //Arrange
            Customer c = new("username", "password");
            Account acc = new("username", "name");
            c.AddAccount(acc);
            Assert.Contains(acc, c.Accounts);
            //Act
            Account? actualAcc = c.GetAccountByName("name");
            Account? nullAcc = c.GetAccountByName("noname");
            //Assert
            Assert.Equal(acc, actualAcc);
            Assert.Null(nullAcc);
        }
    }
}