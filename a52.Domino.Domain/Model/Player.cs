using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    public class Player
    {

        public Player()
        {
            this.Tokens = new List<Token>();
        }

        /// <summary>
        /// - Index: Order of in the game
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// - PlayerName: Name of the player
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// - IsMachine: Indicate if is a human or the computer that will play
        /// </summary>
        public bool IsMachine { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// - Score: the point earned by the player
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// - Tabs[]: list of tabs that is in the her hand  
        /// </summary>
        public List<Token> Tokens { get; set; }

        public override string ToString()
        {
            var result = this.PlayerName;
            return result;
        }
    }
}
