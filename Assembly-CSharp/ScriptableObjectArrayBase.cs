using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class ScriptableObjectArrayBase<T> : ScriptableObject, IEnumerable, ICollection<T>, IList<T>, IEnumerable<T>
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x0001702C File Offset: 0x0001522C
	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return this.array.GetEnumerator();
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0001703C File Offset: 0x0001523C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.array.GetEnumerator();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0001704C File Offset: 0x0001524C
	void ICollection<T>.Add(T item)
	{
		this.array.Add(item);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001705C File Offset: 0x0001525C
	void ICollection<T>.Clear()
	{
		this.array.Clear();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001706C File Offset: 0x0001526C
	bool ICollection<T>.Contains(T item)
	{
		return Array.IndexOf<T>(this.array, item) != -1;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00017080 File Offset: 0x00015280
	void ICollection<T>.CopyTo(T[] array, int arrayIndex)
	{
		this.array.CopyTo(array, arrayIndex);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00017090 File Offset: 0x00015290
	bool ICollection<T>.Remove(T item)
	{
		return this.array.Remove(item);
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060004AF RID: 1199 RVA: 0x000170A0 File Offset: 0x000152A0
	int ICollection<T>.Count
	{
		get
		{
			return this.array.Count;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000170B0 File Offset: 0x000152B0
	bool ICollection<T>.IsReadOnly
	{
		get
		{
			return this.array.IsReadOnly;
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x000170C0 File Offset: 0x000152C0
	int IList<T>.IndexOf(T item)
	{
		return this.array.IndexOf(item);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x000170D0 File Offset: 0x000152D0
	void IList<T>.Insert(int index, T item)
	{
		this.array.Insert(index, item);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x000170E0 File Offset: 0x000152E0
	void IList<T>.RemoveAt(int index)
	{
		this.array.RemoveAt(index);
	}

	// Token: 0x170000B3 RID: 179
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

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00017110 File Offset: 0x00015310
	public T[] array
	{
		get
		{
			return this._array ?? global::ScriptableObjectArrayBase<T>.konst.empty;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00017124 File Offset: 0x00015324
	public int Length
	{
		get
		{
			return (this._array != null) ? this._array.Length : 0;
		}
	}

	// Token: 0x170000B6 RID: 182
	public T this[int i]
	{
		get
		{
			return this.array[i];
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00017150 File Offset: 0x00015350
	public global::ScriptableObjectArrayBase<T>.Enumerator GetEnumerator()
	{
		return new global::ScriptableObjectArrayBase<T>.Enumerator(this._array);
	}

	// Token: 0x04000421 RID: 1057
	[SerializeField]
	private T[] _array;

	// Token: 0x020000E0 RID: 224
	private static class konst
	{
		// Token: 0x04000422 RID: 1058
		public static readonly T[] empty = new T[0];
	}

	// Token: 0x020000E1 RID: 225
	public struct Enumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00017170 File Offset: 0x00015370
		public Enumerator(T[] array)
		{
			this.array = (array ?? global::ScriptableObjectArrayBase<T>.konst.empty);
			this.i = -1;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0001718C File Offset: 0x0001538C
		object IEnumerator.Current
		{
			get
			{
				return this.array[this.i];
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000171A4 File Offset: 0x000153A4
		public T Current
		{
			get
			{
				return this.array[this.i];
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000171B8 File Offset: 0x000153B8
		public void Reset()
		{
			this.i = -1;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000171C4 File Offset: 0x000153C4
		public bool MoveNext()
		{
			return ++this.i < (this.array ?? global::ScriptableObjectArrayBase<T>.konst.empty).Length;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000171F8 File Offset: 0x000153F8
		public void Dispose()
		{
		}

		// Token: 0x04000423 RID: 1059
		private T[] array;

		// Token: 0x04000424 RID: 1060
		private int i;
	}
}
