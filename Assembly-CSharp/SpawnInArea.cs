using System;
using UnityEngine;

// Token: 0x02000941 RID: 2369
public class SpawnInArea : MonoBehaviour
{
	// Token: 0x06004FA0 RID: 20384 RVA: 0x00151398 File Offset: 0x0014F598
	private void RandomPositionOnTerrain(GameObject obj)
	{
		Vector3 size = Terrain.activeTerrain.terrainData.size;
		Vector3 vector = default(Vector3);
		bool flag = false;
		while (!flag)
		{
			vector = Terrain.activeTerrain.transform.position;
			float num = Random.Range(0f, size.x);
			float num2 = Random.Range(0f, size.z);
			vector.x += num;
			vector.y += size.y + this.Offset;
			vector.z += num2;
			if (this.SpawnMap)
			{
				int num3 = Mathf.RoundToInt((float)this.SpawnMap.width * num / size.x);
				int num4 = Mathf.RoundToInt((float)this.SpawnMap.height * num2 / size.z);
				float grayscale = this.SpawnMap.GetPixel(num3, num4).grayscale;
				flag = (grayscale > 0f && Random.Range(0f, 1f) < grayscale);
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(vector, -Vector3.up, ref raycastHit))
				{
					float distance = raycastHit.distance;
					if (raycastHit.transform.name != "Terrain" && this.TerrainOnly)
					{
						flag = false;
					}
					vector.y -= distance - this.AboveGround;
				}
				else
				{
					flag = false;
				}
			}
		}
		obj.transform.position = vector;
		base.transform.Rotate(Vector3.up * (float)Random.Range(0, 360), 0);
	}

	// Token: 0x04002E66 RID: 11878
	public Texture2D SpawnMap;

	// Token: 0x04002E67 RID: 11879
	private float Offset = 10f;

	// Token: 0x04002E68 RID: 11880
	private float AboveGround = 1f;

	// Token: 0x04002E69 RID: 11881
	private bool TerrainOnly = true;
}
