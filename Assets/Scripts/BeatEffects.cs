using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEffects : MonoBehaviour
{
	private int i;
	private bool active;
	public GameObject rightWall;
	public GameObject leftWall;
	// Start is called before the first frame update
	void Start()
    {
		transform.position += new Vector3(0f, 0f, 0.5f);
		active = true;
		i = 1;
		//Select the instance of AudioProcessor and pass a reference
		//to this object
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.onBeat.AddListener(onOnbeatDetected);
		processor.onSpectrum.AddListener(onSpectrum);
	}
	void Update()
    {
		transform.position += new Vector3(0f, 0f, 0.1f);
	}
	//this event will be called every time a beat is detected.
	//Change the threshold parameter in the inspector
	//to adjust the sensitivity
	void onOnbeatDetected()
	{

		Vector3 newRWalllPos = new Vector3(rightWall.transform.position.x, rightWall.transform.position.y, transform.position.z);
		GameObject newWall = Instantiate(rightWall, newRWalllPos, transform.rotation);
		newWall.AddComponent<WallBehavior>();
		//gameObject.active = !active;
		active = !active;
		UnityEngine.Debug.Log("Beat!!!");
		//UnityEngine.Debug.Log(i);
		i++;
	}

	//This event will be called every frame while music is playing
	void onSpectrum(float[] spectrum)
	{
		//The spectrum is logarithmically averaged
		//to 12 bands

		for (int i = 0; i < spectrum.Length; ++i)
		{
			Vector3 start = new Vector3(i, 0, 0);
			Vector3 end = new Vector3(i, spectrum[i], 0);
			UnityEngine.Debug.DrawLine(start, end);
		}
	}

}
/*
 * using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class BeatEffects : MonoBehaviour
{
	private int i;


	private bool active;
	public GameObject rightWall;
	public GameObject leftWall;
	void Start()
	{
		active = true;
		i = 1;
		//Select the instance of AudioProcessor and pass a reference
		//to this object
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.onBeat.AddListener(onOnbeatDetected);
		processor.onSpectrum.AddListener(onSpectrum);
	}

	//this event will be called every time a beat is detected.
	//Change the threshold parameter in the inspector
	//to adjust the sensitivity
	void onOnbeatDetected()
	{
		Vector3 
		Instantiate(leftWall, GameObject.Find("Squid").transform.position, transform.rotation);
		//gameObject.active = !active;
		active = !active;
		UnityEngine.Debug.Log("Beat!!!");
		UnityEngine.Debug.Log(i);
		i++;
	}

	//This event will be called every frame while music is playing
	void onSpectrum(float[] spectrum)
	{
		//The spectrum is logarithmically averaged
		//to 12 bands

		for (int i = 0; i < spectrum.Length; ++i)
		{
			Vector3 start = new Vector3(i, 0, 0);
			Vector3 end = new Vector3(i, spectrum[i], 0);
			UnityEngine.Debug.DrawLine(start, end);
		}
	}
}*/