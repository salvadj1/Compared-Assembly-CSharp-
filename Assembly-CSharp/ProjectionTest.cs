using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000686 RID: 1670
[ExecuteInEditMode]
[AddComponentMenu("Precision/Tests/Projection Test")]
public class ProjectionTest : MonoBehaviour
{
	// Token: 0x17000B38 RID: 2872
	// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000D6A28 File Offset: 0x000D4C28
	public Matrix4x4 UnityMatrix
	{
		get
		{
			return this.unity;
		}
	}

	// Token: 0x17000B39 RID: 2873
	// (get) Token: 0x060039EA RID: 14826 RVA: 0x000D6A30 File Offset: 0x000D4C30
	public Matrix4x4G GMatrix
	{
		get
		{
			return this.facep;
		}
	}

	// Token: 0x17000B3A RID: 2874
	// (get) Token: 0x060039EB RID: 14827 RVA: 0x000D6A38 File Offset: 0x000D4C38
	public Matrix4x4 UnityMatrixCasted
	{
		get
		{
			return this.unity2;
		}
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x000D6A40 File Offset: 0x000D4C40
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

	// Token: 0x060039ED RID: 14829 RVA: 0x000D6A74 File Offset: 0x000D4C74
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

	// Token: 0x060039EE RID: 14830 RVA: 0x000D6B08 File Offset: 0x000D4D08
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

	// Token: 0x060039EF RID: 14831 RVA: 0x000D6BC4 File Offset: 0x000D4DC4
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

	// Token: 0x060039F0 RID: 14832 RVA: 0x000D6D70 File Offset: 0x000D4F70
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

	// Token: 0x060039F1 RID: 14833 RVA: 0x000D6E54 File Offset: 0x000D5054
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

	// Token: 0x04001DE8 RID: 7656
	private const float cellWidth = 100f;

	// Token: 0x04001DE9 RID: 7657
	private const float cellHeight = 30f;

	// Token: 0x04001DEA RID: 7658
	private GUIContent[,,] contents;

	// Token: 0x04001DEB RID: 7659
	private Rect[,,] rects;

	// Token: 0x04001DEC RID: 7660
	private Matrix4x4 unity = Matrix4x4.identity;

	// Token: 0x04001DED RID: 7661
	private Matrix4x4 lastUnity;

	// Token: 0x04001DEE RID: 7662
	private Matrix4x4G facep = Matrix4x4G.identity;

	// Token: 0x04001DEF RID: 7663
	private Matrix4x4 unity2;

	// Token: 0x04001DF0 RID: 7664
	public float near = 1f;

	// Token: 0x04001DF1 RID: 7665
	public float aspect = -1f;

	// Token: 0x04001DF2 RID: 7666
	public float far = 1000f;

	// Token: 0x04001DF3 RID: 7667
	public float fov = 60f;

	// Token: 0x04001DF4 RID: 7668
	public bool revMul;

	// Token: 0x04001DF5 RID: 7669
	public bool revG;
}
