using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a52.Domino.Domain.Model
{
    /*
     * Board
	- Positions[]: is a matrix of position in the space (20x20) where the movement are placed
		- Movements
	- CurrentUpValue: the value of the upper direction that the tab has to have.                   *
	- CurrentLowValue: the value of the lower direction that the tab has to have.                  *
	- Movements[]: List of movements that had been made.                                           *
	- MoveCount: Counter for the movements of the game                                             *
	- LastPlayer: the last player that make the movement                                           *

	+ Move(movement): make a movement. Put a domino in the board

     * 
     * */
    public class Board
    {
        /// <summary>
        /// - CurrentUpValue: the value of the upper direction that the tab has to have.
        /// </summary>
        public int UpValue { get; private set; }

        /// <summary>
        /// - CurrentLowValue: the value of the lower direction that the tab has to have.
        /// </summary>
        public int DownValue { get; private set; }

        /// <summary>
        /// - Movements[]: List of movements that had been made.
        /// </summary>
        public List<Movement> Movements { get; private set; }

        /// <summary>
        /// - MoveCount: Counter for the movements of the game
        /// </summary>
        public int MoveCount { get; private set; }

        /// <summary>
        /// - LastPlayer: the last player that make the movement
        /// </summary>
        public Player LastPlayer { get; private set; }

        public Board()
        {
            Init();
        }

        void Init()
        {

            this.UpValue = 0;
            this.DownValue = 0;

            this.Movements = new List<Movement>();
            this.MoveCount = 0;

            this.LastPlayer = null;

        }

        public void Move(Movement movement)
        {
            this.Movements.Add(movement);
            this.LastPlayer = movement.CurrentPlayer;

            if (this.MoveCount.Equals(0))
            {
                this.UpValue = movement.CurrentToken.Up_Value;
                this.DownValue = movement.CurrentToken.Down_Value;
            }
            else
            {
                switch (movement.CurrentDirection)
                {
                    case Direction.Up:
                        if (movement.CurrentToken.Up_Value.Equals(this.UpValue))
                            this.UpValue = movement.CurrentToken.Down_Value;
                        else this.UpValue = movement.CurrentToken.Up_Value;
                        break;

                    case Direction.Down:
                        if (movement.CurrentToken.Up_Value.Equals(this.DownValue))
                            this.DownValue = movement.CurrentToken.Down_Value;
                        else this.DownValue = movement.CurrentToken.Up_Value;

                        break;
                }
            }

            this.MoveCount += 1;

        }

    }


    public enum Direction
    {
        Auto = 0,
        Up = 1,
        Down = 2
    }
}
