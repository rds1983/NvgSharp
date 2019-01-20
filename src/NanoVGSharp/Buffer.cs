using System;

namespace NanoVGSharp
{
	internal class Buffer<T>
	{
		private T[] _array;
		private int _count = 0;

		public T[] Array
		{
			get { return _array; }
		}

		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		public int Capacity
		{
			get { return _array.Length; }
		}

		public T this[int index]
		{
			get { return _array[index]; }
			set { _array[index] = value; }
		}

		public T this[ulong index]
		{
			get { return _array[index]; }
			set { _array[index] = value; }
		}

		public Buffer(int capacity)
		{
			_array = new T[capacity];
		}

		public void Clear()
		{
			_count = 0;
		}

		public void EnsureSize(int required)
		{
			if (_array.Length >= required) return;

			// Realloc
			var oldData = _array;

			var newSize = _array.Length;
			while (newSize < required)
			{
				newSize *= 2;
			}

			_array = new T[newSize];

			System.Array.Copy(oldData, _array, oldData.Length);
		}

		public void Add(T item)
		{
			EnsureSize(_count + 1);

			_array[_count] = item;
			++_count;
		}

		public ArraySegment<T> ToArraySegment()
		{
			return new ArraySegment<T>(Array, 0, Count);
		}
	}
}
