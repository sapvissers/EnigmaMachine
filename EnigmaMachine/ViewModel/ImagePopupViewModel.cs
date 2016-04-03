using EnigmaMachine.Helpers;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EnigmaMachine.ViewModel
{
    public class ImagePopupViewModel : ObservableObject
    {
        private Bitmap processedImage = null;
        private BitmapSource processedImageSource = null;

        private ICommand saveImageCommand;

        public Bitmap ProcessedImage
        {
            get
            {
                return processedImage;
            }
            set
            {
                if (processedImage != value)
                {
                    processedImage = value;

                    OnPropertyChanged("ProcessedImage");
                }
            }
        }
        public BitmapSource ProcessedImageSource
        {
            get
            {
                return processedImageSource;
            }
            set
            {
                if (processedImageSource != value)
                {
                    processedImageSource = value;

                    OnPropertyChanged("ProcessedImageSource");
                }
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                if (saveImageCommand == null)
                {
                    saveImageCommand = new RelayCommand(SaveImage);
                }
                return saveImageCommand;
            }
        }

        private void SaveImage(object parameter)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG(*.png)|*.png";
            saveDialog.AddExtension = true;

            Stream fileStream;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((fileStream = saveDialog.OpenFile()) != null)
                {
                    fileStream.Close();

                    processedImage.Save(saveDialog.FileName);
                }
            }
        }
    }
}

