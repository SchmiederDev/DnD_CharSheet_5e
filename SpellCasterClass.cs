namespace DnD_CharSheet_5e
{
    public class SpellCasterClass
    {
        public string SCC_Name { set; get; }
        public string SpellCastingAbility_Name { set; get; }
        public string SpellCastingAbility_Key { set; get; }
        public int SpellSaveDC { set; get; }
        public int SpellAttackBonus { set; get; }
        public int BaseModifierSaveDC { get;} = 8;
        public int BaseModifierAtkBonus { get; } = 10;

        public void Calculate_SpellSaveDC()
        {
            Ability tempSpellCastingAbility = SheetManager.CS_Manager_Inst.character.Abilities.Find(ability => ability.AbilityName == SpellCastingAbility_Name);
            SpellSaveDC = BaseModifierSaveDC + tempSpellCastingAbility.Modifier + SheetManager.CS_Manager_Inst.character.ProficiencyBonus;
        }

        public void Calculate_SpellAttackBonus()
        {
            Ability tempSpellCastingAbility = SheetManager.CS_Manager_Inst.character.Abilities.Find(ability => ability.AbilityName == SpellCastingAbility_Name);
            SpellAttackBonus = BaseModifierAtkBonus + tempSpellCastingAbility.Modifier + SheetManager.CS_Manager_Inst.character.ProficiencyBonus;
        }

        public void Generate_AbilityKey()
        {
            string tempAbilityKey = SpellCastingAbility_Name.Remove(3);
            SpellCastingAbility_Key = tempAbilityKey.ToUpper();
        }

    }
}
