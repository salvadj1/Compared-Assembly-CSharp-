using System;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public class TerrainMap : ScriptableObject
{
	// Token: 0x170009C8 RID: 2504
	// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000AB654 File Offset: 0x000A9854
	public int width
	{
		get
		{
			return this._width;
		}
	}

	// Token: 0x170009C9 RID: 2505
	// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000AB65C File Offset: 0x000A985C
	public int height
	{
		get
		{
			return this._height;
		}
	}

	// Token: 0x170009CA RID: 2506
	// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000AB664 File Offset: 0x000A9864
	public int count
	{
		get
		{
			return this._width * this._height;
		}
	}

	// Token: 0x170009CB RID: 2507
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

	// Token: 0x170009CC RID: 2508
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

	// Token: 0x06002D7C RID: 11644 RVA: 0x000AB6B4 File Offset: 0x000A98B4
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

	// Token: 0x0400176D RID: 5997
	[SerializeField]
	private string[] _guids;

	// Token: 0x0400176E RID: 5998
	[SerializeField]
	private int _width;

	// Token: 0x0400176F RID: 5999
	[SerializeField]
	private int _height;

	// Token: 0x04001770 RID: 6000
	public float baseHeight;

	// Token: 0x04001771 RID: 6001
	public Vector3 scale;

	// Token: 0x04001772 RID: 6002
	public Terrain copyFrom;

	// Token: 0x04001773 RID: 6003
	public TerrainData root;
}
