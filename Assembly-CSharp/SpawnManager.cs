using System;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class SpawnManager : MonoBehaviour
{
	// Token: 0x06002211 RID: 8721 RVA: 0x0007DA3C File Offset: 0x0007BC3C
	public virtual void AddPlayerSpawn(GameObject spawn)
	{
		global::ServerManagement.Get().AddPlayerSpawn(spawn);
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x0007DA4C File Offset: 0x0007BC4C
	public virtual void RemovePlayerSpawn(GameObject spawn)
	{
		global::ServerManagement serverManagement = global::ServerManagement.Get();
		if (serverManagement)
		{
			serverManagement.RemovePlayerSpawn(spawn);
		}
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x0007DA74 File Offset: 0x0007BC74
	private void Awake()
	{
		global::SpawnManager._spawnMan = this;
		this.InstallSpawns();
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x0007DA84 File Offset: 0x0007BC84
	private void InstallSpawns()
	{
		global::SpawnManager._spawnPoints = new global::SpawnManager.SpawnData[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			global::SpawnManager._spawnPoints[i].pos = child.position;
			global::SpawnManager._spawnPoints[i].rot = child.rotation;
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x0007DB64 File Offset: 0x0007BD64
	private void Update()
	{
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x0007DB68 File Offset: 0x0007BD68
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

	// Token: 0x06002217 RID: 8727 RVA: 0x0007DC34 File Offset: 0x0007BE34
	public static void GetRandomSpawn(out Vector3 pos, out Quaternion rot)
	{
		int num = Random.Range(0, global::SpawnManager._spawnPoints.Length);
		pos = global::SpawnManager._spawnPoints[num].pos;
		rot = global::SpawnManager._spawnPoints[num].rot;
		global::SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x0007DC84 File Offset: 0x0007BE84
	public static void GetClosestSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.PositiveInfinity;
		int num2 = -1;
		for (int i = 0; i < global::SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - global::SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - global::SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - global::SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			global::SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = global::SpawnManager._spawnPoints[num2].pos;
			rot = global::SpawnManager._spawnPoints[num2].rot;
		}
		global::SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x0007DDA4 File Offset: 0x0007BFA4
	public static void GetCloseSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.PositiveInfinity;
		int num2 = -1;
		for (int i = 0; i < global::SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - global::SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - global::SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - global::SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 < num && num3 > 40f)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			global::SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = global::SpawnManager._spawnPoints[num2].pos;
			rot = global::SpawnManager._spawnPoints[num2].rot;
		}
		global::SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x0007DED0 File Offset: 0x0007C0D0
	public static void GetFarthestSpawn(Vector3 point, out Vector3 pos, out Quaternion rot)
	{
		float num = float.NegativeInfinity;
		int num2 = -1;
		for (int i = 0; i < global::SpawnManager._spawnPoints.Length; i++)
		{
			Vector3 vector;
			vector.x = point.x - global::SpawnManager._spawnPoints[i].pos.x;
			vector.y = point.y - global::SpawnManager._spawnPoints[i].pos.y;
			vector.z = point.z - global::SpawnManager._spawnPoints[i].pos.z;
			float num3 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num3 > num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num2 == -1)
		{
			global::SpawnManager.GetRandomSpawn(out pos, out rot);
		}
		else
		{
			pos = global::SpawnManager._spawnPoints[num2].pos;
			rot = global::SpawnManager._spawnPoints[num2].rot;
		}
		global::SpawnManager.RandomizeAndScanSpawnPosition(ref pos);
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x0007DFF0 File Offset: 0x0007C1F0
	public global::SpawnManager Get()
	{
		return global::SpawnManager._spawnMan;
	}

	// Token: 0x04001036 RID: 4150
	private const float kRandomizeSpawnRadius = 10f;

	// Token: 0x04001037 RID: 4151
	public static global::SpawnManager.SpawnData[] _spawnPoints;

	// Token: 0x04001038 RID: 4152
	public static global::SpawnManager _spawnMan;

	// Token: 0x020003CD RID: 973
	public struct SpawnData
	{
		// Token: 0x04001039 RID: 4153
		public Vector3 pos;

		// Token: 0x0400103A RID: 4154
		public Quaternion rot;
	}
}
