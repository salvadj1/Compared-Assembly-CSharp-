using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[ExecuteInEditMode]
public class FPGrassProbabilities : ScriptableObject, global::IFPGrassAsset
{
	// Token: 0x060002BA RID: 698 RVA: 0x0000E558 File Offset: 0x0000C758
	private void OnEnable()
	{
		if (!this.enabled)
		{
			this.enabled = true;
			this.Initialize();
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000E574 File Offset: 0x0000C774
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

	// Token: 0x060002BC RID: 700 RVA: 0x0000E5AC File Offset: 0x0000C7AC
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

	// Token: 0x060002BD RID: 701 RVA: 0x0000E680 File Offset: 0x0000C880
	public void StartEditing()
	{
		this.applyLock++;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000E690 File Offset: 0x0000C890
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

	// Token: 0x060002BF RID: 703 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
	private void UpdatePixels(bool apply = false)
	{
		if (this.applyLock == 0)
		{
			Color[] array = this.GetPixels();
			Texture2D texture2D = this.GetTexture(1);
			if (this.linear)
			{
				global::FPGrassProbabilities.Linear.SetLinearPixels(array, texture2D);
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

	// Token: 0x060002C0 RID: 704 RVA: 0x0000E754 File Offset: 0x0000C954
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
		if (global::FPGrass.Support.DetailProbabilityFilterMode == null)
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
				filterMode = global::FPGrass.Support.DetailProbabilityFilterMode,
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

	// Token: 0x060002C1 RID: 705 RVA: 0x0000E848 File Offset: 0x0000CA48
	public Texture2D GetTexture()
	{
		return this.GetTexture(0);
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000E854 File Offset: 0x0000CA54
	public void Initialize()
	{
		if (this.probabilityTexture)
		{
			this.pixels = null;
			this.GetPixels();
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000E874 File Offset: 0x0000CA74
	private static bool SetDif(ref float current, float value)
	{
		if (current != value)
		{
			current = value;
			return true;
		}
		return false;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000E884 File Offset: 0x0000CA84
	public void SetDetailProperty(int splatChannel, int detailIndex, int detailID, float probability)
	{
		Color[] array = this.GetPixels();
		int num = 4 * splatChannel + detailIndex;
		int num2 = num + 16;
		float value = (float)((detailID >= 0) ? ((detailID <= 256) ? detailID : 256) : 0) / 256f;
		float value2 = (probability >= 0f) ? ((probability <= 1f) ? probability : 1f) : 0f;
		bool flag = false;
		if (global::FPGrassProbabilities.SetDif(ref array[num].a, value))
		{
			flag = true;
		}
		if (global::FPGrassProbabilities.SetDif(ref array[num2].a, value2))
		{
			flag = true;
		}
		if (flag)
		{
			this.UpdatePixels(true);
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000E944 File Offset: 0x0000CB44
	public int GetDetailID(int splatChannel, int detailIndex)
	{
		return (int)(this.GetPixels()[4 * splatChannel + detailIndex].a * 256f);
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000E964 File Offset: 0x0000CB64
	public float GetDetailProbability(int splatChannel, int detailIndex)
	{
		return this.GetPixels()[4 * splatChannel + detailIndex + 16].a;
	}

	// Token: 0x040001E2 RID: 482
	private const int kBillnearPixelSize = 4;

	// Token: 0x040001E3 RID: 483
	private const int kWidth = 16;

	// Token: 0x040001E4 RID: 484
	private const int kHeight = 2;

	// Token: 0x040001E5 RID: 485
	private const TextureFormat kDetailProbabilityFormat = 1;

	// Token: 0x040001E6 RID: 486
	private const sbyte kTOpt_Default = 0;

	// Token: 0x040001E7 RID: 487
	private const sbyte kTOpt_NoSetPixelsOnCreate = 1;

	// Token: 0x040001E8 RID: 488
	private const sbyte kTOpt_NoApplyPixelsOnCreate = 3;

	// Token: 0x040001E9 RID: 489
	private const sbyte kTOpt_ReCreate = 4;

	// Token: 0x040001EA RID: 490
	[Obsolete]
	[SerializeField]
	private Texture2D probabilityTexture;

	// Token: 0x040001EB RID: 491
	[HideInInspector]
	[SerializeField]
	private Color[] pixels;

	// Token: 0x040001EC RID: 492
	[NonSerialized]
	private Texture2D texture;

	// Token: 0x040001ED RID: 493
	[NonSerialized]
	private bool linear;

	// Token: 0x040001EE RID: 494
	[NonSerialized]
	private int applyLock;

	// Token: 0x040001EF RID: 495
	[NonSerialized]
	private bool updateQueued;

	// Token: 0x040001F0 RID: 496
	[NonSerialized]
	private bool applyQueued;

	// Token: 0x040001F1 RID: 497
	[NonSerialized]
	private bool enabled;

	// Token: 0x02000056 RID: 86
	private static class Linear
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000E994 File Offset: 0x0000CB94
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
							global::FPGrassProbabilities.Linear.B[num4].r = P[num3].r;
							global::FPGrassProbabilities.Linear.B[num4].g = P[num3].g;
							global::FPGrassProbabilities.Linear.B[num4].b = P[num3].b;
							global::FPGrassProbabilities.Linear.B[num4].a = P[num3].a;
							l++;
							num2++;
						}
						k++;
						num++;
					}
				}
			}
			Dest.SetPixels(0, 0, 64, 8, global::FPGrassProbabilities.Linear.B, 0);
		}

		// Token: 0x040001F2 RID: 498
		private const int kB = 4;

		// Token: 0x040001F3 RID: 499
		private const int kW = 16;

		// Token: 0x040001F4 RID: 500
		private const int kH = 2;

		// Token: 0x040001F5 RID: 501
		private const int kP_Stride = 16;

		// Token: 0x040001F6 RID: 502
		private const int kB_Stride = 64;

		// Token: 0x040001F7 RID: 503
		private static readonly Color[] B = new Color[512];
	}
}
