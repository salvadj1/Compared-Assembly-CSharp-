using System;
using System.Collections.Generic;

// Token: 0x020001A6 RID: 422
public sealed class TempList<T> : List<T>, IDisposable
{
	// Token: 0x06000CA5 RID: 3237 RVA: 0x00030B48 File Offset: 0x0002ED48
	private TempList()
	{
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00030B50 File Offset: 0x0002ED50
	private TempList(IEnumerable<T> enumerable) : base(enumerable)
	{
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00030B5C File Offset: 0x0002ED5C
	private void Activate()
	{
		if (!this.active)
		{
			if (this.inDump)
			{
				throw new InvalidOperationException();
			}
			if (global::TempList<T>.activeCount == 0)
			{
				global::TempList<T>.firstActive = this;
				global::TempList<T>.lastActive = this;
				this.p = (this.n = false);
				this.prev = (this.next = null);
			}
			else if (global::TempList<T>.activeCount == 1)
			{
				global::TempList<T>.lastActive = this;
				this.p = true;
				this.n = false;
				this.prev = global::TempList<T>.firstActive;
				this.next = null;
				global::TempList<T>.firstActive.n = true;
				global::TempList<T>.firstActive.next = this;
			}
			else
			{
				this.p = true;
				this.n = false;
				this.prev = global::TempList<T>.lastActive;
				global::TempList<T>.lastActive.n = true;
				global::TempList<T>.lastActive.next = this;
				global::TempList<T>.lastActive = this;
				this.next = null;
			}
			global::TempList<T>.activeCount++;
			this.active = true;
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00030C5C File Offset: 0x0002EE5C
	private void Deactivate()
	{
		if (this.active)
		{
			if (this.inDump)
			{
				throw new InvalidOperationException();
			}
			if (global::TempList<T>.lastActive == this)
			{
				if (global::TempList<T>.firstActive != this)
				{
					global::TempList<T>.lastActive = this.prev;
					this.prev.n = false;
					this.prev.next = null;
				}
				else
				{
					global::TempList<T>.lastActive = null;
					global::TempList<T>.firstActive = null;
				}
			}
			else if (global::TempList<T>.firstActive == this)
			{
				this.next.p = false;
				this.next.prev = null;
				global::TempList<T>.firstActive = this.next;
			}
			else
			{
				this.prev.next = this.next;
				this.next.prev = this.prev;
			}
			this.prev = null;
			this.next = null;
			this.p = false;
			this.n = false;
			this.active = false;
			global::TempList<T>.activeCount--;
		}
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00030D58 File Offset: 0x0002EF58
	private void Bin()
	{
		if (!this.inDump)
		{
			if (this.active)
			{
				throw new InvalidOperationException();
			}
			this.next = global::TempList<T>.dump;
			int num = global::TempList<T>.dumpCount;
			global::TempList<T>.dumpCount = num + 1;
			if (num != 0)
			{
				global::TempList<T>.dump.prev = this;
			}
			global::TempList<T>.dump = this;
			this.inDump = true;
			this.Clear();
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00030DBC File Offset: 0x0002EFBC
	private static bool Resurrect(out global::TempList<T> twl)
	{
		if (global::TempList<T>.dumpCount != 0)
		{
			twl = global::TempList<T>.dump;
			global::TempList<T>.dump = ((--global::TempList<T>.dumpCount != 0) ? twl.prev : null);
			twl.inDump = false;
			twl.prev = null;
			return true;
		}
		twl = null;
		return false;
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00030E14 File Offset: 0x0002F014
	public static global::TempList<T> New()
	{
		global::TempList<T> result;
		if (global::TempList<T>.Resurrect(out result))
		{
			return result;
		}
		return new global::TempList<T>();
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00030E34 File Offset: 0x0002F034
	public static global::TempList<T> New(IEnumerable<T> windows)
	{
		global::TempList<T> tempList;
		if (global::TempList<T>.Resurrect(out tempList))
		{
			tempList.AddRange(windows);
			return tempList;
		}
		return new global::TempList<T>(windows);
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00030E5C File Offset: 0x0002F05C
	public void Dispose()
	{
		this.Deactivate();
		this.Bin();
	}

	// Token: 0x04000836 RID: 2102
	private static global::TempList<T> dump;

	// Token: 0x04000837 RID: 2103
	private static int dumpCount;

	// Token: 0x04000838 RID: 2104
	private static global::TempList<T> lastActive;

	// Token: 0x04000839 RID: 2105
	private static global::TempList<T> firstActive;

	// Token: 0x0400083A RID: 2106
	private static int activeCount;

	// Token: 0x0400083B RID: 2107
	private global::TempList<T> prev;

	// Token: 0x0400083C RID: 2108
	private global::TempList<T> next;

	// Token: 0x0400083D RID: 2109
	private bool inDump;

	// Token: 0x0400083E RID: 2110
	private bool active;

	// Token: 0x0400083F RID: 2111
	private bool p;

	// Token: 0x04000840 RID: 2112
	private bool n;
}
