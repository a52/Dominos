using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    /*
Position
	is a coordinate in the board where can be placed tokens by movements. Will be managed by the board.
	- X: X axis position
	- Y: Y axis position
	- Movement: Referent to the movement that is contained
	- IsEmpty: boolean value that indicated if there are a movement contained
	
	+ GetPlayer: player of the movement. reference to the movement
	+ GetGetToken: token of the movement. Reference to the movement
    */
    public class Position
    {
        /// <summary>
        /// X: X axis position
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// - Y: Y axis position
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// - Movement: Referent to the movement that is contained
        /// </summary>
        public Movement Movement { get; set; }

        /// <summary>
        /// - IsEmpty: boolean value that indicated if there are a movement contained
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                var result = true;

                result = (this.Movement == null);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// + GetPlayer: player of the movement. reference to the movement
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            Player result = null;

            if (this.Movement != null)
                result = this.Movement.CurrentPlayer;

            return result;
        }

        /// <summary>
        /// + GetGetToken: token of the movement. Reference to the movement
        /// </summary>
        /// <returns></returns>
        public Token GetToken()
        {
            Token result = null;

            if (this.Movement != null)
                result = this.Movement.CurrentToken;

            return result;
        }
    }
}
