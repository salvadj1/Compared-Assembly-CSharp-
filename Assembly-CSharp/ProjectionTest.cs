using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x0200074B RID: 1867
[AddComponentMenu("Precision/Tests/Projection Test")]
[ExecuteInEditMode]
public class ProjectionTest : MonoBehaviour
{
	// Token: 0x17000BBA RID: 3002
	// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x000DF408 File Offset: 0x000DD608
	public Matrix4x4 UnityMatrix
	{
		get
		{
			return this.unity;
		}
	}

	// Token: 0x17000BBB RID: 3003
	// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000DF410 File Offset: 0x000DD610
	public Matrix4x4G GMatrix
	{
		get
		{
			return this.facep;
		}
	}

	// Token: 0x17000BBC RID: 3004
	// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000DF418 File Offset: 0x000DD618
	public Matrix4x4 UnityMatrixCasted
	{
		get
		{
			return this.unity2;
		}
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x000DF420 File Offset: 0x000DD620
	private Matrix4x4G MultG(Matrix4x4G a, Matrix4x4G b)
	{
		Matrix4x4G result;
		if (this.revG)
		{
			Matrix4x4G.Mult(ref b, ref a, ref result);
		}
		else
		{
			Matrix4x4G.Mult(ref a, ref b, ref result);
		}
		return result;
	}

	// Token: 0x06003DE5 RID: 15845 RVA: 0x000DF454 File Offset: 0x000DD654
	private void Update()
	{
		double num = (this.aspect <= 0f) ? ((double)Screen.height / (double)Screen.width) : ((double)this.aspect);
		this.unity = Matrix4x4.Perspective(this.fov, (float)num, this.near, this.far);
		double num2 = (double)this.fov;
		double num3 = (double)this.near;
		double num4 = (double)this.far;
		Matrix4x4G.Perspective(ref num2, ref num, ref num3, ref num4, ref this.facep);
		this.unity2 = this.facep.f;
	}

	// Token: 0x06003DE6 RID: 15846 RVA: 0x000DF4E8 File Offset: 0x000DD6E8
	private void Awake()
	{
		this.contents = new GUIContent[3, 4, 4];
		this.rects = new Rect[3, 4, 4];
		float num = 20f;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				float num2 = 600f;
				for (int k = 0; k < 4; k++)
				{
					this.contents[i, j, k] = new GUIContent();
					this.rects[i, j, k] = new Rect(num2, num, 100f, 30f);
					num2 += 102f;
				}
				num += 32f;
			}
			num += 10f;
		}
	}

	// Token: 0x06003DE7 RID: 15847 RVA: 0x000DF5A4 File Offset: 0x000DD7A4
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
		if (this.lastUnity != this.unity)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.contents[num, i, j].text = this.unity[i, j].ToString();
				}
			}
			num = 1;
			for (int k = 0; k < 4; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					this.contents[num, k, l].text = this.facep[k, l].ToString();
				}
			}
			num = 2;
			for (int m = 0; m < 4; m++)
			{
				for (int n = 0; n < 4; n++)
				{
					this.contents[num, m, n].text = this.unity2[m, n].ToString();
				}
			}
			this.lastUnity = this.unity;
		}
		GUIStyle textField = GUI.skin.textField;
		for (int num2 = 0; num2 < 3; num2++)
		{
			for (int num3 = 0; num3 < 4; num3++)
			{
				for (int num4 = 0; num4 < 4; num4++)
				{
					this.DrawLabel(num2, num3, num4, textField);
				}
			}
		}
	}

	// Token: 0x06003DE8 RID: 15848 RVA: 0x000DF750 File Offset: 0x000DD950
	private Color RCCol(int col, int row)
	{
		Color result;
		switch (row | col % 2 << 2)
		{
		case 0:
			result = Color.red;
			break;
		case 1:
			result = Color.green;
			break;
		case 2:
			result = Color.blue;
			break;
		case 3:
			result = Color.magenta;
			break;
		case 4:
			result = Color.cyan;
			break;
		case 5:
			result = Color.yellow;
			break;
		case 6:
			result = Color.gray;
			break;
		case 7:
			result = Color.black;
			break;
		default:
			result = Color.clear;
			break;
		}
		if (col >= 2)
		{
			result.r += 0.25f;
			result.g += 0.25f;
			result.b += 0.25f;
		}
		return result;
	}

	// Token: 0x06003DE9 RID: 15849 RVA: 0x000DF834 File Offset: 0x000DDA34
	private void DrawLabel(int m, int col, int row, GUIStyle style)
	{
		if (this.contents[m, col, row].text != this.contents[0, col, row].text)
		{
			GUI.contentColor = this.RCCol(col, row);
		}
		else
		{
			GUI.contentColor = Color.white;
		}
		GUI.Label(this.rects[m, col, row], this.contents[m, col, row], style);
	}

	// Token: 0x04001FE0 RID: 8160
	private const float cellWidth = 100f;

	// Token: 0x04001FE1 RID: 8161
	private const float cellHeight = 30f;

	// Token: 0x04001FE2 RID: 8162
	private GUIContent[,,] contents;

	// Token: 0x04001FE3 RID: 8163
	private Rect[,,] rects;

	// Token: 0x04001FE4 RID: 8164
	private Matrix4x4 unity = Matrix4x4.identity;

	// Token: 0x04001FE5 RID: 8165
	private Matrix4x4 lastUnity;

	// Token: 0x04001FE6 RID: 8166
	private Matrix4x4G facep = Matrix4x4G.identity;

	// Token: 0x04001FE7 RID: 8167
	private Matrix4x4 unity2;

	// Token: 0x04001FE8 RID: 8168
	public float near = 1f;

	// Token: 0x04001FE9 RID: 8169
	public float aspect = -1f;

	// Token: 0x04001FEA RID: 8170
	public float far = 1000f;

	// Token: 0x04001FEB RID: 8171
	public float fov = 60f;

	// Token: 0x04001FEC RID: 8172
	public bool revMul;

	// Token: 0x04001FED RID: 8173
	public bool revG;
}
