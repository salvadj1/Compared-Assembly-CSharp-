using System;
using UnityEngine;

// Token: 0x0200044E RID: 1102
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraLayerDepths : MonoBehaviour
{
	// Token: 0x06002853 RID: 10323 RVA: 0x0009E124 File Offset: 0x0009C324
	private void OnPreCull()
	{
		if (this.spherical != this.spherical_ || this.layer00 != this.layer00_ || this.layer01 != this.layer01_ || this.layer02 != this.layer02_ || this.layer03 != this.layer03_ || this.layer04 != this.layer04_ || this.layer05 != this.layer05_ || this.layer06 != this.layer06_ || this.layer07 != this.layer07_ || this.layer08 != this.layer08_ || this.layer09 != this.layer09_ || this.layer10 != this.layer10_ || this.layer11 != this.layer11_ || this.layer12 != this.layer12_ || this.layer13 != this.layer13_ || this.layer14 != this.layer14_ || this.layer15 != this.layer15_ || this.layer16 != this.layer16_ || this.layer17 != this.layer17_ || this.layer18 != this.layer18_ || this.layer19 != this.layer19_ || this.layer20 != this.layer20_ || this.layer21 != this.layer21_ || this.layer22 != this.layer22_ || this.layer23 != this.layer23_ || this.layer24 != this.layer24_ || this.layer25 != this.layer25_ || this.layer26 != this.layer26_ || this.layer27 != this.layer27_ || this.layer28 != this.layer28_ || this.layer29 != this.layer29_ || this.layer30 != this.layer30_ || this.layer31 != this.layer31_)
		{
			this.Awake();
		}
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x0009E368 File Offset: 0x0009C568
	private static bool Set(ref float m, float v)
	{
		if (m == v)
		{
			return false;
		}
		m = v;
		return true;
	}

	// Token: 0x17000925 RID: 2341
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
				flag = CameraLayerDepths.Set(ref this.layer00, value);
				break;
			case 1:
				flag = CameraLayerDepths.Set(ref this.layer01, value);
				break;
			case 2:
				flag = CameraLayerDepths.Set(ref this.layer02, value);
				break;
			case 3:
				flag = CameraLayerDepths.Set(ref this.layer03, value);
				break;
			case 4:
				flag = CameraLayerDepths.Set(ref this.layer04, value);
				break;
			case 5:
				flag = CameraLayerDepths.Set(ref this.layer05, value);
				break;
			case 6:
				flag = CameraLayerDepths.Set(ref this.layer06, value);
				break;
			case 7:
				flag = CameraLayerDepths.Set(ref this.layer07, value);
				break;
			case 8:
				flag = CameraLayerDepths.Set(ref this.layer08, value);
				break;
			case 9:
				flag = CameraLayerDepths.Set(ref this.layer09, value);
				break;
			case 10:
				flag = CameraLayerDepths.Set(ref this.layer10, value);
				break;
			case 11:
				flag = CameraLayerDepths.Set(ref this.layer11, value);
				break;
			case 12:
				flag = CameraLayerDepths.Set(ref this.layer12, value);
				break;
			case 13:
				flag = CameraLayerDepths.Set(ref this.layer13, value);
				break;
			case 14:
				flag = CameraLayerDepths.Set(ref this.layer14, value);
				break;
			case 15:
				flag = CameraLayerDepths.Set(ref this.layer15, value);
				break;
			case 16:
				flag = CameraLayerDepths.Set(ref this.layer16, value);
				break;
			case 17:
				flag = CameraLayerDepths.Set(ref this.layer17, value);
				break;
			case 18:
				flag = CameraLayerDepths.Set(ref this.layer18, value);
				break;
			case 19:
				flag = CameraLayerDepths.Set(ref this.layer19, value);
				break;
			case 20:
				flag = CameraLayerDepths.Set(ref this.layer20, value);
				break;
			case 21:
				flag = CameraLayerDepths.Set(ref this.layer21, value);
				break;
			case 22:
				flag = CameraLayerDepths.Set(ref this.layer22, value);
				break;
			case 23:
				flag = CameraLayerDepths.Set(ref this.layer23, value);
				break;
			case 24:
				flag = CameraLayerDepths.Set(ref this.layer24, value);
				break;
			case 25:
				flag = CameraLayerDepths.Set(ref this.layer25, value);
				break;
			case 26:
				flag = CameraLayerDepths.Set(ref this.layer26, value);
				break;
			case 27:
				flag = CameraLayerDepths.Set(ref this.layer27, value);
				break;
			case 28:
				flag = CameraLayerDepths.Set(ref this.layer28, value);
				break;
			case 29:
				flag = CameraLayerDepths.Set(ref this.layer29, value);
				break;
			case 30:
				flag = CameraLayerDepths.Set(ref this.layer30, value);
				break;
			case 31:
				flag = CameraLayerDepths.Set(ref this.layer31, value);
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

	// Token: 0x06002857 RID: 10327 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
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

	// Token: 0x06002858 RID: 10328 RVA: 0x0009E890 File Offset: 0x0009CA90
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

	// Token: 0x0400142A RID: 5162
	[SerializeField]
	private float layer00;

	// Token: 0x0400142B RID: 5163
	[SerializeField]
	private float layer01;

	// Token: 0x0400142C RID: 5164
	[SerializeField]
	private float layer02;

	// Token: 0x0400142D RID: 5165
	[SerializeField]
	private float layer03;

	// Token: 0x0400142E RID: 5166
	[SerializeField]
	private float layer04;

	// Token: 0x0400142F RID: 5167
	[SerializeField]
	private float layer05;

	// Token: 0x04001430 RID: 5168
	[SerializeField]
	private float layer06;

	// Token: 0x04001431 RID: 5169
	[SerializeField]
	private float layer07;

	// Token: 0x04001432 RID: 5170
	[SerializeField]
	private float layer08;

	// Token: 0x04001433 RID: 5171
	[SerializeField]
	private float layer09;

	// Token: 0x04001434 RID: 5172
	[SerializeField]
	private float layer10;

	// Token: 0x04001435 RID: 5173
	[SerializeField]
	private float layer11;

	// Token: 0x04001436 RID: 5174
	[SerializeField]
	private float layer12;

	// Token: 0x04001437 RID: 5175
	[SerializeField]
	private float layer13;

	// Token: 0x04001438 RID: 5176
	[SerializeField]
	private float layer14;

	// Token: 0x04001439 RID: 5177
	[SerializeField]
	private float layer15;

	// Token: 0x0400143A RID: 5178
	[SerializeField]
	private float layer16;

	// Token: 0x0400143B RID: 5179
	[SerializeField]
	private float layer17;

	// Token: 0x0400143C RID: 5180
	[SerializeField]
	private float layer18;

	// Token: 0x0400143D RID: 5181
	[SerializeField]
	private float layer19;

	// Token: 0x0400143E RID: 5182
	[SerializeField]
	private float layer20;

	// Token: 0x0400143F RID: 5183
	[SerializeField]
	private float layer21;

	// Token: 0x04001440 RID: 5184
	[SerializeField]
	private float layer22;

	// Token: 0x04001441 RID: 5185
	[SerializeField]
	private float layer23;

	// Token: 0x04001442 RID: 5186
	[SerializeField]
	private float layer24;

	// Token: 0x04001443 RID: 5187
	[SerializeField]
	private float layer25;

	// Token: 0x04001444 RID: 5188
	[SerializeField]
	private float layer26;

	// Token: 0x04001445 RID: 5189
	[SerializeField]
	private float layer27;

	// Token: 0x04001446 RID: 5190
	[SerializeField]
	private float layer28;

	// Token: 0x04001447 RID: 5191
	[SerializeField]
	private float layer29;

	// Token: 0x04001448 RID: 5192
	[SerializeField]
	private float layer30;

	// Token: 0x04001449 RID: 5193
	[SerializeField]
	private float layer31;

	// Token: 0x0400144A RID: 5194
	[SerializeField]
	private bool spherical;

	// Token: 0x0400144B RID: 5195
	[NonSerialized]
	private float layer00_;

	// Token: 0x0400144C RID: 5196
	[NonSerialized]
	private float layer01_;

	// Token: 0x0400144D RID: 5197
	[NonSerialized]
	private float layer02_;

	// Token: 0x0400144E RID: 5198
	[NonSerialized]
	private float layer03_;

	// Token: 0x0400144F RID: 5199
	[NonSerialized]
	private float layer04_;

	// Token: 0x04001450 RID: 5200
	[NonSerialized]
	private float layer05_;

	// Token: 0x04001451 RID: 5201
	[NonSerialized]
	private float layer06_;

	// Token: 0x04001452 RID: 5202
	[NonSerialized]
	private float layer07_;

	// Token: 0x04001453 RID: 5203
	[NonSerialized]
	private float layer08_;

	// Token: 0x04001454 RID: 5204
	[NonSerialized]
	private float layer09_;

	// Token: 0x04001455 RID: 5205
	[NonSerialized]
	private float layer10_;

	// Token: 0x04001456 RID: 5206
	[NonSerialized]
	private float layer11_;

	// Token: 0x04001457 RID: 5207
	[NonSerialized]
	private float layer12_;

	// Token: 0x04001458 RID: 5208
	[NonSerialized]
	private float layer13_;

	// Token: 0x04001459 RID: 5209
	[NonSerialized]
	private float layer14_;

	// Token: 0x0400145A RID: 5210
	[NonSerialized]
	private float layer15_;

	// Token: 0x0400145B RID: 5211
	[NonSerialized]
	private float layer16_;

	// Token: 0x0400145C RID: 5212
	[NonSerialized]
	private float layer17_;

	// Token: 0x0400145D RID: 5213
	[NonSerialized]
	private float layer18_;

	// Token: 0x0400145E RID: 5214
	[NonSerialized]
	private float layer19_;

	// Token: 0x0400145F RID: 5215
	[NonSerialized]
	private float layer20_;

	// Token: 0x04001460 RID: 5216
	[NonSerialized]
	private float layer21_;

	// Token: 0x04001461 RID: 5217
	[NonSerialized]
	private float layer22_;

	// Token: 0x04001462 RID: 5218
	[NonSerialized]
	private float layer23_;

	// Token: 0x04001463 RID: 5219
	[NonSerialized]
	private float layer24_;

	// Token: 0x04001464 RID: 5220
	[NonSerialized]
	private float layer25_;

	// Token: 0x04001465 RID: 5221
	[NonSerialized]
	private float layer26_;

	// Token: 0x04001466 RID: 5222
	[NonSerialized]
	private float layer27_;

	// Token: 0x04001467 RID: 5223
	[NonSerialized]
	private float layer28_;

	// Token: 0x04001468 RID: 5224
	[NonSerialized]
	private float layer29_;

	// Token: 0x04001469 RID: 5225
	[NonSerialized]
	private float layer30_;

	// Token: 0x0400146A RID: 5226
	[NonSerialized]
	private float layer31_;

	// Token: 0x0400146B RID: 5227
	[NonSerialized]
	private bool spherical_;
}
