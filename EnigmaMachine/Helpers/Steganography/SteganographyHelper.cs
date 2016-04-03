using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace EnigmaMachine.Helpers.Steganography
{
    public static class SteganographyHelper
    {
        const string header = "0000000011111111";
        const string footer = "0000000000000000";

        public delegate void UpdateProgressEventHandler(double progress);
        public static event UpdateProgressEventHandler UpdateProgress;

        private delegate bool PixelDataFunction(ref Bitmap image, ref int binaryTextIndex, ref string binaryMessage, ref string binaryColor);

        /// <summary>
        /// Generates image with hidden text in the least significant bits of the pixel information
        /// </summary>
        /// <param name="inputText">Text to hide</param>
        /// <param name="imagePath">Image to hide the text in</param>
        /// <returns></returns>
        public static Bitmap GenerateImage(string inputText, string imagePath)
        {
            if (String.IsNullOrEmpty(inputText))
            {
                System.Windows.MessageBox.Show("Please insert text to complete this process.");
                return null;
            }

            if (String.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                System.Windows.MessageBox.Show("Please select an image to complete this process.");
                return null;
            }

            // Convert input text to binary string
            string binaryMessage = prepairInputText(inputText);
            int binaryMessageIndex = 0;

            // Generate bitmap image
            Bitmap image = (Bitmap)Image.FromFile(imagePath);

            int amountOfPixelsNeeded = calculateAmountOfPixelsNeeded(inputText);
            if ((image.Height * image.Width) < amountOfPixelsNeeded)
            {
                string message = String.Format("Amount of data doesn't fit inside this image, you need at least {0} pixels", amountOfPixelsNeeded);
                System.Windows.MessageBox.Show(message);
                return null;
            }

            UpdateProgress(0);

            loopOverAllPixels(ref image, ref binaryMessageIndex, ref binaryMessage, insertDataInImage);

            return image;
        }

        /// <summary>
        /// Extracts hiden text from an image
        /// </summary>
        /// <param name="imagePath">Path to the image</param>
        /// <returns>String that was extracted from the image</returns>
        public static string ExtractTextFromImage(string imagePath)
        {
            if (String.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                System.Windows.MessageBox.Show("Please select an image to complete this process.");
                return null;
            }

            // Generate bitmap image
            Bitmap image = (Bitmap)Image.FromFile(imagePath);

            string binaryMessage = "";
            int binaryMessageIndex = 0;

            UpdateProgress(0);

            loopOverAllPixels(ref image, ref binaryMessageIndex, ref binaryMessage, extractDataFromImage);

            int lengthOfMessageWithoutHeaderAndFooter = binaryMessage.Length - header.Length - footer.Length;
            string binaryOutput = binaryMessage.Substring(header.Length, lengthOfMessageWithoutHeaderAndFooter);

            byte[] byteOutput = convertBinaryStringIntoByteArray(binaryOutput);

            string output = Encoding.UTF8.GetString(byteOutput);

            UpdateProgress(100);

            return output;
        }

        #region Loop over and manipulate / extract data from pixels
        private static void loopOverAllPixels(ref Bitmap image, ref int binaryMessageIndex, ref string binaryMessage, PixelDataFunction pixelDataFunction)
        {
            // Loop over vertical pixels
            for (int h = 0, imageHeight = image.Height; h < imageHeight; h++)
            {
                if (loopOverHorizontalPixels(ref image, h, ref binaryMessageIndex, ref binaryMessage, pixelDataFunction))
                {
                    break;
                }
            }
        }

        private static bool loopOverHorizontalPixels(ref Bitmap image, int height, ref int binaryMessageIndex, ref string binaryMessage, PixelDataFunction pixelDataFunction)
        {
            bool finishedProcess = false;

            // Loop over horizontal pixels
            for (int w = 0, imageWidth = image.Width; w < imageWidth; w++)
            {
                finishedProcess = loopOverColors(ref image, height, w, ref binaryMessageIndex, ref binaryMessage, pixelDataFunction);

                if (finishedProcess)
                {
                    break;
                }
            }

            return finishedProcess;
        }

        private static bool loopOverColors(ref Bitmap image, int height, int width, ref int binaryMessageIndex, ref string binaryMessage, PixelDataFunction pixelDataFunction)
        {
            bool finishedProcess = false;

            // Construct array of binary RGB values
            Color pixel = image.GetPixel(width, height);
            string[] binaryPixel = getBinaryPixel(pixel);

            // Loop over RGB colors individualy
            for (int i = 0, amountOfColors = binaryPixel.Length; i < amountOfColors; i++)
            {
                finishedProcess = pixelDataFunction(ref image, ref binaryMessageIndex, ref binaryMessage, ref binaryPixel[i]);

                if (finishedProcess)
                {
                    break;
                }
            }

            // Convert colors to pixel
            Color mutatedPixel = Color.FromArgb(getColorFromBinaryString(binaryPixel[0]),
                                                getColorFromBinaryString(binaryPixel[1]),
                                                getColorFromBinaryString(binaryPixel[2]));

            image.SetPixel(width, height, mutatedPixel);

            return finishedProcess;
        }

        private static bool insertDataInImage(ref Bitmap image, ref int binaryTextIndex, ref string binaryMessage, ref string binaryColor)
        {
            bool finishedProcess = false;

            binaryColor = binaryColor.Remove(binaryColor.Length - 1);
            binaryColor += binaryMessage[binaryTextIndex];

            binaryTextIndex++;

            UpdateProgress(binaryTextIndex * 100 / binaryMessage.Length);

            finishedProcess = (binaryTextIndex >= binaryMessage.Length);

            return finishedProcess;
        }

        private static bool extractDataFromImage(ref Bitmap image, ref int binaryTextIndex, ref string binaryMessage, ref string binaryColor)
        {
            bool finishedProcess = false;

            // Add last digit of the binary color information to the binary message string
            binaryMessage += binaryColor[binaryColor.Length - 1];

            // Check if required header exist, if not, we can't extract any information from this image
            if (binaryMessage.Length == header.Length &&
                binaryMessage != header)
            {
                System.Windows.MessageBox.Show("No hiden data was found in this image.");

                binaryMessage = "";
                finishedProcess = true;
            }

            // Check if we found the footer in the data, which means we reached the end of the hiden data
            if (binaryMessage.Length > (header.Length + footer.Length) &&
                binaryMessage.Length % 8 != 0 &&
                binaryMessage.Substring(binaryMessage.Length - footer.Length) == footer)
            {
                finishedProcess = true;
            }

            return finishedProcess;
        }
        #endregion

        private static string prepairInputText(string inputText)
        {
            // Convert input text to binary array
            byte[] binaryText = Encoding.UTF8.GetBytes(inputText);

            string binaryString = header;

            UpdateProgress(0);

            for (int i = 0, binaryLength = binaryText.Length; i < binaryLength; i++)
            {
                binaryString += Convert.ToString(binaryText[i], 2).ToString().PadLeft(8, '0');

                UpdateProgress(i * 100 / binaryLength);
            }

            binaryString += footer;

            return binaryString;
        }

        private static string getBinaryStringFromColor(int color)
        {
            string result = Convert.ToString(color, 2);
            result = result.ToString().PadLeft(8, '0');

            return result;
        }

        private static int getColorFromBinaryString(string binaryString)
        {
            int result = 0;

            result = Convert.ToInt32(binaryString, 2);
            return result;
        }

        private static int calculateAmountOfPixelsNeeded(string data)
        {
            /// P: amount of pixels
            /// C: amount of characters
            /// W: image width
            /// H: image height
            /// 
            /// P = H * W
            /// C = ((3 * P) - 32) / 8
            /// P = ((8 * C) - 32) / 3

            int amountOfCharacters = data.Length;

            double amountOfPixels = ((8 * amountOfCharacters) - 32) / 3;

            return (int)Math.Ceiling(amountOfPixels);
        }

        private static byte[] convertBinaryStringIntoByteArray(string binaryString)
        {
            byte[] outputBytes = new byte[binaryString.Length / 8];

            UpdateProgress(0);

            // Loop over every byte to fill it with data
            for (int i = 0, binaryCount = outputBytes.Length; i < binaryCount; i++)
            {
                outputBytes[i] = Convert.ToByte(binaryString.Substring(8 * i, 8), 2);

                UpdateProgress(i * 100 / binaryCount);
            }

            return outputBytes;
        }

        private static string[] getBinaryPixel(Color pixel)
        {
            string[] binaryPixel = new string[]{
                getBinaryStringFromColor(pixel.R),
                getBinaryStringFromColor(pixel.G),
                getBinaryStringFromColor(pixel.B)
            };

            return binaryPixel;
        }
    }
}
