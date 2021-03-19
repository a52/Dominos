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

        /// <summary>
        ///  - Positions[]: is a matrix of position in the space (20x8) where the movement are placed
        /// </summary>
        public Position[,] Positions
        {
            get
            {
                return this.positions;
            }
        }

        public int LenghtX { get { return this._X_POSITION_SIZE; } }
        public int LenghtY { get { return this._Y_POSITION_SIZE; } }

        private Position[,] positions;
        private int _START_X_POSITION = 14;
        private int _START_Y_POSITION = 4;
        private int _X_POSITION_SIZE = 28;
        private int _Y_POSITION_SIZE = 8;

        private int lastDownX;
        private int lastDownY;
        private int lastUpX;
        private int lastUpY;


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
            this.positions = new Position[_X_POSITION_SIZE, _Y_POSITION_SIZE];
            this.lastUpX = this._START_X_POSITION;
            this.lastUpY = this._START_Y_POSITION;
            this.lastDownX = this._START_X_POSITION;
            this.lastDownY = this._START_Y_POSITION;

        }

        public void Move(Movement movement)
        {
            this.Movements.Add(movement);
            this.LastPlayer = movement.CurrentPlayer;

            if (this.MoveCount.Equals(0))
            {
                this.UpValue = movement.CurrentToken.Up;
                this.DownValue = movement.CurrentToken.Down;
                SetUpPosition(movement);
            }
            else
            {
                switch (movement.CurrentDirection)
                {
                    case Direction.Up:
                        if (movement.CurrentToken.Up.Equals(this.UpValue))
                            this.UpValue = movement.CurrentToken.Down;
                        else this.UpValue = movement.CurrentToken.Up;

                        SetUpPosition(movement);

                        break;

                    case Direction.Down:
                        if (movement.CurrentToken.Up.Equals(this.DownValue))
                            this.DownValue = movement.CurrentToken.Down;
                        else this.DownValue = movement.CurrentToken.Up;

                        SetUpPosition(movement);

                        break;
                }
            }

            this.MoveCount += 1;
            
        }

        #region Private methods

        private void SetPosition(Movement m, Position p)
        {

        }

        private void SetUpPosition(Movement m)
        {

            var p = new Position(this.lastUpX, this.lastUpY);
            p.Movement = m;
            this.lastUpX--;

            this.Positions[p.X, p.Y] = p;
            
        }
        private void SetDownPosition(Movement m)
        {

            var p = new Position(this.lastDownX, this.lastDownY);
            p.Movement = m;
            this.lastDownX++;
            this.Positions[p.X, p.Y] = p;
        }





        #endregion

    }


    public enum Direction
    {
        Auto = 0,
        Up = 1,
        Down = 2
    }
}
