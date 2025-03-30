namespace Merge.Common.Utils
{
	public static class ArrayUtils
	{
		public static T[] Resize<T>(this T[] array, int size)
		{
			if (size < 0)
			{
				return array;
			}

			T[] newArray = new T[size];

			for (var i = 0; i < size; i++)
			{
				if (i < array.Length)
				{
					newArray[i] = array[i];
				}
			}

			return newArray;
		}

		public static T[,] Resize<T>(this T[,] array, int size1, int size2)
		{
			if (size1 < 0 || size2 < 0)
			{
				return array;
			}

			T[,] newArray = new T[size1, size2];

			for (int i = 0; i < size1; i++)
			{
				if (i < array.GetLength(0))
				{
					for (int j = 0; j < size2; j++)
					{
						if (j < array.GetLength(1))
						{
							newArray[i,j] = array[i, j];
						}
					}
				}
			}

			return newArray;
		}
	}
}
