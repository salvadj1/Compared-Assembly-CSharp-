using System;
using UnityEngine;

// Token: 0x02000504 RID: 1284
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraLayerDepths : MonoBehaviour
{
	// Token: 0x06002BE3 RID: 11235 RVA: 0x000A40A4 File Offset: 0x000A22A4
	private void OnPreCull()
	{
		if (this.spherical != this.spherical_ || this.layer00 != this.layer00_ || this.layer01 != this.layer01_ || this.layer02 != this.layer02_ || this.layer03 != this.layer03_ || this.layer04 != this.layer04_ || this.layer05 != this.layer05_ || this.layer06 != this.layer06_ || this.layer07 != this.layer07_ || this.layer08 != this.layer08_ || this.layer09 != this.layer09_ || this.layer10 != this.layer10_ || this.layer11 != this.layer11_ || this.layer12 != this.layer12_ || this.layer13 != this.layer13_ || this.layer14 != this.layer14_ || this.layer15 != this.layer15_ || this.layer16 != this.layer16_ || this.layer17 != this.layer17_ || this.layer18 != this.layer18_ || this.layer19 != this.layer19_ || this.layer20 != this.layer20_ || this.layer21 != this.layer21_ || this.layer22 != this.layer22_ || this.layer23 != this.layer23_ || this.layer24 != this.layer24_ || this.layer25 != this.layer25_ || this.layer26 != this.layer26_ || this.layer27 != this.layer27_ || this.layer28 != this.layer28_ || this.layer29 != this.layer29_ || this.layer30 != this.layer30_ || this.layer31 != this.layer31_)
		{
			this.Awake();
		}
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000A42E8 File Offset: 0x000A24E8
	private static bool Set(ref float m, float v)
	{
		if (m == v)
		{
			return false;
		}
		m = v;
		return true;
	}

	// Token: 0x1700098D RID: 2445
	public float this[int layer]
	{
		get
		{
			switch (layer)
			{
			case 0:
				return this.layer00;
			case 1:
				return this.layer01;
			case 2:
				return this.layer02;
			case 3:
				return this.layer03;
			case 4:
				return this.layer04;
			case 5:
				return this.layer05;
			case 6:
				return this.layer06;
			case 7:
				return this.layer07;
			case 8:
				return this.layer08;
			case 9:
				return this.layer09;
			case 10:
				return this.layer10;
			case 11:
				return this.layer11;
			case 12:
				return this.layer12;
			case 13:
				return this.layer13;
			case 14:
				return this.layer14;
			case 15:
				return this.layer15;
			case 16:
				return this.layer16;
			case 17:
				return this.layer17;
			case 18:
				return this.layer18;
			case 19:
				return this.layer19;
			case 20:
				return this.layer20;
			case 21:
				return this.layer21;
			case 22:
				return this.layer22;
			case 23:
				return this.layer23;
			case 24:
				return this.layer24;
			case 25:
				return this.layer25;
			case 26:
				return this.layer26;
			case 27:
				return this.layer27;
			case 28:
				return this.layer28;
			case 29:
				return this.layer29;
			case 30:
				return this.layer30;
			case 31:
				return this.layer31;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
		set
		{
			bool flag;
			switch (layer)
			{
			case 0:
				flag = global::CameraLayerDepths.Set(ref this.layer00, value);
				break;
			case 1:
				flag = global::CameraLayerDepths.Set(ref this.layer01, value);
				break;
			case 2:
				flag = global::CameraLayerDepths.Set(ref this.layer02, value);
				break;
			case 3:
				flag = global::CameraLayerDepths.Set(ref this.layer03, value);
				break;
			case 4:
				flag = global::CameraLayerDepths.Set(ref this.layer04, value);
				break;
			case 5:
				flag = global::CameraLayerDepths.Set(ref this.layer05, value);
				break;
			case 6:
				flag = global::CameraLayerDepths.Set(ref this.layer06, value);
				break;
			case 7:
				flag = global::CameraLayerDepths.Set(ref this.layer07, value);
				break;
			case 8:
				flag = global::CameraLayerDepths.Set(ref this.layer08, value);
				break;
			case 9:
				flag = global::CameraLayerDepths.Set(ref this.layer09, value);
				break;
			case 10:
				flag = global::CameraLayerDepths.Set(ref this.layer10, value);
				break;
			case 11:
				flag = global::CameraLayerDepths.Set(ref this.layer11, value);
				break;
			case 12:
				flag = global::CameraLayerDepths.Set(ref this.layer12, value);
				break;
			case 13:
				flag = global::CameraLayerDepths.Set(ref this.layer13, value);
				break;
			case 14:
				flag = global::CameraLayerDepths.Set(ref this.layer14, value);
				break;
			case 15:
				flag = global::CameraLayerDepths.Set(ref this.layer15, value);
				break;
			case 16:
				flag = global::CameraLayerDepths.Set(ref this.layer16, value);
				break;
			case 17:
				flag = global::CameraLayerDepths.Set(ref this.layer17, value);
				break;
			case 18:
				flag = global::CameraLayerDepths.Set(ref this.layer18, value);
				break;
			case 19:
				flag = global::CameraLayerDepths.Set(ref this.layer19, value);
				break;
			case 20:
				flag = global::CameraLayerDepths.Set(ref this.layer20, value);
				break;
			case 21:
				flag = global::CameraLayerDepths.Set(ref this.layer21, value);
				break;
			case 22:
				flag = global::CameraLayerDepths.Set(ref this.layer22, value);
				break;
			case 23:
				flag = global::CameraLayerDepths.Set(ref this.layer23, value);
				break;
			case 24:
				flag = global::CameraLayerDepths.Set(ref this.layer24, value);
				break;
			case 25:
				flag = global::CameraLayerDepths.Set(ref this.layer25, value);
				break;
			case 26:
				flag = global::CameraLayerDepths.Set(ref this.layer26, value);
				break;
			case 27:
				flag = global::CameraLayerDepths.Set(ref this.layer27, value);
				break;
			case 28:
				flag = global::CameraLayerDepths.Set(ref this.layer28, value);
				break;
			case 29:
				flag = global::CameraLayerDepths.Set(ref this.layer29, value);
				break;
			case 30:
				flag = global::CameraLayerDepths.Set(ref this.layer30, value);
				break;
			case 31:
				flag = global::CameraLayerDepths.Set(ref this.layer31, value);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (flag)
			{
				this.Awake();
			}
		}
	}

	// Token: 0x06002BE7 RID: 11239 RVA: 0x000A4764 File Offset: 0x000A2964
	[ContextMenu("Ensure Layer Depths Set")]
	private void EnsureLayerDepthsSet()
	{
		float[] layerCullDistances = base.camera.layerCullDistances;
		if (layerCullDistances == null)
		{
			this.Awake();
		}
		else if (layerCullDistances.Length != 32)
		{
			this.Awake();
		}
		else
		{
			bool flag = false;
			for (int i = 0; i < 32; i++)
			{
				if (layerCullDistances[i] != this[i])
				{
					flag = true;
					this.Awake();
					break;
				}
			}
			if (!flag)
			{
				return;
			}
		}
		if (this.spherical != base.camera.layerCullSpherical)
		{
			this.Awake();
			Debug.Log("Layer Depths Were Not Set", this);
			return;
		}
	}

	// Token: 0x06002BE8 RID: 11240 RVA: 0x000A4810 File Offset: 0x000A2A10
	private void Awake()
	{
		this.layer00_ = this.layer00;
		this.layer01_ = this.layer01;
		this.layer02_ = this.layer02;
		this.layer03_ = this.layer03;
		this.layer04_ = this.layer04;
		this.layer05_ = this.layer05;
		this.layer06_ = this.layer06;
		this.layer07_ = this.layer07;
		this.layer08_ = this.layer08;
		this.layer09_ = this.layer09;
		this.layer10_ = this.layer10;
		this.layer11_ = this.layer11;
		this.layer12_ = this.layer12;
		this.layer13_ = this.layer13;
		this.layer14_ = this.layer14;
		this.layer15_ = this.layer15;
		this.layer16_ = this.layer16;
		this.layer17_ = this.layer17;
		this.layer18_ = this.layer18;
		this.layer19_ = this.layer19;
		this.layer20_ = this.layer20;
		this.layer21_ = this.layer21;
		this.layer22_ = this.layer22;
		this.layer23_ = this.layer23;
		this.layer24_ = this.layer24;
		this.layer25_ = this.layer25;
		this.layer26_ = this.layer26;
		this.layer27_ = this.layer27;
		this.layer28_ = this.layer28;
		this.layer29_ = this.layer29;
		this.layer30_ = this.layer30;
		this.layer31_ = this.layer31;
		float[] layerCullDistances = new float[]
		{
			this.layer00,
			this.layer01,
			this.layer02,
			this.layer03,
			this.layer04,
			this.layer05,
			this.layer06,
			this.layer07,
			this.layer08,
			this.layer09,
			this.layer10,
			this.layer11,
			this.layer12,
			this.layer13,
			this.layer14,
			this.layer15,
			this.layer16,
			this.layer17,
			this.layer18,
			this.layer19,
			this.layer20,
			this.layer21,
			this.layer22,
			this.layer23,
			this.layer24,
			this.layer25,
			this.layer26,
			this.layer27,
			this.layer28,
			this.layer29,
			this.layer30,
			this.layer31
		};
		base.camera.layerCullDistances = layerCullDistances;
		base.camera.layerCullSpherical = this.spherical;
	}

	// Token: 0x040015AD RID: 5549
	[SerializeField]
	private float layer00;

	// Token: 0x040015AE RID: 5550
	[SerializeField]
	private float layer01;

	// Token: 0x040015AF RID: 5551
	[SerializeField]
	private float layer02;

	// Token: 0x040015B0 RID: 5552
	[SerializeField]
	private float layer03;

	// Token: 0x040015B1 RID: 5553
	[SerializeField]
	private float layer04;

	// Token: 0x040015B2 RID: 5554
	[SerializeField]
	private float layer05;

	// Token: 0x040015B3 RID: 5555
	[SerializeField]
	private float layer06;

	// Token: 0x040015B4 RID: 5556
	[SerializeField]
	private float layer07;

	// Token: 0x040015B5 RID: 5557
	[SerializeField]
	private float layer08;

	// Token: 0x040015B6 RID: 5558
	[SerializeField]
	private float layer09;

	// Token: 0x040015B7 RID: 5559
	[SerializeField]
	private float layer10;

	// Token: 0x040015B8 RID: 5560
	[SerializeField]
	private float layer11;

	// Token: 0x040015B9 RID: 5561
	[SerializeField]
	private float layer12;

	// Token: 0x040015BA RID: 5562
	[SerializeField]
	private float layer13;

	// Token: 0x040015BB RID: 5563
	[SerializeField]
	private float layer14;

	// Token: 0x040015BC RID: 5564
	[SerializeField]
	private float layer15;

	// Token: 0x040015BD RID: 5565
	[SerializeField]
	private float layer16;

	// Token: 0x040015BE RID: 5566
	[SerializeField]
	private float layer17;

	// Token: 0x040015BF RID: 5567
	[SerializeField]
	private float layer18;

	// Token: 0x040015C0 RID: 5568
	[SerializeField]
	private float layer19;

	// Token: 0x040015C1 RID: 5569
	[SerializeField]
	private float layer20;

	// Token: 0x040015C2 RID: 5570
	[SerializeField]
	private float layer21;

	// Token: 0x040015C3 RID: 5571
	[SerializeField]
	private float layer22;

	// Token: 0x040015C4 RID: 5572
	[SerializeField]
	private float layer23;

	// Token: 0x040015C5 RID: 5573
	[SerializeField]
	private float layer24;

	// Token: 0x040015C6 RID: 5574
	[SerializeField]
	private float layer25;

	// Token: 0x040015C7 RID: 5575
	[SerializeField]
	private float layer26;

	// Token: 0x040015C8 RID: 5576
	[SerializeField]
	private float layer27;

	// Token: 0x040015C9 RID: 5577
	[SerializeField]
	private float layer28;

	// Token: 0x040015CA RID: 5578
	[SerializeField]
	private float layer29;

	// Token: 0x040015CB RID: 5579
	[SerializeField]
	private float layer30;

	// Token: 0x040015CC RID: 5580
	[SerializeField]
	private float layer31;

	// Token: 0x040015CD RID: 5581
	[SerializeField]
	private bool spherical;

	// Token: 0x040015CE RID: 5582
	[NonSerialized]
	private float layer00_;

	// Token: 0x040015CF RID: 5583
	[NonSerialized]
	private float layer01_;

	// Token: 0x040015D0 RID: 5584
	[NonSerialized]
	private float layer02_;

	// Token: 0x040015D1 RID: 5585
	[NonSerialized]
	private float layer03_;

	// Token: 0x040015D2 RID: 5586
	[NonSerialized]
	private float layer04_;

	// Token: 0x040015D3 RID: 5587
	[NonSerialized]
	private float layer05_;

	// Token: 0x040015D4 RID: 5588
	[NonSerialized]
	private float layer06_;

	// Token: 0x040015D5 RID: 5589
	[NonSerialized]
	private float layer07_;

	// Token: 0x040015D6 RID: 5590
	[NonSerialized]
	private float layer08_;

	// Token: 0x040015D7 RID: 5591
	[NonSerialized]
	private float layer09_;

	// Token: 0x040015D8 RID: 5592
	[NonSerialized]
	private float layer10_;

	// Token: 0x040015D9 RID: 5593
	[NonSerialized]
	private float layer11_;

	// Token: 0x040015DA RID: 5594
	[NonSerialized]
	private float layer12_;

	// Token: 0x040015DB RID: 5595
	[NonSerialized]
	private float layer13_;

	// Token: 0x040015DC RID: 5596
	[NonSerialized]
	private float layer14_;

	// Token: 0x040015DD RID: 5597
	[NonSerialized]
	private float layer15_;

	// Token: 0x040015DE RID: 5598
	[NonSerialized]
	private float layer16_;

	// Token: 0x040015DF RID: 5599
	[NonSerialized]
	private float layer17_;

	// Token: 0x040015E0 RID: 5600
	[NonSerialized]
	private float layer18_;

	// Token: 0x040015E1 RID: 5601
	[NonSerialized]
	private float layer19_;

	// Token: 0x040015E2 RID: 5602
	[NonSerialized]
	private float layer20_;

	// Token: 0x040015E3 RID: 5603
	[NonSerialized]
	private float layer21_;

	// Token: 0x040015E4 RID: 5604
	[NonSerialized]
	private float layer22_;

	// Token: 0x040015E5 RID: 5605
	[NonSerialized]
	private float layer23_;

	// Token: 0x040015E6 RID: 5606
	[NonSerialized]
	private float layer24_;

	// Token: 0x040015E7 RID: 5607
	[NonSerialized]
	private float layer25_;

	// Token: 0x040015E8 RID: 5608
	[NonSerialized]
	private float layer26_;

	// Token: 0x040015E9 RID: 5609
	[NonSerialized]
	private float layer27_;

	// Token: 0x040015EA RID: 5610
	[NonSerialized]
	private float layer28_;

	// Token: 0x040015EB RID: 5611
	[NonSerialized]
	private float layer29_;

	// Token: 0x040015EC RID: 5612
	[NonSerialized]
	private float layer30_;

	// Token: 0x040015ED RID: 5613
	[NonSerialized]
	private float layer31_;

	// Token: 0x040015EE RID: 5614
	[NonSerialized]
	private bool spherical_;
}
