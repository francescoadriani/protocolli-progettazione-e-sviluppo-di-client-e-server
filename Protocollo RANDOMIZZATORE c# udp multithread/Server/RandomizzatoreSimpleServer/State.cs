using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizzatoreServerPool
{
    public class State
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public State(Double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        private State() { }
    }
}
