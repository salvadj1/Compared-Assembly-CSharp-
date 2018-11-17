using System;

// Token: 0x020002A3 RID: 675
[Serializable]
public class CullGridSetup
{
	// Token: 0x06001807 RID: 6151 RVA: 0x0005E500 File Offset: 0x0005C700
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

	// Token: 0x06001808 RID: 6152 RVA: 0x0005E568 File Offset: 0x0005C768
	protected CullGridSetup(CullGridSetup copyFrom)
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

	// Token: 0x06001809 RID: 6153 RVA: 0x0005E5E8 File Offset: 0x0005C7E8
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

	// Token: 0x0600180A RID: 6154 RVA: 0x0005E684 File Offset: 0x0005C884
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

	// Token: 0x0600180B RID: 6155 RVA: 0x0005E7F4 File Offset: 0x0005C9F4
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

	// Token: 0x0600180C RID: 6156 RVA: 0x0005E91C File Offset: 0x0005CB1C
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

	// Token: 0x04000CD7 RID: 3287
	public int cellSquareDimension;

	// Token: 0x04000CD8 RID: 3288
	public int cellsWide;

	// Token: 0x04000CD9 RID: 3289
	public int cellsTall;

	// Token: 0x04000CDA RID: 3290
	public int groupBegin;

	// Token: 0x04000CDB RID: 3291
	public int gatheringCellsWide;

	// Token: 0x04000CDC RID: 3292
	public int gatheringCellsTall;

	// Token: 0x04000CDD RID: 3293
	public int gatheringCellsCenter;

	// Token: 0x04000CDE RID: 3294
	public int[] gatheringCellsBits;
}
