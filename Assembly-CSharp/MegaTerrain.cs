using System;
using UnityEngine;

// Token: 0x0200059D RID: 1437
public class MegaTerrain : MonoBehaviour
{
	// Token: 0x06002E8A RID: 11914 RVA: 0x000B2F74 File Offset: 0x000B1174
	private void Start()
	{
	}

	// Token: 0x06002E8B RID: 11915 RVA: 0x000B2F78 File Offset: 0x000B1178
	private void Update()
	{
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x000B2F7C File Offset: 0x000B117C
	[ContextMenu("Generate")]
	private void Generate()
	{
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
			}
		}
	}

	// Token: 0x06002E8D RID: 11917 RVA: 0x000B2FB0 File Offset: 0x000B11B0
	public Terrain FindTerrain(int x, int y)
	{
		string text = string.Concat(new object[]
		{
			this.name_base,
			"_x",
			x,
			"_y",
			y
		});
		return (!(GameObject.Find(text) != null)) ? null : GameObject.Find(text).GetComponent<Terrain>();
	}

	// Token: 0x06002E8E RID: 11918 RVA: 0x000B3018 File Offset: 0x000B1218
	[ContextMenu("Stitch")]
	private void Stitch()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Terrain terrain = this.FindTerrain(i, j);
				if (terrain)
				{
					Debug.Log("found terrain");
					Terrain terrain2 = this.FindTerrain(i - 1, j);
					Terrain terrain3 = this.FindTerrain(i + 1, j);
					Terrain terrain4 = this.FindTerrain(i, j + 1);
					Terrain terrain5 = this.FindTerrain(i, j - 1);
					terrain.SetNeighbors(terrain2, terrain4, terrain3, terrain5);
					if (terrain2)
					{
					}
				}
				else
				{
					Debug.Log(string.Concat(new object[]
					{
						"couldnt find terrain :",
						this.name_base,
						"_x",
						i,
						"_y",
						j
					}));
				}
			}
		}
	}

	// Token: 0x0400191D RID: 6429
	public TerrainData _rootTerrainData;

	// Token: 0x0400191E RID: 6430
	public Terrain[] _terrains;

	// Token: 0x0400191F RID: 6431
	public string name_base = "rust_terrain";
}
