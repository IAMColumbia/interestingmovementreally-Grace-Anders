using MonogameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InterestingMovementsReally
{
    class Player : MovableSprite
    {
        public Player(Game game) : base(game)
        {
            this.Direction = new Vector2(1, 0);
            this.Speed = 100;

            this.MaxSpeed = 300;
            this.JumpHeight = -40;
            this.Acceleration = 10;
            this.Color = Color.Cyan;

            this.GravityDir = new Vector2(1, 0);
            this.GravityAccel = 1.8f;

            this.UpKey = Keys.Up;
            this.DownKey = Keys.Down;
            this.RightKey = Keys.Right;
            this.LeftKey = Keys.Left;
        }
    }
}
