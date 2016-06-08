using UnityEngine;
using System.Collections;


public class ScreenShot : MonoBehaviour {
    public bool graf;
 void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (graf)
            {
                Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

                tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                tex.Apply();

                // Encode texture into PNG
                byte[] bytes = tex.EncodeToPNG();
                Object.Destroy(tex);
                Debug.Log(bytes[50]);
                graf = false;
            }
        }
    }

    
 
}

