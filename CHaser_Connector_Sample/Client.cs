public static class Client
{
    public static void Run(Controller controller)
    {
        //ここにコードを書きます。
        while (true)
        {
            controller.GetReady();
            controller.SearchUp();

            controller.GetReady();
            controller.SearchLeft();

            controller.GetReady();
            controller.SearchDown();

            controller.GetReady();
            controller.SearchRight();

            controller.GetReady();
            controller.LookUp();

            controller.GetReady();
            controller.LookLeft();

            controller.GetReady();
            controller.LookDown();

            controller.GetReady();
            controller.LookRight();
        }
    }
}