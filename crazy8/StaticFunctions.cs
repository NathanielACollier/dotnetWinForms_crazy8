using System;


namespace crazy8
{
    class StaticFunctions
    {

        /*
         Used in bubble sort 
         */
        private static void swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        /*
        Variation on bubble sort that does parallel arrays
        For future reference bubble sort is the simplest sort to code but one of the more difficult to understand
        */
        public static void BubbleSort(int[] target, int[] parallel)
        {
            // target is the one that will be sorted by
            // parallel is parallel to target so when
            // target is swaped parallel must be swaped also
            // The bubble sort is a very simple sort
            for (int i = target.Length; i >= 1; --i)
            {
                // this line used to read j<= i but this of course causes a IndexOutOfBounds exception
                //  so I changed it to j<i. I don't have time right now to research the algorithm and figure out
                //  what it is supposed to be.
                for (int j = 1; j < i; ++j)
                {
                    if (target[j - 1] > target[j])
                    {
                        swap(ref target[j - 1], ref target[j]);
                        // this means we also swap the same in parallel
                        swap(ref parallel[j - 1], ref parallel[j]);
                    }
                }
            }
        }
    }
}
