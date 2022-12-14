using System;

namespace dziennik
{
    public class Statistics
    {

        public double Max;

        public double Min;

        public double Sum;

        public int Count;

        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }

        public Statistics()
        {
            Count = 0;
            Sum = 0.00;
            Max = double.MinValue;
            Min = double.MaxValue;
        }

        public void Add(double number)
        {
            Sum += number;
            Count++;
            Min = Math.Min(Min, number);
            Max = Math.Max(Max, number);
        }
    }
}
