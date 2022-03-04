using System;
using System.Collections.Generic;

namespace DnD_CharSheet_5e
{
    public class Spell
    {
        public string SpellName { set; get; }

        public int SpellLevel { set; get; }

        public string School { set; get; }

        public string CastingTime { set; get; }

        public string Range { set; get; }

        public string Components { set; get; }

        public string Duration { set; get; }

        public string Description { set; get; }
    }
}
