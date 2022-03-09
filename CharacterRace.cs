using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class CharacterRace
    {
        public Race RaceBackground { get; set; }
        public Subrace CharakterSubrace { get; set; }

        public Feat HumanFeat { get; set; }

        public Skill AdditionalSkillProficiency_One { get; set; }
        public Skill AdditionalSkillProficiency_Two { get; set; }

        public List<Proficiency> RaceProficiencies { get; set; }
        public List<Race_SpellAbility> RaceSpellAbilities { get; set; }


        public CharacterRace()
        {
            RaceBackground = new Race();
            CharakterSubrace = new Subrace();

            HumanFeat = new Feat();

            RaceProficiencies = new List<Proficiency>();
            RaceSpellAbilities = new List<Race_SpellAbility>();
        }
    }
}
