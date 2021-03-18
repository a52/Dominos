using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Token
    {
        /// <summary>
        /// Upper value of the domino token
        /// </summary>
        public int Up_Value { get; set; }
        
        /// <summary>
        /// Lower value of the domino token
        /// </summary>
        public int Down_Value { get; set; }
        //public int Position { get; set; }

        /// <summary>
        /// Indicate if the token is in the board
        /// </summary>
        public bool IsOnBoard { get; set; }

        /// <summary>
        /// Indicate is the token had been assigned to some player
        /// </summary>
        public bool IsAssigned { get; set; }

        /// <summary>
        /// Value of the token
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}", Up_Value, Down_Value);
        }

    }
}
