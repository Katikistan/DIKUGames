namespace BreakoutTests;
public static class CreateGL {
    private static bool created = false;

    public static void CreateOpenGL() {
        if (!created) {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            created = true;
        }
    }

}