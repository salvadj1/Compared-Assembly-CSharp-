using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class ScriptableObjectArrayBase<T> : ScriptableObject, IEnumerable, ICollection<T>, IList<T>, IEnumerable<T>
{
	// Token: 0x0600042A RID: 1066 RVA: 0x00015664 File Offset: 0x00013864
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this.array.GetEnumerator();
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00015674 File Offset: 0x00013874
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.array.GetEnumerator();
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00015684 File Offset: 0x00013884
	void ICollection<T>.Add(T item)
	{
		this.array.Add(item);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00015694 File Offset: 0x00013894
	void ICollection<T>.Clear()
	{
		this.array.Clear();
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000156A4 File Offset: 0x000138A4
	bool ICollection<T>.Contains(T item)
	{
		return Array.IndexOf<T>(this.array, item) != -1;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000156B8 File Offset: 0x000138B8
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.array.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000156C8 File Offset: 0x000138C8
	bool ICollection<T>.Remove(T item)
	{
		return this.array.Remove(item);
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x000156D8 File Offset: 0x000138D8
	int ICollection<T>.Count
	{
		get
		{
			return this.array.Count;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000432 RID: 1074 RVA: 0x000156E8 File Offset: 0x000138E8
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return this.array.IsReadOnly;
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000156F8 File Offset: 0x000138F8
	int IList<T>.IndexOf(T item)
	{
		return this.array.IndexOf(item);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00015708 File Offset: 0x00013908
	void IList<T>.Insert(int index, T item)
	{
		this.array.Insert(index, item);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00015718 File Offset: 0x00013918
	void IList<T>.RemoveAt(int index)
	{
		this.array.RemoveAt(index);
	}

	// Token: 0x17000099 RID: 153
	T IList<T>.this[int index]
	{
		get
		{
			return this.array[index];
		}
		set
		{
			this.array[index] = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x00015748 File Offset: 0x00013948
	public T[] array
	{
		get
		{
			return this._array ?? ScriptableObjectArrayBase<T>.konst.empty;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001575C File Offset: 0x0001395C
	public int Length
	{
		get
		{
			return (this._array != null) ? this._array.Length : 0;
		}
	}

	// Token: 0x1700009C RID: 156
	public T this[int i]
	{
		get
		{
			return this.array[i];
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00015788 File Offset: 0x00013988
	public ScriptableObjectArrayBase<T>.Enumerator GetEnumerator()
	{
		return new ScriptableObjectArrayBase<T>.Enumerator(this._array);
	}

	// Token: 0x040003B2 RID: 946
	[SerializeField]
	private T[] _array;

	// Token: 0x020000CC RID: 204
	private static class konst
	{
		// Token: 0x040003B3 RID: 947
		public static readonly T[] empty = new T[0];
	}

	// Token: 0x020000CD RID: 205
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x000157A8 File Offset: 0x000139A8
		public Enumerator(T[] array)
		{
			this.array = (array ?? ScriptableObjectArrayBase<T>.konst.empty);
			this.i = -1;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000157C4 File Offset: 0x000139C4
		object IEnumerator.Current
		{
			get
			{
				return this.array[this.i];
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000157DC File Offset: 0x000139DC
		public T Current
		{
			get
			{
				return this.array[this.i];
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000157F0 File Offset: 0x000139F0
		public void Reset()
		{
			this.i = -1;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000157FC File Offset: 0x000139FC
		public bool MoveNext()
		{
			return ++this.i < (this.array ?? ScriptableObjectArrayBase<T>.konst.empty).Length;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00015830 File Offset: 0x00013A30
		public void Dispose()
		{
		}

		// Token: 0x040003B4 RID: 948
		private T[] array;

		// Token: 0x040003B5 RID: 949
		private int i;
	}
}
