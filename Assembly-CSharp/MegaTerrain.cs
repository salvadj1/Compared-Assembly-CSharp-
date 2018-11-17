using System;
using UnityEngine;

// Token: 0x020004E2 RID: 1250
public class MegaTerrain : MonoBehaviour
{
	// Token: 0x06002AD8 RID: 10968 RVA: 0x000AB1DC File Offset: 0x000A93DC
	private void Start()
	{
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x000AB1E0 File Offset: 0x000A93E0
	private void Update()
	{
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x000AB1E4 File Offset: 0x000A93E4
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

	// Token: 0x06002ADB RID: 10971 RVA: 0x000AB218 File Offset: 0x000A9418
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

	// Token: 0x06002ADC RID: 10972 RVA: 0x000AB280 File Offset: 0x000A9480
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

	// Token: 0x04001760 RID: 5984
	public TerrainData _rootTerrainData;

	// Token: 0x04001761 RID: 5985
	public Terrain[] _terrains;

	// Token: 0x04001762 RID: 5986
	public string name_base = "rust_terrain";
}
