using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200093E RID: 2366
public class CraterMaker : MonoBehaviour
{
	// Token: 0x06004F94 RID: 20372 RVA: 0x00150BCC File Offset: 0x0014EDCC
	public void Create(Vector3 position, float radius, float depth, float noise)
	{
		this.Create(new Vector2(position.x, position.z), radius, depth, noise);
	}

	// Token: 0x06004F95 RID: 20373 RVA: 0x00150BEC File Offset: 0x0014EDEC
	public void Create(Vector2 position, float radius, float depth, float noise)
	{
		base.StartCoroutine(this.RealCreate(position, radius, depth, noise));
	}

	// Token: 0x06004F96 RID: 20374 RVA: 0x00150C00 File Offset: 0x0014EE00
	public IEnumerator RealCreate(Vector2 position, float radius, float depth, float noise)
	{
		TerrainData tdata = this.MyTerrain.terrainData;
		Vector3 size = tdata.size;
		Vector3 pos = this.MyTerrain.transform.position;
		position.x -= pos.x;
		position.y -= pos.y;
		float scale = (float)tdata.heightmapResolution / size.x;
		int width = (int)Mathf.Floor(radius * scale);
		int xpos = (int)Mathf.Floor((position.x - radius) * scale);
		int ypos = (int)Mathf.Floor((position.y - radius) * scale);
		float[,] heights = tdata.GetHeights(xpos, ypos, width * 2, width * 2);
		float heightscale = depth / (size.y * 2f);
		for (int i = 0; i < width * 2; i++)
		{
			for (int j = 0; j < width * 2; j++)
			{
				float mod = Mathf.SmoothStep(1f, 0f, Mathf.Abs((float)width - (float)i) / (float)width) * Mathf.SmoothStep(1f, 0f, Mathf.Abs((float)width - (float)j) / (float)width);
				mod *= heightscale;
				if (noise > 0f)
				{
					mod += mod * heightscale * depth * Random.value * noise;
				}
				heights[i, j] -= mod;
			}
		}
		tdata.SetHeights(xpos, ypos, heights);
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		scale = (float)tdata.alphamapResolution / size.x;
		width = (int)Mathf.Floor(radius * scale);
		xpos = (int)Mathf.Floor((position.x - radius) * scale);
		ypos = (int)Mathf.Floor((position.y - radius) * scale);
		float[,,] textures = tdata.GetAlphamaps(xpos, ypos, width * 2, width * 2);
		int splats = textures.Length / (width * width * 4);
		for (int k = 0; k < width * 2; k++)
		{
			for (int l = 0; l < width * 2; l++)
			{
				float mod2 = Mathf.SmoothStep(1f, 0f, Mathf.Abs((float)width - (float)k) / (float)width) * Mathf.SmoothStep(1f, 0f, Mathf.Abs((float)width - (float)l) / (float)width);
				textures[k, l, this.insidetextureindex] += mod2;
				for (int s = 0; s < splats; s++)
				{
					if (s == this.insidetextureindex)
					{
						textures[k, l, s] += mod2;
					}
					else
					{
						textures[k, l, s] -= textures[k, l, s] * mod2;
					}
				}
				float sum = 0f;
				for (int s2 = 0; s2 < splats; s2++)
				{
					sum += textures[k, l, s2];
				}
				for (int s3 = 0; s3 < splats; s3++)
				{
					textures[k, l, s3] *= 1f / sum;
				}
			}
		}
		tdata.SetAlphamaps(xpos, ypos, textures);
		yield break;
	}

	// Token: 0x04002E40 RID: 11840
	public Terrain MyTerrain;

	// Token: 0x04002E41 RID: 11841
	public int insidetextureindex;
}
