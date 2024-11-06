namespace Game
{
    public delegate void TetrisisHandler(object o, EventArgs e);
    public class EventArgs
    {
        public readonly int RowsCompleted;
        public EventArgs(int r)
        {
            RowsCompleted = r;
        }
    }
}
