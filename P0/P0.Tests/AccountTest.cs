using P0.APP.Model;

namespace P0.Tests{
    public class AccountTest
    {
        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 100)]
        public void TestAccountCreation(bool testBalance, int expectedBalance)
        {
            Account acc;
            //Arrange 
            if(!testBalance) acc = new("owner", "name");
            else acc = new("owner", "name", 100);
            //Act
            string actualOwner = acc.Owner;
            string actualName = acc.Name;
            double actualBal = acc.Balance;
            //Assert
            Assert.Equal("owner", actualOwner);
            Assert.Equal("name", actualName);
            Assert.Equal(expectedBalance, actualBal);
        }

        [Fact]
        public void TestNameChange()
        {
            //Arrange 
            Account acc = new("owner", "name");
            Assert.Equal("name", acc.Name);
            //Act
            acc.Name = "checking";
            //Assert
            Assert.Equal("checking", acc.Name);
        }

        [Theory]
        [InlineData(50, 50, true)]
        [InlineData(-10, 0, false)]
        public void TestDeposit(int depositAmount, int expectedBalance, bool expectedResult)
        {
            //Arrange
            Account acc = new("owner", "name");
            //Act
            Assert.Equal(0, acc.Balance);
            bool actualResult = acc.Deposit(depositAmount);
            //Assert
            Assert.Equal(expectedBalance, acc.Balance);
            Assert.Equal(expectedResult, actualResult);
            
        }

        [Theory]
        [InlineData(50, 50, true)]
        [InlineData(-10, 100, false)]
        [InlineData(150, 100, false)]
        public void TestWithdraw(double withdrawAmount, double expectedRemainingBal, bool expectedResult)
        {
            //Arrange
            Account acc = new("owner", "name");
            acc.Deposit(100);
            //Act
            Assert.Equal(100, acc.Balance);
            bool actualResult = acc.Withdraw(withdrawAmount);
            //Assert
            Assert.Equal(expectedRemainingBal, acc.Balance);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}