using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}

        [TestMethod()]
        public void TestThatCarIsParkedInRightSpot()
        {
        IDatabase mockDB = mocks.StrictMock<IDatabase>();
        String carLocation = "SRC Lot";
        String anotherCarLocation = "Home";
        Expect.Call(mockDB.getCarLocation(5)).Return(carLocation);
        Expect.Call(mockDB.getCarLocation(1025)).Return(anotherCarLocation);
        mocks.ReplayAll();
        Car target = ObjectMother.BMW();        target.Database = mockDB;
        String result;
        result = target.getCarLocation(1025);
        Assert.AreEqual(anotherCarLocation, result);
        result = target.getCarLocation(5);
        Assert.AreEqual(carLocation, result);
        mocks.VerifyAll();
        }

		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
	}
}
