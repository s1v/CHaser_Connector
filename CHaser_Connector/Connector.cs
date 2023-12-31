﻿using CHaserConnector.exception;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Connector: IDisposable
{
    public int CurrentTurn { get; protected set; } = 1;

    protected string ip { get; init; }
    protected int port { get; init; }
    protected string name { get; init; }
    protected bool doLogging { get; init; }
    protected Socket socketClient { get; init; }

    /// <summary>
    /// CHaserサーバー接続クライアント
    /// </summary>
    /// <param name="ip">接続先IPアドレス</param>
    /// <param name="port">接続先ポート番号</param>
    /// <param name="name">表示名</param>
    /// <param name="doLogging">ログ出力するかい？</param>
    public Connector(string ip, int port, string name, bool doLogging = true)
    {
        this.ip = ip;
        this.port = port;
        this.name = name;
        this.doLogging = doLogging;

        this.socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    /// <summary>
    /// CHaserサーバーに接続する
    /// </summary>
    /// <exception cref="ConnectException">サーバー接続失敗時に発生</exception>
    public Socket Connect()
    {
        try
        {
            //接続
            socketClient.Connect(new IPEndPoint(IPAddress.Parse(ip), port));

            //クライアント情報送信
            Send($"{name}\r\n");

            if (doLogging)
            {
                Console.WriteLine(
                    "Connection completed.\n" +
                    "Plz wait game start...\n"
                );
            }

            return socketClient;
        }
        catch (SocketException e)
        {
            if (doLogging)
            {
                Console.WriteLine(
                    "Failed to connect CHaser server."
                );
            }
            throw new ConnectException(e);
        }
    }

    /// <summary>
    /// サーバー接続を閉じる
    /// </summary>
    public void Dispose()
    {
        try
        {
            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
            socketClient.Dispose();
            this.Dispose();
        }
        catch
        {
            return;
        }
    }

    /// <summary>
    /// 操作用オブジェクトを取得する
    /// </summary>
    /// <returns>操作用オブジェクト</returns>
    public Controller GetController() {
        return new Controller(this);
    }

    /// <summary>
    /// CHaserサーバーへ文字列を送信する
    /// </summary>
    /// <param name="sendString">送信する文字列</param>
    /// <exception cref="ConnectException">サーバー接続失敗時に発生</exception>
    protected void Send(string sendString)
    {
        try
        {
            socketClient.Send(Encoding.UTF8.GetBytes(sendString));
        }
        catch (SocketException e)
        {
            //接続失敗時
            throw new ConnectException(e);
        }
    }

    /// <summary>
    /// CHaserサーバーと通信する
    /// </summary>
    /// <param name="orderString">送信する命令</param>
    /// <returns>受信した情報</returns>
    /// <exception cref="ConnectException">サーバー接続失敗時に発生</exception>
    protected char[] Receive()
    {
        try
        {
            byte[] bytes = new byte[4096];
            int response = socketClient.Receive(bytes);
            return Encoding.UTF8.GetString(bytes, 0, response).ToCharArray();
        }
        catch (SocketException e)
        {
            //接続失敗時
            throw new ConnectException(e);
        }
    }

    /// <summary>
    /// CHaserサーバーと通信する
    /// </summary>
    /// <param name="orderString">送信する命令</param>
    /// <returns>受信した情報</returns>
    /// <exception cref="DesyncException">サーバーとの同期失敗時に発生</exception>
    /// <exception cref="GameSetException">ゲーム終了時に発生</exception>
    /// <exception cref="UnknownException">内部エラー発生時に発生</exception>
    public FieldObject[] Order(OrderName order)
    {
        try
        {
            if (!socketClient.IsBound) throw new UnconnectedException();

            if (order == OrderName.GetReady)
            {
                if (!(Receive()[0] == ControlCode.TurnStart)) //接続確認
                {
                    //ダメだった場合
                    throw new DesyncException();
                }

                if (!doLogging) CurrentTurn++;
            }

            //サーバーへ命令送信
            Send($"{((OrderCode)order).ToString()}\r\n");
            if (doLogging)
            {
                if (++CurrentTurn % 2 == 0) Console.WriteLine("************************************\n");
                Console.WriteLine($"Turn.{CurrentTurn / 2}: {order.ToString()}");
            }

            //サーバーから情報受信
            FieldObject[] response = Receive().Take(10).Select(chara => (FieldObject)(int.Parse(chara.ToString()))).ToArray();

            if (order != OrderName.GetReady)
            {
                Send($"{ControlCode.TurnEnd}\r\n");
            }

            switch ((GameStatus)response[0])
            {
                case GameStatus.Progress:
                    response = response.Skip(1).Take(9).ToArray();
                    if (doLogging)
                    {
                        WriteFieldInfo(response, order);
                    }
                    return response;

                case GameStatus.Finished:
                    throw new GameSetException();

                default:
                    throw new UnknownException();
            }
        }
        catch (CHaserConnectorException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new UnknownException(e);
        }
    }

    protected void WriteFieldInfo(FieldObject[] infos, OrderName order)
    {
        int i = 0;
        foreach (FieldObject info in infos)
        {
            switch (info)
            {
                case FieldObject.none:
                    Console.Write("[　]");
                    break;
                case FieldObject.block:
                    Console.Write("[□]");
                    break;
                case FieldObject.item:
                    Console.Write("[☆]");
                    break;
                case FieldObject.enemy:
                    Console.Write("[×]");
                    break;
            }
            if (++i % 3 == 0 && order is not (OrderName.SearchDown or OrderName.SearchUp or OrderName.SearchLeft or OrderName.SearchRight))
            {
                Console.WriteLine();
            }
            else if (i % 9 == 0) Console.WriteLine();
        }
        Console.WriteLine();
    }
}