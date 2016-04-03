using EnigmaMachine.Helpers;
using EnigmaMachine.Helpers.Steganography;
using EnigmaMachine.View;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EnigmaMachine.ViewModel
{
    public class SteganographyEncryptViewModel : ObservableObject
    {
        private string inputText = "";
        private string imagePath = "";

        private Bitmap steganographyImage;

        private double progress = 0;

        private ICommand loadImageCommand;
        private ICommand generateImageCommand;

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
        public ICommand GenerateImageCommand
        {
            get
            {
                if (generateImageCommand == null)
                {
                    generateImageCommand = new RelayCommand(GenerateImage);
                }
                return generateImageCommand;
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

        private void GenerateImage(object parameter)
        {
            Task task = new Task(generateImage);
            task.Start();

            task.ContinueWith((antecedent) =>
            {
                showImagePopup();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void generateImage()
        {
            SteganographyHelper.UpdateProgress += OnUpdateProgress;

            steganographyImage = SteganographyHelper.GenerateImage(inputText, imagePath);

            if (steganographyImage == null)
            {
                return;
            }

            SteganographyHelper.UpdateProgress -= OnUpdateProgress;
        }

        private void showImagePopup()
        {
            BitmapSource steganographySource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(steganographyImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            ImagePopupView imagePopupView = new ImagePopupView();
            ImagePopupViewModel imagePopupContext = new ImagePopupViewModel();

            imagePopupView.DataContext = imagePopupContext;
            imagePopupContext.ProcessedImage = steganographyImage;
            imagePopupContext.ProcessedImageSource = steganographySource;

            imagePopupView.Show();
        }

        private void OnUpdateProgress(double progress)
        {
            Progress = progress;
        }
    }
}

