namespace Game
{
    class BaseClass
    {
        protected static int block_size = 4;

        protected static bool[] arrBlock = new bool[block_size << 2];
        protected static windowRect TetrisField = new windowRect();
        protected static Point m_blockpos = new Point();
        protected static StructBlock m_block = new StructBlock();
        protected static StructBlockStyle[] arrField;    
    }
}
