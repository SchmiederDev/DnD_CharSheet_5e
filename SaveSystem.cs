﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace DnD_CharSheet_5e
{
    /* PURPOSE OF 'Save Sytem':
     * 
     * As the name might suggest, this class serves the purpose of allowing users to save their Characters and load existing ones
     * including all the relevant data: Character values, Items in the Inventory, learned Spells and Skills, Background Story etc.
     * 
     * In terms of code this of course means serialization and deserialization of objects to and from files.
     * 
     */

    public static class SaveSystem
    {
        #region METHODS FOR SAVING CHARACTERS = SERIALIZATION

        // Using a Binary Formatter might not be the safest option for storing data.
        // But, since this app will only run on local devices and the data of a D&D-Character is also not sensitive information about the user 
        // I think it serves its purpose here in terms of simplicity.

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

        #endregion

        #region METHODS FOR LOADING CHARACTERS = DESERIALIZATION

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

        #endregion

    }
}
