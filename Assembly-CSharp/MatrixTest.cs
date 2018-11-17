using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000748 RID: 1864
[AddComponentMenu("Precision/Tests/Matrix Test")]
[ExecuteInEditMode]
public class MatrixTest : MonoBehaviour
{
	// Token: 0x06003DD2 RID: 15826 RVA: 0x000DEBA8 File Offset: 0x000DCDA8
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

	// Token: 0x06003DD3 RID: 15827 RVA: 0x000DEBDC File Offset: 0x000DCDDC
	private void Update()
	{
		Matrix4x4 matrix4x;
		Matrix4x4G matrix4x4G;
		if (this.transforms != null && this.transforms.Length > 0)
		{
			if (this.revMul)
			{
				int i = this.transforms.Length - 1;
				matrix4x = this.transforms[i].unity;
				matrix4x4G = this.transforms[i].facep;
				for (i--; i >= 0; i--)
				{
					matrix4x = this.transforms[i].unity * matrix4x;
					matrix4x4G = this.MultG(this.transforms[i].facep, matrix4x4G);
				}
			}
			else
			{
				int j = 0;
				matrix4x = this.transforms[j].unity;
				matrix4x4G = this.transforms[j].facep;
				for (j++; j < this.transforms.Length; j++)
				{
					matrix4x *= this.transforms[j].unity;
					matrix4x4G = this.MultG(matrix4x4G, this.transforms[j].facep);
				}
			}
		}
		else
		{
			matrix4x = Matrix4x4.identity;
			matrix4x4G = Matrix4x4G.identity;
		}
		if (this.projection)
		{
			matrix4x = this.projection.UnityMatrix * matrix4x;
			matrix4x4G = this.MultG(this.projection.GMatrix, matrix4x4G);
		}
		this.unity = matrix4x;
		this.facep = matrix4x4G;
	}

	// Token: 0x06003DD4 RID: 15828 RVA: 0x000DED2C File Offset: 0x000DCF2C
	private void Awake()
	{
		this.contents = new GUIContent[2, 4, 4];
		this.rects = new Rect[2, 4, 4];
		float num = 20f;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				float num2 = 20f;
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

	// Token: 0x06003DD5 RID: 15829 RVA: 0x000DEDE8 File Offset: 0x000DCFE8
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
		if (this.lastUnity != this.unity || this.projection != this.lastProjectionTest)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.contents[num, i, j].text = this.unity[i, j].ToString("0.#####");
				}
			}
			num = 1;
			for (int k = 0; k < 4; k++)
			{
				for (int l = 0; l < 4; l++)
				{
					this.contents[num, k, l].text = this.facep[k, l].ToString("0.#####");
				}
			}
			this.lastProjectionTest = this.projection;
			this.lastUnity = this.unity;
		}
		GUIStyle textField = GUI.skin.textField;
		for (int m = 0; m < 2; m++)
		{
			for (int n = 0; n < 4; n++)
			{
				for (int num2 = 0; num2 < 4; num2++)
				{
					this.DrawLabel(m, n, num2, textField);
				}
			}
		}
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x000DEF68 File Offset: 0x000DD168
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

	// Token: 0x06003DD7 RID: 15831 RVA: 0x000DF04C File Offset: 0x000DD24C
	private void DrawLabel(int m, int col, int row, GUIStyle style)
	{
		if (this.contents[m, col, row].text != this.contents[(m + 1) % 2, col, row].text)
		{
			GUI.contentColor = this.RCCol(col, row);
		}
		else
		{
			GUI.contentColor = Color.white;
		}
		GUI.Label(this.rects[m, col, row], this.contents[m, col, row], style);
	}

	// Token: 0x04001FCB RID: 8139
	private const float cellWidth = 100f;

	// Token: 0x04001FCC RID: 8140
	private const float cellHeight = 30f;

	// Token: 0x04001FCD RID: 8141
	private const string formatFloat = "0.#####";

	// Token: 0x04001FCE RID: 8142
	private GUIContent[,,] contents;

	// Token: 0x04001FCF RID: 8143
	private Rect[,,] rects;

	// Token: 0x04001FD0 RID: 8144
	private Matrix4x4 unity = Matrix4x4.identity;

	// Token: 0x04001FD1 RID: 8145
	private Matrix4x4 lastUnity;

	// Token: 0x04001FD2 RID: 8146
	private Matrix4x4G facep = Matrix4x4G.identity;

	// Token: 0x04001FD3 RID: 8147
	public global::MatrixTest.TRS[] transforms;

	// Token: 0x04001FD4 RID: 8148
	public bool revMul;

	// Token: 0x04001FD5 RID: 8149
	public bool revG;

	// Token: 0x04001FD6 RID: 8150
	public global::ProjectionTest projection;

	// Token: 0x04001FD7 RID: 8151
	private global::ProjectionTest lastProjectionTest;

	// Token: 0x02000749 RID: 1865
	[Serializable]
	public class TRS
	{
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000DF0CC File Offset: 0x000DD2CC
		public TRS()
		{
			this.S = Vector3.one;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000DF0E0 File Offset: 0x000DD2E0
		public Quaternion R_unity
		{
			get
			{
				return Quaternion.Euler(this.eulerR);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000DF0F0 File Offset: 0x000DD2F0
		public QuaternionG R_facep
		{
			get
			{
				Vector3G vector3G;
				vector3G..ctor(this.eulerR);
				QuaternionG result;
				QuaternionG.Euler(ref vector3G, ref result);
				return result;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000DF114 File Offset: 0x000DD314
		public Matrix4x4 unity
		{
			get
			{
				return Matrix4x4.TRS(this.T, this.R_unity, this.S);
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000DF130 File Offset: 0x000DD330
		public Matrix4x4G facep
		{
			get
			{
				Vector3G vector3G;
				vector3G..ctor(this.T);
				QuaternionG r_facep = this.R_facep;
				Vector3G vector3G2;
				vector3G2..ctor(this.S);
				Matrix4x4G result;
				Matrix4x4G.TRS(ref vector3G, ref r_facep, ref vector3G2, ref result);
				return result;
			}
		}

		// Token: 0x04001FD8 RID: 8152
		public Vector3 T;

		// Token: 0x04001FD9 RID: 8153
		public Vector3 eulerR;

		// Token: 0x04001FDA RID: 8154
		public Vector3 S;
	}
}
