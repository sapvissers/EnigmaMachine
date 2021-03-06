﻿using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EnigmaMachine.Helpers.EnigmaEncoder
{
    /// <summary>
    /// Basic interface for all EnigmaEncoders
    /// </summary>
    public interface IEnigmaEncoder
    {
        ObservableCollection<string> GetAvailableUkwTypes();
        ObservableCollection<string> GetAvailableRotors();
        UkwType GetUkwType();
        Rotor[] GetRotors();
        int[] GetRotorPositions();
        Dictionary<char, char> GetPlugboard();

        string Encode(string input);
    }
}
