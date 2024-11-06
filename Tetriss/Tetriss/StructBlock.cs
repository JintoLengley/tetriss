namespace Game
{
    struct StructBlock
    {
        public RotationEnum angel;
        public BlockTypeNum type;
        public StructBlock(RotationEnum newAngel, BlockTypeNum newType)
        { 
        angel = newAngel;
        type = newType;
        }
    }
}