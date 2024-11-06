using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class TetrisClass : BaseClass
    {
        public event TetrisisHandler ProcessEvent;
        public BlockClass block = new BlockClass();

        public TetrisClass(windowRect wrField)
        {
            TetrisField = wrField;
            BuildField();
        }
        public void BuildField()
        {
            arrField = new StructBlockStyle[TetrisField.width * TetrisField.height];
        }
        public static void DrawField(Point pt, windowRect wrBlockAdj)
        {
            int w = TetrisField.width;
            int h = TetrisField.height;
            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    if (((StructBlockStyle)arrField[col + row * w]).isBlock)
                    {
                        Console.ForegroundColor = ((StructBlockStyle)arrField[col +row * w]).color;
                        Console.SetCursorPosition(TetrisField.left + col, TetrisField.top + row);
                        Console.Write("▓");
                    }
                    else
                    {
                        Console.SetCursorPosition(TetrisField.left + col, TetrisField.top + row);
                        Console.Write(" ");
                    }
                }

            }
            Console.ResetColor();
        }
        public bool Collided(Point pt, windowRect wrBlockAdj)
        {
            int sx = pt.x - TetrisField.left;
            int sy = pt.y - TetrisField.top;
            int w = TetrisField.width;
            int blockIndex;
            int fieldIndex;
            for (int row = 0; row < wrBlockAdj.height; row++)
            {
                for (int col = 0; col < wrBlockAdj.width; col++)
                {
                    blockIndex = (wrBlockAdj.left + col) + ((wrBlockAdj.top + row) * block_size);
                    fieldIndex = ((sx + sy * w) + col) + row * w;
                    if (arrBlock[blockIndex] && ((StructBlockStyle)arrField[fieldIndex]).isBlock)
                    {
                        return true;
                    }

                }
            }
            return false;

        }
        public void SentToField(Point pt, windowRect wrBlockAdj)
        {
            int blockIndex;
            int fieldIndex;
            for (int row = 0; row < wrBlockAdj.height; row++)
            {
                for (int col = 0; col < wrBlockAdj.width; col++)
                {
                    blockIndex = (wrBlockAdj.left + col) + (wrBlockAdj.top + row) * block_size;
                    fieldIndex = (pt.x - TetrisField.left + col) + (pt.y - TetrisField.top + row) * TetrisField.width;

                    if (arrBlock[blockIndex])
                        arrField[fieldIndex] = new StructBlockStyle(block.Color(block.Type), true);
                }
                
            }
            ProcessRow();
        }
        public void ProcessRow()
        {
            int w = TetrisField.width;
            int h = TetrisField.height;
            int rowCounter = h - 1;
            int rowTotal = 0;
            bool isFullLine = true;
            StructBlockStyle[] arrData = new StructBlockStyle[TetrisField.width * TetrisField.height];
            for (int row = h - 1; row >= 0; row--)
            {
                for (int col = w - 1; (col >= 0) && isFullLine; col--)
                {
                    if (!((StructBlockStyle)arrField[row * w + col]).isBlock)
                    {
                        isFullLine = false;
                    }
                }
                if(!isFullLine)
                {
                    for (int col = w - 1; col >= 0; col--)
                    {
                        arrData[col + rowCounter * w] = arrField[row * w + col];
                    }
                    rowCounter--;
                    isFullLine = true;
                }
                else
                {
                    rowTotal++;
                }
            }
            arrField = arrData;
            EventArgs e = new EventArgs(rowTotal);
            RaisesEvent((object)this, e);
        }
        private void RaisesEvent(object o, EventArgs e)
        {
            if (ProcessEvent != null)
                ProcessEvent(o, e);
        }
            

             
    }
       
}

             
         
       

