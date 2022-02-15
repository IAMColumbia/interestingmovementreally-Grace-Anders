using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary
{
    public class InputHandler
    {
        private KeyboardState pastKeyboardState;
        private KeyboardState keyboardState;

        private MouseState pastMouseState;
        public MouseState mouseState;


        public InputHandler()
        {
            pastKeyboardState = Keyboard.GetState();
            pastMouseState = Mouse.GetState();
        }

        #region Check Keyboard State
        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && pastKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && pastKeyboardState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && pastKeyboardState.IsKeyUp(key));
        }
        #endregion

        #region Check Mouse State
        //Tried abstracting to "IsClickDown" while sending a ButtonState
        public bool RightButtonPressed()
        {

            return (mouseState.RightButton == ButtonState.Pressed && pastMouseState.RightButton == ButtonState.Released);
        }

        public bool LeftButtonPressed()
        {
            return (mouseState.LeftButton == ButtonState.Pressed && pastMouseState.LeftButton == ButtonState.Released);
        }
        #endregion

        public void Update()
        {
            pastKeyboardState = keyboardState;
            pastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }
    }
}

