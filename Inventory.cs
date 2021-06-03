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

        int platinum;
        int gold;
        int silver;
        int copper;

        public void Set_Platinum_byTxt(string platinumTxt)
        {
            if(int.TryParse(platinumTxt, out int number))
            {
                platinum = int.Parse(platinumTxt);
            }

            else
            {
                MessageBox.Show($"Invalid input. Please enter an integer number.");
            }
        }

        public void Set_Platinum(int amount)
        {
            platinum = amount;
        }

        public int Get_Platinum()
        {
            return platinum;
        }

        public void Set_Gold_byTxt(string goldTxt)
        {
            if (int.TryParse(goldTxt, out int number))
            {
                gold = int.Parse(goldTxt);
            }

            else
            {
                MessageBox.Show($"Invalid input. Please enter an integer number.");
            }
        }

        public void Set_Gold(int amount)
        {
            gold = amount;
        }

        public int Get_Gold()
        {
            return gold;
        }

        public void Set_Silver_byTxt(string silverTxt)
        {
            if (int.TryParse(silverTxt, out int number))
            {
                silver = int.Parse(silverTxt);
            }
           
            else
            {
                MessageBox.Show($"Invalid input. Please enter an integer number.");
            }
        }

        public void Set_Silver(int amount)
        {
            silver = amount;
        }

        public int Get_Silver()
        {
            return silver;
        }

        public void Set_Copper_byTxt(string copperTxt)
        {
            if (int.TryParse(copperTxt, out int number))
            {
                copper = int.Parse(copperTxt);
            }           

            else
            {
                MessageBox.Show($"Invalid input. Please enter an integer number.");
            }
        }

        public void Set_Copper(int amount)
        {
            copper = amount;
        }

        public int Get_Copper()
        {
            return copper;
        }

        public void Add_Item(Item newItem)
        {
            cItems.Add(newItem);
        }

        public Item Find_Item_byID(string id)
        {
            Item tempItem = new Item();

            foreach (Item mItem in cItems)
            {
                if (mItem.Item_ID == id)
                {
                    tempItem = mItem;
                }
            }

            return tempItem;
        }

        public void Remove_Item(Item item_toRemove)
        {
            cItems.Remove(item_toRemove);
        }

        public void Remove_Item_byID(string id)                             // What if the Item exists in the Inventory several times + additional Button_ID?
        {
            foreach(Item cItem in cItems)
            {
                if(cItem.Item_ID == id)
                {
                    cItems.Remove(cItem);
                }
            }
        }

        public void Add_Weapon(Weapon newWeapon)
        {
            cWeapons.Add(newWeapon);
        }

        public Weapon Find_Weapon_byID(string id)
        {
            Weapon tempWeapon = new Weapon();

            foreach (Weapon cWeapon in cWeapons)
            {
                if (cWeapon.Item_ID == id)
                {
                    tempWeapon = cWeapon;
                }
            }

            return tempWeapon;
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
            Armor tempArmor = new Armor();

            foreach(Armor armor in cArmor)
            {
                if(armor.Item_ID == id)
                {
                    tempArmor = armor;
                }
            }

            return tempArmor;
        }

        public void Remove_Armor(Armor armor_toRemove)
        {
            cArmor.Remove(armor_toRemove);
        }

        public bool Pay_Item(Coin coin)
        {            
            if(coin.CoinKey == "PP")
            {
                if (coin.Price <= platinum)
                {
                    platinum -= coin.Price;
                    return true;
                }

                else
                {
                    int tempPlatinum = platinum;
                    tempPlatinum -= coin.Price;
                    int priceRemainderGold = Math.Abs(tempPlatinum * 10);

                    if (priceRemainderGold <= gold)
                    {
                        gold -= priceRemainderGold;
                        platinum = 0;
                        return true;
                    }

                    else
                    {
                        int tempGold = gold;
                        tempGold -= priceRemainderGold;
                        int priceRemainderSilver = Math.Abs(tempGold * 10);

                        if (priceRemainderSilver <= silver)
                        {
                            silver -= priceRemainderSilver;
                            platinum = 0;
                            gold = 0;
                            return true;
                        }

                        else
                        {
                            int tempSilver = silver;
                            tempSilver -= priceRemainderSilver;
                            int priceRemainderCopper = Math.Abs(tempSilver * 10);

                            if (priceRemainderCopper <= copper)
                            {
                                copper -= priceRemainderCopper;
                                platinum = 0;
                                gold = 0;
                                silver = 0;
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
                if (coin.Price <= gold)
                {
                    gold -= coin.Price;

                    return true;
                }

                else
                {
                    int tempGold = gold;
                    tempGold -= coin.Price;
                    int goldPriceRemainder = Math.Abs(tempGold);
                    int tempPlatinumGold = platinum * 10;

                    if (tempPlatinumGold >= goldPriceRemainder)
                    {
                        tempPlatinumGold -= goldPriceRemainder;
                        int tempPlatinumRem = tempPlatinumGold;
                        int goldRem = tempPlatinumRem % 10;
                        int tempPlatinum = tempPlatinumRem - goldRem;
                        platinum = tempPlatinum / 10;
                        gold += goldRem;

                        return true;
                    }

                    else 
                    {
                        goldPriceRemainder -= tempPlatinumGold;
                        int priceRemainderSilver = goldPriceRemainder * 10;

                        if (priceRemainderSilver <= silver)
                        {
                            silver -= priceRemainderSilver;
                            platinum = 0;
                            gold = 0;

                            return true;
                        }

                        else
                        {
                            int tempSilver = silver;
                            tempSilver -= priceRemainderSilver;
                            int priceRemainderCopper = Math.Abs(tempSilver * 10);

                            if (priceRemainderCopper <= copper)
                            {
                                copper -= priceRemainderCopper;
                                platinum = 0;
                                gold = 0;
                                silver = 0;

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
                if (coin.Price <= silver)
                {
                    silver -= coin.Price;

                    return true;
                }

                else
                {
                    int tempSilver = silver;
                    tempSilver -= coin.Price;
                    int silverPriceRemainder = Math.Abs(tempSilver);
                    int tempGoldSilver = gold * 10;

                    if (silverPriceRemainder <= tempGoldSilver)
                    {
                        tempGoldSilver -= silverPriceRemainder;
                        int silverRem = tempGoldSilver % 10;
                        int tempGold = tempGoldSilver - silverRem;
                        gold = tempGold / 10;
                        silver = 0;
                        silver += silverRem;

                        return true;
                        
                    }

                    else 
                    {
                        int priceRemainder = silverPriceRemainder - tempGoldSilver;
                        int tempPlatinumSilver = platinum * 100;

                        if (priceRemainder <= tempPlatinumSilver)
                        {
                            tempPlatinumSilver -= priceRemainder;
                            platinum = tempPlatinumSilver / 100;
                            gold = 0;
                            silver = 0;

                            return true;
                        }

                        else
                        {
                            int copperPriceRemainder_inSilver = tempPlatinumSilver - priceRemainder;
                            int copperPriceRemainder_inCopper = copperPriceRemainder_inSilver * 10;

                            if (copperPriceRemainder_inCopper <= copper)
                            {
                                copper -= copperPriceRemainder_inCopper;
                                platinum = 0;
                                gold = 0;
                                silver = 0;

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
                if (coin.Price <= copper)
                {
                    copper -= coin.Price;
                    return true;
                }

                else 
                {
                    int tempCopper = copper;
                    tempCopper -= coin.Price;
                    int copperPriceRemainder = Math.Abs(tempCopper);

                    int tempCopperSilver = silver * 10;

                    if (copperPriceRemainder <= tempCopperSilver)
                    {
                        tempCopperSilver -= copperPriceRemainder;
                        int copperRem = tempCopperSilver % 10;
                        int tempSilver = tempCopperSilver - copperRem;
                        silver = tempSilver / 10;
                        copper = 0;
                        copper += copperRem;

                        return true;                        
                    }

                    else 
                    {                        
                        int tempGold_inCopper = gold * 100;

                        if(copperPriceRemainder <= tempGold_inCopper)
                        {
                            tempGold_inCopper -= copperPriceRemainder;
                            int copperRem = tempGold_inCopper % 10;
                            int tempGold = tempGold_inCopper - copperRem;
                            gold = tempGold / 10;
                            copper = 0;
                            copper += copperRem;

                            return true;
                        }

                        else
                        {
                           if(platinum > 0)
                            {
                                int tempPlatinumCopper = platinum * 1000;
                                int tempPlatinum_inCopper = tempPlatinumCopper - copperPriceRemainder;

                                int tempRem = tempPlatinum_inCopper % 1000;
                                int tempPlatinum = tempPlatinum_inCopper - tempRem;
                                platinum = tempPlatinum / 1000;

                                int tempSilverRem = tempRem % 10;
                                int tempGold = tempRem - tempSilverRem;
                                gold = tempGold / 10;
                                silver += tempSilverRem;
                                copper = 0;

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
    }
}
