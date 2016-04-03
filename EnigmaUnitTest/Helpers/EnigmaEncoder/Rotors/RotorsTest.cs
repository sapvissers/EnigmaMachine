using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;

namespace EnigmaUnitTest.UnitTests.Helpers.EnigmaEncoder.Rotors
{
    [TestClass]
    public class RotorsTest
    {
        #region SetPosition
        [TestMethod]
        public void SetPosition_InsertCharacterPosition_CheckPositionProperty()
        {
            // Arrange
            char character = 'd';
            RotorTestClass RotorTestClass = new RotorTestClass();

            // Act
            RotorTestClass.SetPosition(character);

            // Assert
            Assert.AreEqual(4, RotorTestClass.GetPosition());
        }
        #endregion

        #region Rotate
        [TestMethod]
        public void Rotate_InsertStartingPosition_ExpectingNoNotchAndPositionIs5()
        {
            // Arrange
            char character = 'd';
            RotorTestClass RotorTestClass = new RotorTestClass();
            RotorTestClass.SetPosition(character);

            // Act
            bool notch = RotorTestClass.Rotate();

            // Assert
            Assert.IsFalse(notch);
            Assert.AreEqual(5, RotorTestClass.GetPosition());
        }

        [TestMethod]
        public void Rotate_InsertStartingPosition_ExpectingNotchAndPositionIs25()
        {
            // Arrange
            char character = 'x';
            RotorTestClass RotorTestClass = new RotorTestClass();
            RotorTestClass.SetPosition(character);

            // Act
            bool notch = RotorTestClass.Rotate();

            // Assert
            Assert.IsTrue(notch);
            Assert.AreEqual(25, RotorTestClass.GetPosition());
        }

        [TestMethod]
        public void Rotate_InsertStartingPositionAndRotateTwice_ExpectingCharacterAToBecomeM()
        {
            // Arrange
            char character = 'A';
            RotorTestClass RotorTestClass = new RotorTestClass();
            RotorTestClass.SetPosition(character);

            // Act
            RotorTestClass.Rotate();
            RotorTestClass.Rotate();

            // Assert
            /// START  A---E
            /// 1 TURN A---K
            /// 2 TURN A---M

            Assert.AreEqual('M', RotorTestClass.RotatedWiring[character]);
        }
        #endregion
    }
}
