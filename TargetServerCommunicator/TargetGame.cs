using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetServerCommunicator
{

    /// <summary>
    /// Information about a game
    /// </summary>
    public class TargetGame
    {
        public TargetGame(string name)
        {
            Name    = name;
            Targets = new Collection<Target>();
        }

        public string Name { get; set; }

        public ICollection<Target> Targets { get; set; }
    }
}
