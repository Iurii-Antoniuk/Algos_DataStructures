using System;
using System.Collections.Concurrent;
using System.Globalization;

namespace Sorting
{
    public class Sorting_Repository
    {
        private static void Swap(int[]array, int i, int j)
        {
            if (i == j)
                return;
            else
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        public static void BubbleSort(int[] array)
        {
            for (int partInd = array.Length-1; partInd > 0; partInd--)
            {
                for (int i = 0; i < partInd; i++)
                {
                    if (array[i] > array[i + 1])
                        Swap(array, i, i + 1);
                }
            }
        }

        public static void SelectionSort(int[] array)
        {
            for (int partInd = array.Length - 1; partInd > 0; partInd--)
            {
                int largestInd = 0;
                for (int i = 1; i <= partInd; i++)
                {
                    if (array[i] > array[largestInd])
                        largestInd = i;
                }
                Swap(array, largestInd, partInd);
            }
        }

        public static void InsertionSort(int[] array)
        {
            for (int partInd = 1; partInd < array.Length; partInd++)
            {
                int currentUnsorted = array[partInd];
                int i;
                for (i = partInd; (i > 0) && (array[i-1] > currentUnsorted); i--)
                {
                    array[i] = array[i - 1];
                }
                array[i] = currentUnsorted;
            }
        }

        public static void MergeSort(int[] array)
        {
            int[] aux = new int[array.Length];
            Sort(0, array.Length-1);

            void Sort(int low, int high)
            {
                if (high <= low)
                    return;

                int mid = (high + low) / 2;
                Sort(low, mid);
                Sort(mid+1, high);
                Array.Copy(array, low, aux, low, high - low + 1);
                Merge(low, mid, high);
            }

            void Merge(int low, int mid, int high)
            {
                if (array[mid] <= array[mid + 1])
                    return;
                
                int i = low;
                int j = mid+1;

                for (int k = low; k <= high; k++)
                {
                    if (i > mid) array[k] = aux[j++];
                    else if (j > high) array[k] = aux[i++];
                    else if (aux[j] < aux[i]) 
                        array[k] = aux[j++];
                    else
                        array[k] = aux[i++];
                }
            }
        }

        public static void QuickSort(int[] array)
        {
            Sort(0, array.Length-1);

            void Sort(int low, int high)
            {
                if (high <= low)
                    return;

                int p = Partition(low, high);
                Sort(low, p - 1);
                Sort(p+1, high);
            }

            int Partition(int low, int high)
            {
                int i = low;
                int j = high + 1;
                int pivot = array[low];

                while(true)
                {
                    while(array[++i] < pivot)
                    {
                        if (i >= high)
                            break;
                    }
                    while(array[--j] > pivot)
                    {
                        if (i <= low)
                            break;
                    }

                    if (j <= i)
                        break;
                    Swap(array, i, j);
                }
                Swap(array, low, j);
                return j;
            }
        }
    }
}
