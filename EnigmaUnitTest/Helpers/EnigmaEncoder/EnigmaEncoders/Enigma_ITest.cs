using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EnigmaMachine.Helpers.EnigmaEncoder;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I;

namespace EnigmaUnitTest.UnitTests.Helpers.EnigmaEncoder.EnigmaEncoders
{
    [TestClass]
    public class Enigma_ITest
    {
        #region Encode
        [TestMethod]
        public void Encode_InsertNormalString_ReturnEncodedString()
        {
            // Arrange
            string input = "aaa";

            ///  |R-1|R-2|R-3|U-A|R-3|R-2|R-1|
            ///1 A---K---L---V---I---Q---Q---G
            ///2 A---M---W---U---P---H---L---C
            ///3 A---F---I---R---N---N---T---I
            string expectedOutput = "GCI";

            UkwType ukwType = new UkwA();
            Rotor[] rotors = new Rotor[] { new Rotor1(), new Rotor2(), new Rotor3() };
            int[] rotorPositions = new int[] { 1, 1, 1 }; // A, A, A

            for (int i = 0, amountOfRotors = rotors.Length; i < amountOfRotors; i++)
            {
                rotors[i].SetPosition(rotorPositions[i]);
            }

            Dictionary<char, char> plugboard = new Dictionary<char, char>();
            IEnigmaEncoder encoder = new Enigma_I(ukwType, rotors, rotorPositions, plugboard);

            // Act
            string output = encoder.Encode(input);

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void Encode_InsertEncodedString_ReturnDecodedString()
        {
            // Arrange
            string input = "gci";

            ///  |R-1|R-2|R-3|U-A|R-3|R-2|R-1|
            ///1 A---K---L---V---I---Q---Q---G
            ///2 A---M---W---U---P---H---L---C
            ///3 A---F---I---R---N---N---T---I
            string expectedOutput = "AAA";

            UkwType ukwType = new UkwA();
            Rotor[] rotors = new Rotor[] { new Rotor1(), new Rotor2(), new Rotor3() };
            int[] rotorPositions = new int[] { 1, 1, 1 }; // A, A, A

            for (int i = 0, amountOfRotors = rotors.Length; i < amountOfRotors; i++)
            {
                rotors[i].SetPosition(rotorPositions[i]);
            }

            Dictionary<char, char> plugboard = new Dictionary<char, char>();
            IEnigmaEncoder encoder = new Enigma_I(ukwType, rotors, rotorPositions, plugboard);

            // Act
            string output = encoder.Encode(input);

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void Encode_EncodeMessageWithPlugboardSetup_ReturnEncodedString()
        {
            // Arrange
            string input = "aaa";

            ///  |PLU|R-1|R-2|R-3|U-A|R-3|R-2|R-1|PLU|
            ///1 A---D---L---H---P---U---W---M---B---B
            ///2 A---D---G---R---W---K---U---H---N---L
            ///3 A---D---D---K---X---H---D---C---V---V
            string expectedOutput = "BLV";

            UkwType ukwType = new UkwA();
            Rotor[] rotors = new Rotor[] { new Rotor1(), new Rotor2(), new Rotor3() };
            int[] rotorPositions = new int[] { 1, 1, 1 }; // A, A, A

            for (int i = 0, amountOfRotors = rotors.Length; i < amountOfRotors; i++)
            {
                rotors[i].SetPosition(rotorPositions[i]);
            }

            Dictionary<char, char> plugboard = new Dictionary<char, char>();
            plugboard['A'] = 'D';
            plugboard['D'] = 'A';
            plugboard['N'] = 'L';
            plugboard['L'] = 'N';

            IEnigmaEncoder encoder = new Enigma_I(ukwType, rotors, rotorPositions, plugboard);

            // Act
            string output = encoder.Encode(input);

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void Encode_DecodeMessageWithPlugboardSetup_ReturnDecodedString()
        {
            // Arrange
            string input = "BLV";

            ///  |PLU|R-1|R-2|R-3|U-A|R-3|R-2|R-1|PLU|
            ///1 A---D---L---H---P---U---W---M---B---B
            ///2 A---D---G---R---W---K---U---H---N---L
            ///3 A---D---D---K---X---H---D---C---V---V
            string expectedOutput = "AAA";

            UkwType ukwType = new UkwA();
            Rotor[] rotors = new Rotor[] { new Rotor1(), new Rotor2(), new Rotor3() };
            int[] rotorPositions = new int[] { 1, 1, 1 }; // A, A, A

            for (int i = 0, amountOfRotors = rotors.Length; i < amountOfRotors; i++)
            {
                rotors[i].SetPosition(rotorPositions[i]);
            }

            Dictionary<char, char> plugboard = new Dictionary<char, char>();
            plugboard['A'] = 'D';
            plugboard['D'] = 'A';
            plugboard['N'] = 'L';
            plugboard['L'] = 'N';

            IEnigmaEncoder encoder = new Enigma_I(ukwType, rotors, rotorPositions, plugboard);

            // Act
            string output = encoder.Encode(input);

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }
        #endregion
    }
}
