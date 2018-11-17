using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x0200074C RID: 1868
[AddComponentMenu("Precision/Tests/Quaternion Test")]
[ExecuteInEditMode]
public class QuaternionTest : MonoBehaviour
{
	// Token: 0x06003DEB RID: 15851 RVA: 0x000DF8D0 File Offset: 0x000DDAD0
	private void Update()
	{
		if (this.R == null || this.R.Length == 0)
		{
			this.unity = Quaternion.identity;
			this.facep = QuaternionG.identity;
		}
		else if (this.revMul)
		{
			int i = this.R.Length - 1;
			this.unity = Quaternion.Euler(this.R[i]);
			Vector3G vector3G;
			vector3G..ctor(this.R[i]);
			QuaternionG.Euler(ref vector3G, ref this.facep);
			for (i--; i >= 0; i--)
			{
				this.unity = Quaternion.Euler(this.R[i]) * this.unity;
				vector3G.f = this.R[i];
				QuaternionG quaternionG;
				QuaternionG.Euler(ref vector3G, ref quaternionG);
				QuaternionG.Mult(ref quaternionG, ref this.facep, ref this.facep);
			}
		}
		else
		{
			int j = 0;
			this.unity = Quaternion.Euler(this.R[j]);
			Vector3G vector3G2;
			vector3G2..ctor(this.R[j]);
			QuaternionG.Euler(ref vector3G2, ref this.facep);
			for (j++; j < this.R.Length; j++)
			{
				this.unity *= Quaternion.Euler(this.R[j]);
				vector3G2.f = this.R[j];
				QuaternionG quaternionG2;
				QuaternionG.Euler(ref vector3G2, ref quaternionG2);
				QuaternionG quaternionG3 = this.facep;
				QuaternionG.Mult(ref quaternionG3, ref quaternionG2, ref this.facep);
			}
		}
	}

	// Token: 0x06003DEC RID: 15852 RVA: 0x000DFA98 File Offset: 0x000DDC98
	private void Awake()
	{
		this.contents = new GUIContent[3, 4];
		this.rects = new Rect[3, 4];
		float num = 400f;
		for (int i = 0; i < 3; i++)
		{
			float num2 = 20f;
			for (int j = 0; j < 4; j++)
			{
				this.contents[i, j] = new GUIContent();
				this.rects[i, j] = new Rect(num2, num, 100f, 30f);
				num2 += 102f;
			}
			num += 32f;
		}
		this.contents[2, 0].text = "Degrees:";
	}

	// Token: 0x06003DED RID: 15853 RVA: 0x000DFB48 File Offset: 0x000DDD48
	private void OnGUI()
	{
		if (Event.current.type != 7)
		{
			return;
		}
		if (this.contents == null)
		{
			this.Awake();
		}
		if (this.lastUnity != this.unity || this.nonHomogenous != this.nonHomogenousWas)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				this.contents[num, i].text = this.unity[i].ToString("0.######");
			}
			num = 1;
			for (int j = 0; j < 4; j++)
			{
				this.contents[num, j].text = this.facep[j].ToString("0.######");
			}
			num = 2;
			Vector3G vector3G;
			if (this.nonHomogenous)
			{
				QuaternionG.ToEulerNonHomogenious(ref this.facep, ref vector3G);
			}
			else
			{
				QuaternionG.ToEuler(ref this.facep, ref vector3G);
			}
			for (int k = 1; k < 4; k++)
			{
				this.contents[num, k].text = vector3G[k - 1].ToString("0.######");
			}
			this.nonHomogenousWas = this.nonHomogenous;
			this.lastUnity = this.unity;
		}
		GUIStyle textField = GUI.skin.textField;
		for (int l = 0; l < 3; l++)
		{
			for (int m = 0; m < 4; m++)
			{
				GUI.Label(this.rects[l, m], this.contents[l, m], textField);
			}
		}
	}

	// Token: 0x04001FEE RID: 8174
	private const float cellWidth = 100f;

	// Token: 0x04001FEF RID: 8175
	private const float cellHeight = 30f;

	// Token: 0x04001FF0 RID: 8176
	private const string formatFloat = "0.######";

	// Token: 0x04001FF1 RID: 8177
	private GUIContent[,] contents;

	// Token: 0x04001FF2 RID: 8178
	private Rect[,] rects;

	// Token: 0x04001FF3 RID: 8179
	private Quaternion unity = Quaternion.identity;

	// Token: 0x04001FF4 RID: 8180
	private Quaternion lastUnity;

	// Token: 0x04001FF5 RID: 8181
	private QuaternionG facep = QuaternionG.identity;

	// Token: 0x04001FF6 RID: 8182
	public Vector3[] R;

	// Token: 0x04001FF7 RID: 8183
	public bool revMul;

	// Token: 0x04001FF8 RID: 8184
	public bool nonHomogenous;

	// Token: 0x04001FF9 RID: 8185
	private bool nonHomogenousWas;
}
