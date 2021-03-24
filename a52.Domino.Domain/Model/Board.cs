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
        private int _START_X_POSITION = 13;
        private int _START_Y_POSITION = 4;
        private int _X_POSITION_SIZE = 28;
        private int _Y_POSITION_SIZE = 8;

        //private int lastDownX;
        //private int lastDownY;
        //private int lastUpX;
        //private int lastUpY;

        private Coords lastUpPoint;
        private Coords lastDownPoint;


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
            //this.lastUpX = this._START_X_POSITION;
            //this.lastUpY = this._START_Y_POSITION;
            //this.lastDownX = this._START_X_POSITION;
            //this.lastDownY = this._START_Y_POSITION;

            this.lastUpPoint = new Coords(this._START_X_POSITION, this._START_Y_POSITION);
            this.lastDownPoint = new Coords(this._START_X_POSITION, this._START_Y_POSITION);

        }

        public void Move(Movement movement)
        {
            this.Movements.Add(movement);
            this.LastPlayer = movement.CurrentPlayer;

            if (this.MoveCount.Equals(0))
            {
                this.UpValue = movement.CurrentToken.Up;
                this.DownValue = movement.CurrentToken.Down;
                SetPosition(movement, Direction.Up);
            }
            else
            {
                switch (movement.CurrentDirection)
                {
                    case Direction.Up:
                        if (movement.CurrentToken.Up.Equals(this.UpValue))
                            this.UpValue = movement.CurrentToken.Down;
                        else this.UpValue = movement.CurrentToken.Up;

                        SetPosition(movement, Direction.Up);

                        break;

                    case Direction.Down:
                        if (movement.CurrentToken.Up.Equals(this.DownValue))
                            this.DownValue = movement.CurrentToken.Down;
                        else this.DownValue = movement.CurrentToken.Up;

                        SetPosition(movement, Direction.Down);

                        break;

                    default:
                        throw new Exception("Direction has to be specify.");
                        break;
                }
            }

            this.MoveCount += 1;

        }

        #region Private methods

        private void SetPosition(Movement m, Direction d)
        {
            Coords point = new Coords();

            if (d == Direction.Up)
                point = getUpPoint();
            else point = getDownPoint();


            var p = new Position(point.X, point.Y);
            p.Movement = m;

            this.positions[point.X, point.Y] = p;

        }

        private Coords getUpPoint()
        {
            Coords result = new Coords(this.lastUpPoint.X, this.lastUpPoint.Y);


            /// Values when is the first movement of the game
            if (this.MoveCount == 0)
            {
                result.X = this._START_X_POSITION;
                result.Y = this._START_Y_POSITION;
            }
            /// movement before reaching the top
            else if (this.lastUpPoint.X > 0 && this.lastUpPoint.Y == this._START_Y_POSITION)
            {
                result.X--;
            }
            /// movemnt when you are in the top but not in the start left
            else if (this.lastUpPoint.X == 0 && this.lastUpPoint.Y > 0)
            {
                result.Y--;
            }
            /// movement when you are in the top left
            else
            {
                result.X ++;
            }

            this.lastUpPoint = result;

            return result;
        }

        private Coords getDownPoint()
        {

            Coords result = new Coords(lastDownPoint.X, lastDownPoint.Y);

            /// Values when is the first movement of the game
            if (this.MoveCount == 0)
            {
                result.X = this._START_X_POSITION;
                result.Y = this._START_Y_POSITION;
            }
            /// movement before reaching the button
            else if (this.lastDownPoint.X < (this._X_POSITION_SIZE - 1) && this.lastDownPoint.Y == this._START_Y_POSITION)
            {
                result.X++;
            }
            /// movemnt when you are in the button but not in the end right
            else if (this.lastDownPoint.X == (this._X_POSITION_SIZE - 1) && this.lastDownPoint.Y < (this._Y_POSITION_SIZE - 1))
            {
                result.Y++;
            }
            /// movement when you are in the top right
            else
            {
                result.X--;
            }

            lastDownPoint = result;

            return result;
        }




        #endregion

    }
    struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        
        
    }

    public enum Direction
    {
        Auto = 0,
        Up = 1,
        Down = 2
    }
}
