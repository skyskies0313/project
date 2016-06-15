using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Net;
using System.Text;
using System.IO;
using MsgPack;
using UnityStandardAssets.Vehicles.Car;

namespace WS{

    public class Net : MonoBehaviour
    {
        WebSocket ws; // 通信を行うオブジェクトの生成
        public string WSAddress = "ws://127.0.0.1:3000/"; // 接続するURLの定義
        public Texture2D tex; // カメラ画像を保存する変数の定義
        public MsgPack.CompiledPacker packer = new MsgPack.CompiledPacker(); // バイナリ化するクラスの生成
        public static string command;
        private int framecount=0;
        public static byte[] sendData;
        public static byte[][] data;
        public static byte[] bytes;
        void Start()
        { // 起動時に実行される関数
            Connect(); // サーバに接続する
        }
        public static string get_command(){
            return command;
            }

        void Connect()
        { // 接続する関数の定義

            ws = new WebSocket(WSAddress);  // 指定されたURLに接続するオブジェクトを定義する
            ws.OnMessage += (sender, e) =>
            {  // メッセージが送られてきた時に実行する命令
                print("on message");
                 command = (string)e.Data;  // 送られてきたデータ(Object型)をstr型に変換           
                // ~~~~コマンドを入力して車を動かす~~~~
                // ~~~~報酬と画像を取得する~~~~
                // ~~~~報酬と画像を送信する~~~~
            };
            ws.Connect(); // サーバに接続する
        }

        IEnumerator GetImage()
        {   // 画像を取得する関数の定義
            yield return new WaitForEndOfFrame(); // 画面の描画が終わるまで待つ
            tex = new Texture2D(Screen.width, Screen.height, // カメラ画像を保存する変数
                TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); // カメラ画像を読み込む
            tex.Apply(); // 描画を確定する
            bytes = tex.EncodeToPNG(); // 取得したカメラ画像をPNGのバイト列に変換
            Destroy(tex); // 取得したカメラ画像を破棄する
    
            data = new byte[2][];
            data[0] = bytes;
            data[1] = Calc.CalcReward();
            sendData = packer.Pack(data);
            ws.Send(sendData); // サーバに送る
            print("sended Image");
            yield break; // コルーチンを終了する
        }

        void Update()
        { 
            //if (framecount ==3)
            if(Input.GetKeyDown("g"))
            { 
                StartCoroutine("GetImage"); // GetImage関数をコルーチンとして実行する
                //Debug.Log(data[1]);
               // framecount = 0;
            }
            //framecount++;
        }
    }
}