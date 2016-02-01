using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I
{
	public class Rotor3 : Rotor
	{
		public Rotor3()
		{
			Identifier = "III";

			wiring.Add('A', 'B');
			wiring.Add('B', 'D');
			wiring.Add('C', 'F');
			wiring.Add('D', 'H');
			wiring.Add('E', 'J');
			wiring.Add('F', 'L');
			wiring.Add('G', 'C');
			wiring.Add('H', 'P');
			wiring.Add('I', 'R');
			wiring.Add('J', 'T');
			wiring.Add('K', 'X');
			wiring.Add('L', 'V');
			wiring.Add('M', 'Z');
			wiring.Add('N', 'N');
			wiring.Add('O', 'Y');
			wiring.Add('P', 'E');
			wiring.Add('Q', 'I');
			wiring.Add('R', 'W');
			wiring.Add('S', 'G');
			wiring.Add('T', 'A');
			wiring.Add('U', 'K');
			wiring.Add('V', 'M');
			wiring.Add('W', 'U');
			wiring.Add('X', 'S');
			wiring.Add('Y', 'Q');
			wiring.Add('Z', 'O');

			notch = 'D';
		}
	}
}
