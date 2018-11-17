using System;

// Token: 0x020002E0 RID: 736
[Serializable]
public class CullGridSetup
{
	// Token: 0x06001997 RID: 6551 RVA: 0x00062E74 File Offset: 0x00061074
	public CullGridSetup()
	{
		this.cellSquareDimension = 200;
		this.cellsWide = 80;
		this.cellsTall = 80;
		this.groupBegin = 100;
		this.gatheringCellsWide = 3;
		this.gatheringCellsTall = 3;
		this.gatheringCellsCenter = 4;
		this.gatheringCellsBits = new int[]
		{
			-8193,
			-101897
		};
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x00062EDC File Offset: 0x000610DC
	protected CullGridSetup(global::CullGridSetup copyFrom)
	{
		this.cellSquareDimension = copyFrom.cellSquareDimension;
		this.cellsWide = copyFrom.cellsWide;
		this.cellsTall = copyFrom.cellsTall;
		this.groupBegin = copyFrom.groupBegin;
		this.gatheringCellsWide = copyFrom.gatheringCellsWide;
		this.gatheringCellsTall = copyFrom.gatheringCellsTall;
		this.gatheringCellsCenter = copyFrom.gatheringCellsCenter;
		this.gatheringCellsBits = (int[])copyFrom.gatheringCellsBits.Clone();
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x00062F5C File Offset: 0x0006115C
	public bool GetGatheringBit(int x, int y)
	{
		if (x >= this.gatheringCellsWide || x < 0)
		{
			throw new ArgumentOutOfRangeException("x", "must be < gatheringCellsWide && >= 0");
		}
		if (y >= this.gatheringCellsTall || y < 0)
		{
			throw new ArgumentOutOfRangeException("y", "must be < gatheringCellsTall && >= 0");
		}
		int num = y * this.gatheringCellsWide + x;
		int num2 = num / 32;
		int num3 = num % 32;
		return this.gatheringCellsBits == null || this.gatheringCellsBits.Length <= num2 || (this.gatheringCellsBits[num2] & 1 << num3) == 1 << num3;
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x00062FF8 File Offset: 0x000611F8
	public void SetGatheringBit(int x, int y, bool v)
	{
		if (x >= this.gatheringCellsWide || x < 0)
		{
			throw new ArgumentOutOfRangeException("x", "must be < gatheringCellsWide && >= 0");
		}
		if (y >= this.gatheringCellsTall || y < 0)
		{
			throw new ArgumentOutOfRangeException("y", "must be < gatheringCellsTall && >= 0");
		}
		int num = y * this.gatheringCellsWide + x;
		int num2 = num / 32;
		int num3 = num % 32;
		if (this.gatheringCellsBits == null)
		{
			Array.Resize<int>(ref this.gatheringCellsBits, num2 + 1);
			for (int i = 0; i < num2; i++)
			{
				this.gatheringCellsBits[i] = -1;
			}
			if (!v)
			{
				this.gatheringCellsBits[num2] = ~(1 << num3);
			}
			else
			{
				this.gatheringCellsBits[num2] = -1;
			}
		}
		else if (this.gatheringCellsBits.Length <= num2)
		{
			int num4 = this.gatheringCellsBits.Length;
			Array.Resize<int>(ref this.gatheringCellsBits, num2 + 1);
			for (int j = num4 + 1; j <= num2; j++)
			{
				this.gatheringCellsBits[j] = -1;
			}
			if (!v)
			{
				this.gatheringCellsBits[num2] &= ~(1 << num3);
			}
		}
		else if (v)
		{
			this.gatheringCellsBits[num2] |= 1 << num3;
		}
		else
		{
			this.gatheringCellsBits[num2] &= ~(1 << num3);
		}
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x00063168 File Offset: 0x00061368
	public void ToggleGatheringBit(int x, int y)
	{
		if (x >= this.gatheringCellsWide || x < 0)
		{
			throw new ArgumentOutOfRangeException("x", "must be < gatheringCellsWide && >= 0");
		}
		if (y >= this.gatheringCellsTall || y < 0)
		{
			throw new ArgumentOutOfRangeException("y", "must be < gatheringCellsTall && >= 0");
		}
		int num = y * this.gatheringCellsWide + x;
		int num2 = num / 32;
		int num3 = num % 32;
		if (this.gatheringCellsBits == null)
		{
			Array.Resize<int>(ref this.gatheringCellsBits, num2 + 1);
			for (int i = 0; i < num2; i++)
			{
				this.gatheringCellsBits[i] = -1;
			}
			this.gatheringCellsBits[num2] = ~(1 << num3);
		}
		else if (this.gatheringCellsBits.Length <= num2)
		{
			int num4 = this.gatheringCellsBits.Length;
			Array.Resize<int>(ref this.gatheringCellsBits, num2 + 1);
			for (int j = num4 + 1; j < num2; j++)
			{
				this.gatheringCellsBits[j] = -1;
			}
			this.gatheringCellsBits[num2] = ~(1 << num3);
		}
		else
		{
			this.gatheringCellsBits[num2] ^= 1 << num3;
		}
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x00063290 File Offset: 0x00061490
	public void SetGatheringDimensions(int gatheringCellsWide, int gatheringCellsTall)
	{
		if (this.gatheringCellsWide == gatheringCellsWide && this.gatheringCellsTall == gatheringCellsTall)
		{
			return;
		}
		this.gatheringCellsWide = gatheringCellsWide;
		this.gatheringCellsTall = gatheringCellsTall;
		this.gatheringCellsCenter = this.gatheringCellsWide / 2 + this.gatheringCellsTall / 2 * this.gatheringCellsWide;
	}

	// Token: 0x04000E12 RID: 3602
	public int cellSquareDimension;

	// Token: 0x04000E13 RID: 3603
	public int cellsWide;

	// Token: 0x04000E14 RID: 3604
	public int cellsTall;

	// Token: 0x04000E15 RID: 3605
	public int groupBegin;

	// Token: 0x04000E16 RID: 3606
	public int gatheringCellsWide;

	// Token: 0x04000E17 RID: 3607
	public int gatheringCellsTall;

	// Token: 0x04000E18 RID: 3608
	public int gatheringCellsCenter;

	// Token: 0x04000E19 RID: 3609
	public int[] gatheringCellsBits;
}
