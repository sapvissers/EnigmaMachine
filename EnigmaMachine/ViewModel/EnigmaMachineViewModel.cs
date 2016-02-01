using EnigmaMachine.Helpers;
using EnigmaMachine.Helpers.EnigmaEncoder;
using EnigmaMachine.Helpers.EnigmaEncoder.Rotors;
using EnigmaMachine.Helpers.EnigmaEncoder.UkwTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnigmaMachine.ViewModel
{
	class EnigmaMachineViewModel : ObservableObject
	{
		private string inputText;
		private string outputText;

		private ObservableCollection<string> availableUkwTypes = new ObservableCollection<string>();
		private ObservableCollection<string> availableRotors = new ObservableCollection<string>();

		private string selectedEnigmaEncoderName = "";
		private string selectedUwkTypeName = "";

		private string[] selectedRotorNames = new string[3];
		private string[] rotorPositions = new string[3];

		private string plugboard = "";

		private ICommand updateCommand;

		public string InputText 
		{
			get
			{
				return inputText;
			}
			set 
			{
				if (inputText != value)
				{
					inputText = value;
					OnPropertyChanged("InputText");
				}
			}
		}
		public string OutputText
		{
			get
			{
				return outputText;
			}
			set
			{
				if (outputText != value)
				{
					outputText = value;
					OnPropertyChanged("OutputText");
				}
			}
		}
		public ObservableCollection<string> AvailableEnigmaEncoders
		{
			get
			{
				ObservableCollection<string> availableEnigmaEncoders = new ObservableCollection<string>();
				availableEnigmaEncoders.Add("Enigma_I");

				return availableEnigmaEncoders;
            }
		}
		public string SelectedEnigmaEncoderName
		{
			get
			{
				return selectedEnigmaEncoderName;
			}
			set
			{
				if (selectedEnigmaEncoderName != value)
				{
					selectedEnigmaEncoderName = value;

					setAvailableRotorsAndUkwTypes(selectedEnigmaEncoderName);

					OnPropertyChanged("SelectedEnigmaEncoderName");
				}
			}
		}

		public ObservableCollection<string> AvailableUkwTypes
		{
			get
			{
				return availableUkwTypes;
			}
			set
			{
				if (availableUkwTypes != value)
				{
					availableUkwTypes = value;
					OnPropertyChanged("AvailableUkwTypes");
				}
			}
		}
		public string SelectedUwkTypeName
		{
			get
			{
				return selectedUwkTypeName;
			}
			set
			{
				if (selectedUwkTypeName != value)
				{
					selectedUwkTypeName = value;
					OnPropertyChanged("SelectedUwkTypeName");
				}
			}
		}

		public ObservableCollection<string> AvailableRotors
		{
			get
			{
				return availableRotors;
			}
			set
			{
				if (availableRotors != value)
				{
					availableRotors = value;
					OnPropertyChanged("AvailableRotors");
				}
			}
		}

		public string SelectedRotorName1
		{
			get
			{
				return selectedRotorNames[0];
			}
			set
			{
				if (selectedRotorNames[0] != value)
				{
					selectedRotorNames[0] = value;
					OnPropertyChanged("SelectedRotorName1");
				}
			}
		}
		public string SelectedRotorName2
		{
			get
			{
				return selectedRotorNames[1];
			}
			set
			{
				if (selectedRotorNames[1] != value)
				{
					selectedRotorNames[1] = value;
					OnPropertyChanged("SelectedRotorName2");
				}
			}
		}
		public string SelectedRotorName3
		{
			get
			{
				return selectedRotorNames[2];
			}
			set
			{
				if (selectedRotorNames[2] != value)
				{
					selectedRotorNames[2] = value;
					OnPropertyChanged("SelectedRotorName3");
				}
			}
		}
		public string[] SelectedRotorNames
		{
			get
			{
				return selectedRotorNames;
			}
			set
			{
				if (selectedRotorNames != value)
				{
					selectedRotorNames = value;
					OnPropertyChanged("SelectedRotorNames");
				}
			}
		}

		public string RotorPosition1
		{
			get
			{
				return rotorPositions[0];
			}
			set
			{
				if (rotorPositions[0] != value)
				{
					rotorPositions[0] = value;
					OnPropertyChanged("RotorPosition1");
				}
			}
		}
		public string RotorPosition2
		{
			get
			{
				return rotorPositions[1];
			}
			set
			{
				if (rotorPositions[1] != value)
				{
					rotorPositions[1] = value;
					OnPropertyChanged("RotorPosition2");
				}
			}
		}
		public string RotorPosition3
		{
			get
			{
				return rotorPositions[2];
			}
			set
			{
				if (rotorPositions[2] != value)
				{
					rotorPositions[2] = value;
					OnPropertyChanged("RotorPosition3");
				}
			}
		}
		public string[] RotorPositions
		{
			get
			{
				return rotorPositions;
			}
			set
			{
				if (rotorPositions != value)
				{
					rotorPositions = value;
					OnPropertyChanged("RotorPositions");
				}
			}
		}

		public string Plugboard
		{
			get
			{
				return plugboard;
			}
			set
			{
				if (plugboard != value)
				{
					plugboard = value;
					OnPropertyChanged("Plugboard");
				}
			}
		}

		public ICommand UpdateCommand
		{
			get
			{
				if (updateCommand == null)
				{
					updateCommand = new RelayCommand(Update);
				}
				return updateCommand;
			}
		}

		private void Update(object parameter)
		{
			int[] rotorIndexes = new int[rotorPositions.Length];
			for (int i = 0; i < rotorPositions.Length; i++ )
			{
				rotorIndexes[i] = CharacterConverter.CharacterToNumber(rotorPositions[i][0]);
			}


			Dictionary<char, char> plugboardCharacters = new Dictionary<char, char>();

			string[] pairs = plugboard.Split(';');

			for (int i = 0; i < pairs.Length; i++)
			{
				string[] letters = pairs[i].Split(':');

				if(letters.Length == 2)
				{
					plugboardCharacters[letters[0].ToUpper()[0]] = letters[1].ToUpper()[0];
				}
			}

			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(selectedEnigmaEncoderName, selectedUwkTypeName, selectedRotorNames, rotorIndexes, plugboardCharacters);

			OutputText = enigmaEncoder.encode(inputText);
		}

		private void setAvailableRotorsAndUkwTypes(string enigmaEncoderName)
		{
			Type type = Type.GetType("EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders." + enigmaEncoderName);
			IEnigmaEncoder enigmaEncoder = (IEnigmaEncoder)Activator.CreateInstance(type);
			
			AvailableUkwTypes = enigmaEncoder.GetAvailableUkwTypes();

			AvailableRotors = enigmaEncoder.GetAvailableRotors();
		}
	}
}

