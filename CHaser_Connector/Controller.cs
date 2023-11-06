public class Controller {
    protected Connector connector { get; init; }

    /// <summary>
    /// CHaser操作クライアント
    /// </summary>
    /// <param name="connector"></param>
    public Controller(Connector connector) {
        this.connector = connector;
    }

    /// <summary>
    /// 周囲の情報を取得します。必ずターンのはじめに実⾏必要があります。
    /// </summary>
    /// <returns>周囲9マスの情報</returns>
    public FieldObject[] GetReady() => connector.Order(OrderName.GetReady);

    /// <summary>
    /// 上に移動します。
    /// </summary>
    /// <returns></returns>
    public FieldObject[] WalkUp() => connector.Order(OrderName.WalkUp);

    /// <summary>
    /// 下に移動します。
    /// </summary>
    /// <returns></returns>
    public FieldObject[] WalkDown() => connector.Order(OrderName.WalkDown);

    /// <summary>
    /// 左に移動します。
    /// </summary>
    /// <returns></returns>
    public FieldObject[] WalkLeft() => connector.Order(OrderName.WalkLeft);

    /// <summary>
    /// 右に移動します。
    /// </summary>
    /// <returns></returns>
    public FieldObject[] WalkRight() => connector.Order(OrderName.WalkRight);

    /// <summary>
    /// 正⽅形状に上9マスの情報を取得します。
    /// </summary>
    /// <returns>上9マスの情報(正⽅形状)</returns>
    public FieldObject[] LookUp() => connector.Order(OrderName.LookUp);

    /// <summary>
    /// 正⽅形状に下9マスの情報を取得します。
    /// </summary>
    /// <returns>下9マスの情報(正⽅形状)</returns>
    public FieldObject[] LookDown() => connector.Order(OrderName.LookDown);

    /// <summary>
    /// 正⽅形状に左9マスの情報を取得します。
    /// </summary>
    /// <returns>左9マスの情報(正⽅形状)</returns>
    public FieldObject[] LookLeft() => connector.Order(OrderName.LookLeft);

    /// <summary>
    /// 正⽅形状に右9マスの情報を取得します。
    /// </summary>
    /// <returns>右9マスの情報(正⽅形状)</returns>
    public FieldObject[] LookRight() => connector.Order(OrderName.LookRight);

    /// <summary>
    /// 直線状に上9マスの情報を取得します。
    /// </summary>
    /// <returns>上9マスの情報(直線状)</returns>
    public FieldObject[] SearchUp() => connector.Order(OrderName.SearchUp);

    /// <summary>
    /// 直線状に下9マスの情報を取得します。
    /// </summary>
    /// <returns>下9マスの情報(直線状)</returns>
    public FieldObject[] SearchDown() => connector.Order(OrderName.SearchDown);

    /// <summary>
    /// 直線状に左9マスの情報を取得します。
    /// </summary>
    /// <returns>左9マスの情報(直線状)</returns>
    public FieldObject[] SearchLeft() => connector.Order(OrderName.SearchLeft);

    /// <summary>
    /// 直線状に右9マスの情報を取得します。
    /// </summary>
    /// <returns>右9マスの情報(直線状)</returns>
    public FieldObject[] SearchRight() => connector.Order(OrderName.SearchRight);

    /// <summary>
    /// 上にブロックを置きます。
    /// </summary>
    /// <returns>周囲9マスの情報</returns>
    public FieldObject[] PutUp() => connector.Order(OrderName.PutUp);

    /// <summary>
    /// 下にブロックを置きます。
    /// </summary>
    /// <returns>周囲9マスの情報</returns>
    public FieldObject[] PutDown() => connector.Order(OrderName.PutDown);

    /// <summary>
    /// 左にブロックを置きます。
    /// </summary>
    /// <returns>周囲9マスの情報</returns>
    public FieldObject[] PutLeft() => connector.Order(OrderName.PutLeft);

    /// <summary>
    /// 右にブロックを置きます。
    /// </summary>
    /// <returns>周囲9マスの情報</returns>
    public FieldObject[] PutRight() => connector.Order(OrderName.PutRight);
}