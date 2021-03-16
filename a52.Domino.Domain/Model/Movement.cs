using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Movement
    {
        /// <summary>
        /// - Index: Order in which the movement was made
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// - Date: Date and hour in which the movement was made
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// - Player: the player who made the movement
        /// </summary>
        public Player CurrentPlayer { get; set; }

        /// <summary>
        /// - Tab: the domino who was played
        /// </summary>
        public Token CurrentToken { get; set; }

        /// <summary>
        /// - Direction: how the domino was posisioned. Up or Down
        /// </summary>
        public Direction CurrentDirection { get; set; }

    }
}
