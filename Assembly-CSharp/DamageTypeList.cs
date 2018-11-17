using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
[Serializable]
public sealed class DamageTypeList
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x00029008 File Offset: 0x00027208
	public DamageTypeList()
	{
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00029010 File Offset: 0x00027210
	public DamageTypeList(DamageTypeList copyFrom) : this()
	{
		if (copyFrom == null || copyFrom.damageArray == null)
		{
			this.damageArray = new float[6];
		}
		else if (copyFrom.damageArray.Length == 6)
		{
			this.damageArray = (float[])copyFrom.damageArray.Clone();
		}
		else
		{
			this.damageArray = new float[6];
			if (copyFrom.damageArray.Length > 6)
			{
				for (int i = 0; i < 6; i++)
				{
					this.damageArray[i] = copyFrom.damageArray[i];
				}
			}
			else
			{
				for (int j = 0; j < copyFrom.damageArray.Length; j++)
				{
					this.damageArray[j] = copyFrom.damageArray[j];
				}
			}
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000290D8 File Offset: 0x000272D8
	public DamageTypeList(float generic, float bullet, float melee, float explosion, float radiation, float cold)
	{
		this.damageArray = new float[6];
		this.damageArray[0] = generic;
		this.damageArray[1] = bullet;
		this.damageArray[2] = melee;
		this.damageArray[3] = explosion;
		this.damageArray[4] = radiation;
		this.damageArray[5] = cold;
	}

	// Token: 0x170002E3 RID: 739
	public float this[int index]
	{
		get
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException();
			}
			return (this.damageArray != null && this.damageArray.Length > index) ? this.damageArray[index] : 0f;
		}
		set
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.damageArray == null || this.damageArray.Length <= index)
			{
				Array.Resize<float>(ref this.damageArray, 6);
			}
			this.damageArray[index] = value;
		}
	}

	// Token: 0x170002E4 RID: 740
	public float this[DamageTypeIndex index]
	{
		get
		{
			return this[(int)index];
		}
		set
		{
			this[(int)index] = value;
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x000291E4 File Offset: 0x000273E4
	public void SetArmorValues(DamageTypeList copyFrom)
	{
		if (this.damageArray == null || this.damageArray.Length != 6)
		{
			if (copyFrom == null || copyFrom.damageArray == null)
			{
				this.damageArray = new float[6];
			}
			else if (copyFrom.damageArray.Length == 6)
			{
				this.damageArray = (float[])copyFrom.damageArray.Clone();
			}
			else
			{
				this.damageArray = new float[6];
				if (copyFrom.damageArray.Length > 6)
				{
					for (int i = 0; i < 6; i++)
					{
						this.damageArray[i] = copyFrom.damageArray[i];
					}
				}
				else
				{
					for (int j = 0; j < copyFrom.damageArray.Length; j++)
					{
						this.damageArray[j] = copyFrom.damageArray[j];
					}
				}
			}
		}
		else if (copyFrom.damageArray == null)
		{
			if (this.damageArray == null || this.damageArray.Length != 6)
			{
				this.damageArray = new float[6];
			}
			else
			{
				for (int k = 0; k < 6; k++)
				{
					this.damageArray[k] = 0f;
				}
			}
		}
		else if (copyFrom.damageArray.Length >= 6)
		{
			for (int l = 0; l < 6; l++)
			{
				this.damageArray[l] = copyFrom.damageArray[l];
			}
		}
		else
		{
			int m;
			for (m = 0; m < copyFrom.damageArray.Length; m++)
			{
				this.damageArray[m] = copyFrom.damageArray[m];
			}
			while (m < 6)
			{
				this.damageArray[m++] = 0f;
			}
		}
	}

	// Token: 0x040006B7 RID: 1719
	private const int kDamageIndexCount = 6;

	// Token: 0x040006B8 RID: 1720
	[SerializeField]
	private float[] damageArray;
}
