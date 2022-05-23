using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DnD_CharSheet_5e
{
    public class ImageHandler
    {
        public static ImageHandler ImgHandlerInst;

        public string[] ImageFileNames { get; set; }

        public List<Uri> UriList { get; } = new List<Uri>();

        public Image DieImage { get; private set; } = new Image();

        public ImageHandler()
        {
            if(ImgHandlerInst != null)
            {
                ImgHandlerInst = this;
            }
        }

        public void Set_Uris()
        {           

            foreach(string fileName in ImageFileNames)
            {
                Uri tempUri = new Uri(fileName);
                UriList.Add(tempUri);
            }
        }

        public BitmapImage Get_SourceUri(string itemName)
        {
            string tempPath = FileManager.FM_Inst.ImagesFolder + "\\" + itemName + ".png";
            
            Uri tempUri = new Uri(tempPath);

            if(UriList.Contains(tempUri))
            {
                BitmapImage bitmapImage = new BitmapImage(tempUri);
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
    }
}
