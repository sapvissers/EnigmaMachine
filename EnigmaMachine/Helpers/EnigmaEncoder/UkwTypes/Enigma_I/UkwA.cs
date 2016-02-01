using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes.Enigma_I
{
	public class UkwA : UkwType
	{
		public UkwA()
		{
			Identifier = "UKW-A";

			wiring.Add('A', 'E');
			wiring.Add('B', 'J');
			wiring.Add('C', 'M');
			wiring.Add('D', 'Z');
			wiring.Add('E', 'A');
			wiring.Add('F', 'L');
			wiring.Add('G', 'Y');
			wiring.Add('H', 'X');
			wiring.Add('I', 'V');
			wiring.Add('J', 'B');
			wiring.Add('K', 'W');
			wiring.Add('L', 'F');
			wiring.Add('M', 'C');
			wiring.Add('N', 'R');
			wiring.Add('O', 'Q');
			wiring.Add('P', 'U');
			wiring.Add('Q', 'O');
			wiring.Add('R', 'N');
			wiring.Add('S', 'T');
			wiring.Add('T', 'S');
			wiring.Add('U', 'P');
			wiring.Add('V', 'I');
			wiring.Add('W', 'K');
			wiring.Add('X', 'H');
			wiring.Add('Y', 'G');
			wiring.Add('Z', 'D');
		}
	}
}
