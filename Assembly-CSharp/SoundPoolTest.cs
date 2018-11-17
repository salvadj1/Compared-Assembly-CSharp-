using System;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class SoundPoolTest : MonoBehaviour
{
	// Token: 0x06003DEF RID: 15855 RVA: 0x000DFD0C File Offset: 0x000DDF0C
	private void OnEnable()
	{
		this.first = true;
	}

	// Token: 0x06003DF0 RID: 15856 RVA: 0x000DFD18 File Offset: 0x000DDF18
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

	// Token: 0x06003DF1 RID: 15857 RVA: 0x000DFE08 File Offset: 0x000DE008
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
		GUI.Box(new Rect((float)(Screen.width - 256), 0f, 256f, 24f), "Total Sound Nodes   " + global::SoundPool.totalCount);
		GUI.Box(new Rect((float)(Screen.width - 256), 30f, 256f, 24f), "Playing Sound Nodes " + global::SoundPool.playingCount);
		GUI.Box(new Rect((float)(Screen.width - 256), 60f, 256f, 24f), "Reserve Sound Nodes " + global::SoundPool.reserveCount);
		if (GUI.Button(new Rect((float)(Screen.width - 128), 90f, 128f, 24f), "Drain Reserves"))
		{
			global::SoundPool.DrainReserves();
		}
		if (GUI.Button(new Rect((float)(Screen.width - 128), 120f, 128f, 24f), "Drain"))
		{
			global::SoundPool.Drain();
		}
		if (GUI.Button(new Rect((float)(Screen.width - 128), 150f, 128f, 24f), "Stop All"))
		{
			global::SoundPool.Stop();
		}
	}

	// Token: 0x04001FFA RID: 8186
	public AudioClip[] clips;

	// Token: 0x04001FFB RID: 8187
	public Transform[] on;

	// Token: 0x04001FFC RID: 8188
	public float chanceOn;

	// Token: 0x04001FFD RID: 8189
	public float intervalPlayRandomClip;

	// Token: 0x04001FFE RID: 8190
	private float lastTime;

	// Token: 0x04001FFF RID: 8191
	private bool first;
}
