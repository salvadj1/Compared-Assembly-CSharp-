using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[ExecuteInEditMode]
public class FPGrassProbabilities : ScriptableObject, IFPGrassAsset
{
	// Token: 0x06000248 RID: 584 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
	private void OnEnable()
	{
		if (!this.enabled)
		{
			this.enabled = true;
			this.Initialize();
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000CFCC File Offset: 0x0000B1CC
	private void OnDisable()
	{
		if (this.enabled)
		{
			this.enabled = false;
			if (this.texture)
			{
				Object.DestroyImmediate(this.texture, false);
				this.texture = null;
			}
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000D004 File Offset: 0x0000B204
	private Color[] GetPixels()
	{
		if (this.probabilityTexture)
		{
			Debug.LogWarning("ProbabilityTexture is now created at runtime. Saved the pixels off the texture and now dereferencing it", this.probabilityTexture);
			this.pixels = this.probabilityTexture.GetPixels(0, 0, 16, 2, 0);
			this.probabilityTexture = null;
		}
		else if (object.ReferenceEquals(this.pixels, null) || this.pixels.Length != 32)
		{
			this.pixels = new Color[32];
			try
			{
				this.StartEditing();
				for (int i = 0; i < 4; i++)
				{
					this.SetDetailProperty(i, 0, 0, 1f);
				}
			}
			finally
			{
				this.StopEditing();
			}
		}
		return this.pixels;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
	public void StartEditing()
	{
		this.applyLock++;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
	public void StopEditing()
	{
		if (--this.applyLock <= 0)
		{
			this.applyLock = 0;
			if (this.updateQueued)
			{
				this.updateQueued = false;
				bool apply = this.applyQueued;
				this.applyQueued = false;
				this.UpdatePixels(apply);
			}
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000D13C File Offset: 0x0000B33C
	private void UpdatePixels(bool apply = false)
	{
		if (this.applyLock == 0)
		{
			Color[] array = this.GetPixels();
			Texture2D texture2D = this.GetTexture(1);
			if (this.linear)
			{
				FPGrassProbabilities.Linear.SetLinearPixels(array, texture2D);
			}
			else
			{
				texture2D.SetPixels(0, 0, 16, 2, array);
			}
			if (apply)
			{
				texture2D.Apply();
			}
		}
		else
		{
			this.updateQueued = true;
			this.applyQueued = (this.applyQueued || apply);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000D1AC File Offset: 0x0000B3AC
	private Texture2D GetTexture(sbyte TOpt)
	{
		if (this.texture)
		{
			if (((int)TOpt & 4) == 0)
			{
				return this.texture;
			}
			Object.DestroyImmediate(this.texture, false);
			this.texture = null;
		}
		if (FPGrass.Support.DetailProbabilityFilterMode == null)
		{
			this.texture = new Texture2D(16, 2, 1, false, false)
			{
				hideFlags = 4,
				name = "FPGrass Detail Probability (Point)",
				anisoLevel = 0,
				filterMode = 0,
				wrapMode = 1
			};
			this.linear = false;
		}
		else
		{
			this.texture = new Texture2D(64, 8, 1, false, false)
			{
				hideFlags = 4,
				name = "FPGrass Detail Probability (Linear)",
				anisoLevel = 0,
				filterMode = FPGrass.Support.DetailProbabilityFilterMode,
				wrapMode = 1
			};
			this.linear = true;
		}
		if (((int)TOpt & 1) == 0)
		{
			this.UpdatePixels(((int)TOpt & 3) == 0);
		}
		return this.texture;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
	public Texture2D GetTexture()
	{
		return this.GetTexture(0);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000D2AC File Offset: 0x0000B4AC
	public void Initialize()
	{
		if (this.probabilityTexture)
		{
			this.pixels = null;
			this.GetPixels();
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000D2CC File Offset: 0x0000B4CC
	private static bool SetDif(ref float current, float value)
	{
		if (current != value)
		{
			current = value;
			return true;
		}
		return false;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000D2DC File Offset: 0x0000B4DC
	public void SetDetailProperty(int splatChannel, int detailIndex, int detailID, float probability)
	{
		Color[] array = this.GetPixels();
		int num = 4 * splatChannel + detailIndex;
		int num2 = num + 16;
		float value = (float)((detailID >= 0) ? ((detailID <= 256) ? detailID : 256) : 0) / 256f;
		float value2 = (probability >= 0f) ? ((probability <= 1f) ? probability : 1f) : 0f;
		bool flag = false;
		if (FPGrassProbabilities.SetDif(ref array[num].a, value))
		{
			flag = true;
		}
		if (FPGrassProbabilities.SetDif(ref array[num2].a, value2))
		{
			flag = true;
		}
		if (flag)
		{
			this.UpdatePixels(true);
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000D39C File Offset: 0x0000B59C
	public int GetDetailID(int splatChannel, int detailIndex)
	{
		return (int)(this.GetPixels()[4 * splatChannel + detailIndex].a * 256f);
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000D3BC File Offset: 0x0000B5BC
	public float GetDetailProbability(int splatChannel, int detailIndex)
	{
		return this.GetPixels()[4 * splatChannel + detailIndex + 16].a;
	}

	// Token: 0x04000180 RID: 384
	private const int kBillnearPixelSize = 4;

	// Token: 0x04000181 RID: 385
	private const int kWidth = 16;

	// Token: 0x04000182 RID: 386
	private const int kHeight = 2;

	// Token: 0x04000183 RID: 387
	private const TextureFormat kDetailProbabilityFormat = 1;

	// Token: 0x04000184 RID: 388
	private const sbyte kTOpt_Default = 0;

	// Token: 0x04000185 RID: 389
	private const sbyte kTOpt_NoSetPixelsOnCreate = 1;

	// Token: 0x04000186 RID: 390
	private const sbyte kTOpt_NoApplyPixelsOnCreate = 3;

	// Token: 0x04000187 RID: 391
	private const sbyte kTOpt_ReCreate = 4;

	// Token: 0x04000188 RID: 392
	[SerializeField]
	[Obsolete]
	private Texture2D probabilityTexture;

	// Token: 0x04000189 RID: 393
	[SerializeField]
	[HideInInspector]
	private Color[] pixels;

	// Token: 0x0400018A RID: 394
	[NonSerialized]
	private Texture2D texture;

	// Token: 0x0400018B RID: 395
	[NonSerialized]
	private bool linear;

	// Token: 0x0400018C RID: 396
	[NonSerialized]
	private int applyLock;

	// Token: 0x0400018D RID: 397
	[NonSerialized]
	private bool updateQueued;

	// Token: 0x0400018E RID: 398
	[NonSerialized]
	private bool applyQueued;

	// Token: 0x0400018F RID: 399
	[NonSerialized]
	private bool enabled;

	// Token: 0x02000044 RID: 68
	private static class Linear
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		public static void SetLinearPixels(Color[] P, Texture2D Dest)
		{
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = i * 4;
					int k = 0;
					while (k < 4)
					{
						int num2 = j * 4;
						int l = 0;
						while (l < 4)
						{
							int num3 = j * 16 + i;
							int num4 = num2 * 64 + num;
							FPGrassProbabilities.Linear.B[num4].r = P[num3].r;
							FPGrassProbabilities.Linear.B[num4].g = P[num3].g;
							FPGrassProbabilities.Linear.B[num4].b = P[num3].b;
							FPGrassProbabilities.Linear.B[num4].a = P[num3].a;
							l++;
							num2++;
						}
						k++;
						num++;
					}
				}
			}
			Dest.SetPixels(0, 0, 64, 8, FPGrassProbabilities.Linear.B, 0);
		}

		// Token: 0x04000190 RID: 400
		private const int kB = 4;

		// Token: 0x04000191 RID: 401
		private const int kW = 16;

		// Token: 0x04000192 RID: 402
		private const int kH = 2;

		// Token: 0x04000193 RID: 403
		private const int kP_Stride = 16;

		// Token: 0x04000194 RID: 404
		private const int kB_Stride = 64;

		// Token: 0x04000195 RID: 405
		private static readonly Color[] B = new Color[512];
	}
}
