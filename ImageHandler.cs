using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DnD_CharSheet_5e
{
    public class ImageHandler
    {
        #region SINGLETON AND CONSTRUCTOR

        public static ImageHandler ImgHandlerInst;

        public ImageHandler()
        {
            if (ImgHandlerInst != null)
            {
                ImgHandlerInst = this;
            }
        }
        #endregion

        #region PROPERTIES

        public string[] ImageFileNames { get; set; }

        public List<Uri> UriList { get; } = new List<Uri>();

        public Image DieImage { get; private set; } = new Image();

        #endregion

        #region METHODS

        public void Set_Uris()
        {         
            foreach(string fileName in ImageFileNames)
            {
                Uri ImageUri = new Uri(fileName);
                UriList.Add(ImageUri);
            }
        }

        public BitmapImage Get_SourceUri(string itemName)
        {
            string imagePath = FileManager.FM_Inst.ImagesFolder + "\\" + itemName + ".png";
            
            Uri ImageUri = new Uri(imagePath);

            if(UriList.Contains(ImageUri))
            {
                BitmapImage bitmapImage = new BitmapImage(ImageUri);
                return bitmapImage;
            }

            else
            {
                MessageBox.Show($"Couldn't find an Image under this item name.");
                return null;
            }
            
        }

        public void Load_DieImage()
        {
            DieImage.Source = Get_SourceUri("d20_raw_green_transparentBG");
        }

        #endregion
    }
}
