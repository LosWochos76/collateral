public class ExpressionSolver
{
    public static double Solve(TreeNode<string> node)
    {
        if (node == null)
            return 0;

        switch (node.Value)
        {
            case "+": return Solve(node.Left) + Solve(node.Right);
            case "-": return Solve(node.Left) - Solve(node.Right);
            case "*": return Solve(node.Left) * Solve(node.Right);
            case "/": return Solve(node.Left) / Solve(node.Right);
            case "^": return Math.Pow(Solve(node.Left), Solve(node.Right));
            default: return Convert.ToDouble(node.Value);
        }
    }
}