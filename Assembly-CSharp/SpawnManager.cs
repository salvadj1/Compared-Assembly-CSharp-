using System;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class SpawnManager : MonoBehaviour
{
	// Token: 0x06001EAF RID: 7855 RVA: 0x00078640 File Offset: 0x00076840
	public virtual void AddPlayerSpawn(GameObject spawn)
	{
		ServerManagement.Get().AddPlayerSpawn(spawn);
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x00078650 File Offset: 0x00076850
	public virtual void RemovePlayerSpawn(GameObject spawn)
	{
		ServerManagement serverManagement = ServerManagement.Get();
		if (serverManagement)
		{
			serverManagement.RemovePlayerSpawn(spawn);
		}
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x00078678 File Offset: 0x00076878
	private void Awake()
	{
		SpawnManager._spawnMan = this;
		this.InstallSpawns();
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x00078688 File Offset: 0x00076888
	private void InstallSpawns()
	{
		SpawnManager._spawnPoints = new SpawnManager.SpawnData[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			SpawnManager._spawnPoints[i].pos = child.position;
			SpawnManager._spawnPoints[i].rot = child.rotation;
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x00078768 File Offset: 0x00076968
	private void Update()
	{
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x0007876C File Offset: 0x0007696C
	public static bool RandomizeAndScanSpawnPosition(ref Vector3 pos)
	{
		Vector2 vector = Random.insideUnitCircle * 10f;
		Vector3 vector2;
		Vector3 vector3;
		vector2.x = (vector3.x = pos.x + vector.x);
		vector3.y = pos.y + 2000f;
		vector2.y = pos.y - 500f;
		vector2.z = (vector3.z = pos.z + vector.y);
		RaycastHit raycastHit;
		if (!Physics.Linecast(vector3, vector2, ref raycastHit, 525313))
		{
			return false;
		}
		pos = raycastHit.point;
		pos.y += raycastHit.normal.y * 0.25f;
		return true;
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x00078838 File Offset: 0x00076A38
	public static void GetRandomSpawn(out Vector3 pos, out Quaternion rot)
	{
		int num = Random.Range(0, SpawnManager._spawnPoints.Length);
		pos = SpawnManager._spawnPoints[num].pos;
		rot = SpawnManager._spawnPoints[num].rot;
		SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x00078888 File Offset: 0x00076A88
	public static void GetClosestSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.PositiveInfinity;
		int num2 = -1;
		for (int i = 0; i < SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = SpawnManager._spawnPoints[num2].pos;
			rot = SpawnManager._spawnPoints[num2].rot;
		}
		SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x000789A8 File Offset: 0x00076BA8
	public static void GetCloseSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.PositiveInfinity;
		int num2 = -1;
		for (int i = 0; i < SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 < num && num3 > 40f)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = SpawnManager._spawnPoints[num2].pos;
			rot = SpawnManager._spawnPoints[num2].rot;
		}
		SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x00078AD4 File Offset: 0x00076CD4
	public static void GetFarthestSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.NegativeInfinity;
		int num2 = -1;
		for (int i = 0; i < SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 > num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = SpawnManager._spawnPoints[num2].pos;
			rot = SpawnManager._spawnPoints[num2].rot;
		}
		SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x00078BF4 File Offset: 0x00076DF4
	public SpawnManager Get()
	{
		return SpawnManager._spawnMan;
	}

	// Token: 0x04000ED0 RID: 3792
	private const float kRandomizeSpawnRadius = 10f;

	// Token: 0x04000ED1 RID: 3793
	public static SpawnManager.SpawnData[] _spawnPoints;

	// Token: 0x04000ED2 RID: 3794
	public static SpawnManager _spawnMan;

	// Token: 0x02000320 RID: 800
	public struct SpawnData
	{
		// Token: 0x04000ED3 RID: 3795
		public Vector3 pos;

		// Token: 0x04000ED4 RID: 3796
		public Quaternion rot;
	}
}
