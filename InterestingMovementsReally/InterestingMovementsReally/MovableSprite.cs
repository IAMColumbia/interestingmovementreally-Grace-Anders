using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary;
using System;

namespace InterestingMovementsReally
{
    class MovableSprite : Sprite
    {
        InputHandler input;
        public Keys UpKey, DownKey, RightKey, LeftKey;
        public bool ButtonGuide, PlayerInfo;

        Color ButtonGuideColor, PlayerInfoColor;

        public enum SpriteMovementState { Jump, Gravity, FAST, slow };
        public SpriteMovementState CurrentMovementState = SpriteMovementState.Gravity;

        public Vector2 GoTo;

        public SpriteFont font;

        protected Vector2 ControlsPrintout;

        public MovableSprite(Game game) : base(game)
        {
            input = new InputHandler();

            ControlsPrintout = new Vector2(10, 450);//May Adjust
        }

        public override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>("Arial");

            ButtonGuide = true;
            PlayerInfo = true;

            ButtonGuideColor = Color.White;
            PlayerInfoColor = Color.White;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (CurrentMovementState != SpriteMovementState.Jump)
            {
                UpdateGravity();
            }

            KeepPlayerOnScreen();

            UpdateInput();

            base.Update(gameTime);

        }


        Vector2 PlayerInfoLocation = new Vector2(10, 10);
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, string.Format("Up Arrow  Up\nDown Arrow  Down\nRight Arrow  Right\nLeft Arrom  Left\nJ Jump Mode\nG Gravity Mode\nF FAST Mode\nS slow Mode\n+ Increase Speed\n- Decrease Speed\n, Toggle Player Info\n. Toggle Button Guide"), ControlsPrintout, ButtonGuideColor);

            spriteBatch.DrawString(font, string.Format("Game Mode: {0}\nLocation: {1}\nSpeed: {2}\nDirection: {3}\nGravityDir: {4}\nGravtyAccel: {5}\nMouse Click ({6}, {7})",
                this.CurrentMovementState, this.Location, this.Speed, this.Direction, this.GravityDir, this.GravityAccel, this.GoTo.X, this.GoTo.Y), PlayerInfoLocation, PlayerInfoColor);

            base.Draw(spriteBatch);
        }

        public void UpdateGravity()
        {
            this.Direction = this.Direction + (this.GravityDir * this.GravityAccel);
        }

        //public void UpdateSpeed()//Not for Jump
        //{
        //    if (Keyboard.GetState().GetPressedKeys().Length > 0)
        //    {
        //        Speed = 200;
        //    }
        //    else
        //        Speed = 0;
        //}

        public void KeepPlayerOnScreen()
        {

            if (CurrentMovementState == SpriteMovementState.Jump)
            {
                if ((Location.X > game.GraphicsDevice.Viewport.Width - Texture.Width) || (Location.X < 0))
                {
                    Direction = Direction * new Vector2(-1, 1);
                }

                if (Location.Y > 600)
                {
                    Location.Y = 600;
                    Direction.Y = 0;
                    OnGround = true;
                }
            }
            else
            {
                if (Location.X > game.GraphicsDevice.Viewport.Width - Texture.Width)
                {
                    //Negate X
                    Direction = Direction * new Vector2(-1, 1);
                    Location.X = game.GraphicsDevice.Viewport.Width - Texture.Width;
                }

                //X left
                if (Location.X < 0)
                {
                    //Negate X
                    Direction = Direction * new Vector2(-1, 1);
                    Location.X = 0;
                }

                //Y top
                if (Location.Y >
                        game.GraphicsDevice.Viewport.Height - Texture.Height)
                {
                    //Negate Y
                    Direction = Direction * new Vector2(1, -1);
                    Location.Y = game.GraphicsDevice.Viewport.Height - Texture.Height;
                }

                //Y bottom
                if (Location.Y < 0)
                {
                    //Negate Y
                    Direction = Direction * new Vector2(1, -1);
                    Location.Y = 0;
                }
            }
        }

        public void UpdateSpriteModeChange()
        {
            switch (CurrentMovementState)
            {
                case SpriteMovementState.Jump:
                    this.GravityDir = new Vector2(0, 1);
                    this.GravityAccel = 200.0f;
                    this.Friction = 10.0f;
                    this.OnGround = false;
                    //JumpDefault();
                    break;
                case SpriteMovementState.Gravity:
                    GravityDefault();
                    break;
                case SpriteMovementState.FAST:
                    GravityDefault();
                    this.Speed = 300;
                    //FastDefault();
                    break;
                case SpriteMovementState.slow:
                    GravityDefault();
                    this.Speed = 50;
                    //slowDefault();
                    break;
            }
        }

        public void GravityDefault()
        {
            this.GravityAccel = 1.8f;

            this.Friction = 0;
        }

        public void IncreaseSpeed(int value) { this.Speed = this.Speed + value; }

        public void DecreaseSpeed(int value) { this.Speed = this.Speed - value; }


        public void Travel(Vector2 destination)
        {

            if (Math.Abs(Location.X - destination.X) < Speed)
            {
                Location.X = destination.X;
            }
            else if (Location.X < destination.X)
            {
                Location.X += Speed;
            }
            else if (Location.X > destination.X)
            {
                Location.X -= Speed;
            }
        }

        public void VisibilityChange()
        {
            if (ButtonGuide == true)
            {
                ButtonGuideColor = Color.White;
                //ButtonGuideString = "Up Arrow  Up\nDown Arrow  Down\nRight Arrow  Right\nLeft Arrom  Left\nJ Jump Mode\nG Gravity Mode\nF FAST Mode\nS slow Mode\n + Increase Speed\n - Decrease Speed";
            }
            if (ButtonGuide == false)
            {
                ButtonGuideColor = Color.Transparent;
                //ButtonGuideString = "";
            }
            if (PlayerInfo == true)
            {
                PlayerInfoColor = Color.White;
            }
            if (PlayerInfo == false)
            {
                PlayerInfoColor = Color.Transparent;
            }
        }

        public void UpdateInput()
        {
            input.Update();

            #region Keyboard Movement
            if (CurrentMovementState != SpriteMovementState.Jump)
            {

                if (input.IsKeyDown(DownKey))
                {
                    GravityDir = new Vector2(0, 1);
                }
                if (input.IsKeyDown(UpKey))
                {
                    GravityDir = new Vector2(0, -1);
                }
                if (input.IsKeyDown(RightKey))
                {
                    GravityDir = new Vector2(1, 0);
                }
                if (input.IsKeyDown(LeftKey))
                {
                    GravityDir = new Vector2(-1, 0);
                }

            }
            else
            {
                if (input.WasKeyPressed(UpKey))
                {
                    Direction = Direction + new Vector2(0, JumpHeight);
                    OnGround = false;
                }

                if (OnGround)
                {
                    if ((!(input.IsHoldingKey(LeftKey))) && (!(input.IsHoldingKey(RightKey))))
                    {
                        if (Direction.X > 0)
                        {
                            Direction.X = MathHelper.Max(0, Direction.X - Friction);
                        }
                        else
                        {
                            Direction.X = MathHelper.Min(0, Direction.X + Friction);
                        }
                    }

                    if (input.IsHoldingKey(LeftKey))
                    {
                        Direction.X = MathHelper.Max((MaxSpeed * -1.0f), Direction.X - Acceleration);
                    }
                    if (input.IsHoldingKey(RightKey))
                    {
                        Direction.X = MathHelper.Min(MaxSpeed, Direction.X + Acceleration);
                    }
                }
            }
            #endregion


            #region Mode Change
            if (input.WasKeyPressed(Keys.J))
            {
                CurrentMovementState = SpriteMovementState.Jump;
            }
            if (input.WasKeyPressed(Keys.G))
            {
                CurrentMovementState = SpriteMovementState.Gravity;
            }
            if (input.WasKeyPressed(Keys.F))
            {
                CurrentMovementState = SpriteMovementState.FAST;
            }
            if (input.WasKeyPressed(Keys.S))
            {
                CurrentMovementState = SpriteMovementState.slow;
            }
            UpdateSpriteModeChange();
            #endregion

            #region Fun Buttons
            if (input.WasKeyPressed(Keys.R))
            {
                //Character turns the other direction
            }
            if (input.WasKeyPressed(Keys.OemPlus))
            {
                if (Speed < MaxSpeed)
                {
                    IncreaseSpeed(10);

                    //if (input.IsHoldingKey(Keys.OemPlus))
                    //{
                    //    while(Speed < MaxSpeed)
                    //    {
                    //        this.Speed = this.Speed + 10;
                    //    }
                    //}
                    //else if (input.WasKeyPressed(Keys.OemPlus))
                    //{
                    //    this.Speed = this.Speed + 10;
                    //}

                }

            }
            if (input.WasKeyPressed(Keys.OemMinus))
            {
                if (Speed > 0)
                {
                    DecreaseSpeed(10);
                }
            }

            #endregion

            #region View Buttons 
            if (input.WasKeyPressed(Keys.OemPeriod))
            {
                //Toggle Key Guide
                ButtonGuide = !ButtonGuide;
                VisibilityChange();
            }
            if (input.WasKeyPressed(Keys.OemComma))
            {
                //Toggle Player Info
                PlayerInfo = !PlayerInfo;
                VisibilityChange();
            }
            #endregion



            //try using Mouse Click and having the Player travel to where the user clicks?
            if (input.LeftButtonPressed())
            {
                GoTo.X = input.mouseState.X;
                GoTo.Y = input.mouseState.Y;

                if (CurrentMovementState == SpriteMovementState.Jump)
                {
                    Travel(GoTo);
                }

            }
        }

    }
}

