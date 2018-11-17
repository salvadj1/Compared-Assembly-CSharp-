using System;
using UnityEngine;

// Token: 0x02000688 RID: 1672
public class SoundPoolTest : MonoBehaviour
{
	// Token: 0x060039F7 RID: 14839 RVA: 0x000D732C File Offset: 0x000D552C
	private void OnEnable()
	{
		this.first = true;
	}

	// Token: 0x060039F8 RID: 14840 RVA: 0x000D7338 File Offset: 0x000D5538
	private void Update()
	{
		if (this.clips != null && this.intervalPlayRandomClip > 0f)
		{
			float num = Mathf.Max(0.05f, this.intervalPlayRandomClip);
			if (this.first)
			{
				this.lastTime = Time.time - num;
			}
			this.first = false;
			while (Time.time - this.lastTime >= num)
			{
				AudioClip clip = this.clips[Random.Range(0, this.clips.Length)];
				if (this.on != null && this.on.Length > 0 && Random.value <= this.chanceOn)
				{
					clip.Play(this.on[Random.Range(0, this.on.Length)]);
				}
				else
				{
					clip.Play();
				}
				this.lastTime += num;
			}
		}
		else
		{
			this.first = true;
		}
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x000D7428 File Offset: 0x000D5628
	public void OnGUI()
	{
		if (this.clips != null)
		{
			foreach (AudioClip audioClip in this.clips)
			{
				if (GUILayout.Button(audioClip.name, new GUILayoutOption[0]))
				{
					audioClip.Play();
				}
			}
		}
		GUI.Box(new Rect((float)(Screen.width - 256), 0f, 256f, 24f), "Total Sound Nodes   " + SoundPool.totalCount);
		GUI.Box(new Rect((float)(Screen.width - 256), 30f, 256f, 24f), "Playing Sound Nodes " + SoundPool.playingCount);
		GUI.Box(new Rect((float)(Screen.width - 256), 60f, 256f, 24f), "Reserve Sound Nodes " + SoundPool.reserveCount);
		if (GUI.Button(new Rect((float)(Screen.width - 128), 90f, 128f, 24f), "Drain Reserves"))
		{
			SoundPool.DrainReserves();
		}
		if (GUI.Button(new Rect((float)(Screen.width - 128), 120f, 128f, 24f), "Drain"))
		{
			SoundPool.Drain();
		}
		if (GUI.Button(new Rect((float)(Screen.width - 128), 150f, 128f, 24f), "Stop All"))
		{
			SoundPool.Stop();
		}
	}

	// Token: 0x04001E02 RID: 7682
	public AudioClip[] clips;

	// Token: 0x04001E03 RID: 7683
	public Transform[] on;

	// Token: 0x04001E04 RID: 7684
	public float chanceOn;

	// Token: 0x04001E05 RID: 7685
	public float intervalPlayRandomClip;

	// Token: 0x04001E06 RID: 7686
	private float lastTime;

	// Token: 0x04001E07 RID: 7687
	private bool first;
}
