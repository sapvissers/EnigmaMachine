using EnigmaMachine.Helpers;
using EnigmaMachine.Helpers.Steganography;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnigmaMachine.ViewModel
{
    public class SteganographyDecryptViewModel : ObservableObject
    {
        public delegate void ExtractionCompletedEventHandler(string output);
        public event ExtractionCompletedEventHandler ExtractionCompleted;

        private string outputText = "";
        private string imagePath = "";

        private double progress = 0;
        private bool processing = false;

        private ICommand loadImageCommand;
        private ICommand extractDataCommand;

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
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;

                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public double Progress
        {
            get
            {
                return progress;
            }
            set
            {
                if (progress != value)
                {
                    progress = value;

                    OnPropertyChanged("Progress");
                }
            }
        }
        public bool Processing
        {
            get
            {
                return processing;
            }
            set
            {
                if (processing != value)
                {
                    processing = value;

                    OnPropertyChanged("Processing");
                }
            }
        }

        public ICommand LoadImageCommand
        {
            get
            {
                if (loadImageCommand == null)
                {
                    loadImageCommand = new RelayCommand(LoadImage);
                }
                return loadImageCommand;
            }
        }
        public ICommand ExtractDataCommand
        {
            get
            {
                if (extractDataCommand == null)
                {
                    extractDataCommand = new RelayCommand(ExtractData);
                }
                return extractDataCommand;
            }
        }

        private void LoadImage(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
            }
        }

        private void ExtractData(object parameter)
        {
            Task task = new Task(extractData);
            task.Start();

            task.ContinueWith((antecedent) =>
            {
                ExtractionCompleted(outputText);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void extractData()
        {
            SteganographyHelper.UpdateProgress += OnUpdateProgress;

            Processing = true;

            OutputText = SteganographyHelper.ExtractTextFromImage(imagePath);

            Processing = false;

            SteganographyHelper.UpdateProgress -= OnUpdateProgress;
        }

        private void OnUpdateProgress(double progress)
        {
            Progress = progress;
        }
    }
}

