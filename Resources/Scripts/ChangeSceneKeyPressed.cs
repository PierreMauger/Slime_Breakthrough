using System;

public class ChangeSceneKeyPressed : CustomComponent
{
    public float duration = 1.0f;
    public Entity logo;
    public Entity pressSpace;
    public string nextScene;
    private float time = 0.0f;
    private Vector3 defaultScale;

    private static float easeInOutQuad(float x) {
        return x < 0.5 ? 2 * x * x : 1 - MathF.Pow(-2 * x + 2, 2) / 2;
    }

    public void onInit()
    {
        defaultScale = pressSpace.getComponent<Transform>().scale;
    }

    public void onUpdate(float deltaTime)
    {
        time += deltaTime;
        if (InternalCalls.IsKeyDown(0x20)) {
            InternalCalls.SetMouseHidden(true);
            InternalCalls.SetScene(nextScene);
            return;
        }

        float t = time / duration;
        if (t > 1.0f)
            t = 1.0f;
        float alpha = easeInOutQuad(t);

        {
            Vector3 scale = logo.getComponent<Transform>().scale;
            scale.x = alpha;
            scale.y = alpha;
            logo.getComponent<Transform>().scale = scale;
        }

        if (time > duration) {
            Vector3 scale = pressSpace.getComponent<Transform>().scale;
            scale.x = defaultScale.x + MathF.Cos(time) * 0.1f * defaultScale.x;
            scale.y = defaultScale.y + MathF.Cos(time) * 0.1f * defaultScale.y;
            pressSpace.getComponent<Transform>().scale = scale;
        }
    }
}
