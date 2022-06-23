using System;

namespace NvgSharp
{
	internal class ArrayBuffer<T>
	{
		private T[] _array;
		private int _count = 0;

		public T[] Array => _array;

		public int Count => _count;

		public int Capacity => _array.Length;

		public T this[int index]
		{
			get => _array[index];
			set => _array[index] = value;
		}

		public T this[ulong index]
		{
			get => _array[index];
			set => _array[index] = value;
		}

		public ArrayBuffer(int capacity)
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

		public void Add(ArraySegment<T> data)
		{
			if (data.Count == 0)
			{
				return;
			}

			EnsureSize(_count + data.Count);
			System.Array.Copy(data.Array, data.Offset, _array, _count, data.Count);
			_count += data.Count;
		}

		public ArraySegment<T> Allocate(int size)
		{
			EnsureSize(_count + size);

			var offset = _count;
			_count += size;

			return new ArraySegment<T>(_array, offset, size);
		}

		public ArraySegment<T> ToArraySegment() => new ArraySegment<T>(Array, 0, Count);
	}
}
