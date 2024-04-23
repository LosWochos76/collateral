public class HausVomNikolaus
{
    private int counter;
    private int[] current_path;

    public void FindSolutions()
    {
        counter = 0;
        current_path = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        do
        {
            if (IsValidPath())
                Print();
        }
        while (HasNextCandidate());
    }

    private void Print()
    {
        counter++;
        Console.WriteLine("{0}: {1}", 
            counter, 
            string.Join(" -> ", current_path.Select(x => x.ToString()).ToArray())
        );
    }

    private bool HasNextCandidate()
    {
        if (current_path[8] == 5)
            return false;

        current_path[0]++;
        for (int i = 0; i < 9; i++)
        {
            if (current_path[i] == 6)
            {
                current_path[i] = 1;
                current_path[i + 1]++;
            }
        }

        return true;
    }

    private bool IsValidPath()
    {
        return !ContainsForbiddenEdge() &&
            !HasTwoNodesInSeries() &&
            ContainsEachEdgeOnlyOnce();
    }

    private bool ContainsForbiddenEdge()
    {
        for (int i = 1; i < 9; i++)
        {
            if (current_path[i - 1] == 1 && current_path[i] == 5)
                return true;

            if (current_path[i - 1] == 5 && current_path[i] == 1)
                return true;

            if (current_path[i - 1] == 2 && current_path[i] == 5)
                return true;

            if (current_path[i - 1] == 5 && current_path[i] == 2)
                return true;
        }

        return false;
    }

    private bool HasTwoNodesInSeries()
    {
        for (int i = 1; i < 9; i++)
        {
            if (current_path[i - 1] == current_path[i])
                return true;
        }

        return false;
    }

    private bool ContainsEachEdgeOnlyOnce()
    {
        for (int i = 1; i < 9; i++)
        {
            if (EdgeCount(current_path[i], current_path[i - 1]) > 1)
                return false;
        }

        return true;
    }

    private int EdgeCount(int a, int b)
    {
        int count = 0;
        for (int i = 1; i < 9; i++)
            if (current_path[i - 1] == a &&  current_path[i] == b || 
                current_path[i - 1] == b &&  current_path[i] == a)
                count++;

        return count;
    }
}