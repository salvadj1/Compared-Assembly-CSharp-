using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class TerrainMap : ScriptableObject
{
	// Token: 0x17000958 RID: 2392
	// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000A38BC File Offset: 0x000A1ABC
	public int width
	{
		get
		{
			return this._width;
		}
	}

	// Token: 0x17000959 RID: 2393
	// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000A38C4 File Offset: 0x000A1AC4
	public int height
	{
		get
		{
			return this._height;
		}
	}

	// Token: 0x1700095A RID: 2394
	// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000A38CC File Offset: 0x000A1ACC
	public int count
	{
		get
		{
			return this._width * this._height;
		}
	}

	// Token: 0x1700095B RID: 2395
	public string this[int i]
	{
		get
		{
			return this._guids[i];
		}
		set
		{
			this._guids[i] = value;
		}
	}

	// Token: 0x1700095C RID: 2396
	public string this[int x, int y]
	{
		get
		{
			return this[y * this._width + x];
		}
		set
		{
			this[y * this._width + x] = value;
		}
	}

	// Token: 0x060029CA RID: 10698 RVA: 0x000A391C File Offset: 0x000A1B1C
	public void ResizeGUIDS(int width, int height)
	{
		int width2 = this._width;
		int height2 = this._height;
		if (width2 != width || height2 != height)
		{
			string[] guids = this._guids;
			this._guids = new string[width * height];
			this._width = width;
			this._height = height;
			int num = Mathf.Min(width2, width);
			int num2 = Mathf.Min(height2, height);
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					this._guids[i * this._width + j] = guids[i * width2 + j];
				}
			}
		}
	}

	// Token: 0x040015B0 RID: 5552
	[SerializeField]
	private string[] _guids;

	// Token: 0x040015B1 RID: 5553
	[SerializeField]
	private int _width;

	// Token: 0x040015B2 RID: 5554
	[SerializeField]
	private int _height;

	// Token: 0x040015B3 RID: 5555
	public float baseHeight;

	// Token: 0x040015B4 RID: 5556
	public Vector3 scale;

	// Token: 0x040015B5 RID: 5557
	public Terrain copyFrom;

	// Token: 0x040015B6 RID: 5558
	public TerrainData root;
}
