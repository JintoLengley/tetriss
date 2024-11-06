using System.Drawing;

namespace Game
{
    struct StructBlockStyle
    {
        public ConsoleColor color;
        public bool isBlock;
        public StructBlockStyle(ConsoleColor newColor, bool newisBlock)
        {
            color = newColor;
            isBlock = newisBlock;
        }
    }
}