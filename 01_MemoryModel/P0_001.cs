using NUnit.Framework;

namespace CSharpCore.MemoryModel
{
    public class P0_001_ValueVsReference
    {
        struct Point
        {
            public int X, Y;
            public int[] arr;

            public Point()
            {
                X = 1;
                Y = 2;
                arr = [1, 2, 3];
            }

            // public static bool operator ==(Point a, Point b)
            // {
            //     return a.X == b.X && a.Y == b.Y;
            // }

            // public static bool operator !=(Point a, Point b)
            // {
            //     return a.X != b.X && a.Y != b.Y;
            // }
        }

        class PointRef
        {
            public int X, Y;
            public int[] arr = { 1, 2, 3 };

            public PointRef(int x, int y)
            {
                X = x; Y = y;
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

        private void ChangePoint(ref Point p)
        {
            p.X = 500;
        }

        private void ChangePoint(Point p)
        {
            p.X = 500;
        }

        [Test]
        public void P0_001()
        {

            Console.WriteLine("VALUE");

            Point p1 = new Point();
            Point p2 = p1;
            Console.WriteLine("Equal?: " + (p1.Equals(p2)));
            p2.X = 5; //value
            p2.arr[0] = 100; //reference
            p1.arr[2] = 200;
            Assert.That(p2.arr, Is.EqualTo(p1.arr)); //values
            Assert.That(p2.arr, Is.SameAs(p1.arr)); //reference

            ChangePoint(p1);
            ChangePoint(ref p2);
            PrintPoint(p1);
            PrintPoint(p2);
            Assert.That(p2.arr, Is.SameAs(p1.arr)); //reference
            Console.WriteLine("Equal?: " + p1.Equals(p2));


            Console.WriteLine("\n===================\n");



            Console.WriteLine("REF");
            PointRef p1r = new PointRef(2,3);
            PointRef p2r = p1r;
            p1r.X = 6;
            Console.WriteLine(p2r.X);

            Assert.That(p2r, Is.SameAs(p1r)); //reference
            Assert.That(p2r, Is.EqualTo(p1r)); //reference
            Assert.That(p2r.X, Is.EqualTo(p1r.X));


            Console.WriteLine("\n===================\n");

            Point[] points = new Point[3];
            foreach (Point p in points)
            {
                // p.X = 5; cannot modify because its a struct (value type)
                //p is readonly
            }

            PointRef[] pointRefs = new PointRef[3];
            for (int x = 0; x < 3; x++)
            {
                pointRefs[x] = new PointRef(1,2);
            }

            foreach (var p in pointRefs)
            {
                p.X = 6;
            }

            Console.WriteLine(pointRefs[0].X);



            Console.WriteLine("\n===================\n");


            int i = 5;
            object o = i; //boxing
            int j = (int)o; //unboxing

            i = 10;
            Console.WriteLine((int)o);

        }
    }
}