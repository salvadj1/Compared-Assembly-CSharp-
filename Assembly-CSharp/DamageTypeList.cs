using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
[Serializable]
public sealed class DamageTypeList
{
	// Token: 0x06000B66 RID: 2918 RVA: 0x0002CD84 File Offset: 0x0002AF84
	public DamageTypeList()
	{
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0002CD8C File Offset: 0x0002AF8C
	public DamageTypeList(global::DamageTypeList copyFrom) : this()
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

	// Token: 0x06000B68 RID: 2920 RVA: 0x0002CE54 File Offset: 0x0002B054
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

	// Token: 0x17000325 RID: 805
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

	// Token: 0x17000326 RID: 806
	public float this[global::DamageTypeIndex index]
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

	// Token: 0x06000B6D RID: 2925 RVA: 0x0002CF60 File Offset: 0x0002B160
	public void SetArmorValues(global::DamageTypeList copyFrom)
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

	// Token: 0x040007C6 RID: 1990
	private const int kDamageIndexCount = 6;

	// Token: 0x040007C7 RID: 1991
	[SerializeField]
	private float[] damageArray;
}
