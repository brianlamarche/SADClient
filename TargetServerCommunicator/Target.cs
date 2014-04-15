using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetServerCommunicator
{

    /// <summary>
    /// Encapsulates data about a target
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the x position
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Gets the y position
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Gets the z position
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// Gets the value in points of the target 
        /// </summary>
        public double Points { get; set; }
        /// <summary>
        /// Gets the flash rate of the target LED
        /// </summary>
        public double FlashRate { get; set; }
    }
}
