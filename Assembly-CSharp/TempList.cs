using System;
using System.Collections.Generic;

// Token: 0x0200017A RID: 378
public sealed class TempList<T> : List<T>, IDisposable
{
	// Token: 0x06000B75 RID: 2933 RVA: 0x0002CC5C File Offset: 0x0002AE5C
	private TempList()
	{
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0002CC64 File Offset: 0x0002AE64
	private TempList(IEnumerable<T> enumerable) : base(enumerable)
	{
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0002CC70 File Offset: 0x0002AE70
	private void Activate()
	{
		if (!this.active)
		{
			if (this.inDump)
			{
				throw new InvalidOperationException();
			}
			if (TempList<T>.activeCount == 0)
			{
				TempList<T>.firstActive = this;
				TempList<T>.lastActive = this;
				this.p = (this.n = false);
				this.prev = (this.next = null);
			}
			else if (TempList<T>.activeCount == 1)
			{
				TempList<T>.lastActive = this;
				this.p = true;
				this.n = false;
				this.prev = TempList<T>.firstActive;
				this.next = null;
				TempList<T>.firstActive.n = true;
				TempList<T>.firstActive.next = this;
			}
			else
			{
				this.p = true;
				this.n = false;
				this.prev = TempList<T>.lastActive;
				TempList<T>.lastActive.n = true;
				TempList<T>.lastActive.next = this;
				TempList<T>.lastActive = this;
				this.next = null;
			}
			TempList<T>.activeCount++;
			this.active = true;
		}
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0002CD70 File Offset: 0x0002AF70
	private void Deactivate()
	{
		if (this.active)
		{
			if (this.inDump)
			{
				throw new InvalidOperationException();
			}
			if (TempList<T>.lastActive == this)
			{
				if (TempList<T>.firstActive != this)
				{
					TempList<T>.lastActive = this.prev;
					this.prev.n = false;
					this.prev.next = null;
				}
				else
				{
					TempList<T>.lastActive = null;
					TempList<T>.firstActive = null;
				}
			}
			else if (TempList<T>.firstActive == this)
			{
				this.next.p = false;
				this.next.prev = null;
				TempList<T>.firstActive = this.next;
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
			TempList<T>.activeCount--;
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0002CE6C File Offset: 0x0002B06C
	private void Bin()
	{
		if (!this.inDump)
		{
			if (this.active)
			{
				throw new InvalidOperationException();
			}
			this.next = TempList<T>.dump;
			int num = TempList<T>.dumpCount;
			TempList<T>.dumpCount = num + 1;
			if (num != 0)
			{
				TempList<T>.dump.prev = this;
			}
			TempList<T>.dump = this;
			this.inDump = true;
			this.Clear();
		}
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x0002CED0 File Offset: 0x0002B0D0
	private static bool Resurrect(out TempList<T> twl)
	{
		if (TempList<T>.dumpCount != 0)
		{
			twl = TempList<T>.dump;
			TempList<T>.dump = ((--TempList<T>.dumpCount != 0) ? twl.prev : null);
			twl.inDump = false;
			twl.prev = null;
			return true;
		}
		twl = null;
		return false;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0002CF28 File Offset: 0x0002B128
	public static TempList<T> New()
	{
		TempList<T> result;
		if (TempList<T>.Resurrect(out result))
		{
			return result;
		}
		return new TempList<T>();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x0002CF48 File Offset: 0x0002B148
	public static TempList<T> New(IEnumerable<T> windows)
	{
		TempList<T> tempList;
		if (TempList<T>.Resurrect(out tempList))
		{
			tempList.AddRange(windows);
			return tempList;
		}
		return new TempList<T>(windows);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0002CF70 File Offset: 0x0002B170
	public void Dispose()
	{
		this.Deactivate();
		this.Bin();
	}

	// Token: 0x04000722 RID: 1826
	private static TempList<T> dump;

	// Token: 0x04000723 RID: 1827
	private static int dumpCount;

	// Token: 0x04000724 RID: 1828
	private static TempList<T> lastActive;

	// Token: 0x04000725 RID: 1829
	private static TempList<T> firstActive;

	// Token: 0x04000726 RID: 1830
	private static int activeCount;

	// Token: 0x04000727 RID: 1831
	private TempList<T> prev;

	// Token: 0x04000728 RID: 1832
	private TempList<T> next;

	// Token: 0x04000729 RID: 1833
	private bool inDump;

	// Token: 0x0400072A RID: 1834
	private bool active;

	// Token: 0x0400072B RID: 1835
	private bool p;

	// Token: 0x0400072C RID: 1836
	private bool n;
}
