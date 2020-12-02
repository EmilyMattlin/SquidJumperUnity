using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEffects : MonoBehaviour
{
	private int i;
	private bool active;
	public GameObject rightWall;
	public GameObject leftWall;
	public GameObject rightWallx2;
	public GameObject leftWallx2;
	public GameObject rightWallx3;
	public GameObject leftWallx3;
	private enum WallSide { Right, Left };
	private WallSide side;
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
		var values = WallSide.GetValues(typeof(WallSide));
		side = (WallSide)Random.Range(0, System.Enum.GetValues(typeof(WallSide)).Length); 
		GameObject newWall;
		if (side == WallSide.Left)
		{
			Vector3 newLWalllPos = new Vector3(leftWall.transform.position.x, leftWall.transform.position.y, transform.position.z);
			newWall = Instantiate(leftWall, newLWalllPos, transform.rotation);
		}
		else
		{
			Vector3 newRWalllPos = new Vector3(rightWall.transform.position.x, rightWall.transform.position.y, transform.position.z);
			newWall = Instantiate(rightWall, newRWalllPos, transform.rotation);
		}
		newWall.AddComponent<WallBehavior>();
		active = !active;
		//UnityEngine.Debug.Log("Beat!!!");
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