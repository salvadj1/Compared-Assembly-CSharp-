using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000683 RID: 1667
[AddComponentMenu("Precision/Tests/Matrix Test")]
[ExecuteInEditMode]
public class MatrixTest : MonoBehaviour
{
	// Token: 0x060039DA RID: 14810 RVA: 0x000D61C8 File Offset: 0x000D43C8
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

	// Token: 0x060039DB RID: 14811 RVA: 0x000D61FC File Offset: 0x000D43FC
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

	// Token: 0x060039DC RID: 14812 RVA: 0x000D634C File Offset: 0x000D454C
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

	// Token: 0x060039DD RID: 14813 RVA: 0x000D6408 File Offset: 0x000D4608
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

	// Token: 0x060039DE RID: 14814 RVA: 0x000D6588 File Offset: 0x000D4788
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

	// Token: 0x060039DF RID: 14815 RVA: 0x000D666C File Offset: 0x000D486C
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

	// Token: 0x04001DD3 RID: 7635
	private const float cellWidth = 100f;

	// Token: 0x04001DD4 RID: 7636
	private const float cellHeight = 30f;

	// Token: 0x04001DD5 RID: 7637
	private const string formatFloat = "0.#####";

	// Token: 0x04001DD6 RID: 7638
	private GUIContent[,,] contents;

	// Token: 0x04001DD7 RID: 7639
	private Rect[,,] rects;

	// Token: 0x04001DD8 RID: 7640
	private Matrix4x4 unity = Matrix4x4.identity;

	// Token: 0x04001DD9 RID: 7641
	private Matrix4x4 lastUnity;

	// Token: 0x04001DDA RID: 7642
	private Matrix4x4G facep = Matrix4x4G.identity;

	// Token: 0x04001DDB RID: 7643
	public MatrixTest.TRS[] transforms;

	// Token: 0x04001DDC RID: 7644
	public bool revMul;

	// Token: 0x04001DDD RID: 7645
	public bool revG;

	// Token: 0x04001DDE RID: 7646
	public ProjectionTest projection;

	// Token: 0x04001DDF RID: 7647
	private ProjectionTest lastProjectionTest;

	// Token: 0x02000684 RID: 1668
	[Serializable]
	public class TRS
	{
		// Token: 0x060039E0 RID: 14816 RVA: 0x000D66EC File Offset: 0x000D48EC
		public TRS()
		{
			this.S = Vector3.one;
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000D6700 File Offset: 0x000D4900
		public Quaternion R_unity
		{
			get
			{
				return Quaternion.Euler(this.eulerR);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000D6710 File Offset: 0x000D4910
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000D6734 File Offset: 0x000D4934
		public Matrix4x4 unity
		{
			get
			{
				return Matrix4x4.TRS(this.T, this.R_unity, this.S);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000D6750 File Offset: 0x000D4950
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

		// Token: 0x04001DE0 RID: 7648
		public Vector3 T;

		// Token: 0x04001DE1 RID: 7649
		public Vector3 eulerR;

		// Token: 0x04001DE2 RID: 7650
		public Vector3 S;
	}
}
