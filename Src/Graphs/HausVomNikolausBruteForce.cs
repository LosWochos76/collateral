using System;
using System.Collections.Generic;

namespace AUD.Graphs
{
    public class HausVomNikolausBruteForce
    {
        private int[] path = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        private List<Path> paths = new List<Path>();

        public List<Path> FindSolutions()
        {
            while (path[0] < 6)
            {
                if (IsValidPath())
                    AddPath();

                NextCandidate();
            }

            return paths;
        }

        private void NextCandidate()
        {
            path[8]++;

            for (int i = 8; i > 0; i--)
            {
                if (path[i] == 6)
                {
                    path[i] = 1;
                    path[i - 1]++;
                }
            }
        }

        private void AddPath()
        {
            var p = new Path();
            for (int i = 1; i < 9; i++)
                p.PushBack(new UndirectedEdge(path[i - 1], path[i]));

            paths.Add(p);
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
                if (path[i - 1] == 1 && path[i] == 5)
                    return true;

                if (path[i - 1] == 5 && path[i] == 1)
                    return true;

                if (path[i - 1] == 2 && path[i] == 5)
                    return true;

                if (path[i - 1] == 5 && path[i] == 2)
                    return true;
            }

            return false;
        }

        private bool HasTwoNodesInSeries()
        {
            for (int i = 1; i < 9; i++)
            {
                if (path[i - 1] == path[i])
                    return true;
            }

            return false;
        }

        private bool ContainsEachEdgeOnlyOnce()
        {
            for (int i = 1; i < 9; i++)
            {
                if (EdgeCount(path[i], path[i - 1]) > 1)
                    return false;
            }

            return true;
        }

        private int EdgeCount(int a, int b)
        {
            int count = 0;
            for (int i = 1; i < 9; i++)
                if (path[i - 1] == a && path[i] == b || path[i - 1] == b && path[i] == a)
                    count++;

            return count;
        }
    }
}