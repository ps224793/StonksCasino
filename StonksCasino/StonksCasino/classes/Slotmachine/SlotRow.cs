using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Slotmachine
{
    public class SlotRow
    {
        public List<Slot> Slots { get; set; } = new List<Slot>();


        public SlotRow()
        {
            Slots.Add(new Slot() { Name = "Cherry" }) ;
            Slots.Add(new Slot() { Name = "Cherry" });
            Slots.Add(new Slot() { Name = "Cherry" });

        }


    }


}
