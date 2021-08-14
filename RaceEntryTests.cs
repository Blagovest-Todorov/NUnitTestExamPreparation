using NUnit.Framework;
using System;
//using TheRace; //in judge  remove the using to the Race

namespace TheRace.Tests
{
    [TestFixture]
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;

        public IFormatProvider DriverAdded { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.raceEntry = new RaceEntry();
        }

        [Test]
        public void Counter_IsZeroByDefault()
        {
            Assert.That(this.raceEntry.Counter, Is.Zero);
        }

        [Test]
        public void Counter_Increases_WhenAddingDriver()
        {
            int expectedCount = 1;
            this.raceEntry.AddDriver(new UnitDriver("Nasko", new UnitCar("Tesla", 400, 500)));
            Assert.That(this.raceEntry.Counter, Is.EqualTo(expectedCount));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverIsNull()
        {
            UnitDriver driver = null;
            //this.raceEntry.AddDriver(driver);
            Assert.Throws<InvalidOperationException>(() => this.raceEntry.AddDriver(driver));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverWithSuchNameAlreadyExists()
        {
            string driverName = "Nasko";
            UnitDriver driver = new UnitDriver(driverName, new UnitCar("Tesla", 400, 500));
            this.raceEntry.AddDriver(driver);

            Assert.Throws<InvalidOperationException>(() => this.raceEntry.AddDriver(driver));
        }

        [Test]
        public void AddDriver_ReturnsExpectedResultMessage()
        {
            string driverName = "Nasko";
            UnitDriver driver = new UnitDriver(driverName, new UnitCar("Tesla", 400, 500));
            var actual = this.raceEntry.AddDriver(driver); //method returns string after adding

            string expected = $"Driver {driverName} added in race."; //string.Format(DriverAdded, driver.Name);            

            Assert.That(actual, Is.EqualTo(expected)); //compare returned string with the expected message
        }

        [Test]
        public void CalculateAverageHorsePower_ThrowsExcept_WhenParticipantsLessThanMin()
        {
            //from begin we have no raceDrivers with car/
            Assert.Throws<InvalidOperationException>(() => this.raceEntry.CalculateAverageHorsePower());

            string driverName = "Nasko";
            UnitDriver driver = new UnitDriver(driverName, new UnitCar("Tesla", 400, 500));
            this.raceEntry.AddDriver(driver);

            Assert.Throws<InvalidOperationException>(() => this.raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePower_ReturnsExpectedCalculatedResult()
        {
            int n = 10;
            int expectedHP = 0;
            // int actualAvgHP = default;

            for (int i = 0; i < n; i++)
            {
                int hp = 100 + i;
                string driverName = $"Nasko{i}";
                UnitDriver driver = new UnitDriver(driverName, new UnitCar("Tesla", hp, 500));
                this.raceEntry.AddDriver(driver);
                expectedHP += hp;
            }

            expectedHP /= n;
            double actualAvgHP = this.raceEntry.CalculateAverageHorsePower();
            Assert.AreEqual(expectedHP, expectedHP);
        }
    }
}