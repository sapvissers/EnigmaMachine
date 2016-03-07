using EnigmaMachine.Helpers;
using EnigmaMachine.Helpers.EnigmaEncoder;
using EnigmaMachine.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private ICommand steganographyEncryptCommand;
		private ICommand steganographyDecryptCommand;

		private SteganographyDecryptViewModel steganographyDecryptContext;
		private SteganographyDecryptView steganographyDecryptView;
		
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
				if (rotorPositions[0] != value.Substring(value.Length - 1))
				{
					rotorPositions[0] = value.Substring(value.Length - 1);
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
				if (rotorPositions[1] != value.Substring(value.Length - 1))
				{
					rotorPositions[1] = value.Substring(value.Length - 1);
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
				if (rotorPositions[2] != value.Substring(value.Length - 1))
				{
					rotorPositions[2] = value.Substring(value.Length - 1);
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
		public ICommand SteganographyEncryptCommand
		{
			get
			{
				if (steganographyEncryptCommand == null)
				{
					steganographyEncryptCommand = new RelayCommand(SteganographyEncrypt);
				}
				return steganographyEncryptCommand;
			}
		}
		public ICommand SteganographyDecryptCommand
		{
			get
			{
				if (steganographyDecryptCommand == null)
				{
					steganographyDecryptCommand = new RelayCommand(SteganographyDecrypt);
				}
				return steganographyDecryptCommand;
			}
		}

		private void Update(object parameter)
		{
			if (rotorPositions.Length == 0 ||
				selectedEnigmaEncoderName == "" ||
				selectedUwkTypeName == "")
			{
				return;
			}

			int[] rotorIndexes = new int[rotorPositions.Length];
			for (int i = 0; i < rotorPositions.Length; i++ )
			{
				if (rotorPositions[i] == null)
				{
					return;
				}


				rotorIndexes[i] = CharacterConverter.CharacterToNumber(rotorPositions[i][0]);
			}


			Dictionary<char, char> plugboardCharacters = new Dictionary<char, char>();

			string[] pairs = plugboard.Split(';');

			for (int i = 0; i < pairs.Length; i++)
			{
				string[] letters = pairs[i].Split('-');

				if(letters.Length == 2)
				{
					plugboardCharacters[letters[0].ToUpper()[0]] = letters[1].ToUpper()[0];
					plugboardCharacters[letters[1].ToUpper()[0]] = letters[0].ToUpper()[0];
				}
			}

			IEnigmaEncoder enigmaEncoder = EnigmaEncoderFactory.NewEnigmaEncoder(selectedEnigmaEncoderName, selectedUwkTypeName, selectedRotorNames, rotorIndexes, plugboardCharacters);

			OutputText = enigmaEncoder.Encode(inputText);
		}

		private void SteganographyEncrypt(object parameter)
		{
			SteganographyEncryptView steganographyEncryptView = new SteganographyEncryptView();
			SteganographyEncryptViewModel steganographyEncryptContext = new SteganographyEncryptViewModel();
			steganographyEncryptView.DataContext = steganographyEncryptContext;
			steganographyEncryptContext.InputText = outputText;
			steganographyEncryptView.Show();
		}

		private void SteganographyDecrypt(object parameter)
		{
			steganographyDecryptView = new SteganographyDecryptView();
			steganographyDecryptContext = new SteganographyDecryptViewModel();
			steganographyDecryptView.DataContext = steganographyDecryptContext;

			steganographyDecryptContext.ExtractionCompleted += OnExtractionCompleted;
			steganographyDecryptView.Show();
		}

		private void setAvailableRotorsAndUkwTypes(string enigmaEncoderName)
		{
			Type type = Type.GetType("EnigmaMachine.Helpers.EnigmaEncoder.EnigmaEncoders." + enigmaEncoderName);
			IEnigmaEncoder enigmaEncoder = (IEnigmaEncoder)Activator.CreateInstance(type);
			
			AvailableUkwTypes = enigmaEncoder.GetAvailableUkwTypes();

			AvailableRotors = enigmaEncoder.GetAvailableRotors();
		}

		private void OnExtractionCompleted(string output)
		{
			InputText = output;

			steganographyDecryptView.Closed += OnDecryptClosed;
		}

		private void OnDecryptClosed(object sender, System.EventArgs e)
		{
			steganographyDecryptContext.ExtractionCompleted -= OnExtractionCompleted;
			steganographyDecryptView.Closed -= OnDecryptClosed;
		}
	}
}

