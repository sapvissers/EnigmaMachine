using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;

namespace EnigmaUnitTest.UnitTests.Helpers.EnigmaEncoder.Rotors
{
    public class RotorTestClass : Rotor
    {
        public RotorTestClass()
        {
            Identifier = "TESTER";

            wiring.Add('A', 'E');
            wiring.Add('B', 'K');
            wiring.Add('C', 'M');
            wiring.Add('D', 'F');
            wiring.Add('E', 'L');
            wiring.Add('F', 'G');
            wiring.Add('G', 'D');
            wiring.Add('H', 'Q');
            wiring.Add('I', 'V');
            wiring.Add('J', 'Z');
            wiring.Add('K', 'N');
            wiring.Add('L', 'T');
            wiring.Add('M', 'O');
            wiring.Add('N', 'W');
            wiring.Add('O', 'Y');
            wiring.Add('P', 'H');
            wiring.Add('Q', 'X');
            wiring.Add('R', 'U');
            wiring.Add('S', 'S');
            wiring.Add('T', 'P');
            wiring.Add('U', 'A');
            wiring.Add('V', 'I');
            wiring.Add('W', 'B');
            wiring.Add('X', 'R');
            wiring.Add('Y', 'C');
            wiring.Add('Z', 'J');

            notch = 'Y';
        }
    }
}
