using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
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

		string encode(string input);
	}
}
