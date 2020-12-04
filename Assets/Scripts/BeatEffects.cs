using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEffects : MonoBehaviour
{
	public GameObject rightWall;
	public GameObject leftWall;
	public GameObject rightWallx2;
	public GameObject leftWallx2;
	public GameObject rightWallx3;
	public GameObject leftWallx3;
	private enum WallSize { Small, Medium, Large };
	private WallSize size;
	private bool leftSide;

	// Start is called before the first frame update
	void Start()
    {
		transform.position += new Vector3(0f, 0f, 0.5f);
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
		if((Time.time % 1f) > .5f)
        {
			return;
        }
		//var values = WallSide.GetValues(typeof(WallSide));
		size = (WallSize)Random.Range(0, System.Enum.GetValues(typeof(WallSize)).Length); 
		leftSide = !leftSide;
		GameObject newWall;
		if (leftSide)//(side == WallSide.Left)
		{
			switch (size)
			{
				case WallSize.Small:
					 createBuilding(leftWall);
					break;
				case WallSize.Medium:
					createBuilding(leftWallx2);
					break;
				case WallSize.Large:
					createBuilding(leftWallx3);
					break;
				default:
					break;
			}
		}
		else
		{
			switch (size)
			{
				case WallSize.Small:
					createBuilding(rightWall);
					break;
				case WallSize.Medium:
					createBuilding(rightWallx2);
					break;
				case WallSize.Large:
					createBuilding(rightWallx3);
					break;
				default:
					break;
			}
		}
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

	private void createBuilding(GameObject wallObject)
    {
		float yPos = leftWall.transform.position.y + Random.Range(-6f, 6f);
		Vector3 newWalllPos = new Vector3(wallObject.transform.position.x, yPos, transform.position.z + 4f);
		Collider[] hitColliders = Physics.OverlapSphere(newWalllPos, 4f); // 4 is z axis radius of buildin
		
		GameObject newWall = Instantiate(wallObject, newWalllPos, transform.rotation);
		newWall.SetActive(true);
		newWall.AddComponent<WallBehavior>();
	}
}