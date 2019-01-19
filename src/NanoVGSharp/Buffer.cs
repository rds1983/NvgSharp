using System;

namespace NanoVGSharp
{
	public class Buffer<T>
	{
		private T[] _data;
		private int _count = 0;

		public T[] Data
		{
			get { return _data; }
		}

		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}

		public int Capacity
		{
			get { return _data.Length; }
		}

		public T this[int index]
		{
			get { return _data[index]; }
			set { _data[index] = value; }
		}

		public T this[ulong index]
		{
			get { return _data[index]; }
			set { _data[index] = value; }
		}

		public Buffer(int capacity)
		{
			_data = new T[capacity];
		}

		public void Clear()
		{
			_count = 0;
		}

		public void EnsureSize(int required)
		{
			if (_data.Length >= required) return;

			// Realloc
			var oldData = _data;

			var newSize = _data.Length;
			while (newSize < required)
			{
				newSize *= 2;
			}

			_data = new T[newSize];

			Array.Copy(oldData, _data, oldData.Length);
		}

		public void Add(T item)
		{
			EnsureSize(_count + 1);

			_data[_count] = item;
			++_count;
		}
	}
}
