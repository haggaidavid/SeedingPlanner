using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedingPlanner
{
    class Seeding
    {
        public Bag Bag { get; set; }

        public Tray Tray { get; set; }

        public int FromRow { get; set; }

        public int ToRow { get; set; }

        public int Count { get; set; }

        public Seeding() { }

        public Seeding(Bag bag, Tray tray, int fromRow, int toRow, int count)
        {
            Bag = bag;
            Tray = tray;
            FromRow = fromRow;
            ToRow = toRow;
            Count = count;
        }
    }
}
