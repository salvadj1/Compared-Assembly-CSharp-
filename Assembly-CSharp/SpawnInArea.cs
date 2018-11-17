using System;
using UnityEngine;

// Token: 0x0200084B RID: 2123
public class SpawnInArea : MonoBehaviour
{
	// Token: 0x06004ADF RID: 19167 RVA: 0x00146DD4 File Offset: 0x00144FD4
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

	// Token: 0x04002BF8 RID: 11256
	public Texture2D SpawnMap;

	// Token: 0x04002BF9 RID: 11257
	private float Offset = 10f;

	// Token: 0x04002BFA RID: 11258
	private float AboveGround = 1f;

	// Token: 0x04002BFB RID: 11259
	private bool TerrainOnly = true;
}
