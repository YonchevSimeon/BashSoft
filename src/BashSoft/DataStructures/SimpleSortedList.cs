namespace BashSoft.DataStructures
{
    using System;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;
    
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private int size;
        private IComparer<T> comparison;
        
        public SimpleSortedList(IComparer<T> comparison, int capacity)
        {
            if(capacity < 0)
            {
                throw new NegativeCapacityException();
            }

            this.comparison = comparison;

            this.innerCollection = new T[capacity];
        }

        public SimpleSortedList(int capacity)
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity) { }

        public SimpleSortedList(IComparer<T> comparison)
            : this(comparison, DefaultSize) { }

        public SimpleSortedList()
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), DefaultSize) { }

        public int Size => this.size;

        public int Capacity => this.innerCollection.Length;

        public void Add(T element)
        {
            if(element == null)
            {
                throw new ArgumentNullException();
            }

            if(this.innerCollection.Length == this.Size)
            {
                this.Resize();
            }

            this.innerCollection[this.size++] = element;
            
            // Using Quicksort with collection, startIndex, endIndex, customComparator
            this.Quicksort(this.innerCollection, 0, this.Size - 1, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if(this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }

            foreach (T element in collection)
            {
                if(element == null)
                {
                    throw new ArgumentNullException();
                }

                this.innerCollection[this.size++] = element;
                
            }

            // Using Quicksort with collection, startIndex, endIndex, customComparator
            this.Quicksort(this.innerCollection, 0, this.Size - 1, this.comparison);
        }

        public string JoinWith(string joiner)
        {
            if(joiner == null)
            {
                throw new ArgumentNullException();
            }

            StringBuilder builder = new StringBuilder();

            foreach (T element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - joiner.Length, joiner.Length);

            return builder.ToString();
        }

        public bool Remove(T element)
        {
            if(element == null)
            {
                throw new ArgumentNullException();
            }

            bool hasBeenRemoved = false;

            int indexOfRemovedElement = 0;

            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;

                    this.innerCollection[i] = default(T);

                    hasBeenRemoved = true;

                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }

                this.innerCollection[--this.size] = default(T);
                
            }

            return hasBeenRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];

            Array.Copy(this.innerCollection, newCollection, this.Size);

            this.innerCollection = newCollection;
        }
        
        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.innerCollection.Length * 2;

            while(this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];

            Array.Copy(this.innerCollection, newCollection, this.Size);

            this.innerCollection = newCollection;
        }

        //Quicksorting array with custom comparator
        private void Quicksort(T[] array, int startIndex, int endIndex, IComparer<T> comparer)
        {
            int left = startIndex;
            int right = endIndex;
            int pivot = startIndex;
            startIndex++;

            while(endIndex >= startIndex)
            {
                if(comparer.Compare(array[startIndex], array[pivot]) >= 0 && 
                    comparer.Compare(array[endIndex], array[pivot]) < 0)
                {
                    this.Swap(array, startIndex, endIndex);
                }
                else if(comparer.Compare(array[startIndex], array[pivot]) >= 0)
                {
                    endIndex--;
                }
                else if(comparer.Compare(array[endIndex], array[pivot]) < 0)
                {
                    startIndex++;
                }
                else
                {
                    endIndex--;
                    startIndex++;
                }
            }

            this.Swap(array, pivot, endIndex);
            pivot = endIndex;
            if(pivot > left)
            {
                this.Quicksort(array, left, pivot, comparer);
            }
            if(right > pivot + 1)
            {
                this.Quicksort(array, pivot + 1, right, comparer);
            }
        }
        
        private void Swap(T[] array, int left, int right)
        {
            T temp = array[right];
            array[right] = array[left];
            array[left] = temp;
        }
    }
}
