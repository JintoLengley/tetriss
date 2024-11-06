using System.Data;

namespace Game
{
    class BlockClass : BaseClass
    {
        public RotationEnum Angel
        {
            get
            {
                return m_block.angel;
            }
            set
            {
                m_block.angel = value;
            }
        }
        public BlockTypeNum Type
        {
            get
            {
                return m_block.type;
            }
            set
            {
                m_block.type = value;
            }
        }
        public Point Location
        {
            get
            {
                return new Point(m_blockpos.x, m_blockpos.y);
            }
            set
            {
                m_blockpos = value;
            }

        }
        public int Size
        {
            get
            {
                return block_size;
            }
        }
        public ConsoleColor Color(BlockTypeNum typeBlock)
        {
            switch (typeBlock)
            {
                case BlockTypeNum.block01:
                    return ConsoleColor.Red;
                case BlockTypeNum.block02:
                    return ConsoleColor.Blue;
                case BlockTypeNum.block03:
                    return ConsoleColor.Magenta;
                case BlockTypeNum.block04:
                    return ConsoleColor.Yellow;
                case BlockTypeNum.block05:
                    return ConsoleColor.Cyan;
                case BlockTypeNum.block06:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.Green;

            }
        }
        public StructBlock Generate()
        {
            Random rnd = new Random();
            return new StructBlock((RotationEnum)rnd.Next(0, Enum.GetNames(typeof(RotationEnum)).Length), 
                   (BlockTypeNum)rnd.Next(0, Enum.GetNames(typeof(BlockTypeNum)).Length));
        }
        public windowRect Rotate(RotationEnum newAngel)
        {
            windowRect wrBlock = new windowRect();
            Angel = newAngel;
            Build();
            Adjustment(ref wrBlock);
            return wrBlock;
        }
        public void Build()
        {
            arrBlock = GetBlockData(new StructBlock(Angel, Type));
        }
        public bool[] GetBlockData(StructBlock structBlock)
        {
            bool[] arrData = new bool[block_size << 2];
            switch (structBlock.type)
            { 
            case BlockTypeNum.block01:
                    if (structBlock.angel.Equals(RotationEnum.deg0) ||
                        structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[2] = true;
                        arrData[6] = true;
                        arrData[10] = true;
                        arrData[14] = true;
                    }
                    else 
                    {
                        arrData[12] = true;
                        arrData[13] = true;
                        arrData[14] = true;
                        arrData[15] = true;
                    }
                    break;
                    case BlockTypeNum.block02:
                    arrData[0] = true;
                    arrData[1] = true;
                    arrData[4] = true;
                    arrData[5] = true;
                    break;
                    case BlockTypeNum.block03:
                    if (structBlock.angel.Equals(RotationEnum.deg0) ||
                        structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[7] = true;
                        arrData[9] = true;
                    }
                    else
                    {
                        arrData[1] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[10] = true;
                    }
                    break;
                case BlockTypeNum.block04:
                    if (structBlock.angel.Equals(RotationEnum.deg0) ||
                        structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[4] = true;
                        arrData[5] = true;
                        arrData[9] = true;
                        arrData[10] = true;
                    }
                    else
                    {
                        arrData[2] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[9] = true;
                    }
                    break;
                case BlockTypeNum.block05:
                    if (structBlock.angel.Equals(RotationEnum.deg0))
                      
                    {
                        arrData[4] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[9] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg90))
                    {
                        arrData[1] = true;
                        arrData[4] = true;
                        arrData[5] = true;
                        arrData[9] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[5] = true;
                        arrData[8] = true;
                        arrData[9] = true;
                        arrData[10] = true;
                    }
                    else
                    {
                        arrData[1] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[9] = true;
                    }
                    break;
                case BlockTypeNum.block06:
                    if (structBlock.angel.Equals(RotationEnum.deg0))
                        {
                        arrData[4] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[8] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg90))
                    {
                        arrData[0] = true;
                        arrData[1] = true;
                        arrData[5] = true;
                        arrData[9] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[6] = true;
                        arrData[8] = true;
                        arrData[9] = true;
                        arrData[10] = true;
                    }
                    else
                    {
                        arrData[1] = true;
                        arrData[5] = true;
                        arrData[9] = true;
                        arrData[10] = true;
                    }
                    break;
                case BlockTypeNum.block07:
                    if (structBlock.angel.Equals(RotationEnum.deg0))
                    {
                        arrData[4] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                        arrData[10] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg90))
                    {
                        arrData[1] = true;
                        arrData[5] = true;
                        arrData[8] = true;
                        arrData[9] = true;
                    }
                    else if (structBlock.angel.Equals(RotationEnum.deg180))
                    {
                        arrData[4] = true;
                        arrData[8] = true;
                        arrData[9] = true;
                        arrData[10] = true;
                    }
                    else
                    {
                        arrData[1] = true;
                        arrData[2] = true;
                        arrData[5] = true;
                        arrData[6] = true;
                    }
                    break;
            }
            return arrData;
        }

        public void Adjustment(ref windowRect wrBlock)
        {
        Adjustment(ref wrBlock, arrBlock);
        }
        public void Adjustment(ref windowRect wrBlock, bool[] arrData)
        { 
            wrBlock = new windowRect();
            int col;
            int row;
            bool isAdj;
            isAdj = true;
            for (col = 0; col < block_size; col++)
            {
                for (row = 0; row < block_size; row++)
                {
                    if (arrData[col + row * block_size])
                    {
                        isAdj = false;
                        break;
                    }
                    if (isAdj)
                    {
                        wrBlock.left++;
                    }
                    else
                        break;

                }
            }
            isAdj = true;
            for (row = 0; row < block_size; row++)
            {
                for (col = 0; col < block_size; col++)
                {
                    if (arrData[col + row * block_size])
                    {
                        isAdj = false;
                        break;
                    }
                    if (isAdj)
                    {
                        wrBlock.top++;
                    }
                    else
                        break;
                }
            }
            isAdj = true;
            for (col = block_size - 1; col >= 0; col--)
            {
                for(row = 0; row < block_size; row++)
                {
                    if(arrBlock[col + row * block_size])
                    {
                        isAdj = false;
                        break;  
                    }
                    if(isAdj)
                    {
                        wrBlock.width++;
                    }
                    else
                        break;

                }
            }
            wrBlock.width = block_size - (wrBlock.left + wrBlock.width);
            isAdj = true;
            for(row = block_size - 1; row >= 0; row--)
            {
                for (col = 0; col < block_size; col++)
                {
                    if (arrBlock[col + row * block_size])
                    {
                        isAdj = false;
                    }
                    if (isAdj)
                    {
                        wrBlock.height++;
                    }
                    else
                        break;
                }
                
            }
            wrBlock.height = block_size - (wrBlock.top + wrBlock.height);



        }
        public void Draw(Point pt, windowRect wrBlockAdj, bool isRotateUpdate)
        {
            if(!Location.x.Equals(pt.x) || !Location.y.Equals(pt.y) || isRotateUpdate)
            {
                TetrisClass.DrawField(pt, wrBlockAdj);
                Console.ForegroundColor = Color(Type);
                for(int row = wrBlockAdj.top; row < wrBlockAdj.top + wrBlockAdj.height; row++)
                {
                    for(int col = wrBlockAdj.left; col < wrBlockAdj.left + wrBlockAdj.width; col++)
                    {
                        if (arrBlock[col + row * block_size])
                        {
                            Console.SetCursorPosition(pt.x + col - wrBlockAdj.left, pt.y + row - wrBlockAdj.top);
                            Console.WriteLine("#");
                        }    
                    }
                    Console.ResetColor();
                    Location = pt;
                }
            }
        }
        public void Preview(Point pt, StructBlock structBlock)
        {
            windowRect wrBlockAdj = new windowRect();
            bool[] arrData = GetBlockData(structBlock);

            Adjustment(ref wrBlockAdj, arrData);
            Console.ForegroundColor = Color(structBlock.type);
            for(int row = wrBlockAdj.top; row < wrBlockAdj.top + wrBlockAdj.height; row++)
            {
                for(int col = wrBlockAdj.left; col < wrBlockAdj.left+wrBlockAdj.width; col++)
                {
                    if (arrData[col + row * block_size])
                    {
                        Console.SetCursorPosition(pt.x + col - wrBlockAdj.left - wrBlockAdj.width / 2, pt.y+row - wrBlockAdj.top - wrBlockAdj.height / 2);
                        Console.WriteLine("#");
                    }
                }
                Console.ResetColor();
            }
        }
        public RotationEnum getNextAngle(int rotateOption)
        {
            if (rotateOption.Equals(0))
                switch(Angel)
                {
                    case RotationEnum.deg0:
                        return RotationEnum.deg90;
                    case RotationEnum.deg90:
                        return RotationEnum.deg180;
                    case RotationEnum.deg180:
                        return RotationEnum.deg270;
                    default:
                        return RotationEnum.deg0;
                }
            else
                switch(Angel)
                {
                    case RotationEnum.deg0:
                        return RotationEnum.deg270;
                    case RotationEnum.deg270:
                        return RotationEnum.deg180;
                    case RotationEnum.deg180:
                        return RotationEnum.deg90;
                    default:
                        return RotationEnum.deg0;
                }
        }
        public void Assign(StructBlock sbNew)
        {
            Angel = sbNew.angel;
            Type = sbNew.type;
        }

    }
    
}
