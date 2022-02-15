using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary
{
    public class Sprite
    {
        protected Vector2 Direction;
        protected Vector2 Location;
        protected Vector2 Origin;
        protected float Rotate;
        protected float Speed;
        protected float MaxSpeed;

        protected Vector2 GravityDir;
        protected float GravityAccel;

        protected float Friction;
        protected float Acceleration;
        protected int JumpHeight;
        protected bool OnGround;

        protected Color Color;
        protected Texture2D Texture;
        public string TextureName { get; set; }

        protected Game game;

        public Sprite(Game game)
        {
            this.game = game;
        }

        public virtual void LoadContent()
        {
            if (string.IsNullOrEmpty(TextureName))
            {
                TextureName = "LittleGuy";//Not circular WARNING
            }

            Texture = game.Content.Load<Texture2D>(TextureName);

            this.Location = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);

            this.Direction = new Vector2(1, 0);

            this.Origin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);

            Speed = 100;
        }

        float time;
        public virtual void Update(GameTime gameTime)
        {
            time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            this.Location = this.Location + ((this.Direction * this.Speed) * (time / 1000));

            this.Direction = this.Direction + (this.GravityDir * this.GravityAccel) * (time / 1000);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, new Rectangle((int)this.Location.X, (int)this.Location.Y, (int)(this.Texture.Width), (int)(this.Texture.Height)), null, Color, MathHelper.ToRadians(this.Rotate), this.Origin, SpriteEffects.None, 0);

        }
    
    }
}
