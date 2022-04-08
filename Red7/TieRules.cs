using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red7
{
    internal class TieRules
    {
        private CardCollection p1;
        private CardCollection p2;

        public TieRules(CardCollection p1, CardCollection p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        private Player TieBreak()
        {
            return MostCards() ?? HighestNumber() ?? BestColour();
        }

        private Player MostCards()
        {
            if (p1.GetNumberOfCards() == p2.GetNumberOfCards())
                return null;

            return null;
        }

        private Player HighestNumber()
        {
            return null;
        }

        private Player BestColour()
        {
            return null;
        }
    }
}
