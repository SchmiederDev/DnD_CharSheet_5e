using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DnD_CharSheet_5e
{
    public class ImageHandler
    {
        public string[] ImageFileNames { get; set; }

        public List<Uri> uriList { get; } = new List<Uri>();

        public void Set_Uris()
        {           

            foreach(string fileName in ImageFileNames)
            {
                Uri tempUri = new Uri(fileName);
                uriList.Add(tempUri);
            }
        }

        public BitmapImage Get_SourceUri(string itemName)
        {
            string tempPath = FileManager.FM_Inst.Get_ImagesFolder() + "\\" + itemName + ".png";
            
            Uri tempUri = new Uri(tempPath);

            if(uriList.Contains(tempUri))
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
    }
}
