namespace AUD.Graphs
{
    public class UndirectedEdge : IEdge
    {
        public int Start { get; set; }
        public int End { get; set; }

        public UndirectedEdge(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override bool Equals(object obj)
        {
            var edge = obj as UndirectedEdge;
            if (edge == null)
                return false;

            return edge.Start == Start && edge.End == End ||
                edge.End == Start && edge.Start == End;
        }

        public override string ToString()
        {
            return "(" + Start + "," + End + ")";
        }

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + Start.GetHashCode();
            hashCode = hashCode * -1521134295 + End.GetHashCode();
            return hashCode;
        }
    }
}