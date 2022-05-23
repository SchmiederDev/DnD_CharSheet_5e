using System;
using System.Collections.Generic;
using System.Windows;

namespace DnD_CharSheet_5e
{
    [Serializable]
    public class Inventory
    {
        public List<Item> cItems { get; } = new List<Item>();                                                 // c = 'character'
        public List<Weapon> cWeapons { get; } = new List<Weapon>();
        public List<Armor> cArmor { get; } = new List<Armor>();

        public int Platinum { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }

        public int Copper { get; set; }

        const string ValueErrorMessage = "Invalid input. Please enter an integer number.";
        const string ItemNotFoundMsg = "Item not found";



        public void Set_Platinum_byTxt(string PlatinumTxt)
        {
            if(int.TryParse(PlatinumTxt, out int platinumPrice))
            {
                Platinum = platinumPrice;
            }

            else
            {
                MessageBox.Show(ValueErrorMessage);
            }
        }

        
        public void Set_Gold_byTxt(string GoldTxt)
        {
            if (int.TryParse(GoldTxt, out int goldPrice))
            {
                Gold = goldPrice;
            }

            else
            {
                MessageBox.Show(ValueErrorMessage);
            }
        }       

        public void Set_Silver_byTxt(string SilverTxt)
        {
            if (int.TryParse(SilverTxt, out int silverPrice))
            {
                Silver = silverPrice;
            }
           
            else
            {
                MessageBox.Show(ValueErrorMessage);
            }
        }
       
        public void Set_Copper_byTxt(string CopperTxt)
        {
            if (int.TryParse(CopperTxt, out int copperPrice))
            {
                Copper = copperPrice;
            }           

            else
            {
                MessageBox.Show(ValueErrorMessage);
            }
        }

        public void Add_Item(Item newItem)
        {
            cItems.Add(newItem);            
        }

        public Item Find_Item_byID(string id)
        {
            Item tempItem = cItems.Find(itemElement => itemElement.Item_ID == id);

            if(tempItem != null)
            {
                return tempItem;
            }

            else
            {
                MessageBox.Show(ItemNotFoundMsg);
                return null;
            }
        }

        public void Remove_Item(Item item_toRemove)
        {
            cItems.Remove(item_toRemove);
        }

        public void Remove_Item_byID(string id)                             // What if the Item exists in the Inventory several times + additional Button_ID?
        {
            Item tempItem = cItems.Find(itemElement => itemElement.Item_ID == id);

            if(tempItem != null)
            {
                cItems.Remove(tempItem);
            }

            else
            {
                MessageBox.Show(ItemNotFoundMsg);
            }
        }

        public void Add_Weapon(Weapon newWeapon)
        {
            cWeapons.Add(newWeapon);            
        }

        public Weapon Find_Weapon_byID(string id)
        {
            Weapon tempWeapon = cWeapons.Find(weaponElement => weaponElement.Item_ID == id);

            if (tempWeapon != null)
            {
                return tempWeapon;
            }

            else
            {
                MessageBox.Show(ItemNotFoundMsg);
                return null;
            }
        }

        public void Remove_Weapon(Weapon weapon_toRemove)
        {
            cWeapons.Remove(weapon_toRemove);            
        }

        public void Add_Armor(Armor newArmor)
        {
            cArmor.Add(newArmor);
        }

        public Armor Find_Armor_byID(string id)
        {
            Armor tempArmor = cArmor.Find(armorElement => armorElement.Item_ID == id);


            if (tempArmor != null)
            {
                return tempArmor;
            }

            else
            {
                MessageBox.Show(ItemNotFoundMsg);
                return null;
            }
        }

        public void Remove_Armor(Armor armor_toRemove)
        {
            cArmor.Remove(armor_toRemove);
        }

        public bool Pay_Item(Coin coin)
        {            
            if(coin.CoinKey == "PP")
            {
                if (coin.Price <= Platinum)
                {
                    Platinum -= coin.Price;
                    return true;
                }

                else
                {
                    int tempPlatinum = Platinum;
                    tempPlatinum -= coin.Price;
                    int priceRemainderGold = Math.Abs(tempPlatinum * 10);

                    if (priceRemainderGold <= Gold)
                    {
                        Gold -= priceRemainderGold;
                        Platinum = 0;
                        return true;
                    }

                    else
                    {
                        int tempGold = Gold;
                        tempGold -= priceRemainderGold;
                        int priceRemainderSilver = Math.Abs(tempGold * 10);

                        if (priceRemainderSilver <= Silver)
                        {
                            Silver -= priceRemainderSilver;
                            Platinum = 0;
                            Gold = 0;
                            return true;
                        }

                        else
                        {
                            int tempSilver = Silver;
                            tempSilver -= priceRemainderSilver;
                            int priceRemainderCopper = Math.Abs(tempSilver * 10);

                            if (priceRemainderCopper <= Copper)
                            {
                                Copper -= priceRemainderCopper;
                                Platinum = 0;
                                Gold = 0;
                                Silver = 0;
                                return true;
                            }

                            else
                            {
                                MessageBox.Show($"Sorry you haven't got enough money");
                                return false;
                            }
                        }
                    }
                }
            }

            if (coin.CoinKey == "GP")
            {
                if (coin.Price <= Gold)
                {
                    Gold -= coin.Price;

                    return true;
                }

                else
                {
                    int tempGold = Gold;
                    tempGold -= coin.Price;
                    int GoldPriceRemainder = Math.Abs(tempGold);
                    int tempPlatinumGold = Platinum * 10;

                    if (tempPlatinumGold >= GoldPriceRemainder)
                    {
                        tempPlatinumGold -= GoldPriceRemainder;
                        int tempPlatinumRem = tempPlatinumGold;
                        int GoldRem = tempPlatinumRem % 10;
                        int tempPlatinum = tempPlatinumRem - GoldRem;
                        Platinum = tempPlatinum / 10;
                        Gold += GoldRem;

                        return true;
                    }

                    else 
                    {
                        GoldPriceRemainder -= tempPlatinumGold;
                        int priceRemainderSilver = GoldPriceRemainder * 10;

                        if (priceRemainderSilver <= Silver)
                        {
                            Silver -= priceRemainderSilver;
                            Platinum = 0;
                            Gold = 0;

                            return true;
                        }

                        else
                        {
                            int tempSilver = Silver;
                            tempSilver -= priceRemainderSilver;
                            int priceRemainderCopper = Math.Abs(tempSilver * 10);

                            if (priceRemainderCopper <= Copper)
                            {
                                Copper -= priceRemainderCopper;
                                Platinum = 0;
                                Gold = 0;
                                Silver = 0;

                                return true;
                            }

                            else 
                            {
                                MessageBox.Show($"Sorry you haven't got enough money");

                                return false;
                            }
                        }
                    }
                }
            }

            if (coin.CoinKey == "SP")
            {
                if (coin.Price <= Silver)
                {
                    Silver -= coin.Price;

                    return true;
                }

                else
                {
                    int tempSilver = Silver;
                    tempSilver -= coin.Price;
                    int SilverPriceRemainder = Math.Abs(tempSilver);
                    int tempGoldSilver = Gold * 10;

                    if (SilverPriceRemainder <= tempGoldSilver)
                    {
                        tempGoldSilver -= SilverPriceRemainder;
                        int SilverRem = tempGoldSilver % 10;
                        int tempGold = tempGoldSilver - SilverRem;
                        Gold = tempGold / 10;
                        Silver = 0;
                        Silver += SilverRem;

                        return true;
                        
                    }

                    else 
                    {
                        int priceRemainder = SilverPriceRemainder - tempGoldSilver;
                        int tempPlatinumSilver = Platinum * 100;

                        if (priceRemainder <= tempPlatinumSilver)
                        {
                            tempPlatinumSilver -= priceRemainder;
                            Platinum = tempPlatinumSilver / 100;
                            Gold = 0;
                            Silver = 0;

                            return true;
                        }

                        else
                        {
                            int CopperPriceRemainder_inSilver = tempPlatinumSilver - priceRemainder;
                            int CopperPriceRemainder_inCopper = CopperPriceRemainder_inSilver * 10;

                            if (CopperPriceRemainder_inCopper <= Copper)
                            {
                                Copper -= CopperPriceRemainder_inCopper;
                                Platinum = 0;
                                Gold = 0;
                                Silver = 0;

                                return true;
                            }

                            else
                            {
                                MessageBox.Show($"Sorry you haven't got enough money");

                                return false;
                            }
                        }
                    }
                }
            }

            if (coin.CoinKey == "CP")
            {
                if (coin.Price <= Copper)
                {
                    Copper -= coin.Price;
                    return true;
                }

                else 
                {
                    int tempCopper = Copper;
                    tempCopper -= coin.Price;
                    int CopperPriceRemainder = Math.Abs(tempCopper);

                    int tempCopperSilver = Silver * 10;

                    if (CopperPriceRemainder <= tempCopperSilver)
                    {
                        tempCopperSilver -= CopperPriceRemainder;
                        int CopperRem = tempCopperSilver % 10;
                        int tempSilver = tempCopperSilver - CopperRem;
                        Silver = tempSilver / 10;
                        Copper = 0;
                        Copper += CopperRem;

                        return true;                        
                    }

                    else 
                    {                        
                        int tempGold_inCopper = Gold * 100;

                        if(CopperPriceRemainder <= tempGold_inCopper)
                        {
                            tempGold_inCopper -= CopperPriceRemainder;
                            int CopperRem = tempGold_inCopper % 10;
                            int tempGold = tempGold_inCopper - CopperRem;
                            Gold = tempGold / 10;
                            Copper = 0;
                            Copper += CopperRem;

                            return true;
                        }

                        else
                        {
                           if(Platinum > 0)
                            {
                                int tempPlatinumCopper = Platinum * 1000;
                                int tempPlatinum_inCopper = tempPlatinumCopper - CopperPriceRemainder;

                                int tempRem = tempPlatinum_inCopper % 1000;
                                int tempPlatinum = tempPlatinum_inCopper - tempRem;
                                Platinum = tempPlatinum / 1000;

                                int tempSilverRem = tempRem % 10;
                                int tempGold = tempRem - tempSilverRem;
                                Gold = tempGold / 10;
                                Silver += tempSilverRem;
                                Copper = 0;

                                return true;
                            }

                            else
                            {
                                MessageBox.Show($"Sorry you haven't got enough money");

                                return false;
                            }
                           
                        }
                        
                    }
                }
            }

            else { return false; }
        }

        public void Clear_Inventory()
        {
            Platinum = 0;
            Gold = 0;
            Silver = 0;
            Copper = 0;

            cItems.Clear();
            cWeapons.Clear();
            cArmor.Clear();
        }

    }
}
