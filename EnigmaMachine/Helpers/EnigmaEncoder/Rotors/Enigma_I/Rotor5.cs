namespace EnigmaMachine.Helpers.EnigmaEncoder.Rotors.Enigma_I
{
    public class Rotor5 : Rotor
    {
        public Rotor5()
        {
            Identifier = "V";

            wiring.Add('A', 'V');
            wiring.Add('B', 'Z');
            wiring.Add('C', 'B');
            wiring.Add('D', 'R');
            wiring.Add('E', 'G');
            wiring.Add('F', 'I');
            wiring.Add('G', 'T');
            wiring.Add('H', 'Y');
            wiring.Add('I', 'U');
            wiring.Add('J', 'P');
            wiring.Add('K', 'S');
            wiring.Add('L', 'D');
            wiring.Add('M', 'N');
            wiring.Add('N', 'H');
            wiring.Add('O', 'L');
            wiring.Add('P', 'X');
            wiring.Add('Q', 'A');
            wiring.Add('R', 'W');
            wiring.Add('S', 'M');
            wiring.Add('T', 'J');
            wiring.Add('U', 'Q');
            wiring.Add('V', 'O');
            wiring.Add('W', 'F');
            wiring.Add('X', 'E');
            wiring.Add('Y', 'C');
            wiring.Add('Z', 'K');

            notch = 'H';
        }
    }
}
