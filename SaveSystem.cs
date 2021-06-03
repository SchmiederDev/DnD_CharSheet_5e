using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using Newtonsoft.Json;

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

        public static void Save_CharName(string name, string path, int index)
        {
            if(!File.Exists(path))
            {
                List<string> charNames = new List<string>(5);
                string filler = "Empty Slot";
                
                for(int i = 0; i < charNames.Capacity; i++)
                {
                    if(i == index)
                    {
                        charNames.Insert(index, name);
                    }

                    else
                    {
                        charNames.Add(filler);
                    }
                    
                }                             
                
                File.WriteAllLines(path, charNames);
            }

            else if(File.Exists(path))
            {
                List<string> charNames = File.ReadAllLines(path).ToList();
                charNames.RemoveAt(index);
                charNames.Insert(index, name);
                File.WriteAllLines(path, charNames);
            }

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
        
        public static List<string> Load_CharNames(string path)
        {
            if(File.Exists(path))
            {
                List<string> charNames = File.ReadAllLines(path).ToList();

                return charNames;
            }

            else
            {
                return null;
            }

        }       

    }
}
