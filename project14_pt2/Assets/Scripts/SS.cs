using UnityEngine;
using System.Collections;

public class SS : MonoBehaviour {

  
   void Update()
    {
        StartCoroutine(UploadPNG());
    }

 
    public static IEnumerator UploadPNG()
    {
      
        yield return new WaitForEndOfFrame();
        
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        
        byte[] bytes = tex.EncodeToPNG();

        //Debug.Log(bytes[60]);
        Object.Destroy(tex);

        yield return bytes;
    }
}
