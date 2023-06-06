namespace BreakoutTests;
/// <summary>
/// A class used in testing to avoid 50+ windows opening and crashing tests
/// </summary>
public static class CreateGL {
    private static bool created = false;

    public static void CreateOpenGL() {
        if (!created) {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            created = true;
        }
    }

}