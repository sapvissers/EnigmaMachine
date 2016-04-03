using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachine.Helpers;
using System.Collections.Generic;

namespace EnigmaUnitTest
{
    [TestClass]
    public class CharNumberConverterTest
    {
        #region CharacterToNumber
        [TestMethod]
        public void CharacterToNumber_InsertCharacter_ReturnNumber()
        {
            // Arrange
            char character = 'a';

            // Act
            int result = CharacterConverter.CharacterToNumber(character);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CharacterToNumber_InsertNonAlphabeticCharacter_ReturnException()
        {
            // Arrange
            char character = '#';

            // Act
            int result = CharacterConverter.CharacterToNumber(character);
        }
        #endregion

        #region NumberToCharacter
        [TestMethod]
        public void NumberToCharacter_InsertNumber_ReturnCharacter()
        {
            // Arrange
            int number = 1;

            // Act
            char result = CharacterConverter.NumberToCharacter(number);

            // Assert
            Assert.AreEqual('A', result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberToCharacter_InsertNegativeNumber_ReturnException()
        {
            // Arrange
            int number = -3;

            // Act
            char result = CharacterConverter.NumberToCharacter(number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NumberToCharacter_InsertBiggerNumber_ReturnException()
        {
            // Arrange
            int number = 27;

            // Act
            char result = CharacterConverter.NumberToCharacter(number);
        }
        #endregion

        #region MutateForward
        [TestMethod]
        public void MutateForward_InsertCharAndWiring_ReturnMutatedCharacter()
        {
            // Arrange
            Dictionary<char, char> wiring = new Dictionary<char, char>();
            wiring.Add('X', 'B');
            wiring.Add('B', 'K');

            char character = 'b';

            // Act
            char result = CharacterConverter.MutateForward(character, wiring);

            // Assert
            Assert.AreEqual('K', result);
        }

        [TestMethod]
        public void MutateForward_InsertWiringThatDoesNotContainCharacter_ReturnUnmutatedCharacter()
        {
            // Arrange
            Dictionary<char, char> wiring = new Dictionary<char, char>();
            wiring.Add('X', 'B');
            wiring.Add('A', 'C');

            char character = 'b';

            // Act
            char result = CharacterConverter.MutateForward(character, wiring);

            // Assert
            Assert.AreEqual('B', result);
        }
        #endregion

        #region MutateBackward
        [TestMethod]
        public void MutateBackward_InsertCharAndWiring_ReturnMutatedCharacter()
        {
            // Arrange
            Dictionary<char, char> wiring = new Dictionary<char, char>();
            wiring.Add('X', 'B');
            wiring.Add('B', 'K');

            char character = 'b';

            // Act
            char result = CharacterConverter.MutateBackward(character, wiring);

            // Assert
            Assert.AreEqual('X', result);
        }

        [TestMethod]
        public void MutateBackward_InsertWiringThatDoesNotContainCharacter_ReturnUnmutatedCharacter()
        {
            // Arrange
            Dictionary<char, char> wiring = new Dictionary<char, char>();
            wiring.Add('X', 'A');
            wiring.Add('B', 'C');

            char character = 'b';

            // Act
            char result = CharacterConverter.MutateBackward(character, wiring);

            // Assert
            Assert.AreEqual('B', result);
        }
        #endregion
    }
}
