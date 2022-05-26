using Microsoft.VisualStudio.TestTools.UnitTesting;
using V_Speeds;
using V_Speeds.Model;
using V_Speeds.Model.Aircrafts;
using V_Speeds.ObserverPattern;

namespace V_Speeds_Tester
{
    [TestClass]
    public class ObserverTester : IMyObserver<V_Calculator>, IMyObserver<Aircraft>, IMyObserver<Airfield>, IMyObserver<Atmosphere>
    {
        private readonly V_Calculator vcalc= new();
        private readonly Aircraft ac = new();
        private readonly AircraftAB acab = new();
        private readonly Airfield field = new();
        private readonly Atmosphere atmos = new();
        private TestCase currentCase = TestCase.V_Calculator;
        private double lastRandom;
        
        private enum TestCase
        {
            V_Calculator = 0,
            Aircraft,
            AircraftAB,
            Airfield,
            Atmosphere,
        }

        private void RunCase<T>(IMyObservable<T> subject)
        {
            if (this is IMyObserver<T> t)
            {
                subject.Subscribe(t);
                foreach (var prop in subject.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(double)) // only check properties of type double
                    {
                        lastRandom = System.Random.Shared.NextDouble();
                        prop.SetValue(subject, lastRandom);
                    }
                }
            }
            else Assert.Fail("Something got seriously FUBAR...");
        }

        [TestMethod]
        public void Test_V_Calculator()
        {
            currentCase = TestCase.V_Calculator;
            RunCase(vcalc);
        }

        [TestMethod]
        public void Test_Aircraft()
        {
            currentCase = TestCase.Aircraft;
            RunCase(ac);
        }

        [TestMethod]
        public void Test_AircraftAB()
        {
            currentCase = TestCase.AircraftAB;
            RunCase(acab);

            acab.AB = true; // set afterburner, change ThrAB and RcAB => check if changes are present in Thr and Rc
            lastRandom = System.Random.Shared.NextDouble();
            acab.ThrAB = lastRandom;
            lastRandom = System.Random.Shared.NextDouble();
            acab.RcAB = lastRandom;
        }

        [TestMethod]
        public void Test_Airfield()
        {
            currentCase = TestCase.Airfield;
            RunCase(field);
        }

        [TestMethod]
        public void Test_Atmosphere()
        {
            currentCase = TestCase.Atmosphere;
            RunCase(atmos);
        }

        public void Update(string property)
        {
            // check if property changed to what we expected
            // "Dereference of possibly null reference" warnings are ok, they should not be null, but if they are => FAIL!!!
            switch(currentCase)
            {
                case TestCase.V_Calculator:
                    Assert.AreEqual(vcalc[property], lastRandom);
                    break;
                case TestCase.Aircraft:
                    Assert.AreEqual(ac.GetType().GetProperty(property).GetValue(ac), lastRandom); 
                    break;
                case TestCase.AircraftAB:
                    if (acab.AB && property == "Thr")
                        Assert.AreEqual(acab.GetType().GetProperty(property).GetValue(acab), acab.ThrAB);
                    else if(acab.AB && property == "Rc")
                        Assert.AreEqual(acab.GetType().GetProperty(property).GetValue(acab), acab.RcAB);
                    else
                        Assert.AreEqual(acab.GetType().GetProperty(property).GetValue(acab), lastRandom);
                    break;
                case TestCase.Airfield:
                    Assert.AreEqual(field.GetType().GetProperty(property).GetValue(field), lastRandom);
                    break;
                case TestCase.Atmosphere:
                    Assert.AreEqual(atmos.GetType().GetProperty(property).GetValue(atmos), lastRandom);
                    break;
                default:
                    Assert.Fail("Invalid 'currentCase' in Update!");
                    break;
            }
        }

        // Methods below just check if we're receiving the expected object from the subjects...
        public void Update(V_Calculator value)
        {
            Assert.ReferenceEquals(vcalc, value);
        }

        public void Update(Aircraft value)
        {
            bool equalObject = ac.Equals(value) || acab.Equals(value); // mind AircraftAB...
            Assert.IsTrue(equalObject);
        }

        public void Update(Airfield value)
        {
            Assert.ReferenceEquals(field, value);
        }

        public void Update(Atmosphere value)
        {
            Assert.ReferenceEquals(atmos, value);
        }
    }
}
