using System.Runtime.InteropServices;
using NUnit.Framework;

namespace CSharpCore.MemoryModel
{
    public class P0_002_StackHeap
    {
        struct Point
        {
            public int X, Y;
            public int[] arr;
            public Point()
            {
                arr = [1,2,3];
            }
        }

        private void PrintPoint(Point a)
        {
            Console.Write("X: " + a.X + "  Y: " + a.Y + "      arr: ");
            foreach (int i in a.arr)
            {
                Console.Write(i + ", ");
            }
            Console.Write("\n");
        }

        [Test]
        public void P0_002()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point());
            points.Add(new Point());
            points.Add(new Point());

            PrintPoint(points[0]);
            // points[0].X = 1000; cannot modify directly
            CollectionsMarshal.AsSpan(points)[0].X = 100;
            PrintPoint(points[0]);

            //OR

            Span<Point> span = CollectionsMarshal.AsSpan(points);
            span[1].Y = 1200;

            //Or

            ref Point p = ref span[2];
            p.X = 21;

            PrintPoint(points[0]);
            PrintPoint(points[1]);
            PrintPoint(points[2]);
        }
    }
}