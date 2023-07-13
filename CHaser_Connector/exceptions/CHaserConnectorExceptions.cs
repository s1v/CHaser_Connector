namespace CHaserConnector.exception
{
    public abstract class CHaserConnectorException : Exception
    {
        public CHaserConnectorException(string message) : base(message) { }
        public CHaserConnectorException(string message , Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// CHaserサーバー接続失敗時に発生させるException
    /// </summary>
    public class ConnectException : CHaserConnectorException
    {
        public ConnectException(Exception inner) : base("Failed to connect CHaser server.", inner) { }
    }

    /// <summary>
    /// CHaserサーバーとの動機失敗時に発生させるException
    /// </summary>
    public class DesyncException : CHaserConnectorException
    {
        public DesyncException() : base("Failed to sync CHaser server.") { }
    }

    /// <summary>
    /// ゲームが終了した際に発生させるException
    /// </summary>
    public class GameSetException : CHaserConnectorException
    {
        public GameSetException() : base("Game Set!!") { }
    }

    /// <summary>
    /// その他のExceptionが発生したときにそれをラップするException
    /// </summary>
    public class UnknownException : CHaserConnectorException
    {
        public UnknownException() : base("Failed to connect CHaser server.") { }
        public UnknownException(Exception inner) : base("Failed to connect CHaser server.", inner) { }
    }
}