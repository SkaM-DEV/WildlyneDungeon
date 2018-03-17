using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlyneDungeonAlpha
{
    class LootFactory
    {
        // factory

        public static Loot makeLoot(string sauvegarde)
        {
            Loot retVal;
            // Séparer les champs séparés par ;
            String[] champs = sauvegarde.Split(';');
            if (champs.Length != 6)
            {
                retVal = null;
            }

            else
            {
                //return
                retVal = new Loot(champs[0], champs[1], Convert.ToInt32(champs[2]), Convert.ToInt32(champs[3]), Convert.ToInt32(champs[4]), Convert.ToInt32(champs[5]));
            }

            return retVal;
        }
    }
}
