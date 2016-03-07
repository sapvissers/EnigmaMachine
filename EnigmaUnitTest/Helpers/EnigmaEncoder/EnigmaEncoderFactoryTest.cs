using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnigmaMachine.Helpers.EnigmaEncoder;
using System.Collections.Generic;
using EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I;
using System.Diagnostics;

namespace EnigmaUnitTest.UnitTests.Helpers.EnigmaEncoder
{
	[TestClass]
	public class EnigmaEncoderFactoryTest
	{
		#region NewEnigmaEncoder
		[TestMethod]
		public void NewEnigmaEncoder_InsertEnigmaIWithValideRotorsAndUkwType_ReturnEnigmaI()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";

			string uwkTypeName = "UKW-A";
			UkwType ukwType = new UkwA();

			string[] rotorNames = new string[]{"I","V","IV"};
			Rotor[] rotors = new Rotor[] { new Rotor1(), new Rotor5(), new Rotor4()};

			int[] rotorPositions = new int[] { 5,2,8 }; // E, B, H

			Dictionary<char, char> plugboard = null;
			Dictionary<char, char> testPlugboard = new Dictionary<char, char>();

			IEnigmaEncoder expectedEncoder = new Enigma_I(ukwType, rotors, rotorPositions, testPlugboard);

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);

			// Assert
			bool passedTest = false;

			if (enigmaEncoder.GetUkwType().Identifier == expectedEncoder.GetUkwType().Identifier &&
				enigmaEncoder.GetRotors()[0].Identifier == expectedEncoder.GetRotors()[0].Identifier &&
				enigmaEncoder.GetRotors()[1].Identifier == expectedEncoder.GetRotors()[1].Identifier &&
				enigmaEncoder.GetRotors()[2].Identifier == expectedEncoder.GetRotors()[2].Identifier &&
				enigmaEncoder.GetRotorPositions()[0] == expectedEncoder.GetRotorPositions()[0] &&
				enigmaEncoder.GetRotorPositions()[1] == expectedEncoder.GetRotorPositions()[1] &&
				enigmaEncoder.GetRotorPositions()[2] == expectedEncoder.GetRotorPositions()[2])
			{
				passedTest = true;
			}

			Assert.IsTrue(passedTest);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NewEnigmaEncoder_InsertNonExistingEncoderType_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "does not exist";
			string uwkTypeName = "UKW-A";
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NewEnigmaEncoder_InsertNonExistingUkwType_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = "does not exist";
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NewEnigmaEncoder_InsertNonExistingRotorName_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = "UKW-A";
			string[] rotorNames = new string[] { "does not exist", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void NewEnigmaEncoder_InsertNotMatchingRotorsAndRotorPositions_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = "UKW-A";
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2 }; // E, B
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void NewEnigmaEncoder_InsertNullEncoderType_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = null;
			string uwkTypeName = "UKW-A";
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void NewEnigmaEncoder_InsertNullUkwType_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = null;
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void NewEnigmaEncoder_InsertNullRotorNames_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = "UKW-A";
			string[] rotorNames = null;
			int[] rotorPositions = new int[] { 5, 2, 8 }; // E, B, H
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void NewEnigmaEncoder_InsertNullRotorPositions_ReturnException()
		{
			// Arrange
			string enigmaEncoderType = "Enigma_I";
			string uwkTypeName = "UKW-A";
			string[] rotorNames = new string[] { "I", "V", "IV" };
			int[] rotorPositions = null;
			Dictionary<char, char> plugboard = null;

			// Act
			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(enigmaEncoderType, uwkTypeName, rotorNames, rotorPositions, plugboard);
		}
		#endregion
	}
}
