namespace AUD.Graphs
{
    public class DirectedEdge : UndirectedEdge
    {
        public double Length { get; set; }

        public DirectedEdge(int start, int end) : base(start, end)
        {
            Length = 1;
        }

        public DirectedEdge(int start, int end, double length) : base(start, end)
        {
            Length = length;
        }

        public override bool Equals(object obj)
        {
            var n = obj as DirectedEdge;
            if (n == null)
                return false;

            return n.Start == Start && n.End == End;
        }

        public DirectedEdge Reverse()
        {
            return new DirectedEdge(End, Start);
        }
    }
}