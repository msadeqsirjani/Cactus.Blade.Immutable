using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Immutable.Test
{
    [TestClass]
    public class SemimutableTests
    {
        [TestMethod]
        public void DefaultValueIsUsedWhenValueIsNotChanged()
        {
            var semimutable = new Semimutable<int>(1);

            Assert.AreEqual(1, semimutable.Value);
        }

        [TestMethod]
        public void CanChangeValueProperty()
        {
            var semimutable = new Semimutable<int>(1) { Value = 2 };


            Assert.AreEqual(2, semimutable.Value);
        }

        [TestMethod]
        public void CanChangeValuePropertyWithTheSetValueMethod()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.SetValue(() => 2);

            Assert.AreEqual(2, semimutable.Value);
        }

        [TestMethod]
        public void ResetChangesValueBackToDefault()
        {
            var semimutable = new Semimutable<int>(1) { Value = 2 };


            semimutable.ResetValue();

            Assert.IsTrue(semimutable.HasDefaultValue);
        }

        [TestMethod]
        public void HasDefaultValueIsTrueWhenValueIsNotChanged()
        {
            var semimutable = new Semimutable<int>(1);

            Assert.IsTrue(semimutable.HasDefaultValue);
        }

        [TestMethod]
        public void HasDefaultValueIsFalseWhenValueIsChanged()
        {
            var semimutable = new Semimutable<int>(1) { Value = 2 };

            Assert.IsFalse(semimutable.HasDefaultValue);
        }

        [TestMethod]
        public void HasDefaultValueIsFalseWhenSetValueIsCalled()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.SetValue(() => 2);

            Assert.IsFalse(semimutable.HasDefaultValue);
        }

        [TestMethod]
        public void HasDefaultValueIsTrueWhenResetValueIsCalled()
        {
            var semimutable = new Semimutable<int>(1) { Value = 2 };

            semimutable.ResetValue();

            Assert.IsTrue(semimutable.HasDefaultValue);
        }

        [TestMethod]
        public void IsLockedIsFalseInitially()
        {
            var semimutable = new Semimutable<int>(1);

            Assert.IsFalse(semimutable.IsLocked);
        }

        [TestMethod]
        public void IsLockedIsTrueAfterTheValuePropertyIsAccessed()
        {
            var semimutable = new Semimutable<int>(1);

            var value = semimutable.Value;

            Assert.IsTrue(semimutable.IsLocked);
        }

        [TestMethod]
        public void IsLockedIsTrueAfterLockValueIsCalled()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.LockValue();

            Assert.IsTrue(semimutable.IsLocked);
        }

        [TestMethod]
        public void SettingTheValuePropertyThrowsWhenIsLockedIsTrue()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.LockValue();

            Assert.ThrowsException<InvalidOperationException>(() => semimutable.Value = 2);
        }

        [TestMethod]
        public void CallingTheSetValueMethodThrowsWhenIsLockedIsTrue()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.LockValue();

            Assert.ThrowsException<InvalidOperationException>(() => semimutable.SetValue(() => 2));
        }

        [TestMethod]
        public void CallingTheResetValueMethodThrowsWhenIsLockedIsTrue()
        {
            var semimutable = new Semimutable<int>(1);

            semimutable.LockValue();

            Assert.ThrowsException<InvalidOperationException>(() => semimutable.ResetValue());
        }
    }
}
