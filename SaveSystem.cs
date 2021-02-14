using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DnD_CharSheet_5e
{
    public static class SaveSystem
    {
        public static void SaveCharacter(Character character, string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            
            FileStream fStream = new FileStream(path, FileMode.Create);
            
            CharacterData charData = new CharacterData();
            charData.Transfer_CharData(character);

            binaryFormatter.Serialize(fStream, charData);
            fStream.Close();
        }

        public static CharacterData LoadCharacter(string path)
        {
            if(File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fStream = new FileStream(path, FileMode.Open);

                CharacterData charData = binaryFormatter.Deserialize(fStream) as CharacterData;

                fStream.Close();

                return charData;
            }

            else
            {
                return null;
            }
        }
    }
}
