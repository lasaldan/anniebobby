using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class scrip : MonoBehaviour {

	public float speed = 10.0f;
//	private float X;
//	private float Y;
//	private float Z;
//	public float AccelerometerUpdateInterval = 1.0f / 100.0f;
//	public float LowPassKernelWidthInSeconds = 0.001f;
//	public Vector3 lowPassValue = Vector3.zero;

	public GameObject target;
	public Vector3 centerOffset;
	public float sensitivity = 1000;
	public float horizontalRange = 60;
	public float verticalRange = 30;
	public int filterWindowSize = 5;

	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Queue<Vector3> filter;








	private bool gyroBool;
	private Gyroscope gyro;
	private Quaternion rotFix;
	private Vector3 initial = new Vector3(90, 180, 0);





	// Use this for initialization
	void Start () {
//		Input.compensateSensors = true;
		Input.gyro.enabled = true;
//		initialPosition = transform.position;
//		initialRotation = transform.rotation;
//		filter = new Queue<Vector3>();





		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		gyroBool = SystemInfo.supportsGyroscope;

		Debug.Log("gyro bool = " + gyroBool.ToString());

		if (gyroBool)
		{
			gyro = Input.gyro;
			gyro.enabled = true;

			//rotFix = new Quaternion(0, 0, 0.7071f, 0.7071f);
			rotFix = new Quaternion(0, 0, 0.7071f, 0);
		}


	}








	
	// Update is called once per frame
	void Update () {

		if (SystemInfo.deviceType == DeviceType.Desktop) 
		{

			if (Input.GetKey(KeyCode.RightArrow)){
				transform.Rotate (new Vector3(0,1,0), Space.Self);
			}
			if (Input.GetKey(KeyCode.LeftArrow)){
				transform.Rotate (new Vector3(0,-1,0), Space.Self);
			}
			if (Input.GetKey(KeyCode.UpArrow)){
				transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime), Space.Self);
			}
			if (Input.GetKey(KeyCode.DownArrow)){
				transform.Translate (new Vector3 (0, 0, -speed * Time.deltaTime), Space.Self);
			}


		} else {
			var camRot = gyro.attitude * rotFix;
			transform.eulerAngles = initial;
			transform.localRotation *= camRot;
			//transform.Rotate (-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y * 2, Input.gyro.rotationRateUnbiased.z);




//			transform.position = initialPosition;
//			transform.rotation = initialRotation;
//
//			filter.Enqueue(Input.acceleration);
//			if (filter.Count > filterWindowSize)
//				filter.Dequeue();
//
//			float totalX = 0, totalY = 0;
//			foreach (Vector3 acc in filter)
//			{
//				totalX += acc.x;
//				totalY += acc.y;
//			}
//			float filteredX = totalX / filter.Count;
//			float filteredY = totalY / filter.Count;
//
//			float xc = -filteredX * horizontalRange;
//			float yc = (0.5f + filteredY) * 2 * verticalRange;
//
//			xc = Clamp(xc, -horizontalRange, horizontalRange);
//			yc = Clamp(yc, -verticalRange, verticalRange);
//
//			transform.RotateAround(target.transform.position + centerOffset, Vector3.up, xc);
//			transform.RotateAround(target.transform.position + centerOffset, Vector3.right, yc);
//			X = Input.acceleration.z * -90f;
//			Y = Input.acceleration.x * -90f;
//			Z = 0f;
//			//float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
//			transform.Rotate (-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
//
//			//transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(X, Y, Z)), Time.deltaTime * 5);
		}



	}

	public T Clamp<T>(T val, T min, T max) where T : IComparable<T>
	{
		if (val.CompareTo(min) < 0) return min;
		else if (val.CompareTo(max) > 0) return max;
		else return val;
	}

//	Vector3 lowpass(){
//		float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
//		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
//		return lowPassValue;
//	}
}
