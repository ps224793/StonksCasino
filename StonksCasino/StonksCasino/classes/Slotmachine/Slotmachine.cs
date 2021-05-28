using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Slotmachine
{
    public class Slotmachine
    {
        public List<SlotRow> SlotRows { get; set; } = new List<SlotRow>();

        public Slotmachine()
        {
            SlotRows.Add(new SlotRow() );
        }
    }
}
